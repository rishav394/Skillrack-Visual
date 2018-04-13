using System;
using System.Windows.Forms;

namespace First_Form_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
            textBox1.Select();
            label1.ReadOnly = true;
            label1.Visible = false;
        }
        
        WebBrowser wb = new WebBrowser();
        WebBrowser wb2 = new WebBrowser();
        bool ct = true;
        string pagelink = "";


        private void button1_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            ct = true;
            label1.Text = "";
            textBox1.Enabled = false;
            button1.Enabled = false;
            wb.ScriptErrorsSuppressed = true;
            string temp= "https://vitspot.com/?s="+textBox1.Text;
            wb.Navigate(temp);
            wb.DocumentCompleted += Wb_DocumentCompleted;
        }

        private void Wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (ct)
            {
                var links = wb.Document.GetElementsByTagName("a");
                foreach (HtmlElement link in links)
                {
                    if (link.GetAttribute("className") == "btn dark medium")
                    {
                        pagelink = link.GetAttribute("href");
                        break;
                    }
                }
                
                
                wb2.ScriptErrorsSuppressed = true;
                wb2.Navigate(pagelink.Trim());
               
                wb2.DocumentCompleted += Wb2_DocumentCompleted;
            }
        }

        private void Wb2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (ct)
            {
                ct = false;
                button1.Enabled = true;
                textBox1.Enabled = true;
                
                //label1.Font=new Font("Times New Roman", 12);
                if (Findid() == null)
                {
                    MessageBox.Show("Sorry the entered code was not found.","Please try another id");
                    return;
                }
                string[] words = pagelink.Split('/');
                words[4] = words[4].Replace('-', ' ').ToUpper().Trim();
               
                MessageBox.Show( words[4] + Environment.NewLine+ "Opening now!","Found");
                string data= wb2.Document.GetElementById(Findid()).InnerText;
                label1.Visible = true;
                label1.Text = data;
                
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                button1.Enabled = false;
            }
        }

        public string Findid()
        {
            var links = wb2.Document.GetElementsByTagName("div");

            foreach (HtmlElement link in links)
            {
                 if (link.GetAttribute("className") == "crayon-syntax crayon-theme-sublime-text crayon-font-monaco crayon-os-pc print-yes notranslate")
                {
                    return link.GetAttribute("id");
                }
            }
            return null;
        }

      

    }
}