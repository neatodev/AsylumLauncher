using AsylumLauncher.Properties;
using NLog;
using System.Diagnostics;

namespace AsylumLauncher
{
    internal class FileHandler
    {
        private bool IntroFilesRenamed;
        private readonly string CustomDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Custom");
        private readonly string Legal = Path.Combine(Directory.GetCurrentDirectory(), "..\\BmGame\\Movies\\Legal.bik");
        private readonly string Legalus = Path.Combine(Directory.GetCurrentDirectory(), "..\\BmGame\\Movies\\Legalus.bik");
        private readonly string NvidiaVid = Path.Combine(Directory.GetCurrentDirectory(), "..\\BmGame\\Movies\\nvidia.bik");
        private readonly string UtLogo = Path.Combine(Directory.GetCurrentDirectory(), "..\\BmGame\\Movies\\UtLogo.bik");
        private readonly string baalogo = Path.Combine(Directory.GetCurrentDirectory(), "..\\BmGame\\Movies\\baa_logo_run_v5_h264.bik");
        private readonly string LegalRenamed = Path.Combine(Directory.GetCurrentDirectory(), "..\\BmGame\\Movies\\Legal.bik.bak");
        private readonly string LegalusRenamed = Path.Combine(Directory.GetCurrentDirectory(), "..\\BmGame\\Movies\\Legalus.bik.bak");
        private readonly string NvidiaVidRenamed = Path.Combine(Directory.GetCurrentDirectory(), "..\\BmGame\\Movies\\nvidia.bik.bak");
        private readonly string UtLogoRenamed = Path.Combine(Directory.GetCurrentDirectory(), "..\\BmGame\\Movies\\UtLogo.bik.bak");
        private readonly string baalogoRenamed = Path.Combine(Directory.GetCurrentDirectory(), "..\\BmGame\\Movies\\baa_logo_run_v5_h264.bik.bak");
        public readonly string ConfigDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Square Enix\\Batman Arkham Asylum GOTY\\BmGame\\Config");

        public string BmEnginePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Square Enix\\Batman Arkham Asylum GOTY\\BmGame\\Config\\BmEngine.ini");
        public string UserEnginePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Square Enix\\Batman Arkham Asylum GOTY\\BmGame\\Config\\UserEngine.ini");
        public string BmInputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Square Enix\\Batman Arkham Asylum GOTY\\BmGame\\Config\\BmInput.ini");
        public string UserInputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Square Enix\\Batman Arkham Asylum GOTY\\BmGame\\Config\\UserInput.ini");
        private string BmGamePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Square Enix\\Batman Arkham Asylum GOTY\\BmGame\\Config\\BmGame.ini");
        public FileInfo BmEngine = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Square Enix\\Batman Arkham Asylum GOTY\\BmGame\\Config\\BmEngine.ini"));
        public FileInfo UserInput = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Square Enix\\Batman Arkham Asylum GOTY\\BmGame\\Config\\UserInput.ini"));
        public FileInfo UserEngine = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Square Enix\\Batman Arkham Asylum GOTY\\BmGame\\Config\\UserEngine.ini"));
        public FileInfo BmInput = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Square Enix\\Batman Arkham Asylum GOTY\\BmGame\\Config\\BmInput.ini"));

        private static readonly Logger Nlog = LogManager.GetCurrentClassLogger();


        public FileHandler()
        {
            CheckConfigFilesExist();
            CheckCustomFilesExist();
            CheckIntroVideoFilesRenamed();
            Nlog.Info("Constructor - Successfully initialized FileHandler.");
        }

        private void CheckCustomFilesExist()
        {
            if (!Directory.Exists(CustomDirectoryPath))
            {
                Directory.CreateDirectory(CustomDirectoryPath);
                Nlog.Warn("CheckCustomFilesExist - Can't find the 'Custom' directory. Creating it now.");
            }

            if (!File.Exists(Path.Combine(CustomDirectoryPath, "centre_camera.txt")))
            {
                CreateConfigFile(Path.Combine(CustomDirectoryPath, "centre_camera.txt"), Resources.centre_camera);
                Nlog.Warn("CheckCustomFilesExist - Can't find the 'centre_camera.txt' file. Creating it now.");
            }

            if (!File.Exists(Path.Combine(CustomDirectoryPath, "custom_commands.txt")))
            {
                CreateConfigFile(Path.Combine(CustomDirectoryPath, "custom_commands.txt"), Resources.custom_commands);
                Nlog.Warn("CheckCustomFilesExist - Can't find the 'custom_commands.txt' file. Creating it now.");
            }
        }

