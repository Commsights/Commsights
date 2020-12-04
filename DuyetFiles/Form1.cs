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

namespace DuyetFiles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory();
            string fileName = path + @"\0_fileName.txt";
            DirectoryInfo d = new DirectoryInfo(path);
            List<FileInfo> list = d.GetFiles("*.*").OrderBy(f => f.Name).ToList();
            StringBuilder txt = new StringBuilder();
            using (StreamWriter sw = File.CreateText(fileName))
            {
                foreach (FileInfo file in list)
                {
                    sw.WriteLine(file.Name);
                    txt.AppendLine(file.Name);
                }
            }
        }
    }
}
