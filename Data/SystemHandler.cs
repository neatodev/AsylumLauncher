using Microsoft.Win32;
using NLog;
using System.Globalization;
using System.Management;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AsylumLauncher
{
    internal class SystemHandler
    {
        private string RegDirectory;
        public string GPUData = "";
        public string CPUData = "";
        public bool NvidiaGPU = false;

        private static Logger Nlog = LogManager.GetCurrentClassLogger();

        public SystemHandler()
        {
            Nlog.Info("Constructor - Successfully initialized SystemHandler.");
            CPUData = InitializeCPU().ToUpper();
            GPUData = InitializeGPUValues().ToUpper();
        }

        private string InitializeCPU()
        {
            try
            {
                var CPU = new ManagementObjectSearcher("select * from Win32_Processor").Get().Cast<ManagementObject>().First();
                uint Clockspeed = (uint)CPU["MaxClockSpeed"];
                double GHzSpeed = (double)Clockspeed / 1000;
                Nlog.Info("InitializeCPU - Recognized CPU as {0} with a base clock speed of {1}GHz.", CPU["Name"].ToString().Trim(' '), Math.Round(GHzSpeed, 1));
                var CPUName = CPU["Name"].ToString().Trim(' ');
                if (CPUName.ToUpper().Contains("GHZ"))
                {
                    return CPUName;
                }
                else
                {
                    return CPUName + " @ " + Math.Round(GHzSpeed, 1) + "GHz";
                }
            }
            catch (Exception e)
            {
                Nlog.Error("InitializeCPU - Could not read CPU information. Error: {0}", e);
                Program.MainWindow.BasicToolTip.SetToolTip(Program.MainWindow.CPULabel, "Current date.");
                return DateTime.Now.ToString("dddd, MMMM dd, yyyy", new CultureInfo("en-GB"));
            }
        }

        private string InitializeGPUValues()
        {
            try
            {
                RegDirectory = Path.Combine(Registry.LocalMachine.ToString(), "SYSTEM\\ControlSet001\\Control\\Class\\{4d36e968-e325-11ce-bfc1-08002be10318}\\0000");
                var key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\\ControlSet001\\Control\\Class\\{4d36e968-e325-11ce-bfc1-08002be10318}\\0000");

                if (key == null)
                {
                    RegDirectory = Path.Combine(Registry.LocalMachine.ToString(), "SYSTEM\\ControlSet001\\Control\\Class\\{4d36e968-e325-11ce-bfc1-08002be10318}\\0001");
                }

                var VRam = ConvertVRamValue((object)Registry.GetValue(RegDirectory, "HardwareInformation.qwMemorySize", 0));
                if (VRam.Trim() == "")
                {
                    return SetGPUNameVideoController();
                }
                Nlog.Info("InitializeGPUValues - Recognized GPU as {0} with a total VRAM amount of {1}.", (string)Registry.GetValue(RegDirectory, "DriverDesc", "Could not find GPU name."), ConvertVRamValue((object)Registry.GetValue(RegDirectory, "HardwareInformation.qwMemorySize", 0)));
                string GPUName = (string)Registry.GetValue(RegDirectory, "DriverDesc", "GPU not found.");
                if (GPUName.ToUpper().Contains("NVIDIA"))
                {
                    NvidiaGPU = true;
                }
                return GPUName + " " + ConvertVRamValue((object)Registry.GetValue(RegDirectory, "HardwareInformation.qwMemorySize", 0));
            }
            catch (Exception e)
            {
                Nlog.Error("InitializeGPUValues - Could not read Graphics Card information. Error: {0}", e);
                Program.MainWindow.BasicToolTip.SetToolTip(Program.MainWindow.GPULabel, "Current version.");
                return "Application Version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        private string SetGPUNameVideoController()
        {
            List<string> GPUList = new();
            ManagementObjectSearcher search = new("SELECT * FROM Win32_VideoController");
            foreach (ManagementBaseObject o in search.Get())
            {
                ManagementObject obj = (ManagementObject)o;
                foreach (PropertyData data in obj.Properties)
                {
                    if (data.Name == "Description")
                    {
                        GPUList.Add(data.Value.ToString());
                    }
                }
            }
            var GPU = GPUList[0];
            if (GPUList.Count > 1)
            {
                foreach (string s in GPUList)
                {
                    // check for amd radeon, but exclude integrated graphics
                    if (s.ToUpper().Contains("AMD") && (Regex.IsMatch(s, @"\s*00M\s*$", RegexOptions.IgnoreCase) 
                        || Regex.IsMatch(s, @"\s*0*\s*$", RegexOptions.IgnoreCase) || Regex.IsMatch(s, @"\s*X*XTX\s*$", RegexOptions.IgnoreCase) 
                        || Regex.IsMatch(s, @"\s*X*XT\s*$", RegexOptions.IgnoreCase)))
                    {
                            GPU = s;
                            break;
                    }
                }
                foreach (string s in GPUList)
                {
                    // check for intel arc
                    if (s.ToUpper().Contains("INTEL") && (s.ToUpper().Contains("ARC") || Regex.IsMatch(s, @"\s*A[^ ]*", RegexOptions.IgnoreCase)))
                    {
                        GPU = s;
                        break;
                    }
                }
                foreach (string s in GPUList)
                {
                    // prioritize nvidia
                    if (s.ToUpper().Contains("NVIDIA"))
                    {
                        GPU = s;
                        NvidiaGPU = true;
                        break;
                    }
                }
            }
            Nlog.Warn("SetGPUNameVideoController - Used fallback method to determine GPU as {0}. Could not correctly determine VRAM amount. Your GPU drivers may be corrupted.", GPU);
            return GPU;
        }

        ///<Returns VRAM value in GB in most cases.</Returns>.</summary>
        private string ConvertVRamValue(object VRam)
        {
            try
            {
                Int64 VRamValue = (Int64)VRam;

                var Affix = "MB";
                if (VRamValue >= 1073741824)
                {
                    VRamValue /= 1024;
                    Affix = "GB";
                }
                VRamValue /= 1048576;
                return "(" + VRamValue.ToString() + Affix + ")";
            }
            catch (InvalidCastException)
            {
                return "";
            }
            catch (NullReferenceException)
            {
                return "";
            }
        }
    }
}
