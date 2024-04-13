using AsylumLauncher.Properties;
using NLog;
using System.Diagnostics;
using System.Globalization;
using System.Media;

namespace AsylumLauncher
{
    public partial class AsylumLauncher : Form
    {
        private bool DisplaySetting = false;
        private bool ControlSetting = false;
        private bool FirstStart = false;
        private ImageTooltip ImgToolTip;
        public bool DisplaySettingChanged
        {
            get => DisplaySetting;
            set
            {
                DisplaySetting = value;
                ApplySettingsButton.Enabled = true;
            }
        }
        public bool ControlSettingChanged
        {
            get => ControlSetting;
            set
            {
                ControlSetting = value;
                ApplySettingsButton.Enabled = true;
            }
        }

        private static readonly Logger Nlog = LogManager.GetCurrentClassLogger();

        public AsylumLauncher()
        {
            InitializeComponent();
            InitAdminPerms();
            ImgToolTip = new();
            ImgToolTip.InitialDelay = 50;
            ImgToolTip.AutoPopDelay = 5000000;
            ImgToolTip.AutomaticDelay = 500;
            ImgToolTip.ReshowDelay = 20;
            ImgToolTip.SetToolTip(DefaultColorButton, "Default");
            ImgToolTip.SetToolTip(NoirColorButton, "Monochrome");
            ImgToolTip.SetToolTip(MutedColorButton, "Muted");
            ImgToolTip.SetToolTip(LowContrastColorButton, "Log Profile 1");
            ImgToolTip.SetToolTip(VividColorButton, "Log Profile 2");
            ImgToolTip.SetToolTip(HighContrastColorButton, "High Contrast");
        }

        private void InitAdminPerms()
        {
            if (Program.IsAdmin)
            {
                RunAsAdminButton.Enabled = false;
                hbaopluscheckbox.Enabled = true;
                RunAsAdminButton.BackgroundImage = Resources.monochrome;
            }
        }

        private void ApplySettingsButton_Click(object sender, EventArgs e)
        {
            if (DisplaySettingChanged)
            {
                new IniWriter().WriteAll();
            }
            if (ControlSettingChanged)
            {
                new InputWriter().WriteAll();
            }
            ApplySettingsButton.Enabled = false;
        }

        private void ApplySettingsButton_Click()
        {
            if (DisplaySettingChanged)
            {
                new IniWriter().WriteAll();
            }
            if (ControlSettingChanged)
            {
                new InputWriter().WriteAll();
            }
            ApplySettingsButton.Enabled = false;
        }

