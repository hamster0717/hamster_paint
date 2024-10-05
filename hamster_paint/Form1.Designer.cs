namespace hamster_paint
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Save = new System.Windows.Forms.Button();
            this.circle = new System.Windows.Forms.RadioButton();
            this.line = new System.Windows.Forms.RadioButton();
            this.square = new System.Windows.Forms.RadioButton();
            this.prebtn = new System.Windows.Forms.Button();
            this.nexbtn = new System.Windows.Forms.Button();
            this.openbtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::hamster_paint.Properties.Resources.sad;
            this.pictureBox1.Location = new System.Drawing.Point(12, 51);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1024, 768);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            this.pictureBox1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseWheel);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(12, 12);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 40);
            this.Save.TabIndex = 1;
            this.Save.Text = "储存";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // circle
            // 
            this.circle.AutoSize = true;
            this.circle.Location = new System.Drawing.Point(469, 21);
            this.circle.Name = "circle";
            this.circle.Size = new System.Drawing.Size(73, 22);
            this.circle.TabIndex = 2;
            this.circle.TabStop = true;
            this.circle.Text = "circle";
            this.circle.UseVisualStyleBackColor = true;
            this.circle.CheckedChanged += new System.EventHandler(this.TypeChange);
            // 
            // line
            // 
            this.line.AutoSize = true;
            this.line.Location = new System.Drawing.Point(404, 21);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(59, 22);
            this.line.TabIndex = 3;
            this.line.TabStop = true;
            this.line.Text = "line";
            this.line.UseVisualStyleBackColor = true;
            this.line.CheckedChanged += new System.EventHandler(this.TypeChange);
            // 
            // square
            // 
            this.square.AutoSize = true;
            this.square.Location = new System.Drawing.Point(548, 21);
            this.square.Name = "square";
            this.square.Size = new System.Drawing.Size(78, 22);
            this.square.TabIndex = 4;
            this.square.TabStop = true;
            this.square.Text = "square";
            this.square.UseVisualStyleBackColor = true;
            this.square.CheckedChanged += new System.EventHandler(this.TypeChange);
            // 
            // prebtn
            // 
            this.prebtn.Location = new System.Drawing.Point(174, 12);
            this.prebtn.Name = "prebtn";
            this.prebtn.Size = new System.Drawing.Size(75, 40);
            this.prebtn.TabIndex = 5;
            this.prebtn.Text = "上一步";
            this.prebtn.UseVisualStyleBackColor = true;
            this.prebtn.Click += new System.EventHandler(this.prebtn_Click);
            // 
            // nexbtn
            // 
            this.nexbtn.Location = new System.Drawing.Point(255, 12);
            this.nexbtn.Name = "nexbtn";
            this.nexbtn.Size = new System.Drawing.Size(75, 40);
            this.nexbtn.TabIndex = 6;
            this.nexbtn.Text = "下一步";
            this.nexbtn.UseVisualStyleBackColor = true;
            this.nexbtn.Click += new System.EventHandler(this.nexbtn_Click);
            // 
            // openbtn
            // 
            this.openbtn.Location = new System.Drawing.Point(93, 12);
            this.openbtn.Name = "openbtn";
            this.openbtn.Size = new System.Drawing.Size(75, 40);
            this.openbtn.TabIndex = 7;
            this.openbtn.Text = "打開";
            this.openbtn.UseVisualStyleBackColor = true;
            this.openbtn.Click += new System.EventHandler(this.Open_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 831);
            this.Controls.Add(this.openbtn);
            this.Controls.Add(this.nexbtn);
            this.Controls.Add(this.prebtn);
            this.Controls.Add(this.square);
            this.Controls.Add(this.line);
            this.Controls.Add(this.circle);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.RadioButton circle;
        private System.Windows.Forms.RadioButton line;
        private System.Windows.Forms.RadioButton square;
        private System.Windows.Forms.Button prebtn;
        private System.Windows.Forms.Button nexbtn;
        private System.Windows.Forms.Button openbtn;
    }
}

