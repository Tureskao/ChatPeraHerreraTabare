namespace ChatServidor_PeraHerreraTabare
{
    partial class UDP_ClienteChatForm
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
            this.mostrarTextBox = new System.Windows.Forms.TextBox();
            this.entradaTextBox = new System.Windows.Forms.TextBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mostrarTextBox
            // 
            this.mostrarTextBox.Enabled = false;
            this.mostrarTextBox.Location = new System.Drawing.Point(13, 13);
            this.mostrarTextBox.Multiline = true;
            this.mostrarTextBox.Name = "mostrarTextBox";
            this.mostrarTextBox.ReadOnly = true;
            this.mostrarTextBox.Size = new System.Drawing.Size(750, 450);
            this.mostrarTextBox.TabIndex = 0;
            // 
            // entradaTextBox
            // 
            this.entradaTextBox.Location = new System.Drawing.Point(13, 480);
            this.entradaTextBox.Multiline = true;
            this.entradaTextBox.Name = "entradaTextBox";
            this.entradaTextBox.Size = new System.Drawing.Size(750, 46);
            this.entradaTextBox.TabIndex = 1;
            // 
            // btnEnviar
            // 
            this.btnEnviar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnviar.Enabled = false;
            this.btnEnviar.Location = new System.Drawing.Point(697, 532);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(75, 23);
            this.btnEnviar.TabIndex = 3;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // UDP_ClienteChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.entradaTextBox);
            this.Controls.Add(this.mostrarTextBox);
            this.Name = "UDP_ClienteChatForm";
            this.Text = "Chat THP -Cliente UDP";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServidorPaquetesForm_FormClosing);
            this.Load += new System.EventHandler(this.UDP_ClienteChatForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox mostrarTextBox;
        private System.Windows.Forms.TextBox entradaTextBox;
        private System.Windows.Forms.Button btnEnviar;
    }
}