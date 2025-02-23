using IniParser;
using NLog;
using System.Text.RegularExpressions;

namespace AsylumLauncher
{
    internal class IniWriter
    {
        private readonly string BmEnginePath;
        private readonly string UserEnginePath;
        private string BmEngineTemp;
        private string UserEngineLangValue = "Undefined";
        readonly FileIniDataParser DataParser;
        readonly string[] ExcludedEntries = { "LightComplexityColors", "ShaderComplexityColors" };

        private static Logger Nlog = LogManager.GetCurrentClassLogger();


        public IniWriter()
        {
            BmEnginePath = Program.FileHandler.BmEnginePath;
            UserEnginePath = Program.FileHandler.UserEnginePath;
            DataParser = new FileIniDataParser();
            Nlog.Info("Constructor - Successfully initialized IniWriter.");
        }

        public void WriteAll()
        {
            Program.FileHandler.BmEngine.IsReadOnly = false;
            Program.FileHandler.UserEngine.IsReadOnly = false;
            Program.FileHandler.BmInput.IsReadOnly = false;
            WriteBmEngineBasic();
            WriteBmEngineAdvanced();
            WriteColors();
            WriteTextureGroupLines();
            WriteToTempFile();
            MergeBmEngine();
            WriteToUserEngine();
            Program.FileHandler.RenameIntroVideoFiles();
            Program.FileHandler.BmEngine.IsReadOnly = true;
            Program.FileHandler.UserEngine.IsReadOnly = true;
            Program.FileHandler.BmInput.IsReadOnly = true;
            new IniReader().InitCustomLines();
            Nlog.Info("WriteAll - Successfully wrote settings to 'BmEngine.ini' and 'UserEngine.ini'.");
        }

        private void WriteToTempFile()
        {
            var TempDir = Path.Combine(Environment.CurrentDirectory, "Temp");
            BmEngineTemp = Path.Combine(TempDir, "BmEngineTemp.ini");
            Directory.CreateDirectory(TempDir);
            File.Create(BmEngineTemp).Dispose();

            DataParser.WriteFile(BmEngineTemp, IniHandler.BmEngineData);
        }

        private void MergeBmEngine()
        {
            string[] BmEngineOrig = File.ReadAllLines(BmEnginePath);
            string[] BmEngineNew = File.ReadAllLines(BmEngineTemp);

            for (int i = 0; i < BmEngineNew.Length - 1; i++)
            {
                if (BmEngineNew[i].Contains('='))
                {
                    var LineTrimmed = BmEngineNew[i].Substring(0, BmEngineNew[i].IndexOf('='));
                    for (int j = 0; j < BmEngineOrig.Length; j++)
                    {
                        if (BmEngineOrig[j].Contains('=') && BmEngineOrig[j].Substring(0, BmEngineOrig[j].IndexOf('=')) == LineTrimmed)
                        {
                            if (LineTrimmed == ExcludedEntries[0] || LineTrimmed == ExcludedEntries[1])
                            {
                                continue;
                            }
                            BmEngineOrig[j] = BmEngineNew[i];
                        }
                    }
                }
            }

            using (StreamWriter BmEngineFile = new(BmEnginePath))
            {
                foreach (string Line in BmEngineOrig)
                {
                    BmEngineFile.WriteLine(Line);
                }
                BmEngineFile.Close();
            }
            DeleteTempFolder();
        }

        private void WriteToUserEngine()
        {
            if (UserEngineLangValue != "Undefined")
            {
                string[] UserEngine = File.ReadAllLines(UserEnginePath);
                using (StreamWriter UserEngineFile = new(UserEnginePath))
                {
                    foreach (string Line in UserEngine)
                    {
                        if (Line.Contains("Language"))
                        {
                            UserEngineFile.WriteLine(UserEngineLangValue);
                            continue;
                        }
                        UserEngineFile.WriteLine(Line);
                    }
                    UserEngineFile.Close();
                }
            }
        }

        private void DeleteTempFolder()
        {
            File.Delete(BmEngineTemp);
            Directory.Delete(Path.Combine(Environment.CurrentDirectory, "Temp"));
        }

