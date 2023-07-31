namespace ViewStockNew.Views
{
    partial class MiniLoading
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
            PctLoading = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)PctLoading).BeginInit();
            SuspendLayout();
            // 
            // PctLoading
            // 
            PctLoading.Image = Properties.Resources.MiniLoading;
            PctLoading.Location = new Point(-62, -149);
            PctLoading.Name = "PctLoading";
            PctLoading.Size = new Size(445, 455);
            PctLoading.SizeMode = PictureBoxSizeMode.StretchImage;
            PctLoading.TabIndex = 102;
            PctLoading.TabStop = false;
            // 
            // MiniLoading
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(324, 136);
            Controls.Add(PctLoading);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MiniLoading";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MiniLoading";
            ((System.ComponentModel.ISupportInitialize)PctLoading).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox PctLoading;
    }
}