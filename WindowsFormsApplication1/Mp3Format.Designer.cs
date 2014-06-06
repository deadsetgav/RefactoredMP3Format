namespace Mp3Format
{
    partial class Mp3Format
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OutBrowse = new System.Windows.Forms.Button();
            this.SourceBrowse = new System.Windows.Forms.Button();
            this.chkFixTag = new System.Windows.Forms.CheckBox();
            this.chkMove = new System.Windows.Forms.CheckBox();
            this.chkLog = new System.Windows.Forms.CheckBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.rdoGav = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOut = new System.Windows.Forms.TextBox();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveResultsToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listResults = new System.Windows.Forms.ListView();
            this.btnGo = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.OutBrowse);
            this.groupBox1.Controls.Add(this.SourceBrowse);
            this.groupBox1.Controls.Add(this.chkFixTag);
            this.groupBox1.Controls.Add(this.chkMove);
            this.groupBox1.Controls.Add(this.chkLog);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.rdoGav);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtOut);
            this.groupBox1.Controls.Add(this.txtSource);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(613, 97);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Folders";
            // 
            // OutBrowse
            // 
            this.OutBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OutBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutBrowse.Location = new System.Drawing.Point(588, 45);
            this.OutBrowse.Name = "OutBrowse";
            this.OutBrowse.Size = new System.Drawing.Size(19, 20);
            this.OutBrowse.TabIndex = 9;
            this.OutBrowse.Text = "...";
            this.OutBrowse.UseVisualStyleBackColor = true;
            this.OutBrowse.Click += new System.EventHandler(this.OutBrowse_Click);
            // 
            // SourceBrowse
            // 
            this.SourceBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SourceBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SourceBrowse.Location = new System.Drawing.Point(588, 19);
            this.SourceBrowse.Name = "SourceBrowse";
            this.SourceBrowse.Size = new System.Drawing.Size(19, 20);
            this.SourceBrowse.TabIndex = 4;
            this.SourceBrowse.Text = "...";
            this.SourceBrowse.UseVisualStyleBackColor = true;
            this.SourceBrowse.Click += new System.EventHandler(this.SourceBrowse_Click);
            // 
            // chkFixTag
            // 
            this.chkFixTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkFixTag.AutoSize = true;
            this.chkFixTag.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFixTag.Location = new System.Drawing.Point(463, 74);
            this.chkFixTag.Name = "chkFixTag";
            this.chkFixTag.Size = new System.Drawing.Size(66, 17);
            this.chkFixTag.TabIndex = 8;
            this.chkFixTag.Text = "Fix Tags";
            this.chkFixTag.UseVisualStyleBackColor = true;
            // 
            // chkMove
            // 
            this.chkMove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMove.AutoSize = true;
            this.chkMove.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMove.Location = new System.Drawing.Point(362, 74);
            this.chkMove.Name = "chkMove";
            this.chkMove.Size = new System.Drawing.Size(95, 17);
            this.chkMove.TabIndex = 7;
            this.chkMove.Text = "Delete Original";
            this.chkMove.UseVisualStyleBackColor = true;
            // 
            // chkLog
            // 
            this.chkLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLog.AutoSize = true;
            this.chkLog.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkLog.Location = new System.Drawing.Point(535, 74);
            this.chkLog.Name = "chkLog";
            this.chkLog.Size = new System.Drawing.Size(72, 17);
            this.chkLog.TabIndex = 6;
            this.chkLog.Text = "Log to file";
            this.chkLog.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(168, 73);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(73, 17);
            this.radioButton2.TabIndex = 5;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Pete Style";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // rdoGav
            // 
            this.rdoGav.AutoSize = true;
            this.rdoGav.Location = new System.Drawing.Point(81, 73);
            this.rdoGav.Name = "rdoGav";
            this.rdoGav.Size = new System.Drawing.Size(71, 17);
            this.rdoGav.TabIndex = 4;
            this.rdoGav.TabStop = true;
            this.rdoGav.Text = "Gav Style";
            this.rdoGav.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Source";
            // 
            // txtOut
            // 
            this.txtOut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOut.Location = new System.Drawing.Point(81, 45);
            this.txtOut.Name = "txtOut";
            this.txtOut.Size = new System.Drawing.Size(503, 20);
            this.txtOut.TabIndex = 1;
            // 
            // txtSource
            // 
            this.txtSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSource.Location = new System.Drawing.Point(81, 19);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(503, 20);
            this.txtSource.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveResultsToFileToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(169, 48);
            // 
            // saveResultsToFileToolStripMenuItem
            // 
            this.saveResultsToFileToolStripMenuItem.Name = "saveResultsToFileToolStripMenuItem";
            this.saveResultsToFileToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.saveResultsToFileToolStripMenuItem.Text = "Save results to file";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.listResults);
            this.groupBox2.Location = new System.Drawing.Point(12, 115);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(613, 185);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Results";
            // 
            // listResults
            // 
            this.listResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listResults.Location = new System.Drawing.Point(3, 16);
            this.listResults.Name = "listResults";
            this.listResults.Size = new System.Drawing.Size(607, 166);
            this.listResults.TabIndex = 2;
            this.listResults.UseCompatibleStateImageBehavior = false;
            this.listResults.View = System.Windows.Forms.View.List;
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.Location = new System.Drawing.Point(550, 322);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 3;
            this.btnGo.Text = "Convert!";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 339);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "label3";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(43, 339);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(41, 13);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "see log";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Mp3Format
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 357);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Mp3Format";
            this.Text = "Mp3 Format";
            this.Load += new System.EventHandler(this.Mp3Format_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOut;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveResultsToFileToolStripMenuItem;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton rdoGav;
        private System.Windows.Forms.CheckBox chkFixTag;
        private System.Windows.Forms.CheckBox chkMove;
        private System.Windows.Forms.CheckBox chkLog;
        private System.Windows.Forms.ListView listResults;
        private System.Windows.Forms.Button OutBrowse;
        private System.Windows.Forms.Button SourceBrowse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

