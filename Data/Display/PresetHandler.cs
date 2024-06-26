﻿namespace AsylumLauncher
{
    internal static class PresetHandler
    {
        public static void SetVanilla()
        {
            Program.MainWindow.DetailModeBox.SelectedIndex = 2;
            Program.MainWindow.AmbientOcclusionBox.Checked = true;
            Program.MainWindow.MotionBlurBox.Checked = false;
            Program.MainWindow.DynShadowBox.Checked = true;
            Program.MainWindow.DistortionBox.Checked = true;
            Program.MainWindow.DOFBox.Checked = true;
            Program.MainWindow.LightRayBox.Checked = true;
            Program.MainWindow.LensFlareBox.Checked = true;
            Program.MainWindow.BloomBox.Checked = true;
            Program.MainWindow.AntiAliasingBox.SelectedIndex = 1;
            Program.MainWindow.PhysXBox.SelectedIndex = 0;
            Program.MainWindow.PoolsizeBox.SelectedIndex = 1;
            Program.MainWindow.AnisoBox.SelectedIndex = 0;
            Program.MainWindow.ShadowQualityBox.SelectedIndex = 0;
            Program.MainWindow.shadowcoveragebox.SelectedIndex = 0;
            Program.MainWindow.FrameCapTextBox.Text = "60";
            Program.MainWindow.VsyncBox.SelectedIndex = 1;
            Program.MainWindow.smoothframebox.Checked = true;
        }

        public static void SetOptimized()
        {
            Program.MainWindow.DetailModeBox.SelectedIndex = 2;
            Program.MainWindow.AmbientOcclusionBox.Checked = true;
            Program.MainWindow.MotionBlurBox.Checked = false;
            Program.MainWindow.DynShadowBox.Checked = true;
            Program.MainWindow.DistortionBox.Checked = true;
            Program.MainWindow.DOFBox.Checked = true;
            Program.MainWindow.LightRayBox.Checked = true;
            Program.MainWindow.LensFlareBox.Checked = true;
            Program.MainWindow.BloomBox.Checked = true;
            Program.MainWindow.AntiAliasingBox.SelectedIndex = 1;
            Program.MainWindow.PhysXBox.SelectedIndex = 1;
            Program.MainWindow.PoolsizeBox.SelectedIndex = 2;
            Program.MainWindow.AnisoBox.SelectedIndex = 2;
            Program.MainWindow.ShadowQualityBox.SelectedIndex = 1;
            Program.MainWindow.shadowcoveragebox.SelectedIndex = 1;
            Program.MainWindow.smoothframebox.Checked = true;
            Program.MainWindow.smoothframebox.Checked = true;
        }

        public static void SetDarkKnight()
        {
            Program.MainWindow.DetailModeBox.SelectedIndex = 2;
            Program.MainWindow.AmbientOcclusionBox.Checked = true;
            Program.MainWindow.MotionBlurBox.Checked = false;
            Program.MainWindow.DynShadowBox.Checked = true;
            Program.MainWindow.DistortionBox.Checked = true;
            Program.MainWindow.DOFBox.Checked = true;
            Program.MainWindow.LightRayBox.Checked = true;
            Program.MainWindow.LensFlareBox.Checked = true;
            Program.MainWindow.BloomBox.Checked = true;
            Program.MainWindow.AntiAliasingBox.SelectedIndex = 3;
            Program.MainWindow.PhysXBox.SelectedIndex = 1;
            Program.MainWindow.PoolsizeBox.SelectedIndex = 4;
            Program.MainWindow.AnisoBox.SelectedIndex = 2;
            Program.MainWindow.ShadowQualityBox.SelectedIndex = 3;
            Program.MainWindow.shadowcoveragebox.SelectedIndex = 2;
            Program.MainWindow.smoothframebox.Checked = true;
            Program.MainWindow.smoothframebox.Checked = true;
        }

        public static void SetColorDefault()
        {
            Program.MainWindow.SaturationTrackbar.Value = 100;
            Program.MainWindow.SaturationValueLabel.Text = "100%";
            Program.MainWindow.HighlightsTrackbar.Value = 100;
            Program.MainWindow.HighlightsValueLabel.Text = "100%";
            Program.MainWindow.MidtonesTrackbar.Value = 100;
            Program.MainWindow.MidtonesValueLabel.Text = "100%";
            Program.MainWindow.ShadowsTrackbar.Value = 100;
            Program.MainWindow.ShadowsValueLabel.Text = "100%";
        }

        public static void SetColorNoir()
        {
            Program.MainWindow.SaturationTrackbar.Value = 0;
            Program.MainWindow.SaturationValueLabel.Text = "0%";
            Program.MainWindow.HighlightsTrackbar.Value = 100;
            Program.MainWindow.HighlightsValueLabel.Text = "100%";
            Program.MainWindow.MidtonesTrackbar.Value = 100;
            Program.MainWindow.MidtonesValueLabel.Text = "100%";
            Program.MainWindow.ShadowsTrackbar.Value = 100;
            Program.MainWindow.ShadowsValueLabel.Text = "100%";
        }

        public static void SetColorVivid()
        {
            Program.MainWindow.SaturationTrackbar.Value = 90;
            Program.MainWindow.SaturationValueLabel.Text = "90%";
            Program.MainWindow.HighlightsTrackbar.Value = 70;
            Program.MainWindow.HighlightsValueLabel.Text = "70%";
            Program.MainWindow.MidtonesTrackbar.Value = 110;
            Program.MainWindow.MidtonesValueLabel.Text = "110%";
            Program.MainWindow.ShadowsTrackbar.Value = 100;
            Program.MainWindow.ShadowsValueLabel.Text = "100%";
        }

        public static void SetColorMuted()
        {
            Program.MainWindow.SaturationTrackbar.Value = 85;
            Program.MainWindow.SaturationValueLabel.Text = "85%";
            Program.MainWindow.HighlightsTrackbar.Value = 100;
            Program.MainWindow.HighlightsValueLabel.Text = "100%";
            Program.MainWindow.MidtonesTrackbar.Value = 100;
            Program.MainWindow.MidtonesValueLabel.Text = "100%";
            Program.MainWindow.ShadowsTrackbar.Value = 100;
            Program.MainWindow.ShadowsValueLabel.Text = "100%";
        }

        public static void SetColorHighContrast()
        {
            Program.MainWindow.SaturationTrackbar.Value = 105;
            Program.MainWindow.SaturationValueLabel.Text = "105%";
            Program.MainWindow.HighlightsTrackbar.Value = 105;
            Program.MainWindow.HighlightsValueLabel.Text = "105%";
            Program.MainWindow.MidtonesTrackbar.Value = 95;
            Program.MainWindow.MidtonesValueLabel.Text = "95%";
            Program.MainWindow.ShadowsTrackbar.Value = 100;
            Program.MainWindow.ShadowsValueLabel.Text = "100%";
        }

        public static void SetColorLowContrast()
        {
            Program.MainWindow.SaturationTrackbar.Value = 90;
            Program.MainWindow.SaturationValueLabel.Text = "90%";
            Program.MainWindow.HighlightsTrackbar.Value = 85;
            Program.MainWindow.HighlightsValueLabel.Text = "85%";
            Program.MainWindow.MidtonesTrackbar.Value = 100;
            Program.MainWindow.MidtonesValueLabel.Text = "100%";
            Program.MainWindow.ShadowsTrackbar.Value = 100;
            Program.MainWindow.ShadowsValueLabel.Text = "100%";
        }
    }
}