        private void WriteBmEngineBasic()
        {
            // Resolution
            var ResX = Program.MainWindow.ResolutionBox.SelectedItem.ToString().Substring(0, Program.MainWindow.ResolutionBox.SelectedItem.ToString().LastIndexOf("x"));
            var ResY = Program.MainWindow.ResolutionBox.SelectedItem.ToString().Substring(Program.MainWindow.ResolutionBox.SelectedItem.ToString().LastIndexOf("x") + 1);
            IniHandler.BmEngineData["SystemSettings"]["ResX"] = ResX;
            IniHandler.BmEngineData["SystemSettings"]["ResY"] = ResY;
            Nlog.Info("WriteBmEngineBasic - Set Resolution to {0}x{1}", ResX, ResY);

            // Fullscreen
            if (Program.MainWindow.FullscreenBox.SelectedIndex == 0)
            {
                IniHandler.BmEngineData["SystemSettings"]["Fullscreen"] = "True";
            }
            else
            {
                IniHandler.BmEngineData["SystemSettings"]["Fullscreen"] = "False";
            }
            Nlog.Info("WriteBmEngineBasic - Set Fullscreen to {0}", IniHandler.BmEngineData["SystemSettings"]["Fullscreen"]);

            // Smooth Frames
            if (Program.MainWindow.smoothframebox.Checked)
            {
                IniHandler.BmEngineData["Engine.GameEngine"]["bSmoothFrameRate"] = "True";
            }
            else
            {
                IniHandler.BmEngineData["Engine.GameEngine"]["bSmoothFrameRate"] = "False";
            }
            Nlog.Info("WriteBmEngineBasic - Set bSmoothFrameRate to {0}", IniHandler.BmEngineData["Engine.GameEngine"]["bSmoothFrameRate"]);

            // VSync
            if (Program.MainWindow.VsyncBox.SelectedIndex == 0)
            {
                IniHandler.BmEngineData["SystemSettings"]["UseVsync"] = "True";
            }
            else
            {
                IniHandler.BmEngineData["SystemSettings"]["UseVsync"] = "False";
            }
            Nlog.Info("WriteBmEngineBasic - Set VSync to {0}", IniHandler.BmEngineData["SystemSettings"]["UseVsync"]);

            // Detail Mode
            IniHandler.BmEngineData["SystemSettings"]["DetailMode"] = Program.MainWindow.DetailModeBox.SelectedIndex switch
            {
                1 => "1",
                2 => "2",
                _ => "0",
            };
            Nlog.Info("WriteBmEngineBasic - Set Detail Mode to {0}", IniHandler.BmEngineData["SystemSettings"]["DetailMode"]);

            // Framerate Cap
            short Framecap = short.Parse(Program.MainWindow.FrameCapTextBox.Text.Trim());
            if (Framecap <= 24)
            {
                Program.MainWindow.FrameCapTextBox.Text = "60";
                IniHandler.BmEngineData["Engine.GameEngine"]["MaxSmoothedFrameRate"] = "62";
            }
            else
            {
                Framecap += 2;
                IniHandler.BmEngineData["Engine.GameEngine"]["MaxSmoothedFrameRate"] = Framecap.ToString();
            }
            Nlog.Info("WriteBmEngineBasic - Set Framerate Limit to {0}", (Framecap - 2).ToString());

            // Language
            switch (Program.MainWindow.LanguageBox.SelectedIndex)
            {
                case 0:
                    IniHandler.BmEngineData["Engine.Engine"]["Language"] = "Int";
                    UserEngineLangValue = "Language=Int";
                    break;
                case 1:
                    IniHandler.BmEngineData["Engine.Engine"]["Language"] = "Deu";
                    UserEngineLangValue = "Language=Deu";
                    break;
                case 2:
                    IniHandler.BmEngineData["Engine.Engine"]["Language"] = "Esn";
                    UserEngineLangValue = "Language=Esn";
                    break;
                case 3:
                    IniHandler.BmEngineData["Engine.Engine"]["Language"] = "Fra";
                    UserEngineLangValue = "Language=Fra";
                    break;
                case 4:
                    IniHandler.BmEngineData["Engine.Engine"]["Language"] = "Ita";
                    UserEngineLangValue = "Language=Ita";
                    break;
                case 5:
                    IniHandler.BmEngineData["Engine.Engine"]["Language"] = "Pol";
                    UserEngineLangValue = "Language=Pol";
                    break;
                case 6:
                    IniHandler.BmEngineData["Engine.Engine"]["Language"] = "Rus";
                    UserEngineLangValue = "Language=Rus";
                    break;
                case 7:
                    IniHandler.BmEngineData["Engine.Engine"]["Language"] = "Jpn";
                    UserEngineLangValue = "Language=Jpn";
                    break;
                default:
                    break;
            }
            Nlog.Info("WriteBmEngineBasic - Set Language to {0}", UserEngineLangValue);
        }