        private void CheckConfigFilesExist()
        {
            if (!Directory.Exists(ConfigDirectoryPath))
            {
                Directory.CreateDirectory(ConfigDirectoryPath);
                Nlog.Warn("CheckConfigFilesExist - Can't find configuration directory. Creating it now. Please make sure you have installed the game.");
            }

            if (!File.Exists(BmEnginePath))
            {
                CreateConfigFile(BmEnginePath, Resources.BmEngine);
                Nlog.Warn("CheckConfigFilesExist - Can't find 'BmEngine.ini'. Creating it now.");
            }

            if (!File.Exists(UserEnginePath))
            {
                CreateConfigFile(UserEnginePath, Resources.UserEngine);
                Nlog.Warn("CheckConfigFilesExist - Can't find 'UserEngine.ini'. Creating it now.");
            }

            if (File.Exists(UserInputPath))
            {
                UserInput.IsReadOnly = false;
                string[] UserLines = File.ReadAllLines(UserInputPath);
                if (UserLines.Length < 63)
                {
                    File.Delete(UserInputPath);
                    CreateConfigFile(UserInputPath, Resources.UserInput);
                    Nlog.Info("CheckConfigFilesExist - Overwriting the default 'UserInput.ini' file with a custom-made one.");
                }
            }
            else if (!File.Exists(UserInputPath))
            {
                CreateConfigFile(UserInputPath, Resources.UserInput);
                Nlog.Warn("CheckConfigFilesExist - Can't find 'UserInput.ini'. Creating it now.");
            }
            if (File.Exists(BmInputPath))
            {
                BmInput.IsReadOnly = false;
                string[] BMLines = File.ReadAllLines(BmInputPath);
                if (BMLines.Length < 425)
                {
                    File.Delete(BmInputPath);
                    CreateConfigFile(BmInputPath, Resources.BmInput);
                    BmInput.IsReadOnly = true;
                    Nlog.Info("CheckConfigFilesExist - Overwriting the default 'BmInput.ini' file with a custom-made one.");
                }
            }
            else if (!File.Exists(BmInputPath))
            {
                CreateConfigFile(BmInputPath, Resources.BmInput);
                Nlog.Warn("CheckConfigFilesExist - Can't find 'BmInput.ini'. Creating it now.");
            }

            if (!File.Exists(Path.Combine(ConfigDirectoryPath, "BmCamera.ini")))
            {
                CreateConfigFile(Path.Combine(ConfigDirectoryPath, "BmCamera.ini"), Resources.BmCamera);
                Nlog.Warn("CheckConfigFilesExist - Can't find 'BmCamera.ini'. Creating it now.");
            }

            if (!File.Exists(Path.Combine(ConfigDirectoryPath, "BmCompat.ini")))
            {
                CreateConfigFile(Path.Combine(ConfigDirectoryPath, "BmCompat.ini"), Resources.BmCompat);
                Nlog.Warn("CheckConfigFilesExist - Can't find 'BmCompat.ini'. Creating it now.");
            }

            if (!File.Exists(Path.Combine(ConfigDirectoryPath, "BmGame.ini")))
            {
                CreateConfigFile(Path.Combine(ConfigDirectoryPath, "BmGame.ini"), Resources.BmGame);
                Nlog.Warn("CheckConfigFilesExist - Can't find 'BmGame.ini'. Creating it now.");
            }

            if (!File.Exists(Path.Combine(ConfigDirectoryPath, "BmLightmass.ini")))
            {
                CreateConfigFile(Path.Combine(ConfigDirectoryPath, "BmLightmass.ini"), Resources.BmLightmass);
                Nlog.Warn("CheckConfigFilesExist - Can't find 'BmLightmass.ini'. Creating it now.");
            }

            if (!File.Exists(Path.Combine(ConfigDirectoryPath, "BmUI.ini")))
            {
                CreateConfigFile(Path.Combine(ConfigDirectoryPath, "BmUI.ini"), Resources.BmUI);
                Nlog.Warn("CheckConfigFilesExist - Can't find 'BmUI.ini'. Creating it now.");
            }

            if (!File.Exists(Path.Combine(ConfigDirectoryPath, "UserGame.ini")))
            {
                CreateConfigFile(Path.Combine(ConfigDirectoryPath, "UserGame.ini"), Resources.UserGame);
                Nlog.Warn("CheckConfigFilesExist - Can't find 'UserGame.ini'. Creating it now.");
            }
        }

