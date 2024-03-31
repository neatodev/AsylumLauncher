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
            Program.MainWindow.LanguageBox.SelectedIndex = IniHandler.BmEngineData["Engine.Engine"]["Language"] switch
            {
                "Deu" => 1,
                "Esm" => 2,
                "Esn" => 3,
                "Fra" => 4,
                "Ita" => 5,
                "Jpn" => 6,
                "Kor" => 7,
                "Pol" => 8,
                "Por" => 9,
                "Rus" => 10,
                _ => 0,
            };
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
                case "10":
                    Program.MainWindow.AntiAliasingBox.SelectedIndex = 3;
                    break;
                default:
                    Program.MainWindow.AntiAliasingBox.SelectedIndex = 0;
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
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_World"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[0] = true;
            }
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldNormalMap"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[1] = true;
            }
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldSpecular"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[2] = true;
            }

            //// Asylum Reborn
            //TEXTUREGROUP_Character
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Character"] == Program.IniHandler.TexturePackEnabled[3])
            {
                Program.IniHandler.TexPackEnabled[3] = true;
            }
            //TEXTUREGROUP_CharacterNormalMap
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_CharacterNormalMap"] == Program.IniHandler.TexturePackEnabled[4])
            {
                Program.IniHandler.TexPackEnabled[4] = true;
            }
            ////

            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_CharacterSpecular"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[5] = true;
            }
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[6] = true;
            }
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WeaponNormalMap"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[7] = true;
            }
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WeaponSpecular"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[8] = true;
            }
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Vehicle"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[9] = true;
            }
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_VehicleNormalMap"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[10] = true;
            }
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_VehicleSpecular"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[11] = true;
            }
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Cinematic"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[12] = true;
            }
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Effects"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[13] = true;
            }
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_EffectsNotFiltered"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[14] = true;
            }
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Skybox"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[15] = true;
            }
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_UI"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[16] = true;
            }
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_LightAndShadowMap"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[17] = true;
            }
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_RenderTarget"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[18] = true;
            }
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon3P"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[19] = true;
            }
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon3PNormalMap"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[20] = true;
            }
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon3PSpecular"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[21] = true;
            }
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_World_Hi"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[22] = true;
            }
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldNormalMap_Hi"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[23] = true;
            }
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldSpecular_Hi"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[24] = true;
            }
            if (IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Effects_Hi"].Contains("MaxLODSize=4096"))
            {
                Program.IniHandler.TexPackEnabled[25] = true;
            }

            if (Program.IniHandler.TexPackEnabled.All(x => x))
            {
                Program.MainWindow.texpacksupportbox.SelectedIndex = 2;
            } 
            else if (Program.IniHandler.TexPackEnabled[3] && Program.IniHandler.TexPackEnabled[4])
            {
                Program.MainWindow.texpacksupportbox.SelectedIndex = 1;
            }
            else
            {
                Program.MainWindow.texpacksupportbox.SelectedIndex = 0;
            }
        }
    }
}