        private void WriteBmEngineAdvanced()
        {
            // Anti-Aliasing
            switch (Program.MainWindow.AntiAliasingBox.SelectedIndex)
            {
                case 1:
                    IniHandler.BmEngineData["SystemSettings"]["MaxMultisamples"] = "2";
                    break;
                case 2:
                    IniHandler.BmEngineData["SystemSettings"]["MaxMultisamples"] = "4";
                    break;
                case 3:
                    IniHandler.BmEngineData["SystemSettings"]["MaxMultisamples"] = "8";
                    break;
                case 4:
                    IniHandler.BmEngineData["SystemSettings"]["MaxMultisamples"] = "10";
                    break;
                default:
                    IniHandler.BmEngineData["SystemSettings"]["MaxMultisamples"] = "1";
                    break;
            }
            Nlog.Info("WriteBmEngineAdvanced - Set MaxMultisamples to {0}", IniHandler.BmEngineData["SystemSettings"]["MaxMultisamples"]);

            // Anisotropic Filtering
            IniHandler.BmEngineData["SystemSettings"]["MaxAnisotropy"] = Program.MainWindow.AnisoBox.SelectedIndex switch
            {
                1 => "8",
                2 => "16",
                _ => "4",
            };
            Nlog.Info("WriteBmEngineAdvanced - Set Anisotropic Filtering to {0}", IniHandler.BmEngineData["SystemSettings"]["MaxAnisotropy"]);

            // Ambient Occlusion
            if (Program.MainWindow.AmbientOcclusionBox.Checked)
            {
                IniHandler.BmEngineData["SystemSettings"]["AmbientOcclusion"] = "True";
            }
            else
            {
                IniHandler.BmEngineData["SystemSettings"]["AmbientOcclusion"] = "False";
            }
            Nlog.Info("WriteBmEngineAdvanced - Set Ambient Occlusion to {0}", IniHandler.BmEngineData["SystemSettings"]["AmbientOcclusion"]);

            // Shadow Quality
            switch (Program.MainWindow.ShadowQualityBox.SelectedIndex)
            {
                case 1:
                    IniHandler.BmEngineData["SystemSettings"]["MaxShadowResolution"] = "1024";
                    IniHandler.BmEngineData["SystemSettings"]["ShadowFilterRadius"] = "5.0";
                    break;
                case 2:
                    IniHandler.BmEngineData["SystemSettings"]["MaxShadowResolution"] = "2048";
                    IniHandler.BmEngineData["SystemSettings"]["ShadowFilterRadius"] = "5.0";
                    break;
                case 3:
                    IniHandler.BmEngineData["SystemSettings"]["MaxShadowResolution"] = "4096";
                    IniHandler.BmEngineData["SystemSettings"]["ShadowFilterRadius"] = "3.0";
                    break;
                default:
                    IniHandler.BmEngineData["SystemSettings"]["MaxShadowResolution"] = "512";
                    IniHandler.BmEngineData["SystemSettings"]["ShadowFilterRadius"] = "2.000000";
                    break;
            }
            Nlog.Info("WriteBmEngineAdvanced - Set MaxShadowResolution to {0} and ShadowFilterRadius to {1}", IniHandler.BmEngineData["SystemSettings"]["MaxShadowResolution"], IniHandler.BmEngineData["SystemSettings"]["ShadowFilterRadius"]);

            // Shadow Coverage
            switch (Program.MainWindow.shadowcoveragebox.SelectedIndex)
            {
                case 1:
                    IniHandler.BmEngineData["SystemSettings"]["ShadowDepthBias"] = "0.010000";
                    break;
                case 2:
                    IniHandler.BmEngineData["SystemSettings"]["ShadowDepthBias"] = "0.008000";
                    break;
                default:
                    IniHandler.BmEngineData["SystemSettings"]["ShadowDepthBias"] = "0.012000";
                    break;
            }
            Nlog.Info("WriteBmEngineAdvanced - Set ShadowDepthBias to {0}", IniHandler.BmEngineData["SystemSettings"]["ShadowDepthBias"]);

            // Depth of Field
            if (Program.MainWindow.DOFBox.Checked)
            {
                IniHandler.BmEngineData["SystemSettings"]["DepthOfField"] = "True";
            }
            else
            {
                IniHandler.BmEngineData["SystemSettings"]["DepthOfField"] = "False";
            }
            Nlog.Info("WriteBmEngineAdvanced - Set Depth of Field to {0}", IniHandler.BmEngineData["SystemSettings"]["DepthOfField"]);

            // Motion Blur
            if (Program.MainWindow.MotionBlurBox.Checked)
            {
                IniHandler.BmEngineData["SystemSettings"]["MotionBlur"] = "True";
            }
            else
            {
                IniHandler.BmEngineData["SystemSettings"]["MotionBlur"] = "False";
            }
            Nlog.Info("WriteBmEngineAdvanced - Set Motion Blur to {0}", IniHandler.BmEngineData["SystemSettings"]["MotionBlur"]);

            // Fog Volumes
            if (Program.MainWindow.DynLightBox.Checked)
            {
                IniHandler.BmEngineData["SystemSettings"]["FogVolumes"] = "True";
            }
            else
            {
                IniHandler.BmEngineData["SystemSettings"]["FogVolumes"] = "False";
            }
            Nlog.Info("WriteBmEngineAdvanced - Set Fog Volumes to {0}", IniHandler.BmEngineData["SystemSettings"]["FogVolumes"]);

            // Dynamic Shadows
            if (Program.MainWindow.DynShadowBox.Checked)
            {
                IniHandler.BmEngineData["SystemSettings"]["DynamicShadows"] = "True";
            }
            else
            {
                IniHandler.BmEngineData["SystemSettings"]["DynamicShadows"] = "False";
            }
            Nlog.Info("WriteBmEngineAdvanced - Set Dynamic Shadows to {0}", IniHandler.BmEngineData["SystemSettings"]["DynamicShadows"]);

            // Distortion
            if (Program.MainWindow.DistortionBox.Checked)
            {
                IniHandler.BmEngineData["SystemSettings"]["Distortion"] = "True";
            }
            else
            {
                IniHandler.BmEngineData["SystemSettings"]["Distortion"] = "False";
            }
            Nlog.Info("WriteBmEngineAdvanced - Set Distortion to {0}", IniHandler.BmEngineData["SystemSettings"]["Distortion"]);

            // Lens Flares
            if (Program.MainWindow.LensFlareBox.Checked)
            {
                IniHandler.BmEngineData["SystemSettings"]["LensFlares"] = "True";
            }
            else
            {
                IniHandler.BmEngineData["SystemSettings"]["LensFlares"] = "False";
            }
            Nlog.Info("WriteBmEngineAdvanced - Set Lens Flares to {0}", IniHandler.BmEngineData["SystemSettings"]["LensFlares"]);

            // Bloom
            if (Program.MainWindow.BloomBox.Checked)
            {
                IniHandler.BmEngineData["SystemSettings"]["Bloom"] = "True";
            }
            else
            {
                IniHandler.BmEngineData["SystemSettings"]["Bloom"] = "False";
            }
            Nlog.Info("WriteBmEngineAdvanced - Set Bloom to {0}", IniHandler.BmEngineData["SystemSettings"]["Bloom"]);

            // SH Lighting
            if (Program.MainWindow.LightRayBox.Checked)
            {
                IniHandler.BmEngineData["SystemSettings"]["DisableSphericalHarmonicLights"] = "False";
            }
            else
            {
                IniHandler.BmEngineData["SystemSettings"]["DisableSphericalHarmonicLights"] = "True";
            }
            Nlog.Info("WriteBmEngineAdvanced - Set DisableSphericalHarmonicLights to {0}", IniHandler.BmEngineData["SystemSettings"]["DisableSphericalHarmonicLights"]);

            // PhysX
            IniHandler.BmEngineData["Engine.Engine"]["PhysXLevel"] = Program.MainWindow.PhysXBox.SelectedIndex switch
            {
                1 => "1",
                2 => "2",
                _ => "0",
            };
            Nlog.Info("WriteBmEngineAdvanced - Set PhysX to {0}", IniHandler.BmEngineData["Engine.Engine"]["PhysXLevel"]);

            // Poolsize
            switch (Program.MainWindow.PoolsizeBox.SelectedIndex)
            {
                case 1:
                    IniHandler.BmEngineData["TextureStreaming"]["PoolSize"] = "1024";
                    break;
                case 2:
                    IniHandler.BmEngineData["TextureStreaming"]["PoolSize"] = "2048";
                    break;
                case 3:
                    IniHandler.BmEngineData["TextureStreaming"]["PoolSize"] = "3072";
                    break;
                case 4:
                    IniHandler.BmEngineData["TextureStreaming"]["PoolSize"] = "4096";
                    break;
                case 5:
                    IniHandler.BmEngineData["TextureStreaming"]["PoolSize"] = "0";
                    break;
                default:
                    IniHandler.BmEngineData["TextureStreaming"]["PoolSize"] = "512";
                    break;
            }
            Nlog.Info("WriteBmEngineAdvanced - Set Poolsize to {0}. Set MemoryMargin to {1}", IniHandler.BmEngineData["TextureStreaming"]["PoolSize"], IniHandler.BmEngineData["TextureStreaming"]["MemoryMargin"]);
        }

