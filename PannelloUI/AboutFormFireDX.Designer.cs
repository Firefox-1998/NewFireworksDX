using System.Drawing;
using System.Windows.Forms;
using FireworksDX.WinForms;

namespace AboutFormWithUIPanel
{
    partial class AboutFormFireDX
    {
        private Panel panelTop;
        private Label lblVersion;
        private LinkLabel lnkRepo;
        private Panel panelSettingsHost;
        private Button btnBurst;
        private Button btnGpuInfo;
        private Label lblTitle;
        private FireworksPanel fireworksPanel;
        private ComboBox cmbLanguage;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _settingsForm?.Dispose();
                _settingsForm = null;
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            fireworksPanel = new FireworksPanel();
            panelSettingsHost = new Panel();
            panelTop = new Panel();
            btnBurst = new Button();
            btnGpuInfo = new Button();
            lnkRepo = new LinkLabel();
            lblVersion = new Label();
            cmbLanguage = new ComboBox();
            lblTitle = new Label();
            panelTop.SuspendLayout();
            SuspendLayout();
            // 
            // fireworksPanel
            // 
            fireworksPanel.BackColor = Color.Black;
            fireworksPanel.Dock = DockStyle.Fill;
            fireworksPanel.Location = new Point(0, 79);
            fireworksPanel.Name = "fireworksPanel";
            fireworksPanel.Size = new Size(861, 561);
            fireworksPanel.TabIndex = 0;
            // 
            // panelSettingsHost
            // 
            panelSettingsHost.BackColor = Color.FromArgb(18, 18, 18);
            panelSettingsHost.Dock = DockStyle.Right;
            panelSettingsHost.Location = new Point(861, 79);
            panelSettingsHost.Name = "panelSettingsHost";
            panelSettingsHost.Size = new Size(239, 561);
            panelSettingsHost.TabIndex = 2;
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(10, 10, 10);
            panelTop.BorderStyle = BorderStyle.FixedSingle;
            panelTop.Controls.Add(btnBurst);
            panelTop.Controls.Add(btnGpuInfo);
            panelTop.Controls.Add(lnkRepo);
            panelTop.Controls.Add(lblVersion);
            panelTop.Controls.Add(cmbLanguage);
            panelTop.Controls.Add(lblTitle);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Padding = new Padding(16);
            panelTop.Size = new Size(1100, 79);
            panelTop.TabIndex = 1;
            // 
            // btnBurst
            // 
            btnBurst.BackColor = Color.FromArgb(50, 50, 50);
            btnBurst.Dock = DockStyle.Right;
            btnBurst.FlatStyle = FlatStyle.Flat;
            btnBurst.ForeColor = Color.White;
            btnBurst.Location = new Point(902, 16);
            btnBurst.Name = "btnBurst";
            btnBurst.Size = new Size(90, 45);
            btnBurst.TabIndex = 4;
            btnBurst.Text = "B5";
            btnBurst.UseVisualStyleBackColor = false;
            btnBurst.Click += BtnBurst_Click;
            // 
            // btnGpuInfo
            // 
            btnGpuInfo.BackColor = Color.FromArgb(40, 40, 80);
            btnGpuInfo.Dock = DockStyle.Right;
            btnGpuInfo.FlatStyle = FlatStyle.Flat;
            btnGpuInfo.ForeColor = Color.White;
            btnGpuInfo.Location = new Point(992, 16);
            btnGpuInfo.Name = "btnGpuInfo";
            btnGpuInfo.Size = new Size(90, 45);
            btnGpuInfo.TabIndex = 5;
            btnGpuInfo.Text = "IG";
            btnGpuInfo.UseVisualStyleBackColor = false;
            btnGpuInfo.Click += BtnGpuInfo_Click;
            // 
            // lnkRepo
            // 
            lnkRepo.AutoSize = true;
            lnkRepo.Dock = DockStyle.Left;
            lnkRepo.LinkColor = Color.Cyan;
            lnkRepo.Location = new Point(205, 16);
            lnkRepo.Name = "lnkRepo";
            lnkRepo.Padding = new Padding(0, 8, 0, 0);
            lnkRepo.Size = new Size(27, 23);
            lnkRepo.TabIndex = 3;
            lnkRepo.TabStop = true;
            lnkRepo.Text = "Rep";
            lnkRepo.LinkClicked += LnkRepo_LinkClicked;
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Dock = DockStyle.Left;
            lblVersion.Font = new Font("Segoe UI", 9F);
            lblVersion.ForeColor = Color.LightGray;
            lblVersion.Location = new Point(191, 16);
            lblVersion.Name = "lblVersion";
            lblVersion.Padding = new Padding(0, 8, 0, 0);
            lblVersion.Size = new Size(14, 23);
            lblVersion.TabIndex = 2;
            lblVersion.Text = "V";
            // 
            // cmbLanguage
            // 
            cmbLanguage.Dock = DockStyle.Left;
            cmbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLanguage.FormattingEnabled = true;
            cmbLanguage.Location = new Point(71, 16);
            cmbLanguage.Name = "cmbLanguage";
            cmbLanguage.Size = new Size(120, 23);
            cmbLanguage.TabIndex = 6;
            cmbLanguage.SelectedIndexChanged += CmbLanguage_SelectedIndexChanged;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Dock = DockStyle.Left;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = SystemColors.Control;
            lblTitle.Location = new Point(16, 16);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(55, 45);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "SF";
            // 
            // AboutFormFireDX
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1100, 640);
            Controls.Add(fireworksPanel);
            Controls.Add(panelSettingsHost);
            Controls.Add(panelTop);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(800, 480);
            Name = "AboutFormFireDX";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "About — FireworksDX";
            WindowState = FormWindowState.Maximized;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ResumeLayout(false);
        }
    }
}