        public void CreateConfigFile(string Path, string Resource)
        {
            File.Create(Path).Dispose();
            var FileWriter = new StreamWriter(Path);
            FileWriter.Write(Resource);
            FileWriter.Close();
        }

        public static bool DetectGameExe()
        {
            string GameExe = Path.Combine(Directory.GetCurrentDirectory(), "ShippingPC-BmGame.exe");
            if (File.Exists(GameExe))
            {
                return true;
            }
            return false;
        }

        private void CheckIntroVideoFilesRenamed()
        {
            if (!DetectGameExe())
            {
                Program.MainWindow.SkipIntroBox.Enabled = false;
                return;
            }

            if (File.Exists(Legal) && File.Exists(Legalus) && File.Exists(NvidiaVid) && File.Exists(UtLogo) && File.Exists(LegalRenamed) && File.Exists(baalogo) && File.Exists(LegalusRenamed) && File.Exists(NvidiaVidRenamed) && File.Exists(UtLogoRenamed) && File.Exists(baalogoRenamed))
            {
                File.Delete(LegalRenamed);
                File.Delete(LegalusRenamed);
                File.Delete(NvidiaVidRenamed);
                File.Delete(UtLogoRenamed);
                File.Delete(baalogoRenamed);
                Program.MainWindow.SkipIntroBox.Enabled = true;
                Program.MainWindow.SkipIntroBox.Checked = false;
                IntroFilesRenamed = false;
            }

            if (File.Exists(Legal) && File.Exists(Legalus) && File.Exists(NvidiaVid) && File.Exists(UtLogo) && File.Exists(baalogo))
            {
                Program.MainWindow.SkipIntroBox.Enabled = true;
                Program.MainWindow.SkipIntroBox.Checked = false;
                IntroFilesRenamed = false;
            }

            if (File.Exists(LegalRenamed) && File.Exists(LegalusRenamed) && File.Exists(NvidiaVidRenamed) && File.Exists(UtLogoRenamed) && File.Exists(baalogoRenamed))
            {
                Program.MainWindow.SkipIntroBox.Enabled = true;
                Program.MainWindow.SkipIntroBox.Checked = true;
                IntroFilesRenamed = true;
            }
        }

        public void RenameIntroVideoFiles()
        {
            if (!Program.MainWindow.SkipIntroBox.Enabled || !Program.MainWindow.DisplaySettingChanged)
            {
                return;
            }

            if (!IntroFilesRenamed && Program.MainWindow.SkipIntroBox.Checked)
            {
                File.Move(Legal, LegalRenamed);
                File.Move(Legalus, LegalusRenamed);
                File.Move(NvidiaVid, NvidiaVidRenamed);
                File.Move(UtLogo, UtLogoRenamed);
                File.Move(baalogo, baalogoRenamed);
                IntroFilesRenamed = !IntroFilesRenamed;
                Nlog.Info("RenameIntroVideoFiles - Disabling Startup Movies.");
            }
            else if (IntroFilesRenamed && !Program.MainWindow.SkipIntroBox.Checked)
            {
                File.Move(LegalRenamed, Legal);
                File.Move(LegalusRenamed, Legalus);
                File.Move(NvidiaVidRenamed, NvidiaVid);
                File.Move(UtLogoRenamed, UtLogo);
                File.Move(baalogoRenamed, baalogo);
                IntroFilesRenamed = !IntroFilesRenamed;
                Nlog.Info("RenameIntroVideoFiles - Enabling Startup Movies.");
            }
        }

        public void StartAsAdmin(string Name)
        {
            var pr = new Process
            {
                StartInfo =
                {
                    FileName = Name,
                    UseShellExecute = true,
                    Verb = "runas"
                }
            };

            try 
            {
                pr.Start();

            } catch (Exception)
            {
                return;
            }
            Process.GetCurrentProcess().Kill();
        }
    }
}
