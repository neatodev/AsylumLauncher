using System.Diagnostics.Eventing.Reader;
using System.Text.RegularExpressions;

namespace AsylumLauncher
{
    public partial class ManualTexGroupsForm : Form
    {
        public ManualTexGroupsForm()
        {
            InitializeComponent();
            InitTexGroupsMaskBoxes();
        }

        private void InitTexGroupsMaskBoxes()
        {
            worldmaskbox.Text = Program.IniHandler.CustomLines[0];
            worldnormalmaskbox.Text = Program.IniHandler.CustomLines[1];
            worldspecularmaskbox.Text = Program.IniHandler.CustomLines[2];
            charactermaskbox.Text = Program.IniHandler.CustomLines[3];
            characternormalmaskbox.Text = Program.IniHandler.CustomLines[4];
            characterspecularmaskbox.Text = Program.IniHandler.CustomLines[5];
            weaponmaskbox.Text = Program.IniHandler.CustomLines[6];
            weaponnormalmaskbox.Text = Program.IniHandler.CustomLines[7];
            weaponspecularmaskbox.Text = Program.IniHandler.CustomLines[8];
            vehiclemaskbox.Text = Program.IniHandler.CustomLines[9];
            vehiclenormalmaskbox.Text = Program.IniHandler.CustomLines[10];
            vehiclespecularmaskbox.Text = Program.IniHandler.CustomLines[11];
            cinematicmaskbox.Text = Program.IniHandler.CustomLines[12];
            effectsmaskbox.Text = Program.IniHandler.CustomLines[13];
            effectsnotfilteredmaskbox.Text = Program.IniHandler.CustomLines[14];
            skyboxmaskbox.Text = Program.IniHandler.CustomLines[15];
            uimaskbox.Text = Program.IniHandler.CustomLines[16];
            lightandshadowmaskbox.Text = Program.IniHandler.CustomLines[17];
            rendertargetmaskbox.Text = Program.IniHandler.CustomLines[18];
            weapon3pmaskbox.Text = Program.IniHandler.CustomLines[19];
            weapon3pnormalmaskbox.Text = Program.IniHandler.CustomLines[20];
            weapon3pspecularmaskbox.Text = Program.IniHandler.CustomLines[21];
            worldhimaskbox.Text = Program.IniHandler.CustomLines[22];
            worldnormalhimaskbox.Text = Program.IniHandler.CustomLines[23];
            worldspecularhimaskbox.Text = Program.IniHandler.CustomLines[24];
            effectshimaskbox.Text = Program.IniHandler.CustomLines[25];
        }

