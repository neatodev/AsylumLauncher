using NLog;

namespace AsylumLauncher
{
    internal class IniReader
    {

        private static Logger Nlog = LogManager.GetCurrentClassLogger();

        public void InitDisplay()
        {
            InitDisplayBasic();
            InitDisplayAdvanced();
            InitColors();
            InitCustomLines();
            InitTexturePackFix();
            Program.MainWindow.DisplaySettingChanged = false;
            Program.MainWindow.ApplySettingsButton.Enabled = false;
            Nlog.Info("InitDisplay - Successfully initialized display settings.");
        }

        private void InitDisplayBasic()
        {
            // Resolution
            var ResX = IniHandler.BmEngineData["SystemSettings"]["ResX"];
            var ResY = IniHandler.BmEngineData["SystemSettings"]["ResY"];
            new Resolution().GetResolutions();
            Program.MainWindow.ResolutionBox.Items.Clear();
            foreach (string Resolution in Resolution.ResolutionList)
            {
                Program.MainWindow.ResolutionBox.Items.Add(Resolution);
                if (ResX + "x" + ResY == Resolution)
                {
                    Program.MainWindow.ResolutionBox.SelectedIndex = Program.MainWindow.ResolutionBox.Items.IndexOf(Resolution);
                }
            }
            if (Program.MainWindow.ResolutionBox.Text == "".Trim())
            {
                Program.MainWindow.ResolutionBox.Items.Insert(Program.MainWindow.ResolutionBox.Items.Count, ResX + "x" + ResY);
                Program.MainWindow.ResolutionBox.SelectedIndex = Program.MainWindow.ResolutionBox.Items.Count - 1;
            }

            // Fullscreen
            if (IniHandler.BmEngineData["SystemSettings"]["Fullscreen"] == "True")
            {
                Program.MainWindow.FullscreenBox.SelectedIndex = 0;
            }
            else
            {
                Program.MainWindow.FullscreenBox.SelectedIndex = 1;
            }

            // Smooth Frames
            if (IniHandler.BmEngineData["Engine.GameEngine"]["bSmoothFrameRate"].ToLower().Contains("true"))
            {
                Program.MainWindow.smoothframebox.Checked = true;
            }
            else
            {
                Program.MainWindow.smoothframebox.Checked = false;
            }

            // VSync
            if (IniHandler.BmEngineData["SystemSettings"]["UseVsync"] == "True")
            {
                Program.MainWindow.VsyncBox.SelectedIndex = 0;
            }
            else
            {
                Program.MainWindow.VsyncBox.SelectedIndex = 1;
            }

            // DetailMode
            Program.MainWindow.DetailModeBox.SelectedIndex = IniHandler.BmEngineData["SystemSettings"]["DetailMode"] switch
            {
                "1" => 1,
                "2" => 2,
                _ => 0,
            };

            // Framerate Cap
            double Framecap = double.Parse(IniHandler.BmEngineData["Engine.GameEngine"]["MaxSmoothedFrameRate"]);
            Framecap -= 2;
            Program.MainWindow.FrameCapTextBox.Text = Framecap.ToString();

            // Language
            string language = new System.Globalization.CultureInfo("en-US").TextInfo.ToTitleCase(IniHandler.BmEngineData["Engine.Engine"]["Language"]);
            switch (language)
            {
                case "Int":
                    Program.MainWindow.LanguageBox.SelectedIndex = 0;
                    break;
                case "Deu":
                    Program.MainWindow.LanguageBox.SelectedIndex = 1;
                    break;
                case "Esn":
                    Program.MainWindow.LanguageBox.SelectedIndex = 2;
                    break;
                case "Fra":
                    Program.MainWindow.LanguageBox.SelectedIndex = 3;
                    break;
                case "Ita":
                    Program.MainWindow.LanguageBox.SelectedIndex = 4;
                    break;
                case "Pol":
                    Program.MainWindow.LanguageBox.SelectedIndex = 5;
                    break;
                case "Rus":
                    Program.MainWindow.LanguageBox.SelectedIndex = 6;
                    break;
                case "Jpn":
                    Program.MainWindow.LanguageBox.SelectedIndex = 7;
                    break;
                default:
                    Program.MainWindow.LanguageBox.Items.Add("Unofficial");
                    Program.MainWindow.LanguageBox.SelectedIndex = 5;
                    break;
            }
        }

