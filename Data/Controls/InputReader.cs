using NLog;

namespace AsylumLauncher
{
    internal class InputReader
    {
        private readonly string[] UserInputLines;
        private static string[] BmInputLines = { "", "" };

        private static Logger Nlog = LogManager.GetCurrentClassLogger();

        public InputReader()
        {
            UserInputLines = File.ReadAllLines(Program.FileHandler.UserInputPath);
            Program.MainWindow.ControlSettingChanged = false;
            Program.MainWindow.ApplySettingsButton.Enabled = false;
            Nlog.Info("Constructor - Successfully initialized InputReader.");
        }

        public static void InitBmInputLines()
        {
            BmInputLines[0] = IniHandler.BmInputData["Engine.PlayerInput"]["MouseSensitivity"];
            BmInputLines[1] = IniHandler.BmInputData["Engine.PlayerInput"]["bEnableMouseSmoothing"];
        }

        public void InitControls()
        {
            // Forward
            Program.MainWindow.FwButton1.Text = TrimLine(UserInputLines[5]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.FwButton1);
            // Backward
            Program.MainWindow.BwButton1.Text = TrimLine(UserInputLines[6]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.BwButton1);
            // Left
            Program.MainWindow.LeftButton1.Text = TrimLine(UserInputLines[7]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.LeftButton1);
            // Right
            Program.MainWindow.RightButton1.Text = TrimLine(UserInputLines[8]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.RightButton1);
            // Run/Glide/Use
            Program.MainWindow.RGUButton1.Text = TrimLine(UserInputLines[9]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.RGUButton1);
            // Crouch
            Program.MainWindow.CrouchButton1.Text = TrimLine(UserInputLines[10]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.CrouchButton1);
            // Zoom
            Program.MainWindow.ZoomButton1.Text = TrimLine(UserInputLines[11]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.ZoomButton1);
            // Grapple
            Program.MainWindow.GrappleButton1.Text = TrimLine(UserInputLines[12]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.GrappleButton1);
            // Toggle Crouch
            Program.MainWindow.ToggleCrouchButton1.Text = TrimLine(UserInputLines[13]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.ToggleCrouchButton1);
            // Detective Mode
            Program.MainWindow.DetectiveModeButton1.Text = TrimLine(UserInputLines[18]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.DetectiveModeButton1);
            // Gadget/Strike
            Program.MainWindow.UseGadgetStrikeButton1.Text = TrimLine(UserInputLines[19]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.UseGadgetStrikeButton1);
            // Combat Takedown
            Program.MainWindow.CTDownButton1.Text = TrimLine(UserInputLines[17]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.CTDownButton1);
            // Aim Gadget/Counter/Takedown
            Program.MainWindow.ACTButton1.Text = TrimLine(UserInputLines[22]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.ACTButton1);
            // Gadget Secondary
            Program.MainWindow.GadgetSecButton1.Text = TrimLine(UserInputLines[25]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.GadgetSecButton1);
            // Previous Gadget (Quick Batarang)
            Program.MainWindow.PrevGadgetButton1.Text = TrimLine(UserInputLines[14]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.PrevGadgetButton1);
            // Next Gadget (Quick Batclaw)
            Program.MainWindow.NextGadgetButton1.Text = TrimLine(UserInputLines[15]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.NextGadgetButton1);
            // Cape Stun
            Program.MainWindow.CapeStunButton.Text = TrimLine(UserInputLines[50]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.CapeStunButton);
            // Speedrun Setting 1
            Program.MainWindow.SpeedRunButton.Text = TrimLine(UserInputLines[59]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.SpeedRunButton);
            // Speedrun Setting 2
            Program.MainWindow.DebugMenuButton.Text = TrimLine(UserInputLines[60]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.DebugMenuButton);
            // Open Console
            Program.MainWindow.OpenConsoleButton.Text = TrimLine(UserInputLines[53]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.OpenConsoleButton);
            // Toggle HUD
            Program.MainWindow.ToggleHudButton.Text = TrimLine(UserInputLines[54]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.ToggleHudButton);
            // Reset FoV
            Program.MainWindow.ResetFoVButton.Text = TrimLine(UserInputLines[55]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.ResetFoVButton);
            // Custom FoV 1
            Program.MainWindow.CustomFoV1Button.Text = TrimLine(UserInputLines[56]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.CustomFoV1Button);
            // Custom FoV 1 Slider
            var TrackbarValue = UserInputLines[56].Substring(UserInputLines[56].IndexOf(","));
            TrackbarValue = TrackbarValue.Substring(TrackbarValue.IndexOf("\"") + 5);
            Program.MainWindow.CustomFoV1Trackbar.Value = Int16.Parse(TrackbarValue.Substring(0, TrackbarValue.IndexOf("\"")));
            Program.MainWindow.CustomFoV1ValueLabel.Text = Program.MainWindow.CustomFoV1Trackbar.Value.ToString();
            // Custom FoV 2
            Program.MainWindow.CustomFoV2Button.Text = TrimLine(UserInputLines[57]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.CustomFoV2Button);
            // Custom FoV 2 Slider
            TrackbarValue = UserInputLines[57].Substring(UserInputLines[57].IndexOf(","));
            TrackbarValue = TrackbarValue.Substring(TrackbarValue.IndexOf("\"") + 5);
            Program.MainWindow.CustomFoV2Trackbar.Value = Int16.Parse(TrackbarValue.Substring(0, TrackbarValue.IndexOf("\"")));
            Program.MainWindow.CustomFoV2ValueLabel.Text = Program.MainWindow.CustomFoV2Trackbar.Value.ToString();
            // Centre Camera
            Program.MainWindow.CentreCameraButton.Text = TrimLine(UserInputLines[58]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.CentreCameraButton);
            // View Map (Throw)
            Program.MainWindow.MapButton.Text = TrimLine(UserInputLines[16]);
            Program.InputHandler.ButtonList.Add(Program.MainWindow.MapButton);
            // Mouse Sensitivity
            Program.MainWindow.MouseSensitivityTrackbar.Value = Int16.Parse(BmInputLines[0].Substring(0, BmInputLines[0].LastIndexOf(".")));
            Program.MainWindow.MouseSensitivityValueLabel.Text = BmInputLines[0].Substring(0, BmInputLines[0].LastIndexOf("."));
            // Mouse Smoothing
            if (BmInputLines[1].Equals("true"))
            {
                Program.MainWindow.MouseSmoothingBox.Checked = true;
            }
            else
            {
                Program.MainWindow.MouseSmoothingBox.Checked = false;
            }

            foreach (Button KeyButton in Program.InputHandler.ButtonList)
            {
                if (!KeyButton.Text.Contains("Unbound"))
                {
                    KeyButton.ForeColor = Color.Black;
                }
                else
                {
                    KeyButton.ForeColor = Color.RoyalBlue;
                }
            }
        }

        private string TrimLine(string Line)
        {
            string NewLine;
            try
            {
                NewLine = Line.Substring(17);
            }
            catch (ArgumentOutOfRangeException)
            {
                NewLine = Line;
            }

            if (Line.Contains("Shift=true") && !Line.Contains("bIgnoreShift=true"))
            {
                return "Shift + " + ConvertLine(NewLine.Substring(0, NewLine.IndexOf("\"")));
            }
            if (Line.Contains("Control=true"))
            {
                return "Ctrl + " + ConvertLine(NewLine.Substring(0, NewLine.IndexOf("\"")));
            }
            if (Line.Contains("Alt=true"))
            {
                return "Alt + " + ConvertLine(NewLine.Substring(0, NewLine.IndexOf("\"")));
            }
            return ConvertLine(NewLine.Substring(0, NewLine.IndexOf("\"")));
        }

        private string ConvertLine(string Input)
        {
            for (int i = 0; i < Program.InputHandler.LinesConfigStyle.Length; i++)
            {
                if (Input == Program.InputHandler.LinesConfigStyle[i])
                {
                    return Program.InputHandler.LinesHumanReadable[i];
                }
            }
            return Input;
        }
    }
}
