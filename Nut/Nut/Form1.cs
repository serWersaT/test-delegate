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

            /*
            WebClient wc = new WebClient(); 
            string url1 = @"http://servernut.ru:8080/Test.cs";
            string url2 = @"http://servernut:8080/Test.cs";
            string file1 = @"Test.txt";
            string file2 = @"Test.cs";
            wc.DownloadFile(url1, file1);
            wc.DownloadFile(url2, file2);

            string allCode = File.ReadAllText(file1);
            richTextBox1.Text = allCode;


            dynamic script = CSScript.Evaluator.LoadCode(allCode);  
            
            int result = script.Sum(1, 2);*/



        }



        public string TestA(string name)
        {
            string str1 = "Привет ";
            string str2 = name;
            int number = 1;
            return (number.ToString() + ". Все ОК! " + str1 + str2);
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
            richTextBox1.Text += "          Приминение вложенного метода в лямбда-выражения: " + sum.ToString() + "\r\n";
        }


        private void TestD()
        {
            int a = 5;
            int b = 10;
            int sum = a - b;
            richTextBox1.Text += "          Приминение вложенного метода в делегат: " + sum.ToString() + "\r\n";
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
            message = TestC;
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

        delegate void Countit();
        delegate void Countit1(int end);
        private void анонимныйМетодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Countit count = delegate
            {
                richTextBox1.Text += "Веполнение анонимного метода " + "\r\n";
                for (int i = 0; i < 5; i++)
                    richTextBox1.Text += i.ToString() + " ";

                richTextBox1.Text += "\r\n" + "\r\n";
            };
            count();

            Countit1 count1 = delegate (int end)
            {
                richTextBox1.Text += "Веполнение анонимного метода c передачей аргументв" + "\r\n";
                for (int i = 0; i < end; i++)
                    richTextBox1.Text += i.ToString() + " ";

                richTextBox1.Text += "\r\n" + "\r\n";
            };
            count1(10);

            Func<int, int, int> func = PlusFunc();
            int rezult = func(2, 3);
            richTextBox1.Text += "Резултат обобщенного делегата: " + rezult.ToString();


            richTextBox1.Text += "\r\n" + "\r\n" + "ЛЯМБДА - ВЫРАЖЕНИЯ!!!!!!" + "\r\n";

            Func<int, int, int> func1 = PlusFunc1();
            int rezult1 = func1(2, 3);
            richTextBox1.Text += "Результат выполнения лямбда-выражения: " + rezult1.ToString() + "\r\n";
            richTextBox1.Text += "Маленький прикол. Ошибок не вызывает: " + func1.ToString() + "\r\n";




            richTextBox1.Text += "\r\n" + "\r\n" + "ЛЯМБДА-ВЫРАЖЕНИЕ ВНУТРИ ЛЯМБДА ВЫРАЖЕНИЯ" + "\r\n";
            Func<Func<int, int, int>, int, int> funcsuper = (k, f) => k(3, 5) * f;
            int rezultsuper = funcsuper(func1, 3);
            richTextBox1.Text += "Результат выполнения супер лямбда-выражения: " + rezultsuper.ToString() + "\r\n";
        }


        public Func<int, int, int> PlusFunc()
        {
            Func<int, int, int> func = delegate (int x, int y)
            {
                richTextBox1.Text += "Приминение обобщенных делегатов." + "\r\n";
                TestD();
                return x + y;
            };
            return func;
        }


        public Func<int, int, int> PlusFunc1()
        {
            Func<int, int, int> func1 = (x, y) => x + y;
            {
                richTextBox1.Text += "Приминение анонимного метода в лямбда-выражении" + "\r\n";
                TestC();
            }
            return func1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(Start);
            if (timer1.Enabled == false)
            {
                timer1.Enabled = true;
                button1.Text = "Stop";

                if (start == false)
                {
                    thread.Start();
                }
            }
            else
            {
                timer1.Enabled = false;
                button1.Text = "Start";

            }





        }
        bool start = false;
        delegate void Progress(int value);
        void Start()
        {
            start = true;
            Random rdm = new Random();
            while (true)
            {
                int prg = rdm.Next(0, 1000);
                progressBar2.Invoke(new Progress((s) =>
                {
                    if (s <= 1000) progressBar2.Value = s;
                    else
                    {
                        s = 0;
                        progressBar2.Value = s;
                    }
                }

                ), progressBar2.Value + prg);
                Thread.Sleep(200);
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rdm = new Random();
            int prg = rdm.Next(0, 1000);
            progressBar1.Value = prg;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }



        private void событияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Test1 tst = new Test1();
            tst.onChange += tst_onChange;
            tst.Title = "Click";
        }

        void tst_onChange(object sender, EventArgs e)
        {
            MessageBox.Show("good click");
        }
    }


    
}