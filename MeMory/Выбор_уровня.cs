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
    public partial class Выбор_уровня : Form
    {
        public Выбор_уровня()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            label1.Visible = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                Play newForm = new Play(4);                           
                newForm.Show();    
            }
            rb.Checked = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                Play newForm = new Play(16);
                newForm.Show();               
            }
            rb.Checked = false;
        }

        
    }
}
