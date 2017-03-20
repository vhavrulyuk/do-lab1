using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimplexMethod;

namespace lr1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        

        private int _x_count;
        private int _limitationsCount;
        private TCT[] _limitationsArray; //Масив контролів з обмеженнями.



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            _limitationsCount = (int) xCount.Value;
            _limitationsArray = new TCT[_limitationsCount];
            int hm = 86;
            int wm = 100; 

            for (int i = 0; i < _limitationsCount; i++)
            {
                TCT tmp = new TCT(hm, i.ToString(),_limitationsCount);
                tmp.PasteControl(limitationsGB);
                hm += 30;
                wm += 42;
                if (limitationsGB.Height < hm + 10)
                {
                    limitationsGB.Height += 30;
                }
                if (limitationsGB.Width < wm)
                {
                    limitationsGB.Width += 42;
                }
                _limitationsArray[i] = tmp;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
