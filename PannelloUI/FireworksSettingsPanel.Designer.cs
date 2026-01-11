namespace AboutFormWithUIPanel
{
    partial class FireworksSettingsPanel
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ComboBox comboProfiles;
        private System.Windows.Forms.CheckBox chkGlow;
        private System.Windows.Forms.CheckBox chkTwinkle;
        private System.Windows.Forms.CheckBox chkSmoke;
        private System.Windows.Forms.TrackBar trackIntensity;
        private System.Windows.Forms.Label lblProfile;
        private System.Windows.Forms.Label lblIntensity;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnRandom;
        private System.Windows.Forms.CheckBox chkAudio;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboProfiles = new ComboBox();
            chkGlow = new CheckBox();
            chkTwinkle = new CheckBox();
            chkSmoke = new CheckBox();
            trackIntensity = new TrackBar();
            lblProfile = new Label();
            lblIntensity = new Label();
            btnApply = new Button();
            btnRandom = new Button();
            chkAudio = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)trackIntensity).BeginInit();
            SuspendLayout();
            // 
            // comboProfiles
            // 
            comboProfiles.DropDownStyle = ComboBoxStyle.DropDownList;
            comboProfiles.FormattingEnabled = true;
            comboProfiles.Location = new Point(20, 40);
            comboProfiles.Name = "comboProfiles";
            comboProfiles.Size = new Size(200, 23);
            comboProfiles.TabIndex = 0;
            // 
            // chkGlow
            // 
            chkGlow.AutoSize = true;
            chkGlow.Location = new Point(20, 90);
            chkGlow.Name = "chkGlow";
            chkGlow.Size = new Size(53, 19);
            chkGlow.TabIndex = 1;
            chkGlow.Text = "G";
            // 
            // chkTwinkle
            // 
            chkTwinkle.AutoSize = true;
            chkTwinkle.Location = new Point(20, 120);
            chkTwinkle.Name = "chkTwinkle";
            chkTwinkle.Size = new Size(66, 19);
            chkTwinkle.TabIndex = 2;
            chkTwinkle.Text = "T";
            // 
            // chkSmoke
            // 
            chkSmoke.AutoSize = true;
            chkSmoke.Location = new Point(20, 150);
            chkSmoke.Name = "chkSmoke";
            chkSmoke.Size = new Size(62, 19);
            chkSmoke.TabIndex = 3;
            chkSmoke.Text = "S";
            // 
            // trackIntensity
            // 
            trackIntensity.Location = new Point(20, 226);
            trackIntensity.Maximum = 200;
            trackIntensity.Minimum = 50;
            trackIntensity.Name = "trackIntensity";
            trackIntensity.Size = new Size(200, 45);
            trackIntensity.SmallChange = 5;
            trackIntensity.TabIndex = 4;
            trackIntensity.TickFrequency = 10;
            trackIntensity.Value = 100;
            // 
            // lblProfile
            // 
            lblProfile.AutoSize = true;
            lblProfile.Location = new Point(20, 20);
            lblProfile.Name = "lblProfile";
            lblProfile.Size = new Size(41, 15);
            lblProfile.TabIndex = 5;
            lblProfile.Text = "P";
            // 
            // lblIntensity
            // 
            lblIntensity.AutoSize = true;
            lblIntensity.Location = new Point(20, 208);
            lblIntensity.Name = "lblIntensity";
            lblIntensity.Size = new Size(52, 15);
            lblIntensity.TabIndex = 6;
            lblIntensity.Text = "I";
            // 
            // btnApply
            // 
            btnApply.Location = new Point(20, 277);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(90, 30);
            btnApply.TabIndex = 7;
            btnApply.Text = "A";
            // 
            // btnRandom
            // 
            btnRandom.Location = new Point(130, 277);
            btnRandom.Name = "btnRandom";
            btnRandom.Size = new Size(90, 30);
            btnRandom.TabIndex = 8;
            btnRandom.Text = "R";
            // 
            // chkAudio
            // 
            chkAudio.AutoSize = true;
            chkAudio.Location = new Point(20, 180);
            chkAudio.Name = "chkAudio";
            chkAudio.Size = new Size(111, 19);
            chkAudio.TabIndex = 9;
            chkAudio.Text = "Audio Explosion";
            // 
            // FireworksSettingsPanel
            // 
            ClientSize = new Size(244, 343);
            Controls.Add(comboProfiles);
            Controls.Add(chkGlow);
            Controls.Add(chkTwinkle);
            Controls.Add(chkSmoke);
            Controls.Add(trackIntensity);
            Controls.Add(lblProfile);
            Controls.Add(lblIntensity);
            Controls.Add(btnApply);
            Controls.Add(btnRandom);
            Controls.Add(chkAudio);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FireworksSettingsPanel";
            StartPosition = FormStartPosition.CenterParent;
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)trackIntensity).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion
    }
}
