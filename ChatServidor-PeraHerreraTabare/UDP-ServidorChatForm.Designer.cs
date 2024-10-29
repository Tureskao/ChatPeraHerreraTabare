namespace ChatServidor_PeraHerreraTabare
{
    partial class UDP_ServidorChatForm
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
            mostrarTextBox = new System.Windows.Forms.TextBox();
            entradaTextBox = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // mostrarTextBox
            // 
            mostrarTextBox.Enabled = false;
            mostrarTextBox.Location = new System.Drawing.Point(15, 17);
            mostrarTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            mostrarTextBox.Multiline = true;
            mostrarTextBox.Name = "mostrarTextBox";
            mostrarTextBox.Size = new System.Drawing.Size(857, 599);
            mostrarTextBox.TabIndex = 0;
            // 
            // entradaTextBox
            // 
            entradaTextBox.Location = new System.Drawing.Point(15, 640);
            entradaTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            entradaTextBox.Multiline = true;
            entradaTextBox.Name = "entradaTextBox";
            entradaTextBox.Size = new System.Drawing.Size(857, 60);
            entradaTextBox.TabIndex = 1;
            // 
            // UDP_ServidorChatForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(896, 748);
            Controls.Add(entradaTextBox);
            Controls.Add(mostrarTextBox);
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "UDP_ServidorChatForm";
            Text = "Chat THP - Servidor UDP";
            Load += UDP_ServidorChatForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox mostrarTextBox;
        private System.Windows.Forms.TextBox entradaTextBox;
    }
}