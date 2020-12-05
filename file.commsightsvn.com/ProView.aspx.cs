using System;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;


public partial class ProView : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string file = base.Request.QueryString["file"];
            string type = base.Request.QueryString["type"];
            if ((!string.IsNullOrEmpty(file)) && (!string.IsNullOrEmpty(type)))
            {
                StringBuilder html = new StringBuilder();
                string source = "";
                switch (type)
                {
                    case "Magazine":
                    case "Newspaper":
                        source = "/Pdfs/" + file + ".pdf";
                        html.AppendLine(@"<iframe width='1000' height='600' src='" + source + "'></iframe>");
                        break;
                    case "TV":
                        source = "/Videos/" + file + ".wmv";
                        html.AppendLine(@"<video width='1000' height='600' controls>");
                        html.AppendLine(@"<source src='" + source + "' type='video/mp4'>");
                        html.AppendLine(@"</video>");
                        break;
                    case "mp4":
                        source = "/Videos/" + file + ".mp4";
                        html.AppendLine(@"<video width='1000' height='600' controls>");
                        html.AppendLine(@"<source src='" + source + "' type='video/mp4'>");
                        html.AppendLine(@"</video>");
                        break;
                    case "Radio":
                        source = "/Audios/" + file + ".mp3";
                        html.AppendLine(@"<audio width='1000' height='600' controls>");
                        html.AppendLine(@"<source src='" + source + "' type='audio/mpeg'>");
                        html.AppendLine(@"</audio>");
                        break;
                }
                this.HTML = html.ToString();
            }
        }
    }
    public string HTML { get; set; }
}