        private void InitDisplayAdvanced()
        {
            // Anti-Aliasing
            switch (IniHandler.BmEngineData["SystemSettings"]["MaxMultisamples"])
            {
                case "2":
                    Program.MainWindow.AntiAliasingBox.SelectedIndex = 1;
                    break;
                case "4":
                    Program.MainWindow.AntiAliasingBox.SelectedIndex = 2;
                    break;
                case "8":
                    Program.MainWindow.AntiAliasingBox.SelectedIndex = 3;
                    break;
                case "10":
                    Program.MainWindow.AntiAliasingBox.SelectedIndex = 3;
                    break;
                default:
                    Program.MainWindow.AntiAliasingBox.SelectedIndex = 0;
                    break;
            }

            // Shadow Coverage
            switch (IniHandler.BmEngineData["SystemSettings"]["ShadowDepthBias"])
            {
                case "0.010000":
                    Program.MainWindow.shadowcoveragebox.SelectedIndex = 1;
                    break;
                case "0.008000":
                    Program.MainWindow.shadowcoveragebox.SelectedIndex = 2;
                    break;
                default:
                    Program.MainWindow.shadowcoveragebox.SelectedIndex = 0;
                    break;
            }

            // Anisotropic Filtering
            Program.MainWindow.AnisoBox.SelectedIndex = IniHandler.BmEngineData["SystemSettings"]["MaxAnisotropy"] switch
            {
                "8" => 1,
                "16" => 2,
                _ => 0,
            };

            // Ambient Occlusion
            if (IniHandler.BmEngineData["SystemSettings"]["AmbientOcclusion"] == "True")
            {
                Program.MainWindow.AmbientOcclusionBox.Checked = true;
            }
            else
            {
                Program.MainWindow.AmbientOcclusionBox.Checked = false;
            }

            // Shadow Quality
            // ShadowDepthBias is not read and only modified during the writing process.
            Program.MainWindow.ShadowQualityBox.SelectedIndex = IniHandler.BmEngineData["SystemSettings"]["MaxShadowResolution"] switch
            {
                "1024" => 1,
                "2048" => 2,
                "4096" => 3,
                _ => 0,
            };

            // Depth of Field
            if (IniHandler.BmEngineData["SystemSettings"]["DepthOfField"] == "True")
            {
                Program.MainWindow.DOFBox.Checked = true;
                Program.MainWindow.AdvancedColorBox.Enabled = true;
            }
            else
            {
                Program.MainWindow.DOFBox.Checked = false;
                Program.MainWindow.AdvancedColorBox.Text = "Advanced Color Settings ('Depth of Field' must be enabled to edit.)";
                Program.MainWindow.DefaultColorButton.Enabled = false;
                Program.MainWindow.NoirColorButton.Enabled = false;
                Program.MainWindow.MutedColorButton.Enabled = false;
                Program.MainWindow.LowContrastColorButton.Enabled = false;
                Program.MainWindow.HighContrastColorButton.Enabled = false;
                Program.MainWindow.VividColorButton.Enabled = false;
                Program.MainWindow.SaturationTrackbar.Enabled = false;
                Program.MainWindow.HighlightsTrackbar.Enabled = false;
                Program.MainWindow.MidtonesTrackbar.Enabled = false;
                Program.MainWindow.ShadowsTrackbar.Enabled = false;
            }

            // Motion Blur
            if (IniHandler.BmEngineData["SystemSettings"]["MotionBlur"] == "True")
            {
                Program.MainWindow.MotionBlurBox.Checked = true;
            }
            else
            {
                Program.MainWindow.MotionBlurBox.Checked = false;
            }

            // Fog Volumes
            if (IniHandler.BmEngineData["SystemSettings"]["FogVolumes"] == "True")
            {
                Program.MainWindow.DynLightBox.Checked = true;
            }
            else
            {
                Program.MainWindow.DynLightBox.Checked = false;
            }

            // Dynamic Shadows
            if (IniHandler.BmEngineData["SystemSettings"]["DynamicShadows"] == "True")
            {
                Program.MainWindow.DynShadowBox.Checked = true;
            }
            else
            {
                Program.MainWindow.DynShadowBox.Checked = false;
            }

            // Distortion
            if (IniHandler.BmEngineData["SystemSettings"]["Distortion"] == "True")
            {
                Program.MainWindow.DistortionBox.Checked = true;
            }
            else
            {
                Program.MainWindow.DistortionBox.Checked = false;
            }

            // Lens Flares
            if (IniHandler.BmEngineData["SystemSettings"]["LensFlares"] == "True")
            {
                Program.MainWindow.LensFlareBox.Checked = true;
            }
            else
            {
                Program.MainWindow.LensFlareBox.Checked = false;
            }

            // Bloom
            if (IniHandler.BmEngineData["SystemSettings"]["Bloom"] == "True")
            {
                Program.MainWindow.BloomBox.Checked = true;
            }
            else
            {
                Program.MainWindow.BloomBox.Checked = false;
            }

            // SH Lighting
            if (IniHandler.BmEngineData["SystemSettings"]["DisableSphericalHarmonicLights"] == "False")
            {
                Program.MainWindow.LightRayBox.Checked = true;
            }
            else
            {
                Program.MainWindow.LightRayBox.Checked = false;
            }

            // PhysX
            Program.MainWindow.PhysXBox.SelectedIndex = IniHandler.BmEngineData["Engine.Engine"]["PhysXLevel"] switch
            {
                "1" => 1,
                "2" => 2,
                _ => 0,
            };

            // Poolsize
            Program.MainWindow.PoolsizeBox.SelectedIndex = IniHandler.BmEngineData["TextureStreaming"]["PoolSize"] switch
            {
                "1024" => 1,
                "2048" => 2,
                "3072" => 3,
                "4096" => 4,
                "0" => 5,
                _ => 0,
            };
        }
        private void InitColors()
        {
            // Saturation
            Program.MainWindow.SaturationTrackbar.Value = Program.IniHandler.ColorIniToLauncher(IniHandler.BmEngineData["Engine.Player"]["PP_DesaturationMultiplier"]);
            Program.MainWindow.SaturationValueLabel.Text = Program.MainWindow.SaturationTrackbar.Value.ToString() + "%";

            // Highlights
            Program.MainWindow.HighlightsTrackbar.Value = Program.IniHandler.ColorIniToLauncher(IniHandler.BmEngineData["Engine.Player"]["PP_HighlightsMultiplier"]);
            Program.MainWindow.HighlightsValueLabel.Text = Program.MainWindow.HighlightsTrackbar.Value.ToString() + "%";

            // Midtones
            Program.MainWindow.MidtonesTrackbar.Value = Program.IniHandler.ColorIniToLauncher(IniHandler.BmEngineData["Engine.Player"]["PP_MidTonesMultiplier"]);
            Program.MainWindow.MidtonesValueLabel.Text = Program.MainWindow.MidtonesTrackbar.Value.ToString() + "%";

            // Shadows
            Program.MainWindow.ShadowsTrackbar.Value = Program.IniHandler.ColorIniToLauncher(IniHandler.BmEngineData["Engine.Player"]["PP_ShadowsMultiplier"]);
            Program.MainWindow.ShadowsValueLabel.Text = Program.MainWindow.ShadowsTrackbar.Value.ToString() + "%";
        }

