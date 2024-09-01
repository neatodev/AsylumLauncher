using AsylumLauncher.Properties;
using NLog;
using System.Text.RegularExpressions;

namespace AsylumLauncher
{
    internal class InputWriter
    {
        private string[] UserInputLines;
        private string[] BmInputLines = { "", "" };
        private bool CustomBinds = false;

        private static Logger Nlog = LogManager.GetCurrentClassLogger();

        public InputWriter()
        {
            UserInputLines = File.ReadAllLines(Program.FileHandler.UserInputPath);
            BmInputLines[0] = IniHandler.BmInputData["Engine.PlayerInput"]["MouseSensitivity"];
            BmInputLines[1] = IniHandler.BmInputData["Engine.PlayerInput"]["bEnableMouseSmoothing"];
            Nlog.Info("Constructor - Successfully initialized InputWriter.");
        }

        public void WriteAll()
        {
            WriteControls();
            WriteBmInput();
            Nlog.Info("WriteAll - Successfully wrote settings to 'BmInput.ini' and 'UserInput.ini'.");
        }

        public void WriteControls()
        {
            // Forward
            UserInputLines[5] = ConvertToConfigStyle(Program.MainWindow.FwButton1.Text, 5);
            // Backward
            UserInputLines[6] = ConvertToConfigStyle(Program.MainWindow.BwButton1.Text, 6);
            // Left
            UserInputLines[7] = ConvertToConfigStyle(Program.MainWindow.LeftButton1.Text, 7);
            // Right
            UserInputLines[8] = ConvertToConfigStyle(Program.MainWindow.RightButton1.Text, 8);
            // Run/Glide/Use
            UserInputLines[9] = ConvertToConfigStyle(Program.MainWindow.RGUButton1.Text, 9);
            // Crouch
            UserInputLines[10] = ConvertToConfigStyle(Program.MainWindow.CrouchButton1.Text, 10);
            // Zoom
            UserInputLines[11] = ConvertToConfigStyle(Program.MainWindow.ZoomButton1.Text, 11);
            // Grapple
            UserInputLines[12] = ConvertToConfigStyle(Program.MainWindow.GrappleButton1.Text, 12);
            // Toggle Crouch
            UserInputLines[13] = ConvertToConfigStyle(Program.MainWindow.ToggleCrouchButton1.Text, 13);
            // Detective Mode
            UserInputLines[14] = ConvertToConfigStyle(Program.MainWindow.DetectiveModeButton1.Text, 14);
            // Gadget/Strike
            UserInputLines[15] = ConvertToConfigStyle(Program.MainWindow.UseGadgetStrikeButton1.Text, 15);
            // Aim Gadget/Counter/Takedown
            UserInputLines[18] = ConvertToConfigStyle(Program.MainWindow.ACTButton1.Text, 18);
            // Gadget Secondary/Cape Stun
            UserInputLines[21] = ConvertToConfigStyle(Program.MainWindow.GadgetSecButton1.Text, 21);
            // Previous Gadget
            UserInputLines[24] = ConvertToConfigStyle(Program.MainWindow.PrevGadgetButton1.Text, 24);
            // Next Gadget
            UserInputLines[25] = ConvertToConfigStyle(Program.MainWindow.NextGadgetButton1.Text, 25);
            // Cape Stun
            UserInputLines[50] = ConvertToConfigStyle(Program.MainWindow.CapeStunButton.Text, 50);
            // Speedrun Setting
            UserInputLines[59] = ConvertToConfigStyle(Program.MainWindow.SpeedRunButton.Text, 59);
            // Speedrun 2 Setting
            UserInputLines[60] = ConvertToConfigStyle(Program.MainWindow.DebugMenuButton.Text, 60);
            // Open Console
            UserInputLines[53] = ConvertToConfigStyle(Program.MainWindow.OpenConsoleButton.Text, 53);
            UserInputLines[53] = SetTypeKey(UserInputLines[53]);
            // Toggle HUD
            UserInputLines[54] = ConvertToConfigStyle(Program.MainWindow.ToggleHudButton.Text, 54);
            // Reset FoV
            UserInputLines[55] = ConvertToConfigStyle(Program.MainWindow.ResetFoVButton.Text, 55);
            // Custom FoV 1
            UserInputLines[56] = ConvertToConfigStyle(Program.MainWindow.CustomFoV1Button.Text, 56);
            UserInputLines[56] = UpdateFoVValue(UserInputLines[56], Program.MainWindow.CustomFoV1Trackbar.Value);
            // Custom FoV 2
            UserInputLines[57] = ConvertToConfigStyle(Program.MainWindow.CustomFoV2Button.Text, 57);
            UserInputLines[57] = UpdateFoVValue(UserInputLines[57], Program.MainWindow.CustomFoV2Trackbar.Value);
            // Centre Camera
            UserInputLines[58] = ConvertToConfigStyle(Program.MainWindow.CentreCameraButton.Text, 58);
            // View Map
            UserInputLines[16] = ConvertToConfigStyle(Program.MainWindow.MapButton.Text, 16);

            using (StreamWriter UserInputFile = new(Program.FileHandler.UserInputPath))
            {
                foreach (string Line in UserInputLines)
                {
                    UserInputFile.WriteLine(Line);
                }
                UserInputFile.Close();
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

        public void WriteBmInput()
        {
            Program.FileHandler.BmInput.IsReadOnly = false;
            File.Delete(Program.FileHandler.BmInputPath);
            Program.FileHandler.CreateConfigFile(Program.FileHandler.BmInputPath, Resources.BmInput);
            // Mouse Sensitivity
            BmInputLines[0] = Program.MainWindow.MouseSensitivityValueLabel.Text + ".0";

            // Mouse Smoothing
            if (Program.MainWindow.MouseSmoothingBox.Checked)
            {
                BmInputLines[1] = "true";
            }
            else
            {
                BmInputLines[1] = "false";
            }

            List<string> BmInputFileLines = new();
            foreach (string Line in File.ReadAllLines(Program.FileHandler.BmInputPath))
            {
                BmInputFileLines.Add(Line);
            }

            BmInputFileLines[5] = "MouseSensitivity=" + BmInputLines[0];
            BmInputFileLines[7] = "bEnableMouseSmoothing=" + BmInputLines[1];
            using (StreamWriter BmInputFile = new(Program.FileHandler.BmInputPath))
            {
                for (int i = 0; i < BmInputFileLines.Count; i++)
                {
                    if (i == 209)
                    {
                        for (int j = 5; j < UserInputLines.Length; j++)
                        {
                            try
                            {
                                if (UserInputLines[j].Contains("; Add your own custom keybinds below this line."))
                                {
                                    BmInputFile.WriteLine("; Add your own custom keybinds below this line. (Automatically carried over from UserInput.ini, DO NOT MODIFY!)");
                                    CustomBinds = true;
                                }
                                else if (!UserInputLines[j].Contains(";"))
                                {
                                    if (CustomBinds == true)
                                    {
                                        if (UserInputLines[j].Substring(0, 1).Contains("."))
                                        {
                                            BmInputFile.WriteLine(UserInputLines[j].Substring(1));
                                        }
                                        else
                                        {
                                            BmInputFile.WriteLine(UserInputLines[j]);
                                        }
                                    }
                                    else
                                    {
                                        BmInputFile.WriteLine(UserInputLines[j].Substring(1));
                                    }
                                }
                                else
                                {
                                    BmInputFile.WriteLine(UserInputLines[j]);
                                }
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                BmInputFile.WriteLine(UserInputLines[j]);
                            }
                            BmInputFileLines.Insert(i + j - 5, UserInputLines[j]);
                        }
                        i = BmInputFileLines.IndexOf("[Engine.DebugCameraInput]") - 1;
                    }
                    BmInputFile.WriteLine(BmInputFileLines[i]);
                }
                BmInputFile.Close();
            }
            Program.FileHandler.BmInput.IsReadOnly = true;
            CustomBinds = false;
        }

        private string ConvertToConfigStyle(string Text, int i)
        {
            Nlog.Info("ConvertToConfigStyle - Binding {0} to UserInput on line {1}.", Text, (i + 1).ToString());
            Text = Text.Replace(" ", "");
            string ConfigLine = UserInputLines[i];
            string ConfigLineTrimmed = ConfigLine.Substring(17);
            int Count = ConfigLineTrimmed.Substring(0, ConfigLineTrimmed.IndexOf("\"")).Length;
            string Modifier = "None";
            try
            {
                Modifier = Text.Substring(0, Text.IndexOf('+'));
                Text = ConvertLine(Text.Substring(Text.IndexOf('+') + 1));
            }
            catch (ArgumentOutOfRangeException)
            {
                Text = ConvertLine(Text);
            }
            ConfigLine = ConfigLine.Remove(17, Count).Insert(17, Text);
            ConfigLine = SetModifier(ConfigLine, Modifier);
            return ConfigLine;
        }

        private string SetModifier(string Input, string Modifier)
        {
            if (!Input.Contains("Shift=") && !Input.Contains("Control=") && !Input.Contains("Alt="))
            {
                return Input;
            }

            TimeSpan Time = new(0, 0, 0, 0, 3);

            switch (Modifier)
            {
                case "None":
                    Input = Regex.Replace(Input, @"\bAlt=true\b", "Alt=false", RegexOptions.Compiled, Time);
                    Input = Regex.Replace(Input, @"\bShift=true\b", "Shift=false", RegexOptions.Compiled, Time);
                    Input = Regex.Replace(Input, @"\bControl=true\b", "Control=false", RegexOptions.Compiled, Time);
                    break;
                case "Shift":
                    Input = Regex.Replace(Input, @"\bAlt=true\b", "Alt=false", RegexOptions.Compiled, Time);
                    Input = Regex.Replace(Input, @"\bShift=false\b", "Shift=true", RegexOptions.Compiled, Time);
                    Input = Regex.Replace(Input, @"\bControl=true\b", "Control=false", RegexOptions.Compiled, Time);
                    break;
                case "Ctrl":
                    Input = Regex.Replace(Input, @"\bAlt=true\b", "Alt=false", RegexOptions.Compiled, Time);
                    Input = Regex.Replace(Input, @"\bShift=true\b", "Shift=false", RegexOptions.Compiled, Time);
                    Input = Regex.Replace(Input, @"\bControl=false\b", "Control=true", RegexOptions.Compiled, Time);
                    break;
                case "Alt":
                    Input = Regex.Replace(Input, @"\bAlt=false\b", "Alt=true", RegexOptions.Compiled, Time);
                    Input = Regex.Replace(Input, @"\bShift=true\b", "Shift=false", RegexOptions.Compiled, Time);
                    Input = Regex.Replace(Input, @"\bControl=true\b", "Control=false", RegexOptions.Compiled, Time);
                    break;
            }

            return Input;
        }

        private string SetTypeKey(string ConsoleLine)
        {
            var TrimmedLine = ConsoleLine.Substring(ConsoleLine.IndexOf(","));
            TrimmedLine = TrimmedLine.Substring(TrimmedLine.IndexOf("\"") + 1);
            TrimmedLine = TrimmedLine.Substring(0, TrimmedLine.IndexOf("\""));
            var TypeKeyValue = ConsoleLine.Substring(17);
            TypeKeyValue = TypeKeyValue.Substring(0, TypeKeyValue.IndexOf("\""));
            var NewTypeKey = "set console TypeKey " + TypeKeyValue;

            TimeSpan Time = new(0, 0, 0, 0, 3);

            ConsoleLine = Regex.Replace(ConsoleLine, TrimmedLine, NewTypeKey, RegexOptions.Compiled, Time);
            return ConsoleLine;
        }

        private string UpdateFoVValue(string ConfigLine, int FoVValue)
        {
            var FoVSection = ConfigLine.Substring(ConfigLine.IndexOf(","));
            FoVSection = FoVSection.Substring(FoVSection.IndexOf("\"") + 1);
            FoVSection = FoVSection.Substring(0, FoVSection.IndexOf("\""));
            var UpdatedValue = "fov " + FoVValue;

            TimeSpan Time = new(0, 0, 0, 0, 3);

            ConfigLine = Regex.Replace(ConfigLine, FoVSection, UpdatedValue, RegexOptions.Compiled, Time);
            return ConfigLine;
        }

        private string ConvertLine(string Input)
        {
            for (int i = 0; i < Program.InputHandler.LinesHumanReadable.Length; i++)
            {
                if (Input == Program.InputHandler.LinesHumanReadable[i].Replace(" ", ""))
                {
                    return Program.InputHandler.LinesConfigStyle[i];
                }
            }
            return Input;
        }
    }
}
