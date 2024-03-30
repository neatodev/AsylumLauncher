using NLog;
using NLog.Config;
using NLog.Targets;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace AsylumLauncher
{
    internal static class Program
    {

        private static readonly Logger Nlog = LogManager.GetCurrentClassLogger();

        public static readonly string CurrentTime = DateTime.Now.ToString("dd-MM-yy__hh-mm-ss");

        public static AsylumLauncher MainWindow;

        public static IniHandler IniHandler;

        public static FileHandler FileHandler;

        public static InputHandler InputHandler;

        public static bool IsAdmin;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        ///     Replacement Application for the original Batman: Arkham Asylum BmLauncher
        ///     Offers more configuration options, enables compatibility with High-Res Texture Packs
        ///     and automatically takes care of the ReadOnly properties of each file, removing
        ///     any requirement to manually edit .ini files. Guarantees a much more comfortable user experience.
        ///     @author Neato (https://steamcommunity.com/id/frofoo)
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool IsNewWindow = true;
            using (Mutex mtx = new(true, "{7F85C5E9-214F-4F2A-A949-AA3978D5DAC2}", out IsNewWindow))
            {
                if (IsNewWindow)
                {
                    IsAdmin = CheckIsAdmin();
                    SetupCulture();
                    SetupLogger();
                    InitializeProgram();
                    Application.Run(MainWindow);
                }
                else
                {
                    Process Current = Process.GetCurrentProcess();
                    foreach (Process P in Process.GetProcessesByName(Current.ProcessName))
                    {
                        if (P.Id != Current.Id)
                        {
                            SetForegroundWindow(P.MainWindowHandle);
                            break;
                        }
                    }
                }
            }
        }

        private static bool CheckIsAdmin()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                var principal = new WindowsPrincipal(identity);
                return principal?.IsInRole(WindowsBuiltInRole.Administrator) ?? false;
            }
        }

        private static void SetupCulture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        private static void InitializeProgram()
        {
            Nlog.Info("InitializeProgram - Starting logs at {0} on {1}.", DateTime.Now.ToString("HH:mm:ss"), DateTime.Now.ToString("D", new CultureInfo("en-GB")));
            ApplicationConfiguration.Initialize();
            MainWindow = new AsylumLauncher();
            FileHandler = new FileHandler();
            IniHandler = new IniHandler();
            InputHandler = new InputHandler();
            var SystemHandler = new SystemHandler();
            MainWindow.GPULabel.Text = SystemHandler.GPUData;
            MainWindow.CPULabel.Text = SystemHandler.CPUData;
            new IniReader().InitDisplay();
            new InputReader().InitControls();
        }

        private static void SetupLogger()
        {
            LoggingConfiguration config = new();
            ConsoleTarget logconsole = new("logconsole");
            if (!Directory.Exists("logs"))
            {
                Directory.CreateDirectory("logs");
            }

            FileTarget logfile = new("logfile")
            {
                FileName = Directory.GetCurrentDirectory() + "\\logs\\asylumlauncher_report__" + CurrentTime + ".log"
            };
            DirectoryInfo LogDirectory = new(Directory.GetCurrentDirectory() + "\\logs");
            DateTime OldestAllowedArchive = DateTime.Now - new TimeSpan(3, 0, 0, 0);
            foreach (FileInfo file in LogDirectory.GetFiles())
            {
                if (file.CreationTime < OldestAllowedArchive)
                {
                    file.Delete();
                }
            }

            config.AddRule(LogLevel.Debug, LogLevel.Warn, logconsole);
            config.AddRule(LogLevel.Debug, LogLevel.Warn, logfile);
            LogManager.Configuration = config;
            Nlog.Debug("SetupLogger - Elevated Privileges: {0}", IsAdmin.ToString());
        }
    }
}