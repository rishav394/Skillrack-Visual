using System;
using System.IO;
using System.Windows.Forms;

namespace First_Form_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MaximizeBox = false;
            button1.Enabled = false;
            textBox1.Focus();
            label1.ReadOnly = true;
            label1.Visible = false;
            ProgressBar1.MarqueeAnimationSpeed = 1000;
        }

        WebBrowser wb;
        WebBrowser wb2;
        bool ct;
        string[] words;
        string pagelink = "";


        private void button1_Click(object sender, EventArgs e)
        {
            wb = new WebBrowser();
            wb2 = new WebBrowser();
            ProgressBar1.Increment(10);         //  Incremented 20
            label1.Visible = false;
            saveToolStripMenuItem.Enabled = false;
            ct = true;
            label1.Text = "";
            textBox1.Enabled = false;
            button1.Enabled = false;
            wb.ScriptErrorsSuppressed = true;
            string temp= "https://vitspot.com/?s="+textBox1.Text;
            wb.Navigate(temp);
            ProgressBar1.Increment(20);         //  Incremented 20
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
                ProgressBar1.Increment(20);     //  Incremented 20

                if (pagelink == "")
                {
                    ProgressBar1.Value = 0;
                    MessageBox.Show("Sorry the entered code was not found.", "Please try another id");
                    button1.Enabled = true;
                    textBox1.Enabled = true;
                    wb.Dispose();
                    return;
                }
                wb2.ScriptErrorsSuppressed = true;
                wb2.Navigate(pagelink.Trim());
               
                wb2.DocumentCompleted += Wb2_DocumentCompleted;
                wb.Dispose();
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
                    ProgressBar1.Value = 0;      //  Reset
                    wb2.Dispose();
                    return;
                }
                words = pagelink.Split('/');
                words[4] = words[4].Replace('-', ' ').ToUpper().Trim();

                MessageBox.Show( words[4] + Environment.NewLine+ "Opening now!","Found");
                string data= wb2.Document.GetElementById(Findid()).InnerText;
                label1.Visible = true;
                if (data[0] == 'C' || data[1] == 'C')
                {
                    data = data.Substring(3);
                }
                data = Removecrap(data);
                label1.Focus();
                label1.Text = data;
                saveToolStripMenuItem.Enabled = true;
                ProgressBar1.Value = 100;     //   Set 100 %
                wb2.Dispose();
            }

        }

        private string Removecrap(string data)
        {
            int i;
            for(i = 0; i < data.Length; i++)
            {
                if (data[i] == '1')
                {
                    if (data[i + 3] == '2')
                    {
                        if (data[i + 6] == '3')
                        {
                            break;
                        }
                    }

                }
            }
            return data.Substring(0,i);
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            ProgressBar1.Value = 0;      //  Reset
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

        private void VersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ///Should display the basic info, version, credits and all.
            Version vr = new Version();
            vr.ShowDialog();
        }

        private void DevelopersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ///Should go to gitHub
            ///
            System.Diagnostics.Process.Start("https://github.com/rishav394/Skillrack-Visual");

        }

        private void ExitAltToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "C++ file|*.cpp";
            sfd.FileName = words[4];
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(File.Create(sfd.FileName));
                sw.Write(label1.Text);
                sw.Dispose();
            }

        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            Saveit(sender,e);
        }

        private void label1_KeyDown(object sender, KeyEventArgs e)
        {
            Saveit(sender,e);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Saveit(sender,e);
        }
        
        public void Saveit(Object sender,KeyEventArgs e)
        {
            if (!label1.Visible)
            {
                return;
            }
            if (e.Control && e.KeyCode.ToString() == "S")
            {
                SaveToolStripMenuItem_Click(sender, e);
            }
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ///Checking for updates
            ///Open a new Dialogue box and show the changelogs if updates are available
            //if(Version_control.Vno!="tag from github")
            MessageBox.Show("You are already on the lastest version", "Gracias", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}