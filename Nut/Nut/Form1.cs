using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Security.Permissions;
using System.Threading;
using WbemScripting;

namespace Nut
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*Updates updates = new Updates();
            updates.Instruction();*/


            WebClient wc = new WebClient(); /*скачивает файл с инструкциями*/
            string url1 = @"http://servernut.ru:8080/Test.cs";
            string url2 = @"http://servernut:8080/Test.cs";
            string file1 = @"Test.txt";
            string file2 = @"Test.cs";
            wc.DownloadFile(url1, file1);
            wc.DownloadFile(url2, file2);

            string allCode = File.ReadAllText(file1);
            richTextBox1.Text = allCode;

            /*ссылка на пример http://www.csscript.net/index.html*/

            dynamic script = CSScript.Evaluator.LoadCode(allCode);  /*не понятно где достать script библиотеку*/
            
            int result = script.Sum(1, 2);



        }

        

        public string TestA(string name)
        {
            string str1 = "Привет ";
            string str2 = name;
            int number = 1;
            return(number.ToString() + ". Все ОК! " + str1 + str2);
        }


        public string TestB(string name)
        {
            string str1 = "Пока ";
            string str2 = name;
            int number = 1;
            return (number.ToString() + ". Все ОК! " + str1 + str2);
        }

        private void TestC()
        {
            int a = 5;
            int b = 10;
            int sum = a + b;
            MessageBox.Show("Результат: " + sum.ToString());
        }


        private void TestD()
        {
            int a = 5;
            int b = 10;
            int sum = a - b;
            MessageBox.Show("Результат: " + sum.ToString());
        }


        delegate void StrMod();
        delegate string Hello(string name);
        private void test2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StrMod message;
            StrMod message2;
            StrMod message3;
            Hello hl;
            string str = "Олег";
            message =TestC;
            //MessageBox.Show(TestA(str));  /*так гораздо прощще*/
           // message();

            hl = TestA;
            richTextBox1.Text += hl(str) + "\r\n";
            hl = null;
            hl += TestA;
            hl += TestB;
            richTextBox1.Text += hl(str);

            message2 = TestD;
            message3 = message + message2;  /*сложение делегатов, похоже складываются только однотипные*/
            message3();
        }
    }
}
