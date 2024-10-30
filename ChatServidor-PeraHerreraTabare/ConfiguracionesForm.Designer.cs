
namespace ChatServidor_PeraHerreraTabare
{
    partial class ConfiguracionesForm
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
            this.txb_DireccionIp = new System.Windows.Forms.TextBox();
            this.txb_Puerto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_TipoChat = new System.Windows.Forms.Label();
            this.lbl_PropiedadPort = new System.Windows.Forms.Label();
            this.lbl_PropPort = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txb_DireccionIp
            // 
            this.txb_DireccionIp.Location = new System.Drawing.Point(101, 110);
            this.txb_DireccionIp.Name = "txb_DireccionIp";
            this.txb_DireccionIp.Size = new System.Drawing.Size(262, 23);
            this.txb_DireccionIp.TabIndex = 0;
            this.txb_DireccionIp.TextChanged += new System.EventHandler(this.txb_DireccionIp_TextChanged);
            // 
            // txb_Puerto
            // 
            this.txb_Puerto.Location = new System.Drawing.Point(101, 151);
            this.txb_Puerto.Name = "txb_Puerto";
            this.txb_Puerto.Size = new System.Drawing.Size(99, 23);
            this.txb_Puerto.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Dirección IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Puerto:";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(288, 208);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 4;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(273, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Configure la dirección y el puerto deseados para el";
            // 
            // lbl_TipoChat
            // 
            this.lbl_TipoChat.AutoSize = true;
            this.lbl_TipoChat.Location = new System.Drawing.Point(25, 46);
            this.lbl_TipoChat.Name = "lbl_TipoChat";
            this.lbl_TipoChat.Size = new System.Drawing.Size(0, 15);
            this.lbl_TipoChat.TabIndex = 6;
            // 
            // lbl_PropiedadPort
            // 
            this.lbl_PropiedadPort.AutoSize = true;
            this.lbl_PropiedadPort.Location = new System.Drawing.Point(101, 89);
            this.lbl_PropiedadPort.Name = "lbl_PropiedadPort";
            this.lbl_PropiedadPort.Size = new System.Drawing.Size(0, 15);
            this.lbl_PropiedadPort.TabIndex = 7;
            // 
            // lbl_PropPort
            // 
            this.lbl_PropPort.AutoSize = true;
            this.lbl_PropPort.Location = new System.Drawing.Point(101, 177);
            this.lbl_PropPort.Name = "lbl_PropPort";
            this.lbl_PropPort.Size = new System.Drawing.Size(0, 15);
            this.lbl_PropPort.TabIndex = 8;
            // 
            // ConfiguracionesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 243);
            this.Controls.Add(this.lbl_PropPort);
            this.Controls.Add(this.lbl_PropiedadPort);
            this.Controls.Add(this.lbl_TipoChat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txb_Puerto);
            this.Controls.Add(this.txb_DireccionIp);
            this.Name = "ConfiguracionesForm";
            this.Text = "Configuraciones";
            this.Load += new System.EventHandler(this.ConfiguracionesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txb_DireccionIp;
        private System.Windows.Forms.TextBox txb_Puerto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_TipoChat;
        private System.Windows.Forms.Label lbl_PropiedadPort;
        private System.Windows.Forms.Label lbl_PropPort;
    }
}