using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuyetFiles001
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //string path = Directory.GetCurrentDirectory();
            //string fileName = path + @"\0_fileName.txt";
            //DirectoryInfo d = new DirectoryInfo(path);
            //List<FileInfo> list = d.GetFiles("*.*").OrderBy(f => f.Name).ToList();
            //StringBuilder txt = new StringBuilder();
            //using (StreamWriter sw = File.CreateText(fileName))
            //{
            //    foreach (FileInfo file in list)
            //    {
            //        sw.WriteLine(file.Name);
            //    }
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory();
            string fileName000 = path + @"\0_fileName.txt";
            string fileName001 = path + @"\1_fileName.txt";
            string fileName = path + @"\00_fileName.txt";
            string[] line000 = File.ReadAllLines(fileName000);
            string[] line001 = File.ReadAllLines(fileName001);
            List<string> list = new List<string>();
            using (StreamWriter sw = File.CreateText(fileName))
            {
                foreach (string line0 in line000)
                {
                    bool check = false;
                    foreach (string line1 in line001)
                    {
                        if (line0 == line1)
                        {
                            check = true;
                            break;
                        }
                    }
                    if (check == false)
                    {
                        sw.WriteLine(line0);
                    }
                }
            }
        }
    }
}

