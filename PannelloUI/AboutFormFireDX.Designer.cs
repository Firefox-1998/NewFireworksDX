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
            lblTitle = new Label();
            cmbLanguage = new ComboBox();
            lblVersion = new Label();
            lnkRepo = new LinkLabel();
            panelTop.SuspendLayout();
            SuspendLayout();
            // 
            // fireworksPanel
            // 
            fireworksPanel.BackColor = Color.Black;
            fireworksPanel.Dock = DockStyle.Fill;
            fireworksPanel.Location = new Point(0, 80);
            fireworksPanel.Name = "fireworksPanel";
            fireworksPanel.Size = new Size(861, 560);
            fireworksPanel.TabIndex = 0;
            // 
            // panelSettingsHost
            // 
            panelSettingsHost.BackColor = Color.FromArgb(18, 18, 18);
            panelSettingsHost.Dock = DockStyle.Right;
            panelSettingsHost.Location = new Point(861, 80);
            panelSettingsHost.Name = "panelSettingsHost";
            panelSettingsHost.Size = new Size(239, 560);
            panelSettingsHost.TabIndex = 2;
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(10, 10, 10);
            panelTop.BorderStyle = BorderStyle.FixedSingle;
            panelTop.Controls.Add(btnGpuInfo);
            panelTop.Controls.Add(btnBurst);
            panelTop.Controls.Add(lnkRepo);
            panelTop.Controls.Add(lblVersion);
            panelTop.Controls.Add(cmbLanguage);
            panelTop.Controls.Add(lblTitle);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Padding = new Padding(12, 8, 12, 8);
            panelTop.Size = new Size(1100, 80);
            panelTop.TabIndex = 1;
            // 
            // btnGpuInfo
            // 
            btnGpuInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGpuInfo.BackColor = Color.FromArgb(40, 40, 80);
            btnGpuInfo.FlatStyle = FlatStyle.Flat;
            btnGpuInfo.ForeColor = Color.White;
            btnGpuInfo.Location = new Point(996, 14);
            btnGpuInfo.Name = "btnGpuInfo";
            btnGpuInfo.Size = new Size(90, 50);
            btnGpuInfo.TabIndex = 5;
            btnGpuInfo.Text = "GPU Info";
            btnGpuInfo.UseVisualStyleBackColor = false;
            btnGpuInfo.Click += BtnGpuInfo_Click;
            // 
            // btnBurst
            // 
            btnBurst.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBurst.BackColor = Color.FromArgb(50, 50, 50);
            btnBurst.FlatStyle = FlatStyle.Flat;
            btnBurst.ForeColor = Color.White;
            btnBurst.Location = new Point(900, 14);
            btnBurst.Name = "btnBurst";
            btnBurst.Size = new Size(90, 50);
            btnBurst.TabIndex = 4;
            btnBurst.Text = "Burst x5";
            btnBurst.UseVisualStyleBackColor = false;
            btnBurst.Click += BtnBurst_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(12, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(300, 37);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "FireworksDX — Ghostly";
            // 
            // cmbLanguage
            // 
            cmbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLanguage.FormattingEnabled = true;
            cmbLanguage.Location = new Point(15, 52);
            cmbLanguage.Name = "cmbLanguage";
            cmbLanguage.Size = new Size(150, 23);
            cmbLanguage.TabIndex = 6;
            cmbLanguage.SelectedIndexChanged += CmbLanguage_SelectedIndexChanged;
            // 
            // lblVersion
            // 
            lblVersion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblVersion.Font = new Font("Segoe UI", 9.5F);
            lblVersion.ForeColor = Color.LightGray;
            lblVersion.Location = new Point(550, 18);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(340, 20);
            lblVersion.TabIndex = 2;
            lblVersion.Text = "Version: 1.0.0.0";
            lblVersion.TextAlign = ContentAlignment.TopRight;
            // 
            // lnkRepo
            // 
            lnkRepo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lnkRepo.LinkColor = Color.Cyan;
            lnkRepo.Location = new Point(550, 44);
            lnkRepo.Name = "lnkRepo";
            lnkRepo.Size = new Size(340, 20);
            lnkRepo.TabIndex = 3;
            lnkRepo.TabStop = true;
            lnkRepo.Text = "Open repository / Documentation";
            lnkRepo.TextAlign = ContentAlignment.TopRight;
            lnkRepo.LinkClicked += LnkRepo_LinkClicked;
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