using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace First_Form_App
{
    public partial class Version : Form
    {
        public Version()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

        }

        private void Facebook_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.facebook.com/rishav.rungta");
        }

        private void Github_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/rishav394");
        }

        private void Insta_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.instagram.com/rishav_rungta");
        }

        private void Whatsapp_Click(object sender, EventArgs e)
        {
            Process.Start("mailto:rishav394@gmail.com");
        }

        private void Twit_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.twitter.com");
        }

        private void Steam_Click(object sender, EventArgs e)
        {
            Process.Start("https://steamcommunity.com/profiles/76561198453257076");
        }
    }
}
