using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using IdleMiner.Properties;
using Microsoft.Win32;
using System.Security;
using System.Runtime.InteropServices;

namespace IdleMiner
{
    public partial class MainForm : Form
    {
        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LastInputInfo plii);

        [DllImport("User32.dll")]
        private static extern int DestroyIcon(IntPtr hIcon);

        internal struct LastInputInfo
        {
            public uint cbSize;
            public uint dwTime;
        }

        private const int WITHIN_ALLOWED_INACTIVITY_PERIOD = -1;
        private const int OUTSIDE_ALLOWED_INACTIVITY_PERIOD = 1;
        private const string POCLBM_DEVICE_MATCH_PATTERN = @"^\[(?<id>\d+)\]\s+(?<device>.*)\b(?:.*)?$";

        private readonly Regex _poclbmOutputHashesPerSecondRegex = new Regex(@"^.*\s\[(?<hps>\d+\.\d+)\s[MmKkTt]H/s.*$");

        private Process _miningProcess;
        private TimeSpan _startAfterDelay = new TimeSpan(0,10,0);
        private readonly Dictionary<int, string> _openCLDevices;
        private int _selectedDevice = 0;
        private bool _dismissedBaloonTip = false;
        private string _username;
        private string _password;
        private Uri _poolAddress;
        private readonly string _encryptionKey = GetMachineIdentifierForEncryptionKey();

        private Bitmap _trayBitmap = new Bitmap(16, 16);
        private Graphics _trayGraphic;

