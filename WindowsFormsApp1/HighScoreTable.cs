using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private TextBox textBox1;
        private Button button1;
        private Button button2;
        private TextBox textBox2;
        private string data;
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(498, 330);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(215, 20);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 330);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(447, 20);
            this.button1.TabIndex = 1;
            this.button1.Text = "Введите свой имя чтобы добавить себя в список рекодсменов и нажмите на кнопку";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(304, 251);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(196, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Вывести таблицу рекордов?";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(72, 280);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(641, 289);
            this.textBox2.TabIndex = 3;
            this.textBox2.Visible = false;
            // 
            // Form2
            // 
            this.BackgroundImage = global::WindowsFormsApp1.Resource1.Фон;
            this.ClientSize = new System.Drawing.Size(754, 581);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Name = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public Form2()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint, true);
            UpdateStyles();
        }
        private void Form2_Load(object sender, System.EventArgs e)
        {
            if (File.Exists("table1"))
            {
                if (!File.Exists("Records"))
                {
                    File.Create("Records");
                }
                data = File.ReadAllText("table1");
                File.WriteAllText("table1", "");
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            string c = File.ReadAllText("Records");
            string a = textBox1.Text;
            string b = "\n";
            File.WriteAllText("Records",c + a + " " + data);    
            textBox1.Text = "Готово";
            textBox1.Visible = false;
            button1.Visible = false;
            button1.Enabled = false;
            button2.Visible = true;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            textBox2.Visible = true;
            textBox2.Text = "";
            StreamReader sr = new StreamReader("Records", System.Text.Encoding.Default);

            while (!sr.EndOfStream)
                textBox2.Text += sr.ReadLine() + Environment.NewLine;

            sr.Close();
        }
    }
}