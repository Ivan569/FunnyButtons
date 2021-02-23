using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace FunnyButtons
{
    public partial class Form1 : Form
    {
        private int timerCounter = 0;
        private int nowCount = 0;
        private int recordCount = 0;
        private const string fileName = "record";

        public Form1() => InitializeComponent();

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Width / 2; i++)
                label1.Text += "―";
            timer1.Start();
            timer1.Interval = 1000;
            Deserialize();
            Record.Text = "Record: " + recordCount;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            nowCount++;
            ChangeLocationOfButtons();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            nowCount += 2;
            ChangeLocationOfButtons();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            nowCount += 3;
            ChangeLocationOfButtons();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            nowCount += 4;
            ChangeLocationOfButtons();
        }

        private void ChangeLocationOfButtons()
        {
            Random rnd = new Random();
            button1.Location = new Point(rnd.Next(0, Width - button1.Width * 3), rnd.Next(0, Height - button1.Height * 4 - 7));
            button2.Location = new Point(rnd.Next(0, Width - button2.Width * 3), rnd.Next(0, Height - button2.Height * 4 - 7));
            button3.Location = new Point(rnd.Next(0, Width - button3.Width * 3), rnd.Next(0, Height - button3.Height * 4 - 7));
            button4.Location = new Point(rnd.Next(0, Width - button4.Width * 3), rnd.Next(0, Height - button4.Height * 4 - 7));           
            label2.Text = "Now: " + nowCount;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Time.Text = "Time: " + (++timerCounter).ToString();
            if (timerCounter == 60)
            {
                timer1.Stop();
                MessageBox.Show("Well done! Your account: " + nowCount + "!", "The outcome of the game");
                if (recordCount < nowCount)
                    Serialize(nowCount);
                Process.Start(Assembly.GetEntryAssembly().Location);
                Process.GetCurrentProcess().Kill();
            }
        }       

        private void Serialize(int count)
        {
            FileStream fs = new FileStream(fileName + ".s", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, count);
            fs.Close();
        }

        private void Deserialize()
        {
            try
            {
                FileStream fs = new FileStream(fileName + ".s", FileMode.Open, FileAccess.Read, FileShare.Read);
                BinaryFormatter bf = new BinaryFormatter();
                recordCount = (int)bf.Deserialize(fs);
                fs.Close();
            }
            catch { }
        }               
    }
}