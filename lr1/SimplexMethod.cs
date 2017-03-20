using System;
using System.Drawing;
using System.Windows.Forms;


namespace SimplexMethod
{
    public class SimplexMethod
    {
        public SimplexMethod()
        {
        }
    }


    //класс для создания, добавления на форму и забора занчений с формы
    public class TCT
    {
        public TextBox t1;
        public TextBox t2;
        public ComboBox c;
        public TCT()
        {
            this.t1 = null;
            this.t2 = null;
            this.c = null;
        }
        //создание контролов
        public TCT(int h, string pref)
        {
            this.t1 = new TextBox();
            this.t1.Name = "textbox_tmp" + pref;
            this.t1.Width = 156;
            this.t1.Height = 32;
            this.t1.Location = new Point(10, h);
            this.c = new ComboBox();
            this.c.Name = "combox_tmp" + pref;
            this.c.Items.Add(">=");
            this.c.Items.Add("=");
            this.c.Items.Add("=<");
            this.c.Width = 40;
            this.c.Height = 32;
            this.c.Location = new Point(175, h);
            this.t2 = new TextBox();
            this.t2.Name = "textbox_tmp_z" + pref;
            this.t2.Width = 40;
            this.t2.Height = 32;
            this.t2.Location = new Point(220, h);
        }
        //вставка контролов на групбокс
        public void PasteControl(GroupBox gb)
        {
            gb.Controls.Add(this.t1);
            gb.Controls.Add(this.c);
            gb.Controls.Add(this.t2);
        }
    }
}
