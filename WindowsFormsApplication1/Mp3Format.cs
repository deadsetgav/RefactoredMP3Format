using Domain;
using Domain.Facade;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace Mp3Format
{
    public partial class Mp3Format : Form, IFormatterSettings, ILog
    {

        private StreamWriter _log;

        public Mp3Format()
        {
            InitializeComponent();
        }

        private void Mp3Format_Load(object sender, EventArgs e)
        {
            GetAppSettings();
            PrepareLogFile();
            SetVersionLabel();
        }

        private void GetAppSettings()
        {
            var context = Properties.Settings.Default;
            txtSource.Text = context.SourceFolder;
            txtOut.Text = context.OutFolder;

            chkFixTag.Checked = context.FixTags;
            chkMove.Checked = (context.CopyStyle == CopyType.Move.ToString());
            rdoGav.Checked = (context.FormatStyle == FormatStyle.Gav.ToString());
            radioButton2.Checked = !rdoGav.Checked; 
            chkLog.Checked = context.LogToFile;
        }

        private void SetAppSettings()
        {
            Properties.Settings.Default.SourceFolder = txtSource.Text;
            Properties.Settings.Default.OutFolder = txtOut.Text;

            Properties.Settings.Default.FixTags = chkFixTag.Checked;
            Properties.Settings.Default.LogToFile = chkLog.Checked;

            Properties.Settings.Default.FormatStyle = GetFormatStyle().ToString();
            Properties.Settings.Default.CopyStyle = GetCopyType().ToString();
            Properties.Settings.Default.Save();
        }

        private FormatStyle GetFormatStyle()
        {
            if (rdoGav.Checked)
            {
                return FormatStyle.Gav;
            }
            else
            {
                return FormatStyle.Pete; 
            }
        }

        private CopyType GetCopyType()
        {
            if (chkMove.Checked)
            {
                return CopyType.Move;
            }
            else
            {
                return CopyType.Copy;
            }
        }

        public string SourceDirectoryPath
        {
            get { return txtSource.Text; }
        }

        public string OutputDirectoryPath
        {
            get { return txtOut.Text; }
        }

        public bool FixTags
        {
            get { return chkFixTag.Checked; }
        }

        public FormatStyle Format
        {
            get { return GetFormatStyle(); }
        }

        public CopyType CopyOrMove
        {
            get { return GetCopyType(); }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SetAppSettings();
            base.OnFormClosing(e);
            CloseLog();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            this.groupBox1.Enabled = false;
            this.groupBox2.Enabled = false;
           
            listResults.Items.Clear();
            var process = new Mp3FormatterFacade(this);
            process.ProcessLog = this;
            process.Process();

            this.groupBox1.Enabled = true;
            this.groupBox2.Enabled = true;
        }

        public void WriteVerbose(string info)
        {
            WriteToFile(info);
        }

        public void WriteInfo(string info)
        {
            WriteToFile(info);
            listResults.Items.Add(info);
            listResults.Refresh();
        }

        public void WriteError(string info, Exception ex)
        {
            WriteToFile(info);
            listResults.Items.Add(info);
            listResults.Items.Add(ex.Message);
            listResults.Refresh();
        }

        private void SourceBrowse_Click(object sender, EventArgs e)
        {
            txtSource.Text =  browse(txtSource.Text);           
        }

        private void OutBrowse_Click(object sender, EventArgs e)
        {
            txtOut.Text = browse(txtOut.Text);
        }

        private string browse(string startPath)
        {
            var dialog = new FolderBrowserDialog();
            dialog.SelectedPath = startPath;
            dialog.ShowNewFolderButton = true;
            dialog.ShowDialog();
            return dialog.SelectedPath;
        }

        private void PrepareLogFile()
        {
            _log = new StreamWriter(Properties.Settings.Default.LogFileName,
                                    Properties.Settings.Default.AppendLog);
            _log.AutoFlush = true;
            WriteToFile("UI Starting...");

        }
        private void CloseLog()
        {
            _log.Close();
            _log.Dispose();
        }
        private void WriteToFile(string logText)
        {
            if (chkLog.Checked)
            {
                _log.WriteLine(string.Format ("{0} - {1}",DateTime.Now, logText));
            }
        }

        private void SetVersionLabel()
        {
            label3.Text = Mp3FormatterFacade.VERSION; 
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Properties.Settings.Default.LogFileName);
        }

    }
}
