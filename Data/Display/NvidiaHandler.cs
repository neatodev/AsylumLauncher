using NLog;
using NvAPIWrapper;
using NvAPIWrapper.DRS;
using NvAPIWrapper.Native.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Nlog.Info("Constructor - NVIDIA profile fully processed.");
            }
            catch (NVIDIANotSupportedException e)
            {
                NVIDIA.Initialize();
                Nlog.Warn("Constructor - Caught NVIDIANotSupportedException: {0}.", e);
            }
        }

        private void InitHandler()
        {
        }
    }
}
