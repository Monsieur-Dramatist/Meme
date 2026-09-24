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
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            checkBox1 = new CheckBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 45);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(567, 614);
            dataGridView1.TabIndex = 0;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.White;
            textBox1.Font = new Font("Segoe UI", 12F);
            textBox1.Location = new Point(900, 45);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(280, 34);
            textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Segoe UI", 12F);
            textBox2.Location = new Point(900, 117);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(280, 34);
            textBox2.TabIndex = 2;
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
            label1.Click += label1_Click;
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
            label2.Click += label2_Click;
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
            label3.Click += label3_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(976, 213);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(18, 17);
            checkBox1.TabIndex = 6;
            checkBox1.UseVisualStyleBackColor = true;
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
            // 
            // button3
            // 
            button3.Font = new Font("Script MT Bold", 18F, FontStyle.Bold);
            button3.ForeColor = Color.DarkSlateBlue;
            button3.Location = new Point(770, 512);
            button3.Name = "button3";
            button3.Size = new Size(380, 49);
            button3.TabIndex = 9;
            button3.Text = "Актуальные";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Font = new Font("Script MT Bold", 18F, FontStyle.Bold);
            button4.ForeColor = Color.DarkSlateBlue;
            button4.Location = new Point(770, 607);
            button4.Name = "button4";
            button4.Size = new Size(380, 52);
            button4.TabIndex = 10;
            button4.Text = "По категориям";
            button4.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1243, 701);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(checkBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(dataGridView1);
            ForeColor = Color.White;
            Name = "Form1";
            Text = "База мемов";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private CheckBox checkBox1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}
