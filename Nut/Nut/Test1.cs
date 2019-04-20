using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nut
{
    public class Test1
    {
        public event EventHandler onChange;
        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                onChange(this, new EventArgs());
            }
        }

        public void TestA(string name)
        {
            string str1 = "Привет ";
            string str2 = name;
            int number = 1;
            MessageBox.Show(number.ToString() + ". Все ОК! " + str1 + str2);
        }
    }
}
