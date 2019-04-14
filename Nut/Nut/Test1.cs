using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nut
{
    public class Test1
    {
        public void TestA(string name)
        {
            string str1 = "Привет ";
            string str2 = name;
            int number = 1;
            MessageBox.Show(number.ToString() + ". Все ОК! " + str1 + str2);
        }
    }
}