        private void InitTexturePackFix()
        {
            for (int i = 0; i < Program.IniHandler.CustomLines.Length; i++)
            {
                if (Program.IniHandler.CustomLines[i].Equals("4096"))
                {
                    Program.IniHandler.TexPackEnabled[i] = true;
                }
                if (!Program.IniHandler.CustomLines[i].Equals(Program.IniHandler.ReturnTexGroupValue(Program.IniHandler.TexturePackDefaults[i])))
                {
                    Program.IniHandler.TexPackDisabled[i] = false;
                }
            }

            bool[] IsVanillaExceptReborn = new bool[24];

            for (int i = 0; i < Program.IniHandler.TexPackDisabled.Length; i++)
            {
                if (i == 3 || i == 4)
                {
                    continue;
                }

                if (i > 4)
                {
                    IsVanillaExceptReborn[i - 2] = Program.IniHandler.TexPackDisabled[i];
                }
                else
                {
                    IsVanillaExceptReborn[i] = Program.IniHandler.TexPackDisabled[i];
                }
            }

            if (Program.IniHandler.TexPackEnabled.All(x => x))
            {
                Program.MainWindow.texpacksupportbox.SelectedIndex = 2;
            }
            else if (Program.IniHandler.TexPackDisabled.All(x => x))
            {
                Program.MainWindow.texpacksupportbox.SelectedIndex = 0;
            }
            else if (Program.IniHandler.TexPackEnabled[3] && Program.IniHandler.TexPackEnabled[4] && IsVanillaExceptReborn.All(x => x))
            {
                Program.MainWindow.texpacksupportbox.SelectedIndex = 1;
            }
            else
            {
                Program.MainWindow.texpacksupportbox.SelectedIndex = 3;
            }
        }

