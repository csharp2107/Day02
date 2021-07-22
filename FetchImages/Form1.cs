using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FetchImages
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        String baseURL = "http://51.91.120.89/TABLICE/";
        private async void button1_Click(object sender, EventArgs e)
        {
            List<Task> tasks = new List<Task>();

            WebClient wb = new WebClient();
            string content = wb.DownloadString(baseURL);
            string[] lines = content.Split('\n');
            foreach (var line in lines)
            {

                String imageFile = line.Trim();
                if (String.IsNullOrEmpty(imageFile))
                    continue;

                Task task = Task.Run( ()=> {

                    String url = $"{baseURL}{imageFile}";

                    WebClient client = new WebClient();
                    byte[] data = client.DownloadData(url);
                    using (FileStream fs = new FileStream("c:/tmp/"+imageFile, FileMode.Create))
                    {
                        fs.Write(data, 0, data.Length);
                        fs.Flush();
                    }

                } );
                tasks.Add(task);
                //break;

            }

            await Task.WhenAll(tasks);

        }
    }
}
