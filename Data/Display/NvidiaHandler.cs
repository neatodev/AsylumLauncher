using NLog;
using NvAPIWrapper;
using NvAPIWrapper.DRS;
using NvAPIWrapper.Native.Exceptions;

namespace AsylumLauncher
{
    internal class NvidiaHandler
    {
        private readonly DriverSettingsProfile _prof;
        private readonly DriverSettingsSession _session;

        private static Logger Nlog = LogManager.GetCurrentClassLogger();

        public NvidiaHandler()
        {
            try
            {
                NVIDIA.Initialize();
                Nlog.Debug("Constructor - NVIDIA API initialized.");
                _session = DriverSettingsSession.CreateAndLoad();
                try
                {
                    _session.FindProfileByName("Batman: Arkham Asylum");
                }
                catch (NVIDIAApiException e)
                {
                    Console.WriteLine(e);
                    DriverSettingsProfile profile =
                        DriverSettingsProfile.CreateProfile(_session, "Batman: Arkham Asylum");
                    ProfileApplication profApp = ProfileApplication.CreateApplication(profile, "shippingpc-bmgame.exe");
                    profile = profApp.Profile;
                    profile.SetSetting(KnownSettingId.AmbientOcclusionModeActive, 0);
                    profile.SetSetting(KnownSettingId.AmbientOcclusionMode, 0);
                    _session.Save();
                    Nlog.Warn("Constructor - NVIDIA profile not found. Creating profile: {0}", profile.ToString());
                }
                _prof = _session.FindProfileByName("Batman: Arkham Asylum");
                InitialSetHbaoPlus();
                Nlog.Info("Constructor - NVIDIA profile fully processed.");
            }
            catch (NVIDIANotSupportedException e)
            {
                //NVIDIA.Initialize();
                Program.MainWindow.RunAsAdminButton.Enabled = false;
                Program.MainWindow.hbaopluscheckbox.Enabled = false;
                Nlog.Warn("Constructor - Caught NVIDIANotSupportedException: {0}.", e);
            }
            catch (Exception e)
            {
                Program.MainWindow.RunAsAdminButton.Enabled = false;
                Program.MainWindow.hbaopluscheckbox.Enabled = false;
                Nlog.Error("Constructor - Unexpected critical error during NVAPI initialization: {0}", e);
            }
        }

        public void ToggleHbaoPlus(bool Active)
        {
            Int16 CompValue = 0;

            if (Active)
            {
                try
                {
                    _prof.SetSetting(2916165, 48);
                    _session.Save();
                }
                catch (Exception e)
                {
                    Program.MainWindow.RunAsAdminButton.Enabled = false;
                    Program.MainWindow.hbaopluscheckbox.Enabled = false;
                    Nlog.Error("ToggleHbaoPlus - Unexpected critical error while trying to set the HBAO+ flag: {0}", e);
                }
            }


            try
            {
                if (Active)
                {
                    _prof.SetSetting(KnownSettingId.AmbientOcclusionModeActive, 1);
                    _prof.SetSetting(KnownSettingId.AmbientOcclusionMode, 2);
                    _session.Save();
                }
                else
                {
                    _prof.SetSetting(KnownSettingId.AmbientOcclusionModeActive, 0);
                    _prof.SetSetting(KnownSettingId.AmbientOcclusionMode, 0);
                    _session.Save();
                }

                Nlog.Debug(
                    "ToggleHbaoPlus - setting AmbientOcclusionModeActive to {0}, setting AmbientOcclusionMode to {1}.",
                    _prof.GetSetting(KnownSettingId.AmbientOcclusionModeActive).CurrentValue,
                    _prof.GetSetting(KnownSettingId.AmbientOcclusionMode).CurrentValue);
            }
            catch (NullReferenceException e)
            {
                Nlog.Warn("ToggleHbaoPlus - Caught NullReferenceException: {0}", e);
            }

            catch (Exception e)
            {
                Program.MainWindow.RunAsAdminButton.Enabled = false;
                Program.MainWindow.hbaopluscheckbox.Enabled = false;
                Nlog.Error("ToggleHbaoPlus - Unexpected critical error while trying to set the HBAO+ flag: {0}", e);
            }
        }

        public void InitialSetHbaoPlus()
        {
            string aoActive = "0", aoValue = "0";
            bool HasHbao = true;
            Int16 CompValue = 0;
            try
            {
                aoActive = _prof.GetSetting(KnownSettingId.AmbientOcclusionModeActive).ToString();
                aoValue = _prof.GetSetting(KnownSettingId.AmbientOcclusionMode).ToString();
                CompValue = Int16.Parse(_prof.GetSetting(2916165).CurrentValue.ToString());
            }
            catch (Exception)
            {
                _prof.SetSetting(KnownSettingId.AmbientOcclusionModeActive, 0);
                _prof.SetSetting(KnownSettingId.AmbientOcclusionMode, 0);
                Program.MainWindow.hbaopluscheckbox.Checked = false;
                Nlog.Warn(
                    "InitialSetHbaoPlus - Couldn't find ambient occlusion settings. Generating settings with default(0) values now.");
                _session.Save();
            }

            if (!aoActive.Contains("1") || !aoValue.Contains("2") || CompValue != 48)
            {
                if (CompValue != 48)
                {
                    HasHbao = false;
                }
            }
            else
            {
                Program.MainWindow.hbaopluscheckbox.Checked = true;
            }

            Nlog.Debug("InitialSetHbaoPlus - HBAO+ is currently {0}.", HasHbao.ToString());
        }
    }
}
