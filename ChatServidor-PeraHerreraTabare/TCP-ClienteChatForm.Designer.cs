namespace ChatServidor_PeraHerreraTabare
{
    partial class TCP_ClienteChatForm
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
            this.SuspendLayout();
            // 
            // mostrarTextBox
            // 
            this.mostrarTextBox.Enabled = false;
            this.mostrarTextBox.Location = new System.Drawing.Point(13, 13);
            this.mostrarTextBox.Multiline = true;
            this.mostrarTextBox.Name = "mostrarTextBox";
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
            // TCP_ClienteChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.entradaTextBox);
            this.Controls.Add(this.mostrarTextBox);
            this.Name = "TCP_ClienteChatForm";
            this.Text = "TCP_ClienteChatForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox mostrarTextBox;
        private System.Windows.Forms.TextBox entradaTextBox;
    }
}