        public void InitCustomLines()
        {
            Program.IniHandler.CustomLines[0] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_World"]);
            Program.IniHandler.CustomLines[1] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldNormalMap"]);
            Program.IniHandler.CustomLines[2] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldSpecular"]);
            Program.IniHandler.CustomLines[3] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Character"]);
            Program.IniHandler.CustomLines[4] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_CharacterNormalMap"]);
            Program.IniHandler.CustomLines[5] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_CharacterSpecular"]);
            Program.IniHandler.CustomLines[6] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon"]);
            Program.IniHandler.CustomLines[7] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WeaponNormalMap"]);
            Program.IniHandler.CustomLines[8] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WeaponSpecular"]);
            Program.IniHandler.CustomLines[9] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Vehicle"]);
            Program.IniHandler.CustomLines[10] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_VehicleNormalMap"]);
            Program.IniHandler.CustomLines[11] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_VehicleSpecular"]);
            Program.IniHandler.CustomLines[12] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Cinematic"]);
            Program.IniHandler.CustomLines[13] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Effects"]);
            Program.IniHandler.CustomLines[14] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_EffectsNotFiltered"]);
            Program.IniHandler.CustomLines[15] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Skybox"]);
            Program.IniHandler.CustomLines[16] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_UI"]);
            Program.IniHandler.CustomLines[17] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_LightAndShadowMap"]);
            Program.IniHandler.CustomLines[18] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_RenderTarget"]);
            Program.IniHandler.CustomLines[19] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon3P"]);
            Program.IniHandler.CustomLines[20] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon3PNormalMap"]);
            Program.IniHandler.CustomLines[21] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon3PSpecular"]);
            Program.IniHandler.CustomLines[22] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_World_Hi"]);
            Program.IniHandler.CustomLines[23] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldNormalMap_Hi"]);
            Program.IniHandler.CustomLines[24] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldSpecular_Hi"]);
            Program.IniHandler.CustomLines[25] = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Effects_Hi"]);
        }
    }
}
