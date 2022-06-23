using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace MeMory
{
    public partial class Play : Form
    {
        public Play(int n)           
        {
            InitializeComponent();
            Maketable(n);
            Chooseiconsforplay(n);
        }

        private void Maketable(int n)
        {
            int a = (int)Math.Sqrt(n);
            tableLayoutPanel1.ColumnCount = a;
            tableLayoutPanel1.RowCount = a;
            tableLayoutPanel1.ColumnStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));   /*Percent, 50F*/
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            for (int i = 0; i < a; i++)
                for (int j = 0; j < a; j++)
                {
                    tableLayoutPanel1.Controls.Add
                    (new PictureBox()
                    {
                        Name = i.ToString(),
                        Location = new Point(i, j),
                        Dock = DockStyle.Fill,
                        BackColor = Color.Khaki,
                        BackgroundImageLayout = ImageLayout.Zoom,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                    }
                    );
                }

            if(n>10)
                powertimer.Interval = 800;
            else
                powertimer.Interval = 300;           
            return;
        }


        Random random = new Random();
        private void Chooseiconsforplay(int n)
        {
            List<Image> images = new List<Image>();
            for (int i = 0; i < n; i++)
            {
                images.Add(null);
            }
            for (int i = 0; i < n/2; i++)
            {
                images[i]= Image.FromFile("картиночки\\0" + i + ".png");
                images[i].Tag = i;                                             
                images[n-i-1]= Image.FromFile("картиночки\\0" + i + ".png");
                images[n-i-1].Tag = i;                                           
            }
                
                           
            foreach (Control control in tableLayoutPanel1.Controls)
            /*будем делать одно и то же несколько раз, перебирая и помещая эл-ты панели в control*/
            {
                PictureBox picture = control as PictureBox;
                /*переменную control преобразуют в метку iconlabel*/
                if (picture != null)
                {
                    int a = random.Next(images.Count);
                    /*берем рандомное число*/
                    picture.BackgroundImage = images[a];
                    picture.Tag = images[a].Tag;                                              
                    picture.Image = Image.FromFile("допкартиночки\\рубашка.jpg"); 
                    picture.Click += new EventHandler(Clicks);                      //добавляем каждой карточке действие!
                    images.RemoveAt(a);
                    /*удаляем из листа использованную картинку*/
                }
            }                       
        }


        SoundPlayer right = new SoundPlayer("звуки\\правильно.wav");    //звуки загрузили
        SoundPlayer wrong = new SoundPlayer("звуки\\неправильно.wav");
        

        PictureBox click1 = null;                          //будем отслеживать 2 выбранные карточки
        PictureBox click2 = null;
        /*ссылочные переменные, а не объкты, тк без new*/

        private void Clicks(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)      //если таймер запущен, то больше пока нельзя выбирать
                return;

            PictureBox clicked = sender as PictureBox;
            /*теперь sender - объект типа PictureBox*/
            if (clicked != null)
            {
                if (clicked.Image == null)    //если уже открыта
                    return;
                if (click1 == null)
                {
                    click1 = clicked;
                    click1.Image = null;
                    return;
                }
                click2 = clicked;
                click2.Image = null;
                
                Maybewin();
                
                if (click1.Tag.ToString()==click2.Tag.ToString())        
                {
                    right.Play();
                    click1 = null;
                    click2 = null;
                    return;
                }
                wrong.Play();
                timer1.Start();                 
            }            

        }

        private void timer1_Tick(object sender, EventArgs e)       //закрываем обе карточки через какое-то время
        {
            timer1.Stop();
            click1.Image = Image.FromFile("допкартиночки\\рубашка.jpg");
            click2.Image = Image.FromFile("допкартиночки\\рубашка.jpg");
            click1 = null;
            click2 = null;
        }

        private void Maybewin()       //проверяем, выиграл ли
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                PictureBox im = control as PictureBox;
                if (im != null)
                {
                    if (im.Image != null)
                        return;
                }
            }

            /*если мы вышли сюда, значит перебрали все карточки и все перевернуты*/
            Thread.Sleep(200);
                                                                                     
            Конец newForm = new Конец(true);          
            newForm.Show();
            this.Close();
            /*завершаем игру*/
        }

        private void getridoftime(object sender, EventArgs e)
        {
            progressBartime.PerformStep();
            if (progressBartime.Value == 100)
            {
                powertimer.Stop();
                /*если мы вышли сюда, значит время истекло*/
                Конец newForm = new Конец(false);
                newForm.Show();
                this.Close();
                /*завершаем игру*/
            }
            else { progressBartime.Value++; }
            
        }
        
    }
}
