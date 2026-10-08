namespace MemeApp.WinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            dataGridView1 = new DataGridView();
            txtTitle = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            chkActual = new CheckBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            pictureBox1 = new PictureBox();
            button5 = new Button();
            txtCategory = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(47, 538);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(567, 227);
            dataGridView1.TabIndex = 0;
            // 
            // txtTitle
            // 
            txtTitle.BackColor = Color.White;
            txtTitle.Font = new Font("Segoe UI", 12F);
            txtTitle.Location = new Point(900, 45);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(280, 34);
            txtTitle.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Script MT Bold", 19.8000011F, FontStyle.Bold);
            label1.ForeColor = Color.DarkSlateBlue;
            label1.Location = new Point(667, 37);
            label1.Name = "label1";
            label1.Size = new Size(184, 41);
            label1.TabIndex = 3;
            label1.Text = "Название";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Script MT Bold", 19.8000011F, FontStyle.Bold);
            label2.ForeColor = Color.DarkSlateBlue;
            label2.Location = new Point(667, 105);
            label2.Name = "label2";
            label2.Size = new Size(194, 41);
            label2.TabIndex = 4;
            label2.Text = "Категория";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Script MT Bold", 19.8000011F, FontStyle.Bold);
            label3.ForeColor = Color.DarkSlateBlue;
            label3.Location = new Point(667, 195);
            label3.Name = "label3";
            label3.Size = new Size(252, 41);
            label3.TabIndex = 5;
            label3.Text = "Актуальность";
            // 
            // chkActual
            // 
            chkActual.AutoSize = true;
            chkActual.Location = new Point(976, 213);
            chkActual.Name = "chkActual";
            chkActual.Size = new Size(18, 17);
            chkActual.TabIndex = 6;
            chkActual.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Font = new Font("Script MT Bold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.DarkSlateBlue;
            button1.Location = new Point(770, 308);
            button1.Name = "button1";
            button1.Size = new Size(380, 48);
            button1.TabIndex = 7;
            button1.Text = "Добавить мем";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Script MT Bold", 18F, FontStyle.Bold);
            button2.ForeColor = Color.DarkSlateBlue;
            button2.Location = new Point(770, 402);
            button2.Name = "button2";
            button2.Size = new Size(380, 47);
            button2.TabIndex = 8;
            button2.Text = "Удалить мем";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Script MT Bold", 18F, FontStyle.Bold);
            button3.ForeColor = Color.DarkSlateBlue;
            button3.Location = new Point(770, 598);
            button3.Name = "button3";
            button3.Size = new Size(380, 49);
            button3.TabIndex = 9;
            button3.Text = "Актуальные";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Font = new Font("Script MT Bold", 18F, FontStyle.Bold);
            button4.ForeColor = Color.DarkSlateBlue;
            button4.Location = new Point(770, 713);
            button4.Name = "button4";
            button4.Size = new Size(380, 52);
            button4.TabIndex = 10;
            button4.Text = "По категориям";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(92, 37);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(465, 451);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // button5
            // 
            button5.Font = new Font("Script MT Bold", 18F, FontStyle.Bold);
            button5.ForeColor = Color.DarkSlateBlue;
            button5.Location = new Point(770, 493);
            button5.Name = "button5";
            button5.Size = new Size(380, 49);
            button5.TabIndex = 12;
            button5.Text = "Изменить мем";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // txtCategory
            // 
            txtCategory.FormattingEnabled = true;
            txtCategory.Location = new Point(900, 117);
            txtCategory.Name = "txtCategory";
            txtCategory.Size = new Size(280, 28);
            txtCategory.TabIndex = 13;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1243, 793);
            Controls.Add(txtCategory);
            Controls.Add(button5);
            Controls.Add(pictureBox1);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(chkActual);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtTitle);
            Controls.Add(dataGridView1);
            ForeColor = Color.White;
            Name = "Form1";
            Text = "База мемов";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private TextBox txtTitle;
        private Label label1;
        private Label label2;
        private Label label3;
        private CheckBox chkActual;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private PictureBox pictureBox1;
        private Button button5;
        private ComboBox txtCategory;
    }
}