        public MainForm()
        {
            InitializeComponent();
            _trayGraphic = Graphics.FromImage(_trayBitmap);
            // Parse available devices from poclbm.exe
#if !DEBUG
            try
            {
#endif
                var poclbm = new Process() { StartInfo = new ProcessStartInfo("poclbm.exe") {RedirectStandardOutput = true, UseShellExecute = false, CreateNoWindow = true, WindowStyle = ProcessWindowStyle.Hidden} };
                var t = 0;
                poclbm.Start();
                var poclbmOutput = poclbm.StandardOutput;
                while (!poclbm.HasExited)
                {
                    ++t;
                    Thread.Sleep(1000);
                    if (t <= 10) continue;
                    var result =
                        MessageBox.Show(
                            Resources.Poclbm_Is_Being_Slow_Message,
                            Resources.Poclbm_Is_Being_Slow_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                        Application.Exit();
                }
                string line;
                _openCLDevices = new Dictionary<int, string>();
                while (!poclbmOutput.EndOfStream)
                {
                    line = poclbmOutput.ReadLine();
                    Match m = Regex.Match(line, POCLBM_DEVICE_MATCH_PATTERN);
                    if (m.Success)
                    {
                        _openCLDevices.Add(int.Parse(m.Groups["id"].Value), m.Groups["device"].Value);
                        deviceSelection.Items.Add(m.Groups["device"].Value);
                    }
                }
#if !DEBUG
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    String.Format(Resources.Poclbm_Device_Loading_Error_Message, e.Message),
                    Resources.Poclbm_Error_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
#endif
            // Load settings from the registry
            using (RegistryKey k = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\IdleMiner\"))
            {
                if (k == null)
                {
                    Registry.CurrentUser.CreateSubKey(@"SOFTWARE\IdleMiner\");
                    SaveSettings();
                }
                else
                {
#if !DEBUG
                    try
                    {
#endif
                        _startAfterDelay = new TimeSpan((long)k.GetValue("Delay", 6000000000));
                        delayInput.Text = TimeSpanParser.ToString(_startAfterDelay);
                        tabControl.SelectedTab = bool.Parse(k.GetValue("Advanced",false).ToString())
                                                     ? tabPageAdvanced
                                                     : tabPageBasic;
                        _dismissedBaloonTip = bool.Parse(k.GetValue("AcknowledgedTray",false).ToString());
                        hideLaunchCheckbox.Checked = bool.Parse(k.GetValue("HideLaunch", true).ToString());
                        _selectedDevice = (int) k.GetValue("Device",0);
                        if (Uri.TryCreate(k.GetValue("PoolAddress","").ToString(), UriKind.Absolute, out _poolAddress))
                            addressInput.Text = _poolAddress.ToString();
                        _username = k.GetValue("Username", "").ToString();
                        usernameInput.Text = _username;
                        vectorCheckbox.Checked = bool.Parse(k.GetValue("Vectors", true).ToString());
                        var encryptedPassword = k.GetValue("Password", "").ToString();
                        if (encryptedPassword != "")
                        {
                            _password = new TripleDESStringEncryptor(_encryptionKey).DecryptString(encryptedPassword);
                            passwordInput.Text = _password;
                        }
                        var workSize = k.GetValue("WorkSize", 32).ToString();
                        workSizeComboBox.Text = workSize;
                        if (_openCLDevices.ContainsKey(_selectedDevice))
                            deviceSelection.Text = _openCLDevices[_selectedDevice];
#if !DEBUG
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(String.Format(Properties.Resources.Settings_Loading_Error_Message, e.Message),
                                        Resources.Settings_Loading_Error_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Load defaults
                        _startAfterDelay = new TimeSpan(0, 10, 0);
                        tabControl.SelectedTab = tabPageBasic;
                        _selectedDevice = 0;
                        SaveSettings();
                    }
#endif
                }
            }
            // Map to save settings
            workSizeComboBox.SelectedValueChanged += (sender, args) => SaveSettings();
            vectorCheckbox.CheckStateChanged += (sender, args) => SaveSettings();
            hideLaunchCheckbox.CheckedChanged += (sender, args) => SaveSettings();
            usernameInput.TextChanged += delegate
            {
                _username = usernameInput.Text;
                SaveSettings();
            };
            passwordInput.LostFocus += delegate
            {
                _password = passwordInput.Text;
                SaveSettings();
            };
            deviceSelection.TextChanged += DeviceSelectionProcessChange;
            addressInput.LostFocus += AddressInputCheckAndSave;
            delayInput.LostFocus += DelayInputParseAndSave;
            // Check to skip right to tray
            if (Program.StartToTray)
                Hide();
        }

        private void DeviceSelectionProcessChange(object sender, EventArgs eventArgs)
        {
            foreach (var kvp in _openCLDevices.Where(kvp => kvp.Value == deviceSelection.Text))
            {
                _selectedDevice = kvp.Key;
                break;
            }
            SaveSettings();
        }

        private void DelayInputParseAndSave(object sender, EventArgs eventArgs)
        {
            TimeSpan newDelay;
            if (!TimeSpanParser.FromString(delayInput.Text, out newDelay))
            {
                MessageBox.Show(
                    Resources.Unable_To_Parse_Time_Message,
                    Resources.Unable_To_Parse_Time_Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                delayInput.Focus();
                return;
            }
            _startAfterDelay = newDelay;
            SaveSettings();
        }

        private void AddressInputCheckAndSave(object sender, EventArgs eventArgs)
        {
            Uri parsedUri;
            if (!Uri.TryCreate(addressInput.Text, UriKind.Absolute, out parsedUri))
            {
                MessageBox.Show(Resources.Invalid_URL_Message, Resources.Invalid_URL_Title, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                addressInput.Focus();
                return;
            }
            _poolAddress = parsedUri;
            SaveSettings();
        }

        public static TimeSpan GetIdleTimeSpan()
        {
            var lastInPut = new LastInputInfo();
            lastInPut.cbSize = (uint)Marshal.SizeOf(lastInPut);
            GetLastInputInfo(ref lastInPut);
            return TimeSpan.FromMilliseconds((Environment.TickCount - lastInPut.dwTime));
        }

        private void SaveSettings()
        {
            using (RegistryKey k = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\IdleMiner\", true))
            {
                k.SetValue("Delay", _startAfterDelay.Ticks, RegistryValueKind.QWord);
                k.SetValue("Advanced", (tabControl.SelectedTab == tabPageAdvanced));
                k.SetValue("Device", _selectedDevice, RegistryValueKind.DWord);
                k.SetValue("AcknowledgedTray", _dismissedBaloonTip);
                k.SetValue("Vectors", vectorCheckbox.Checked);
                k.SetValue("HideLaunch", hideLaunchCheckbox.Checked);
                k.SetValue("Program", programInput.Text, RegistryValueKind.String);
                k.SetValue("Arguments", argumentsInput.Text, RegistryValueKind.String);
                if (_username != null) k.SetValue("Username", _username, RegistryValueKind.String);
                if (_password != null)
                    k.SetValue("Password", new TripleDESStringEncryptor(_encryptionKey).EncryptString(_password), RegistryValueKind.ExpandString);
                if (_poolAddress != null) k.SetValue("PoolAddress", _poolAddress.ToString(), RegistryValueKind.String);
                if (workSizeComboBox.Text != "")
                    k.SetValue("WorkSize", int.Parse(workSizeComboBox.Text), RegistryValueKind.DWord);
            }
        }

        private void NotifyIconMouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            checkTimer.Enabled = false;
            Close();
            Application.Exit();
        }

        private void LinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
#if !DEBUG
            try
            {
#endif
                Process.Start(@"http://idleminer.jonnyfunfun.com/");
#if !DEBUG
            }
            catch (Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (Exception other)
            {
                MessageBox.Show(other.Message);
            }
#endif
        }

        private ProcessStartInfo MinerExecutionProcessStartInfo()
        {
            // Don't bother looking here unless we're ready 
            if ((_poolAddress == null) || (_username == null) || (_password == null))
                return null;
            string executable = "poclbm.exe";
            string arguments = "";
            if (tabControl.SelectedTab == tabPageBasic)
            {
                // Basic launch
                arguments += String.Format(@"-d {0}", _selectedDevice);
                if (workSizeComboBox.SelectedText != "")
                    arguments += String.Format(@"-w {0}", workSizeComboBox.SelectedText);
                if (vectorCheckbox.Checked)
                    arguments += @" -v ";
                arguments += String.Format(@"{0}://{1}:{2}@{3}:{4}", _poolAddress.Scheme, SecurityElement.Escape(_username), SecurityElement.Escape(_password), _poolAddress.Host, _poolAddress.Port);
            }
            else
            {
                // Advanced launch
                if (!File.Exists(programInput.Text))
                    return null;
                executable = programInput.Text;
                arguments = argumentsInput.Text;
            }
            var ret = new ProcessStartInfo(executable, arguments) { UseShellExecute = false };
            if (hideLaunchCheckbox.Checked)
            {
                ret.CreateNoWindow = true;
                ret.WindowStyle = ProcessWindowStyle.Hidden;
                ret.RedirectStandardOutput = true;
            }
            return ret;
        }

        static readonly object TimerLock = new object();
        private void CheckTimerTick(object sender, EventArgs e)
        {
            if (!Monitor.TryEnter(TimerLock, 0)) return;
            if ((TimeSpan.Compare(GetIdleTimeSpan(), _startAfterDelay) == WITHIN_ALLOWED_INACTIVITY_PERIOD) &&
                (_miningProcess != null) && (!_miningProcess.HasExited))
            {
                // We need to kill the mining process
                _miningProcess.CancelOutputRead();
                Thread.Sleep(100);
                _miningProcess.Kill();
                Thread.Sleep(1000);
                _miningProcess.Close();
                _miningProcess = null;
                notifyIcon.Icon = Resources.C21;
            }
            else if ((TimeSpan.Compare(GetIdleTimeSpan(), _startAfterDelay) == OUTSIDE_ALLOWED_INACTIVITY_PERIOD) &&
                     ((_miningProcess == null) || (_miningProcess.HasExited)))
            {
                if (MinerExecutionProcessStartInfo() == null)
                    return;
                _miningProcess = Process.Start(MinerExecutionProcessStartInfo());
                _miningProcess.OutputDataReceived += MiningProcessOnOutputDataReceived;
                _miningProcess.BeginOutputReadLine();
                notifyIcon.Icon = Resources.Running;
            }
            Monitor.Exit(TimerLock);
        }

        static readonly object HashUpdateLock = new object();
        private void MiningProcessOnOutputDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
        {
            if (!Monitor.TryEnter(HashUpdateLock, 0)) return;
            var m = _poclbmOutputHashesPerSecondRegex.Match(dataReceivedEventArgs.Data);
            if (!m.Success)
                return;
            WriteTrayIconText(Math.Round(decimal.Parse(m.Groups["hps"].Value), 0).ToString(CultureInfo.InvariantCulture));
            Monitor.Exit(HashUpdateLock);
        }

        private void DeviceSelectionSelectedValueChanged(object sender, EventArgs e)
        {
            foreach (var kvp in _openCLDevices.Where(kvp => ReferenceEquals(deviceSelection.SelectedItem, kvp.Value)))
            {
                _selectedDevice = kvp.Key;
                break; // only want the first
            }
            SaveSettings();
        }

        private void MainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Hide();
                if (!_dismissedBaloonTip)
                    notifyIcon.ShowBalloonTip(5000, Resources.Minimized_Balloon_Title, Resources.Minimized_Balloon_Text,
                                              ToolTipIcon.Info);
                e.Cancel = true;
            }
            SaveSettings();
            _miningProcess.Kill();
            Thread.Sleep(1000);
            _miningProcess.Close();
            Application.Exit();
        }

        private void NotifyIconBalloonTipClicked(object sender, System.EventArgs e)
        {
            _dismissedBaloonTip = true;
            SaveSettings();
        }

        private static string GetMachineIdentifierForEncryptionKey()
        {
            string cpuInfo = string.Empty;
            var mc = new ManagementClass("win32_processor");
            var moc = mc.GetInstances();
            foreach (var mo in moc)
            {
                if (cpuInfo == "")
                {
                    cpuInfo = mo.Properties["processorID"].Value.ToString();
                }
            }
            var dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""C:""");
            dsk.Get();
            var volumeSerial = dsk["VolumeSerialNumber"].ToString();
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(cpuInfo+volumeSerial);
            var hash = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        private void BrowseForProgramButtonClick(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog {Filter = "Executables (*.bat, *.exe)|*.bat,*.exe|All Files (*.*)|*.*", CheckFileExists = true};
            var res = ofd.ShowDialog();
            if (res == DialogResult.OK)
            {
                programInput.Text = ofd.FileName;
                SaveSettings();
            }
        }

        private void WriteTrayIconText(string text)
        {
            var font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular, GraphicsUnit.Pixel);
            var brush = new SolidBrush(Color.CornflowerBlue);
            _trayGraphic.Clear(Color.Transparent);
            _trayGraphic.DrawIcon(Resources.mining, 0, 0);
            _trayGraphic.DrawString(text, font, brush, 0, 9);
            var hIcon = _trayBitmap.GetHicon();
            notifyIcon.Icon = Icon.FromHandle(hIcon);
            DestroyIcon(hIcon);
        }
    }
}
