﻿namespace Autocomp.Communication
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            listView1 = new ListView();
            Czas = new ColumnHeader();
            Typ = new ColumnHeader();
            Treść = new ColumnHeader();
            toolStrip1 = new ToolStrip();
            toolStripLabel1 = new ToolStripLabel();
            toolStripTextBox1 = new ToolStripTextBox();
            toolStripLabel2 = new ToolStripLabel();
            toolStripTextBox2 = new ToolStripTextBox();
            SaveButton = new ToolStripButton();
            toolStripButton1 = new ToolStripButton();
            toolStripButton2 = new ToolStripButton();
            playtoolStripButton = new ToolStripButton();
            pausetoolStripButton = new ToolStripButton();
            resettoolStripButton = new ToolStripButton();
            stoptoolStripButton = new ToolStripButton();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            xToolStripMenuItem = new ToolStripMenuItem();
            xToolStripMenuItem1 = new ToolStripMenuItem();
            xToolStripMenuItem2 = new ToolStripMenuItem();
            xToolStripMenuItem3 = new ToolStripMenuItem();
            textBox1 = new TextBox();
            process1 = new System.Diagnostics.Process();
            toolStripProgressBar1 = new ToolStripProgressBar();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { Czas, Typ, Treść });
            listView1.Location = new Point(2, 43);
            listView1.Margin = new Padding(3, 2, 3, 2);
            listView1.Name = "listView1";
            listView1.Size = new Size(416, 296);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // Czas
            // 
            Czas.Tag = "Czas";
            Czas.Text = "Czas";
            Czas.Width = 100;
            // 
            // Typ
            // 
            Typ.Tag = "Typ";
            Typ.Text = "Typ";
            Typ.Width = 100;
            // 
            // Treść
            // 
            Treść.Tag = "Treść";
            Treść.Text = "Treść";
            Treść.Width = 100;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripLabel1, toolStripTextBox1, toolStripLabel2, toolStripTextBox2, SaveButton, toolStripButton1, toolStripButton2, playtoolStripButton, pausetoolStripButton, resettoolStripButton, stoptoolStripButton, toolStripDropDownButton1, toolStripProgressBar1 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(700, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            toolStrip1.ItemClicked += toolStrip1_ItemClicked;
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(48, 22);
            toolStripLabel1.Text = "Zawiera";
            // 
            // toolStripTextBox1
            // 
            toolStripTextBox1.Name = "toolStripTextBox1";
            toolStripTextBox1.Size = new Size(100, 25);
            toolStripTextBox1.Click += toolStripTextBox1_Click;
            toolStripTextBox1.TextChanged += toolStripTextBox1_TextChanged;
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(67, 22);
            toolStripLabel2.Text = "Nie zawiera";
            // 
            // toolStripTextBox2
            // 
            toolStripTextBox2.Name = "toolStripTextBox2";
            toolStripTextBox2.Size = new Size(100, 25);
            toolStripTextBox2.TextChanged += toolStripTextBox2_TextChanged;
            // 
            // SaveButton
            // 
            SaveButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            SaveButton.Image = (Image)resources.GetObject("SaveButton.Image");
            SaveButton.ImageTransparentColor = Color.Magenta;
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(23, 22);
            SaveButton.Text = "toolStripButton1";
            SaveButton.Click += SaveButton_Click;
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(23, 22);
            toolStripButton1.Text = "toolStripButton1";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // toolStripButton2
            // 
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton2.Image = (Image)resources.GetObject("toolStripButton2.Image");
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(23, 22);
            toolStripButton2.Text = "toolStripButton2";
            toolStripButton2.Click += toolStripButton2_Click;
            // 
            // playtoolStripButton
            // 
            playtoolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            playtoolStripButton.Image = (Image)resources.GetObject("playtoolStripButton.Image");
            playtoolStripButton.ImageTransparentColor = Color.Magenta;
            playtoolStripButton.Name = "playtoolStripButton";
            playtoolStripButton.Size = new Size(23, 22);
            playtoolStripButton.Text = "Odtwarzaj";
            playtoolStripButton.Click += playtoolStripButton_Click;
            // 
            // pausetoolStripButton
            // 
            pausetoolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            pausetoolStripButton.Image = (Image)resources.GetObject("pausetoolStripButton.Image");
            pausetoolStripButton.ImageTransparentColor = Color.Magenta;
            pausetoolStripButton.Name = "pausetoolStripButton";
            pausetoolStripButton.Size = new Size(23, 22);
            pausetoolStripButton.Text = "Zapazuj";
            pausetoolStripButton.Click += pausetoolStripButton_Click;
            // 
            // resettoolStripButton
            // 
            resettoolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            resettoolStripButton.Image = (Image)resources.GetObject("resettoolStripButton.Image");
            resettoolStripButton.ImageTransparentColor = Color.Magenta;
            resettoolStripButton.Name = "resettoolStripButton";
            resettoolStripButton.Size = new Size(23, 22);
            resettoolStripButton.Text = "Zresetuj";
            resettoolStripButton.Click += resettoolStripButton_Click;
            // 
            // stoptoolStripButton
            // 
            stoptoolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            stoptoolStripButton.Image = (Image)resources.GetObject("stoptoolStripButton.Image");
            stoptoolStripButton.ImageTransparentColor = Color.Magenta;
            stoptoolStripButton.Name = "stoptoolStripButton";
            stoptoolStripButton.Size = new Size(23, 22);
            stoptoolStripButton.Text = "Zastopuj";
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { xToolStripMenuItem, xToolStripMenuItem1, xToolStripMenuItem2, xToolStripMenuItem3 });
            toolStripDropDownButton1.Image = (Image)resources.GetObject("toolStripDropDownButton1.Image");
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(29, 22);
            toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // xToolStripMenuItem
            // 
            xToolStripMenuItem.Name = "xToolStripMenuItem";
            xToolStripMenuItem.Size = new Size(180, 22);
            xToolStripMenuItem.Text = "0.25x";
            // 
            // xToolStripMenuItem1
            // 
            xToolStripMenuItem1.Name = "xToolStripMenuItem1";
            xToolStripMenuItem1.Size = new Size(180, 22);
            xToolStripMenuItem1.Text = "0.5x";
            // 
            // xToolStripMenuItem2
            // 
            xToolStripMenuItem2.Name = "xToolStripMenuItem2";
            xToolStripMenuItem2.Size = new Size(180, 22);
            xToolStripMenuItem2.Text = "1x";
            // 
            // xToolStripMenuItem3
            // 
            xToolStripMenuItem3.Name = "xToolStripMenuItem3";
            xToolStripMenuItem3.Size = new Size(180, 22);
            xToolStripMenuItem3.Text = "2x";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(424, 43);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(276, 296);
            textBox1.TabIndex = 2;
            // 
            // process1
            // 
            process1.StartInfo.Domain = "";
            process1.StartInfo.LoadUserProfile = false;
            process1.StartInfo.Password = null;
            process1.StartInfo.StandardErrorEncoding = null;
            process1.StartInfo.StandardInputEncoding = null;
            process1.StartInfo.StandardOutputEncoding = null;
            process1.StartInfo.UserName = "";
            process1.SynchronizingObject = this;
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new Size(100, 22);
            toolStripProgressBar1.Click += toolStripProgressBar1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(textBox1);
            Controls.Add(toolStrip1);
            Controls.Add(listView1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView listView1;
        private ColumnHeader Czas;
        private ColumnHeader Typ;
        private ColumnHeader Treść;
        private ToolStrip toolStrip1;
        private ToolStripLabel toolStripLabel1;
        private ToolStripTextBox toolStripTextBox1;
        private ToolStripLabel toolStripLabel2;
        private ToolStripTextBox toolStripTextBox2;
        private TextBox textBox1;
        private ToolStripButton SaveButton;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripButton playtoolStripButton;
        private ToolStripButton pausetoolStripButton;
        private ToolStripButton resettoolStripButton;
        private ToolStripButton stoptoolStripButton;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem xToolStripMenuItem;
        private ToolStripMenuItem xToolStripMenuItem1;
        private ToolStripMenuItem xToolStripMenuItem2;
        private ToolStripMenuItem xToolStripMenuItem3;
        private System.Diagnostics.Process process1;
        private ToolStripProgressBar toolStripProgressBar1;
    }
}