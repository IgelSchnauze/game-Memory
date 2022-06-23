using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeMory
{
    public partial class Конец : Form
    {
        public Конец(bool b)
        {
            InitializeComponent();
            pictureBox2.Visible = false;
            if (b == true)
            {
                label1.Text = "Воу-воу, неплохо...";
                pictureBox1.Image = Image.FromFile("допкартиночки\\радость.jpg");
            }
            else
            {
                label1.Text = "Кто-то тормозит...";
                pictureBox1.Image = Image.FromFile("допкартиночки\\грусть.jpg");
                Cry();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Close();
        }

        /*все для движения капли*/
        bool b = true;
        int x = 0; int y = 0;
        int i = 0, j = 0, k = 0;
        private void Cry()
        {
            timer1.Start();
            pictureBox2.Visible = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            i++;
            if (k > 4)
            {
                pictureBox2.Top += 20;
            }
            if (pictureBox2.Bottom >= ClientRectangle.Bottom)
            {
                b = false;
                j = i / 2 + i / 4;
                x = 0; y = 0;
                k++;
            }
            if (b == true)
            {
                pictureBox2.Top += 18 + x;
                pictureBox2.Left -= 5 + y;
                x += 1; y += 1;
            }
            else
            {
                if (j == 0)
                {
                    b = true;
                    i = 0;
                    x = 0; y = 0;
                    k++;
                }
                pictureBox2.Left -= 4 + x;
                pictureBox2.Top -= 10 - y;
                x += 1; y += 1;
                j--;
            }
        }
    }
}
