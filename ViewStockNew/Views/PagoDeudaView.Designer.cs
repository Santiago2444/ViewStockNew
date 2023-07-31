namespace ViewStockNew.Views
{
    partial class PagoDeudaView
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            GridComprasDeudas = new DataGridView();
            label16 = new Label();
            pictureBox10 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)GridComprasDeudas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).BeginInit();
            SuspendLayout();
            // 
            // GridComprasDeudas
            // 
            GridComprasDeudas.AllowUserToAddRows = false;
            GridComprasDeudas.AllowUserToDeleteRows = false;
            GridComprasDeudas.AllowUserToResizeColumns = false;
            GridComprasDeudas.AllowUserToResizeRows = false;
            GridComprasDeudas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            GridComprasDeudas.BackgroundColor = Color.FromArgb(50, 50, 50);
            GridComprasDeudas.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Lucida Sans", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            GridComprasDeudas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            GridComprasDeudas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GridComprasDeudas.Location = new Point(291, 67);
            GridComprasDeudas.Name = "GridComprasDeudas";
            GridComprasDeudas.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(50, 50, 50);
            dataGridViewCellStyle2.Font = new Font("Lucida Sans", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            GridComprasDeudas.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(50, 50, 50);
            dataGridViewCellStyle3.Font = new Font("Lucida Sans", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.White;
            GridComprasDeudas.RowsDefaultCellStyle = dataGridViewCellStyle3;
            GridComprasDeudas.RowTemplate.Height = 25;
            GridComprasDeudas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridComprasDeudas.Size = new Size(608, 421);
            GridComprasDeudas.TabIndex = 279;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Lucida Sans", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label16.ForeColor = SystemColors.ControlLightLight;
            label16.Location = new Point(291, 21);
            label16.Name = "label16";
            label16.Size = new Size(101, 28);
            label16.TabIndex = 280;
            label16.Text = "Deudas";
            // 
            // pictureBox10
            // 
            pictureBox10.Image = Properties.Resources.OrangeDivider;
            pictureBox10.Location = new Point(291, 52);
            pictureBox10.Name = "pictureBox10";
            pictureBox10.Size = new Size(607, 11);
            pictureBox10.TabIndex = 281;
            pictureBox10.TabStop = false;
            // 
            // PagoDeudaView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(38, 38, 38);
            ClientSize = new Size(924, 505);
            Controls.Add(pictureBox10);
            Controls.Add(label16);
            Controls.Add(GridComprasDeudas);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "PagoDeudaView";
            Text = "Pagar deuda";
            ((System.ComponentModel.ISupportInitialize)GridComprasDeudas).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView GridComprasDeudas;
        private Label label16;
        private PictureBox pictureBox10;
    }
}