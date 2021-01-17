using Commsights.Data.Helpers;
using Commsights.Data.Models;
using Commsights.Data.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchTool
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnWebsitePriority0_Click(object sender, EventArgs e)
        {
            int indexBegin = 0;
            List<Config> listConfig = ConfigRepository.GetSQLWebsiteByGroupNameAndCodeAndActiveAndIsMenuLeftToList(AppGlobal.CRM, AppGlobal.Website, true, true);
            int listConfigCount = listConfig.Count;
            int indexEnd = indexBegin + 5;
            for (int i = indexBegin; i < indexEnd; i++)
            {
                if (i == listConfigCount)
                {
                    i = indexEnd;
                }
                AsyncCreateProductScanWebsiteNoFilterProduct0001(listConfig[i]);
            }
            MessageBox.Show("Finish");
        }

        private void btnWebsitePriority5_Click(object sender, EventArgs e)
        {

        }

        private void btnWebsitePriority10_Click(object sender, EventArgs e)
        {

        }

        private void btnWebsitePriority15_Click(object sender, EventArgs e)
        {

        }

        private void btnWebsitePriority20_Click(object sender, EventArgs e)
        {

        }

        private void btnWebsitePriority25_Click(object sender, EventArgs e)
        {

        }

        private void btnWebsitePriority30_Click(object sender, EventArgs e)
        {

        }

        private void btnWebsitePriority35_Click(object sender, EventArgs e)
        {

        }

        private void btnWebsitePriority40_Click(object sender, EventArgs e)
        {

        }

        private void btnWebsitePriority45_Click(object sender, EventArgs e)
        {

        }

        private void btnWebsitePriority50_Click(object sender, EventArgs e)
        {

        }

        private void btnWebsitePriority55_Click(object sender, EventArgs e)
        {

        }
        public string AsyncCreateProductScanWebsiteNoFilterProduct0001(Config config)
        {
            try
            {
                if (config != null)
                {
                    List<LinkItem> list = new List<LinkItem>();
                    AppGlobal.LinkFinder001(config.URLFull, config.URLFull, true, list);
                    foreach (LinkItem linkItem in list)
                    {
                        try
                        {
                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(linkItem.Href);
                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                Stream receiveStream = response.GetResponseStream();
                                StreamReader readStream = null;
                                readStream = new StreamReader(receiveStream, Encoding.UTF8);
                                string html = readStream.ReadToEnd();
                                html = html.Replace(@"~", @"");
                                html = AppGlobal.HTMLReplaceAndSplit(html);
                                string title = "";
                                string htmlTitle = html;
                                if ((htmlTitle.Contains(@"<meta property=""og:title"" content=""") == true) || (htmlTitle.Contains(@"<meta property='og:title' content='") == true))
                                {
                                    htmlTitle = htmlTitle.Replace(@"<meta property=""og:title"" content=""", @"~");
                                    htmlTitle = htmlTitle.Replace(@"<meta property='og:title' content='", @"~");
                                    if (htmlTitle.Split('~').Length > 1)
                                    {
                                        htmlTitle = htmlTitle.Split('~')[1];
                                        htmlTitle = htmlTitle.Replace(@"""", @"~");
                                        htmlTitle = htmlTitle.Replace(@"'", @"~");
                                        htmlTitle = htmlTitle.Split('~')[0];
                                        title = htmlTitle.Trim();
                                    }
                                }
                                else
                                {
                                    MatchCollection m1 = Regex.Matches(htmlTitle, @"(<title>.*?</title>)", RegexOptions.Singleline);
                                    if (m1.Count > 0)
                                    {
                                        string value = m1[m1.Count - 1].Groups[1].Value;
                                        if (!string.IsNullOrEmpty(value))
                                        {
                                            value = value.Replace(@"<title>", @"");
                                            value = value.Replace(@"</title>", @"");
                                            title = value.Trim();
                                        }
                                    }
                                }
                                bool isUnicode = AppGlobal.ContainsUnicodeCharacter(title);
                                if ((title.Contains(@"&#") == true) || (isUnicode == false))
                                {
                                    MatchCollection m1 = Regex.Matches(htmlTitle, @"(<title>.*?</title>)", RegexOptions.Singleline);
                                    if (m1.Count > 0)
                                    {
                                        string value = m1[m1.Count - 1].Groups[1].Value;
                                        if (!string.IsNullOrEmpty(value))
                                        {
                                            value = value.Replace(@"<title>", @"");
                                            value = value.Replace(@"</title>", @"");
                                            title = value.Trim();
                                        }
                                    }
                                }
                                if (title.Split('|').Length > 2)
                                {
                                    title = title.Split('|')[1];
                                }
                                if (title.Split('|').Length > 1)
                                {
                                    title = title.Split('|')[0];
                                }
                                title = title.Trim();
                                Product product = new Product();
                                product.Description = "";
                                product.Title = title;
                                product.ParentID = config.ID;
                                product.CategoryID = config.ID;
                                product.Source = AppGlobal.SourceAuto;
                                if (string.IsNullOrEmpty(product.Title))
                                {
                                    product.Title = linkItem.Text;
                                }
                                product.URLCode = linkItem.Href;
                                product.DatePublish = DateTime.Now;
                                AppGlobal.FinderContentAndDatePublish001(html, product);
                                if ((product.DatePublish.Year > 2019) && (product.Active == true))
                                {
                                    if (!string.IsNullOrEmpty(product.Title))
                                    {
                                        product.Title = AppGlobal.Decode(product.Title);
                                        product.MetaTitle = AppGlobal.SetName(product.Title);
                                    }
                                    if (!string.IsNullOrEmpty(product.Description))
                                    {
                                        product.Description = AppGlobal.Decode(product.Description);
                                    }
                                    ProductRepository.InsertSingleItem(product);
                                }
                                response.Close();
                                readStream.Close();
                            }
                        }
                        catch (Exception e1)
                        {
                            string mes1 = e1.Message;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string mes = e.Message;
            }
            return "";
        }
    }
}
