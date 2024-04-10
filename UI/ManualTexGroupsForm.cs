using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            worldmaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_World"]);
            worldnormalmaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldNormalMap"]);
            worldspecularmaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldSpecular"]);
            charactermaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Character"]);
            characternormalmaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_CharacterNormalMap"]);
            characterspecularmaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_CharacterSpecular"]);
            weaponmaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon"]);
            weaponnormalmaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WeaponNormalMap"]);
            weaponspecularmaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WeaponSpecular"]);
            vehiclemaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Vehicle"]);
            vehiclenormalmaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_VehicleNormalMap"]);
            vehiclespecularmaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_VehicleSpecular"]);
            cinematicmaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Cinematic"]);
            effectsmaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Effects"]);
            effectsnotfilteredmaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_EffectsNotFiltered"]);
            skyboxmaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Skybox"]);
            uimaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_UI"]);
            lightandshadowmaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_LightAndShadowMap"]);
            rendertargetmaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_RenderTarget"]);
            weapon3pmaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon3P"]);
            weapon3pnormalmaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon3PNormalMap"]);
            weapon3pspecularmaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Weapon3PSpecular"]);
            worldhimaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_World_Hi"]);
            worldnormalhimaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldNormalMap_Hi"]);
            worldspecularhimaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_WorldSpecular_Hi"]);
            effectshimaskbox.Text = Program.IniHandler.ReturnTexGroupValue(IniHandler.BmEngineData["SystemSettings"]["TEXTUREGROUP_Effects_Hi"]);
        }

        private void worldmaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[0] = worldmaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void worldnormalmaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[1] = worldnormalmaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void worldspecularmaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[2] = worldspecularmaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void charactermaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[3] = charactermaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void characternormalmaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[4] = characternormalmaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void characterspecularmaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[5] = characterspecularmaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void weaponmaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[6] = weaponmaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void weaponnormalmaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[7] = weaponnormalmaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void weaponspecularmaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[8] = weaponspecularmaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void vehiclemaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[9] = vehiclemaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void vehiclenormalmaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[10] = vehiclenormalmaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void vehiclespecularmaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[11] = vehiclespecularmaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void cinematicmaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[12] = cinematicmaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void effectsmaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[13] = effectsmaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void effectsnotfilteredmaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[14] = effectsnotfilteredmaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void skyboxmaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[15] = skyboxmaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void uimaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[16] = uimaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void lightandshadowmaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[17] = lightandshadowmaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void rendertargetmaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[18] = rendertargetmaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void weapon3pmaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[19] = weapon3pmaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void weapon3pnormalmaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[20] = weapon3pnormalmaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void weapon3pspecularmaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[21] = weapon3pspecularmaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void worldhimaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[22] = worldhimaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void worldnormalhimaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[23] = worldnormalhimaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void worldspecularhimaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[24] = worldspecularhimaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }

        private void effectshimaskbox_TextChanged(object sender, EventArgs e)
        {
            Program.IniHandler.CustomLines[25] = effectshimaskbox.Text.Trim();
            Program.MainWindow.DisplaySettingChanged = true;
        }
    }
}
