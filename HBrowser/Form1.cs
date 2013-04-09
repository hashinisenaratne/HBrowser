using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hbrowser
{
    public partial class Form1 : Form
    {
        public static TabControl tc;
        public static ProgressBar pb;
        public static TextBox tb;


        int tabnw=0;
        TabPage nwtab;
        TabPage nxtab;
        string homeaddress="www.google.lk";


        public Form1()
        {
            InitializeComponent();
            tc = tabControl;
            pb = progressBar;
            tb = textBoxAddress;
            new Tab().web.Navigate(homeaddress);
        }


        private void buttonGo_Click(object sender, EventArgs e)
        {
            string rssUrl = textBoxAddress.Text.ToString();
            ((Tab)tabControl.SelectedTab).web.Navigate(rssUrl);
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            ((Tab)tabControl.SelectedTab).web.Refresh();
        }
       

        private void buttonBack_Click(object sender, EventArgs e)
        {
            ((Tab)tabControl.SelectedTab).web.GoBack();
        }

        private void buttonForword_Click(object sender, EventArgs e)
        {
            ((Tab)tabControl.SelectedTab).web.GoForward();
        }

        private void buttonTab_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab=new Tab();
            textBoxAddress.Text = "";
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (tabControl.TabCount == 1)
            {
                Application.Exit();
            }
            else if (tabControl.SelectedIndex != 0)
            {
                tabnw = tabControl.SelectedIndex;
                nwtab = tabControl.SelectedTab;
                nxtab = (TabPage)tabControl.GetControl(--tabnw);
                tabControl.TabPages.Remove(nwtab);
                tabControl.SelectedTab=nxtab;
            }
            else
            {
                tabnw = tabControl.SelectedIndex;
                nwtab = (TabPage)tabControl.GetControl(tabnw);
                tabControl.TabPages.Remove(nwtab);
            }
        }

        private void tabControl_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (((Tab)tabControl.SelectedTab).web.Url != null)
                textBoxAddress.Text = ((Tab)tabControl.SelectedTab).url;
            else
                textBoxAddress.Text = "";
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            ((Tab)tabControl.SelectedTab).web.Stop();
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            ((Tab)tabControl.SelectedTab).web.Navigate(homeaddress);
        }  

    }

    public partial class Tab : TabPage
    {
        public WebBrowser web;
        public string url;

        public Tab()
        {
            Form1.tc.TabPages.Add(this);
            url = "";
            Location = new System.Drawing.Point(4, 22);
            Name = "tabPage";
            Padding = new System.Windows.Forms.Padding(3);
            Size = new System.Drawing.Size(759, 464);
            TabIndex = 0;
            Text = "";
            UseVisualStyleBackColor = true;

            web = new WebBrowser();
            web.Location = new System.Drawing.Point(-4, -1);
            web.MinimumSize = new System.Drawing.Size(20, 20);
            web.Name = "browserWindow";
            web.Size = new System.Drawing.Size(767, 469);
            web.TabIndex = 1;
            web.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(browserWindow_Navigating);
            web.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(browserWindow_Navigated);
            web.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(browserWindow_DocumentCompleted);
            Controls.Add(web);
            web.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        private void browserWindow_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            this.Text = "connecting...";
            Form1.pb.Visible = true;
            Form1.pb.Value = 0;
            Form1.pb.PerformStep();

        }

        private void browserWindow_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            Form1.pb.Step = 90;
            Form1.pb.Visible = false;
            url = web.Url.ToString();
            this.Text = web.Document.Title;
        }
        private void browserWindow_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (((Tab)Form1.tc.SelectedTab) == this)
                Form1.tb.Text = url;         
        }
    }
}