        private void WriteColors()
        {
            // Saturation
            IniHandler.BmEngineData["Engine.Player"]["PP_DesaturationMultiplier"] = Program.IniHandler.ColorLauncherToIni(Program.MainWindow.SaturationTrackbar.Value);
            Nlog.Info("WriteColors - Set Saturation to {0}", IniHandler.BmEngineData["Engine.Player"]["PP_DesaturationMultiplier"]);

            // Highlights
            IniHandler.BmEngineData["Engine.Player"]["PP_HighlightsMultiplier"] = Program.IniHandler.ColorLauncherToIni(Program.MainWindow.HighlightsTrackbar.Value);
            Nlog.Info("WriteColors - Set Highlights to {0}", IniHandler.BmEngineData["Engine.Player"]["PP_HighlightsMultiplier"]);

            // Midtones
            IniHandler.BmEngineData["Engine.Player"]["PP_MidTonesMultiplier"] = Program.IniHandler.ColorLauncherToIni(Program.MainWindow.MidtonesTrackbar.Value);
            Nlog.Info("WriteColors - Set Midtones to {0}", IniHandler.BmEngineData["Engine.Player"]["PP_MidTonesMultiplier"]);

            // Shadows
            IniHandler.BmEngineData["Engine.Player"]["PP_ShadowsMultiplier"] = Program.IniHandler.ColorLauncherToIni(Program.MainWindow.ShadowsTrackbar.Value);
            Nlog.Info("WriteColors - Set Shadows to {0}", IniHandler.BmEngineData["Engine.Player"]["PP_ShadowsMultiplier"]);
        }

