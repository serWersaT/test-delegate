using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Nut
{
    class Updates
    {
        public void Instruction()   //скачаиваем инструкцию для программы
        {
            try
            {
                if (File.Exists("instruction.txt")) /*проверяем наличие файла с инструкциями*/
                {
                    string[] instructionsOld = new string[100];    /*создаем массив со списком устаревших инструкций*/
                    string filenameOld = "instruction.txt";
                    instructionsOld = File.ReadAllLines(filenameOld);
                }

                WebClient wc = new WebClient(); /*скачивает файл с инструкциями*/
                string url = @"http://servernut.ru:8080/instruction.txt";
                string file = @"instruction.txt";
                wc.DownloadFile(url, file);

                string[] instructionsNew = new string[100];    /*создаем массив со списком новых инструкций*/
                string filenameNew = "instruction.txt";
                instructionsNew = File.ReadAllLines(filenameNew);
                
            }
            catch (Exception e) { /* добавить инфу в лог файл */ }
        }
    }
}
