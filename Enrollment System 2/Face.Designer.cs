
namespace Enrollment_System_2
{
    partial class Face
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
            this.label1 = new System.Windows.Forms.Label();
            this.okBTN = new Enrollment_System_2.MVButtons.CJButtons();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(347, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(413, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "To formally continue, face recognition is required.\r\n";
            // 
            // okBTN
            // 
            this.okBTN.BackColor = System.Drawing.Color.Gray;
            this.okBTN.BackgroundColor = System.Drawing.Color.Gray;
            this.okBTN.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.okBTN.BorderRadius = 25;
            this.okBTN.BorderSize = 2;
            this.okBTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.okBTN.FlatAppearance.BorderSize = 0;
            this.okBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okBTN.ForeColor = System.Drawing.Color.White;
            this.okBTN.Location = new System.Drawing.Point(1418, 846);
            this.okBTN.Margin = new System.Windows.Forms.Padding(4);
            this.okBTN.Name = "okBTN";
            this.okBTN.Size = new System.Drawing.Size(220, 62);
            this.okBTN.TabIndex = 28;
            this.okBTN.Text = "OK";
            this.okBTN.TextColor = System.Drawing.Color.White;
            this.okBTN.UseVisualStyleBackColor = false;
            this.okBTN.Click += new System.EventHandler(this.okBTN_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(351, 95);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1287, 730);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Face
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Moccasin;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.okBTN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Face";
            this.Text = "Face";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MVButtons.CJButtons okBTN;
    }
}