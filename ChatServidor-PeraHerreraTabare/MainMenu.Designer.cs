
namespace ChatServidor_PeraHerreraTabare
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TituloMainMenu = new System.Windows.Forms.Label();
            this.cbb_protocol = new System.Windows.Forms.ComboBox();
            this.lb_Protocol = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbb_mode = new System.Windows.Forms.ComboBox();
            this.btn_launch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TituloMainMenu
            // 
            this.TituloMainMenu.AutoSize = true;
            this.TituloMainMenu.Location = new System.Drawing.Point(12, 28);
            this.TituloMainMenu.Name = "TituloMainMenu";
            this.TituloMainMenu.Size = new System.Drawing.Size(246, 45);
            this.TituloMainMenu.TabIndex = 0;
            this.TituloMainMenu.Text = "Bienvenido al Chat THP, por favor seleccione:\r\n• El Protocolo de Transferencia de" +
    " Datos \r\n• El modo en que desea iniciar la Aplicación";
            // 
            // cbb_protocol
            // 
            this.cbb_protocol.FormattingEnabled = true;
            this.cbb_protocol.Items.AddRange(new object[] {
            "TCP",
            "UDP"});
            this.cbb_protocol.Location = new System.Drawing.Point(87, 104);
            this.cbb_protocol.Name = "cbb_protocol";
            this.cbb_protocol.Size = new System.Drawing.Size(268, 23);
            this.cbb_protocol.TabIndex = 1;
            // 
            // lb_Protocol
            // 
            this.lb_Protocol.AutoSize = true;
            this.lb_Protocol.Location = new System.Drawing.Point(13, 107);
            this.lb_Protocol.Name = "lb_Protocol";
            this.lb_Protocol.Size = new System.Drawing.Size(59, 15);
            this.lb_Protocol.TabIndex = 2;
            this.lb_Protocol.Text = "Protocolo";
            this.lb_Protocol.Click += new System.EventHandler(this.label1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Modo";
            // 
            // cbb_mode
            // 
            this.cbb_mode.FormattingEnabled = true;
            this.cbb_mode.Items.AddRange(new object[] {
            "Servidor",
            "Cliente"});
            this.cbb_mode.Location = new System.Drawing.Point(87, 147);
            this.cbb_mode.Name = "cbb_mode";
            this.cbb_mode.Size = new System.Drawing.Size(268, 23);
            this.cbb_mode.TabIndex = 3;
            // 
            // btn_launch
            // 
            this.btn_launch.Location = new System.Drawing.Point(280, 209);
            this.btn_launch.Name = "btn_launch";
            this.btn_launch.Size = new System.Drawing.Size(75, 23);
            this.btn_launch.TabIndex = 5;
            this.btn_launch.Text = "Iniciar";
            this.btn_launch.UseVisualStyleBackColor = true;
            this.btn_launch.Click += new System.EventHandler(this.btn_launch_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 261);
            this.Controls.Add(this.btn_launch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbb_mode);
            this.Controls.Add(this.lb_Protocol);
            this.Controls.Add(this.cbb_protocol);
            this.Controls.Add(this.TituloMainMenu);
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TituloMainMenu;
        private System.Windows.Forms.ComboBox cbb_protocol;
        private System.Windows.Forms.Label lb_Protocol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbb_mode;
        private System.Windows.Forms.Button btn_launch;
    }
}