        private void ManualTexGroupsForm_Load(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void worldmaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(worldmaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 256)
            {
                Program.IniHandler.CustomLines[0] = MinValText.Trim();
                worldmaskbox.Text = MinValText.Trim();
            }
            else
            {
                worldmaskbox.Text = "256";
                Program.IniHandler.CustomLines[0] = "256";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void worldnormalmaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(worldnormalmaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 256)
            {
                Program.IniHandler.CustomLines[1] = MinValText.Trim();
                worldnormalmaskbox.Text = MinValText.Trim();
            }
            else
            {
                worldnormalmaskbox.Text = "256";
                Program.IniHandler.CustomLines[1] = "256";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void worldspecularmaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(worldspecularmaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 256)
            {
                Program.IniHandler.CustomLines[2] = MinValText.Trim();
                worldspecularmaskbox.Text = MinValText.Trim();
            }
            else
            {
                worldspecularmaskbox.Text = "256";
                Program.IniHandler.CustomLines[2] = "256";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void charactermaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(charactermaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 512)
            {
                Program.IniHandler.CustomLines[3] = MinValText.Trim();
                charactermaskbox.Text = MinValText.Trim();
            }
            else
            {
                charactermaskbox.Text = "512";
                Program.IniHandler.CustomLines[3] = "512";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void characternormalmaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(characternormalmaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 512)
            {
                Program.IniHandler.CustomLines[4] = MinValText.Trim();
                characternormalmaskbox.Text = MinValText.Trim();
            }
            else
            {
                characternormalmaskbox.Text = "512";
                Program.IniHandler.CustomLines[4] = "512";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void characterspecularmaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(characterspecularmaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 256)
            {
                Program.IniHandler.CustomLines[5] = MinValText.Trim();
                characterspecularmaskbox.Text = MinValText.Trim();
            }
            else
            {
                characterspecularmaskbox.Text = "256";
                Program.IniHandler.CustomLines[5] = "256";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void weaponmaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(weaponmaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 128)
            {
                Program.IniHandler.CustomLines[6] = MinValText.Trim();
                weaponmaskbox.Text = MinValText.Trim();
            }
            else
            {
                weaponmaskbox.Text = "128";
                Program.IniHandler.CustomLines[6] = "128";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void weaponnormalmaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(weaponnormalmaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 128)
            {
                Program.IniHandler.CustomLines[7] = MinValText.Trim();
                weaponnormalmaskbox.Text = MinValText.Trim();
            }
            else
            {
                weaponnormalmaskbox.Text = "128";
                Program.IniHandler.CustomLines[7] = "128";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void weaponspecularmaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(weaponspecularmaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 128)
            {
                Program.IniHandler.CustomLines[8] = MinValText.Trim();
                weaponspecularmaskbox.Text = MinValText.Trim();
            }
            else
            {
                weaponspecularmaskbox.Text = "128";
                Program.IniHandler.CustomLines[8] = "128";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void vehiclemaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(vehiclemaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 512)
            {
                Program.IniHandler.CustomLines[9] = MinValText.Trim();
                vehiclemaskbox.Text = MinValText.Trim();
            }
            else
            {
                vehiclemaskbox.Text = "512";
                Program.IniHandler.CustomLines[9] = "512";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void vehiclenormalmaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(vehiclenormalmaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 128)
            {
                Program.IniHandler.CustomLines[10] = MinValText.Trim();
                vehiclenormalmaskbox.Text = MinValText.Trim();
            }
            else
            {
                vehiclenormalmaskbox.Text = "128";
                Program.IniHandler.CustomLines[10] = "128";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void vehiclespecularmaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(vehiclespecularmaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 128)
            {
                Program.IniHandler.CustomLines[11] = MinValText.Trim();
                vehiclespecularmaskbox.Text = MinValText.Trim();
            }
            else
            {
                vehiclespecularmaskbox.Text = "128";
                Program.IniHandler.CustomLines[11] = "128";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void cinematicmaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(cinematicmaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 1)
            {
                Program.IniHandler.CustomLines[12] = MinValText.Trim();
                cinematicmaskbox.Text = MinValText.Trim();
            }
            else
            {
                cinematicmaskbox.Text = "1";
                Program.IniHandler.CustomLines[12] = "1";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void effectsmaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(effectsmaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 128)
            {
                Program.IniHandler.CustomLines[13] = MinValText.Trim();
                effectsmaskbox.Text = MinValText.Trim();
            }
            else
            {
                effectsmaskbox.Text = "128";
                Program.IniHandler.CustomLines[13] = "128";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void effectsnotfilteredmaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(effectsnotfilteredmaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 128)
            {
                Program.IniHandler.CustomLines[14] = MinValText.Trim();
                effectsnotfilteredmaskbox.Text = MinValText.Trim();
            }
            else
            {
                effectsnotfilteredmaskbox.Text = "128";
                Program.IniHandler.CustomLines[14] = "128";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void skyboxmaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(skyboxmaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 256)
            {
                Program.IniHandler.CustomLines[15] = MinValText.Trim();
                skyboxmaskbox.Text = MinValText.Trim();
            }
            else
            {
                skyboxmaskbox.Text = "256";
                Program.IniHandler.CustomLines[15] = "256";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void uimaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(uimaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 1024)
            {
                Program.IniHandler.CustomLines[16] = MinValText.Trim();
                uimaskbox.Text = MinValText.Trim();
            }
            else
            {
                uimaskbox.Text = "1024";
                Program.IniHandler.CustomLines[16] = "1024";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void lightandshadowmaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(lightandshadowmaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 1)
            {
                Program.IniHandler.CustomLines[17] = MinValText.Trim();
                lightandshadowmaskbox.Text = MinValText.Trim();
            }
            else
            {
                lightandshadowmaskbox.Text = "1";
                Program.IniHandler.CustomLines[17] = "1";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void rendertargetmaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(rendertargetmaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 1)
            {
                Program.IniHandler.CustomLines[18] = MinValText.Trim();
                rendertargetmaskbox.Text = MinValText.Trim();
            }
            else
            {
                rendertargetmaskbox.Text = "1";
                Program.IniHandler.CustomLines[18] = "1";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void weapon3pmaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(weapon3pmaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 128)
            {
                Program.IniHandler.CustomLines[19] = MinValText.Trim();
                weapon3pmaskbox.Text = MinValText.Trim();
            }
            else
            {
                weapon3pmaskbox.Text = "128";
                Program.IniHandler.CustomLines[19] = "128";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void weapon3pnormalmaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(weapon3pnormalmaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 128)
            {
                Program.IniHandler.CustomLines[20] = MinValText.Trim();
                weapon3pnormalmaskbox.Text = MinValText.Trim();
            }
            else
            {
                weapon3pnormalmaskbox.Text = "128";
                Program.IniHandler.CustomLines[20] = "128";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void weapon3pspecularmaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(weapon3pspecularmaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 128)
            {
                Program.IniHandler.CustomLines[21] = MinValText.Trim();
                weapon3pspecularmaskbox.Text = MinValText.Trim();
            }
            else
            {
                weapon3pspecularmaskbox.Text = "128";
                Program.IniHandler.CustomLines[21] = "128";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void worldhimaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(worldhimaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 256)
            {
                Program.IniHandler.CustomLines[22] = MinValText.Trim();
                worldhimaskbox.Text = MinValText.Trim();
            }
            else
            {
                worldhimaskbox.Text = "256";
                Program.IniHandler.CustomLines[22] = "256";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void worldnormalhimaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(worldnormalhimaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 256)
            {
                Program.IniHandler.CustomLines[23] = MinValText.Trim();
                worldnormalhimaskbox.Text = MinValText.Trim();
            }
            else
            {
                worldnormalhimaskbox.Text = "256";
                Program.IniHandler.CustomLines[23] = "256";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void worldspecularhimaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(worldspecularhimaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 256)
            {
                Program.IniHandler.CustomLines[24] = MinValText.Trim();
                worldspecularhimaskbox.Text = MinValText.Trim();
            }
            else
            {
                worldspecularhimaskbox.Text = "256";
                Program.IniHandler.CustomLines[24] = "256";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void effectshimaskbox_Leave(object sender, EventArgs e)
        {
            string MinValText = Regex.Replace(effectshimaskbox.Text, @"\s", string.Empty);
            Int16 MinValue = Int16.Parse(MinValText.Trim());

            if (MinValue > 128)
            {
                Program.IniHandler.CustomLines[25] = MinValText.Trim();
                effectshimaskbox.Text = MinValText.Trim();
            }
            else
            {
                effectshimaskbox.Text = "128";
                Program.IniHandler.CustomLines[25] = "128";
            }
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void ManualTexGroupsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string ActiveControl = this.ActiveControl.Name;
            switch (ActiveControl)
            {
                case "worldmaskbox":
                    worldmaskbox_Leave(sender, e);
                    break;
                case "worldnormalmaskbox":
                    worldnormalmaskbox_Leave(sender, e);
                    break;
                case "worldspecularmaskbox":
                    worldspecularmaskbox_Leave(sender, e);
                    break;
                case "charactermaskbox":
                    charactermaskbox_Leave(sender, e);
                    break;
                case "characternormalmaskbox":
                    characternormalmaskbox_Leave(sender, e);
                    break;
                case "characterspecularmaskbox":
                    characterspecularmaskbox_Leave(sender, e);
                    break;
                case "weaponmaskbox":
                    weaponmaskbox_Leave(sender, e);
                    break;
                case "weaponnormalmaskbox":
                    weaponnormalmaskbox_Leave(sender, e);
                    break;
                case "weaponspecularmaskbox":
                    weaponspecularmaskbox_Leave(sender, e);
                    break;
                case "vehiclemaskbox":
                    vehiclemaskbox_Leave(sender, e);
                    break;
                case "vehiclenormalmaskbox":
                    vehiclenormalmaskbox_Leave(sender, e);
                    break;
                case "vehiclespecularmaskbox":
                    vehiclespecularmaskbox_Leave(sender, e);
                    break;
                case "cinematicmaskbox":
                    cinematicmaskbox_Leave(sender, e);
                    break;
                case "effectsmaskbox":
                    effectsmaskbox_Leave(sender, e);
                    break;
                case "effectsnotfilteredmaskbox":
                    effectsnotfilteredmaskbox_Leave(sender, e);
                    break;
                case "skyboxmaskbox":
                    skyboxmaskbox_Leave(sender, e);
                    break;
                case "uimaskbox":
                    uimaskbox_Leave(sender, e);
                    break;
                case "lightandshadowmaskbox":
                    lightandshadowmaskbox_Leave(sender, e);
                    break;
                case "rendertargetmaskbox":
                    rendertargetmaskbox_Leave(sender, e);
                    break;
                case "weapon3pmaskbox":
                    weapon3pmaskbox_Leave(sender, e);
                    break;
                case "weapon3pnormalmaskbox":
                    weapon3pnormalmaskbox_Leave(sender, e);
                    break;
                case "weapon3pspecularmaskbox":
                    weapon3pspecularmaskbox_Leave(sender, e);
                    break;
                case "worldhimaskbox":
                    worldhimaskbox_Leave(sender, e);
                    break;
                case "worldnormalhimaskbox":
                    worldnormalhimaskbox_Leave(sender, e);
                    break;
                case "worldspecularhimaskbox":
                    worldspecularhimaskbox_Leave(sender, e);
                    break;
                case "effectshimaskbox":
                    effectshimaskbox_Leave(sender, e);
                    break;
                default:
                    break;
            }
        }
    }
}