        private void WriteTextureGroupLines()
        {
            if (Program.MainWindow.texpacksupportbox.SelectedIndex == 3)
            {
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_World"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[0], "MaxLODSize=" + Program.IniHandler.CustomLines[0]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldNormalMap"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[1], "MaxLODSize=" + Program.IniHandler.CustomLines[1]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldSpecular"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[2], "MaxLODSize=" + Program.IniHandler.CustomLines[2]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Character"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[3], "MaxLODSize=" + Program.IniHandler.CustomLines[3]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_CharacterNormalMap"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[4], "MaxLODSize=" + Program.IniHandler.CustomLines[4]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_CharacterSpecular"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[5], "MaxLODSize=" + Program.IniHandler.CustomLines[5]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[6], "MaxLODSize=" + Program.IniHandler.CustomLines[6]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WeaponNormalMap"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[7], "MaxLODSize=" + Program.IniHandler.CustomLines[7]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WeaponSpecular"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[8], "MaxLODSize=" + Program.IniHandler.CustomLines[8]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Vehicle"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[9], "MaxLODSize=" + Program.IniHandler.CustomLines[9]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_VehicleNormalMap"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[10], "MaxLODSize=" + Program.IniHandler.CustomLines[10]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_VehicleSpecular"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[11], "MaxLODSize=" + Program.IniHandler.CustomLines[11]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Cinematic"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[12], "MaxLODSize=" + Program.IniHandler.CustomLines[12]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Effects"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[13], "MaxLODSize=" + Program.IniHandler.CustomLines[13]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_EffectsNotFiltered"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[14], "MaxLODSize=" + Program.IniHandler.CustomLines[14]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Skybox"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[15], "MaxLODSize=" + Program.IniHandler.CustomLines[15]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_UI"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[16], "MaxLODSize=" + Program.IniHandler.CustomLines[16]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_LightAndShadowMap"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[17], "MaxLODSize=" + Program.IniHandler.CustomLines[17]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_RenderTarget"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[18], "MaxLODSize=" + Program.IniHandler.CustomLines[18]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon3P"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[19], "MaxLODSize=" + Program.IniHandler.CustomLines[19]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon3PNormalMap"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[20], "MaxLODSize=" + Program.IniHandler.CustomLines[20]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon3PSpecular"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[21], "MaxLODSize=" + Program.IniHandler.CustomLines[21]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_World_Hi"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[22], "MaxLODSize=" + Program.IniHandler.CustomLines[22]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldNormalMap_Hi"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[23], "MaxLODSize=" + Program.IniHandler.CustomLines[23]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldSpecular_Hi"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[24], "MaxLODSize=" + Program.IniHandler.CustomLines[24]);
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Effects_Hi"] = ConcatTexString(Program.IniHandler.TexturePackMaximum[25], "MaxLODSize=" + Program.IniHandler.CustomLines[25]);
                Nlog.Info("WriteTextureGroupLines - Set Texture Pack Support to: Custom");
            }
            else if (Program.MainWindow.texpacksupportbox.SelectedIndex == 2)
            {
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_World"] = Program.IniHandler.TexturePackMaximum[0];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldNormalMap"] = Program.IniHandler.TexturePackMaximum[1];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldSpecular"] = Program.IniHandler.TexturePackMaximum[2];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Character"] = Program.IniHandler.TexturePackMaximum[3];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_CharacterNormalMap"] = Program.IniHandler.TexturePackMaximum[4];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_CharacterSpecular"] = Program.IniHandler.TexturePackMaximum[5];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon"] = Program.IniHandler.TexturePackMaximum[6];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WeaponNormalMap"] = Program.IniHandler.TexturePackMaximum[7];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WeaponSpecular"] = Program.IniHandler.TexturePackMaximum[8];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Vehicle"] = Program.IniHandler.TexturePackMaximum[9];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_VehicleNormalMap"] = Program.IniHandler.TexturePackMaximum[10];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_VehicleSpecular"] = Program.IniHandler.TexturePackMaximum[11];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Cinematic"] = Program.IniHandler.TexturePackMaximum[12];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Effects"] = Program.IniHandler.TexturePackMaximum[13];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_EffectsNotFiltered"] = Program.IniHandler.TexturePackMaximum[14];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Skybox"] = Program.IniHandler.TexturePackMaximum[15];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_UI"] = Program.IniHandler.TexturePackMaximum[16];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_LightAndShadowMap"] = Program.IniHandler.TexturePackMaximum[17];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_RenderTarget"] = Program.IniHandler.TexturePackMaximum[18];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon3P"] = Program.IniHandler.TexturePackMaximum[19];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon3PNormalMap"] = Program.IniHandler.TexturePackMaximum[20];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon3PSpecular"] = Program.IniHandler.TexturePackMaximum[21];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_World_Hi"] = Program.IniHandler.TexturePackMaximum[22];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldNormalMap_Hi"] = Program.IniHandler.TexturePackMaximum[23];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldSpecular_Hi"] = Program.IniHandler.TexturePackMaximum[24];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Effects_Hi"] = Program.IniHandler.TexturePackMaximum[25];
                Nlog.Info("WriteTextureGroupLines - Set Texture Pack Support to: Maximum");
            }
            else if (Program.MainWindow.texpacksupportbox.SelectedIndex == 1)
            {
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_World"] = Program.IniHandler.TexturePackEnabled[0];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldNormalMap"] = Program.IniHandler.TexturePackEnabled[1];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldSpecular"] = Program.IniHandler.TexturePackEnabled[2];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Character"] = Program.IniHandler.TexturePackEnabled[3];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_CharacterNormalMap"] = Program.IniHandler.TexturePackEnabled[4];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_CharacterSpecular"] = Program.IniHandler.TexturePackEnabled[5];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon"] = Program.IniHandler.TexturePackEnabled[6];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WeaponNormalMap"] = Program.IniHandler.TexturePackEnabled[7];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WeaponSpecular"] = Program.IniHandler.TexturePackEnabled[8];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Vehicle"] = Program.IniHandler.TexturePackEnabled[9];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_VehicleNormalMap"] = Program.IniHandler.TexturePackEnabled[10];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_VehicleSpecular"] = Program.IniHandler.TexturePackEnabled[11];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Cinematic"] = Program.IniHandler.TexturePackEnabled[12];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Effects"] = Program.IniHandler.TexturePackEnabled[13];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_EffectsNotFiltered"] = Program.IniHandler.TexturePackEnabled[14];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Skybox"] = Program.IniHandler.TexturePackEnabled[15];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_UI"] = Program.IniHandler.TexturePackEnabled[16];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_LightAndShadowMap"] = Program.IniHandler.TexturePackEnabled[17];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_RenderTarget"] = Program.IniHandler.TexturePackEnabled[18];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon3P"] = Program.IniHandler.TexturePackEnabled[19];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon3PNormalMap"] = Program.IniHandler.TexturePackEnabled[20];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon3PSpecular"] = Program.IniHandler.TexturePackEnabled[21];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_World_Hi"] = Program.IniHandler.TexturePackEnabled[22];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldNormalMap_Hi"] = Program.IniHandler.TexturePackEnabled[23];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldSpecular_Hi"] = Program.IniHandler.TexturePackEnabled[24];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Effects_Hi"] = Program.IniHandler.TexturePackEnabled[25];
                Nlog.Info("WriteTextureGroupLines - Set Texture Pack Support to: Asylum Reborn");
            }
            else
            {
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_World"] = Program.IniHandler.TexturePackDefaults[0];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldNormalMap"] = Program.IniHandler.TexturePackDefaults[1];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldSpecular"] = Program.IniHandler.TexturePackDefaults[2];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Character"] = Program.IniHandler.TexturePackDefaults[3];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_CharacterNormalMap"] = Program.IniHandler.TexturePackDefaults[4];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_CharacterSpecular"] = Program.IniHandler.TexturePackDefaults[5];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon"] = Program.IniHandler.TexturePackDefaults[6];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WeaponNormalMap"] = Program.IniHandler.TexturePackDefaults[7];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WeaponSpecular"] = Program.IniHandler.TexturePackDefaults[8];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Vehicle"] = Program.IniHandler.TexturePackDefaults[9];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_VehicleNormalMap"] = Program.IniHandler.TexturePackDefaults[10];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_VehicleSpecular"] = Program.IniHandler.TexturePackDefaults[11];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Cinematic"] = Program.IniHandler.TexturePackDefaults[12];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Effects"] = Program.IniHandler.TexturePackDefaults[13];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_EffectsNotFiltered"] = Program.IniHandler.TexturePackDefaults[14];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Skybox"] = Program.IniHandler.TexturePackDefaults[15];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_UI"] = Program.IniHandler.TexturePackDefaults[16];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_LightAndShadowMap"] = Program.IniHandler.TexturePackDefaults[17];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_RenderTarget"] = Program.IniHandler.TexturePackDefaults[18];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon3P"] = Program.IniHandler.TexturePackDefaults[19];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon3PNormalMap"] = Program.IniHandler.TexturePackDefaults[20];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon3PSpecular"] = Program.IniHandler.TexturePackDefaults[21];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_World_Hi"] = Program.IniHandler.TexturePackDefaults[22];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldNormalMap_Hi"] = Program.IniHandler.TexturePackDefaults[23];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldSpecular_Hi"] = Program.IniHandler.TexturePackDefaults[24];
                IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Effects_Hi"] = Program.IniHandler.TexturePackDefaults[25];
                Nlog.Info("WriteTextureGroupLines - Set Texture Pack Support to: Disabled");
            }
        }

        private string ConcatTexString(string TexLine, string NewValue)
        {
            string CleanedNewValue = Regex.Replace(NewValue, @"\s", string.Empty);
            string Result = TexLine.Replace("MaxLODSize=4096", CleanedNewValue);
            return Result;
        }
    }
}
