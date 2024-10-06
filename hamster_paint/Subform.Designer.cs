namespace hamster_paint
{
    partial class Form2
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
            this.R = new System.Windows.Forms.TrackBar();
            this.G = new System.Windows.Forms.TrackBar();
            this.B = new System.Windows.Forms.TrackBar();
            this.A = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.R)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.G)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.B)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.A)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // R
            // 
            this.R.Location = new System.Drawing.Point(12, 12);
            this.R.Maximum = 255;
            this.R.Name = "R";
            this.R.Size = new System.Drawing.Size(481, 69);
            this.R.TabIndex = 0;
            this.R.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // G
            // 
            this.G.Location = new System.Drawing.Point(12, 87);
            this.G.Maximum = 255;
            this.G.Name = "G";
            this.G.Size = new System.Drawing.Size(481, 69);
            this.G.TabIndex = 1;
            this.G.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // B
            // 
            this.B.Location = new System.Drawing.Point(12, 162);
            this.B.Maximum = 255;
            this.B.Name = "B";
            this.B.Size = new System.Drawing.Size(481, 69);
            this.B.TabIndex = 2;
            this.B.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // A
            // 
            this.A.Location = new System.Drawing.Point(12, 242);
            this.A.Maximum = 255;
            this.A.Name = "A";
            this.A.Size = new System.Drawing.Size(481, 69);
            this.A.TabIndex = 3;
            this.A.Visible = false;
            this.A.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(629, 270);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 41);
            this.button1.TabIndex = 4;
            this.button1.Text = "選擇";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonSelectColor_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(604, 87);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 323);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.A);
            this.Controls.Add(this.B);
            this.Controls.Add(this.G);
            this.Controls.Add(this.R);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.R)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.G)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.B)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.A)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar R;
        private System.Windows.Forms.TrackBar G;
        private System.Windows.Forms.TrackBar B;
        private System.Windows.Forms.TrackBar A;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}