        private void SkipIntroBox_CheckedChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void ManualModeButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "This option removes the 'read-only' flag of the configuration files, allowing for manual edits.\r\n" +
                "Starting the game through the launcher will re-add the 'read-only' flag, so make any desired edits before that.\r\n\r\n" +
                "Clicking 'Yes' will remove the flag, close the application and open your config directory.", @"Enable Manual Editing",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                Program.FileHandler.BmEngine.IsReadOnly = false;
                Program.FileHandler.UserEngine.IsReadOnly = false;
                Program.FileHandler.BmInput.IsReadOnly = false;
                Process.Start("explorer.exe", Program.FileHandler.ConfigDirectoryPath);
                Application.Exit();
            }
        }

        private void MouseSensitivityTrackbar_Scroll(object sender, EventArgs e)
        {
            MouseSensitivityValueLabel.Text = MouseSensitivityTrackbar.Value.ToString();
            ControlSettingChanged = true;
        }

        private void CustomFoV1Trackbar_Scroll(object sender, EventArgs e)
        {
            CustomFoV1ValueLabel.Text = CustomFoV1Trackbar.Value.ToString();
            ControlSettingChanged = true;
        }

        private void CustomFoV2Trackbar_Scroll(object sender, EventArgs e)
        {
            CustomFoV2ValueLabel.Text = CustomFoV2Trackbar.Value.ToString();
            ControlSettingChanged = true;
        }

        private void FwButton1_Click(object sender, EventArgs e)
        {
            new InputForm(FwButton1).ShowDialog();
        }

        private void BwButton1_Click(object sender, EventArgs e)
        {
            new InputForm(BwButton1).ShowDialog();
        }

        private void LeftButton1_Click(object sender, EventArgs e)
        {
            new InputForm(LeftButton1).ShowDialog();
        }

        private void RightButton1_Click(object sender, EventArgs e)
        {
            new InputForm(RightButton1).ShowDialog();
        }

        private void ACTButton1_Click(object sender, EventArgs e)
        {
            new InputForm(ACTButton1).ShowDialog();
        }

        private void PrevGadgetButton1_Click(object sender, EventArgs e)
        {
            new InputForm(PrevGadgetButton1).ShowDialog();
        }

        private void NextGadgetButton1_Click(object sender, EventArgs e)
        {
            new InputForm(NextGadgetButton1).ShowDialog();
        }

        private void UseGadgetStrikeButton1_Click(object sender, EventArgs e)
        {
            new InputForm(UseGadgetStrikeButton1).ShowDialog();
        }

        private void ZoomButton1_Click(object sender, EventArgs e)
        {
            new InputForm(ZoomButton1).ShowDialog();
        }

        private void CrouchButton1_Click(object sender, EventArgs e)
        {
            new InputForm(CrouchButton1).ShowDialog();
        }

        private void ToggleCrouchButton1_Click(object sender, EventArgs e)
        {
            new InputForm(ToggleCrouchButton1).ShowDialog();
        }

        private void RGUButton1_Click(object sender, EventArgs e)
        {
            new InputForm(RGUButton1).ShowDialog();
        }
        private void GrappleButton1_Click(object sender, EventArgs e)
        {
            new InputForm(GrappleButton1).ShowDialog();

        }

        private void GadgetSecButton1_Click(object sender, EventArgs e)
        {
            new InputForm(GadgetSecButton1).ShowDialog();
        }

        private void DetectiveModeButton1_Click(object sender, EventArgs e)
        {
            new InputForm(DetectiveModeButton1).ShowDialog();
        }

        private void ToggleHudButton_Click(object sender, EventArgs e)
        {
            new InputForm(ToggleHudButton).ShowDialog();
        }

        private void SpeedRunButton_Click(object sender, EventArgs e)
        {
            new InputForm(SpeedRunButton).ShowDialog();
        }

        private void CentreCameraButton_Click(object sender, EventArgs e)
        {
            new InputForm(CentreCameraButton).ShowDialog();
        }

        private void CustomFoV1Button_Click(object sender, EventArgs e)
        {
            new InputForm(CustomFoV1Button).ShowDialog();
        }

        private void CustomFoV2Button_Click(object sender, EventArgs e)
        {
            new InputForm(CustomFoV2Button).ShowDialog();
        }

        private void ResetFoVButton_Click(object sender, EventArgs e)
        {
            new InputForm(ResetFoVButton).ShowDialog();
        }

        private void OpenConsoleButton_Click(object sender, EventArgs e)
        {
            new InputForm(OpenConsoleButton).ShowDialog();
        }

        private void ResetControlButton_Click(object sender, EventArgs e)
        {
            Program.InputHandler.ResetControls();
        }

        private void ResetDisplayButton_Click(object sender, EventArgs e)
        {
            Program.IniHandler.ResetDisplay();
        }

        private void CapeStunButton_Click(object sender, EventArgs e)
        {
            new InputForm(CapeStunButton).ShowDialog();
        }

        private void MapButton_Click(object sender, EventArgs e)
        {
            new InputForm(MapButton).ShowDialog();
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            using (Process LaunchGame = new())
            {
                if (FileHandler.DetectGameExe())
                {
                    this.StartGameButton.Image = (Image)Resources.Phase3;
                    this.ActiveControl = null;
                    if (ApplySettingsButton.Enabled)
                    {
                        ApplySettingsButton_Click();
                    }
                    LaunchGame.StartInfo.FileName = "ShippingPC-BmGame.exe";
                    LaunchGame.StartInfo.CreateNoWindow = true;
                    LaunchGame.Start();
                    new SoundPlayer(Resources.startup).PlaySync();
                    Nlog.Info("StartGameButton_Click - Launching the game. Concluding logs at {0} on {1}.", DateTime.Now.ToString("HH:mm:ss"), DateTime.Now.ToString("D", new CultureInfo("en-GB")));
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("Could not find 'ShippingPC-BmGame.exe'.\nIs the Launcher in the correct folder?", "Error!", MessageBoxButtons.OK);
                }
            }
        }

        private void TempLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = @"https://neatodev.github.io/AsylumLauncher/", UseShellExecute = true });
        }

        private void ResolutionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void VsyncBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void FullscreenBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void DetailModeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (texpacksupportbox.SelectedIndex == 1 || texpacksupportbox.SelectedIndex == 2)
            {
                DetailModeBox.SelectedIndex = 2;
            }
            DisplaySettingChanged = true;
        }

        private void LanguageBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void FrameCapTextBox_MaskChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void AmbientOcclusionBox_CheckedChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void MotionBlurBox_CheckedChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void DynShadowBox_CheckedChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void DistortionBox_CheckedChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void ReflectionBox_CheckedChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void DOFBox_CheckedChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
            if (DOFBox.Checked)
            {
                Program.MainWindow.AdvancedColorBox.Text = "ADVANCED COLOR SETTINGS";
                Program.MainWindow.DefaultColorButton.Enabled = true;
                Program.MainWindow.NoirColorButton.Enabled = true;
                Program.MainWindow.MutedColorButton.Enabled = true;
                Program.MainWindow.LowContrastColorButton.Enabled = true;
                Program.MainWindow.HighContrastColorButton.Enabled = true;
                Program.MainWindow.VividColorButton.Enabled = true;
                Program.MainWindow.SaturationTrackbar.Enabled = true;
                Program.MainWindow.HighlightsTrackbar.Enabled = true;
                Program.MainWindow.MidtonesTrackbar.Enabled = true;
                Program.MainWindow.ShadowsTrackbar.Enabled = true;

            }
            else
            {
                Program.MainWindow.AdvancedColorBox.Text = "ADVANCED COLOR SETTINGS ('Depth of Field' must be enabled to edit.)";
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
        }

        private void LightRayBox_CheckedChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void DynLightBox_CheckedChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void LensFlareBox_CheckedChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void BloomBox_CheckedChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void AntiAliasingBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void MVSSBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void HbaoBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void PhysXBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void PoolsizeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void AnisoBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void TessellationBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void ShadowQualityBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void ShadowDrawDistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void ShadowSoftnessBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void VanillaPresetButton_Click(object sender, EventArgs e)
        {
            PresetHandler.SetVanilla();
            DisplaySettingChanged = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PresetHandler.SetOptimized();
            DisplaySettingChanged = true;
        }

        private void DarkKnightPresetButton_Click(object sender, EventArgs e)
        {
            PresetHandler.SetDarkKnight();
            DisplaySettingChanged = true;
        }

        private void DebugMenuButton_Click(object sender, EventArgs e)
        {
            new InputForm(DebugMenuButton).ShowDialog();
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == tabControl1.SelectedIndex)
            {
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text,
                    tabControl1.TabPages[e.Index].Font,
                    Brushes.RoyalBlue,
                    new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
            }
            else
            {
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text,
                    tabControl1.TabPages[e.Index].Font,
                    Brushes.Black,
                    new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
            }
        }

        private void StartGameButton_MouseEnter(object sender, EventArgs e)
        {
            this.StartGameButton.BackgroundImage = (Image)Resources.Phase2;
        }

        private void StartGameButton_MouseLeave(object sender, EventArgs e)
        {
            this.StartGameButton.BackgroundImage = (Image)Resources.Phase1;
        }

        private void NeatoLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = @"https://www.nexusmods.com/users/81089053", UseShellExecute = true });
        }

        private void GPUnityLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = @"https://www.youtube.com/c/GPUnity", UseShellExecute = true });
        }

        private void PayPalLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = @"https://www.paypal.com/donate/?hosted_button_id=LG7YTKP4JYN5S", UseShellExecute = true });
        }

        private void StartGameButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.StartGameButton.BackgroundImage = (Image)Resources.Phase3;
            }
        }

        private void MouseSmoothingBox_Click(object sender, EventArgs e)
        {
            this.ControlSettingChanged = true;
        }

        private void FrameCapTextBox_TextChanged(object sender, EventArgs e)
        {
            this.DisplaySettingChanged = true;
        }

        private void CTDownButton1_Click(object sender, EventArgs e)
        {
            new InputForm(CTDownButton1).ShowDialog();
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = @"https://web.archive.org/web/20230310012657/https://www.speedrun.com/arkhamasylum/guide/ng47r", UseShellExecute = true });
        }

        private void SaturationTrackbar_Scroll(object sender, EventArgs e)
        {
            SaturationValueLabel.Text = SaturationTrackbar.Value.ToString() + "%";
            DisplaySettingChanged = true;
        }

        private void HighlightsTrackbar_Scroll(object sender, EventArgs e)
        {
            HighlightsValueLabel.Text = HighlightsTrackbar.Value.ToString() + "%";
            DisplaySettingChanged = true;
        }

        private void MidtonesTrackbar_Scroll(object sender, EventArgs e)
        {
            MidtonesValueLabel.Text = MidtonesTrackbar.Value.ToString() + "%";
            DisplaySettingChanged = true;
        }

        private void ShadowsTrackbar_Scroll(object sender, EventArgs e)
        {
            ShadowsValueLabel.Text = ShadowsTrackbar.Value.ToString() + "%";
            DisplaySettingChanged = true;
        }

        private void RunAsAdminButton_Click(object sender, EventArgs e)
        {
            Program.FileHandler.StartAsAdmin(Process.GetCurrentProcess().ProcessName + ".exe");
        }

        private void hbaopluscheckbox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Program.NvidiaHandler.ToggleHbaoPlus(hbaopluscheckbox.Checked);
            }
            catch (NullReferenceException) { }
        }

        private void texpacksupportbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((texpacksupportbox.SelectedIndex == 1 || texpacksupportbox.SelectedIndex == 2) && (PoolsizeBox.SelectedIndex == 0 || PoolsizeBox.SelectedIndex == 1) && FirstStart)
            {
                PoolsizeBox.SelectedIndex = 2;
            }

            if (texpacksupportbox.SelectedIndex == 2 && FirstStart)
            {
                MessageBox.Show("Only select \"Ultra HD\" if you have AsylumHD or an equivalent texture pack installed that covers ALL texture groups.", "Selecting this option may cause errors!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (texpacksupportbox.SelectedIndex == 3 && FirstStart)
            {
                new ManualTexGroupsForm().ShowDialog();
            }

            if (DetailModeBox.SelectedIndex == 0 || DetailModeBox.SelectedIndex == 1)
            {
                DetailModeBox.SelectedIndex = 2;
            }
            DisplaySettingChanged = true;
            FirstStart = true;
        }

        private void shadowcoveragebox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void smoothframebox_CheckedChanged(object sender, EventArgs e)
        {
            DisplaySettingChanged = true;
        }

        private void DefaultColorButton_Click(object sender, EventArgs e)
        {
            PresetHandler.SetColorDefault();
            DisplaySettingChanged = true;
        }

        private void NoirColorButton_Click(object sender, EventArgs e)
        {
            PresetHandler.SetColorNoir();
            DisplaySettingChanged = true;
        }

        private void MutedColorButton_Click(object sender, EventArgs e)
        {
            PresetHandler.SetColorMuted();
            DisplaySettingChanged = true;
        }

        private void LowContrastColorButton_Click(object sender, EventArgs e)
        {
            PresetHandler.SetColorLowContrast();
            DisplaySettingChanged = true;
        }

        private void VividColorButton_Click(object sender, EventArgs e)
        {
            PresetHandler.SetColorVivid();
            DisplaySettingChanged = true;
        }

        private void HighContrastColorButton_Click(object sender, EventArgs e)
        {
            PresetHandler.SetColorHighContrast();
            DisplaySettingChanged = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Random number = new Random();

            if ((number.NextDouble() * (100.0 - 1.0)) + 1.0 <= 7.0)
            {
                Process.Start(new ProcessStartInfo { FileName = @"https://en.wikipedia.org/wiki/Baguette", UseShellExecute = true });
            }
            else
            {
                Process.Start(new ProcessStartInfo { FileName = @"https://www.nexusmods.com/users/6875632", UseShellExecute = true });
            }
        }
    }
}