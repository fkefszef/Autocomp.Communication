namespace Autocomp.Communication
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
            listView1 = new ListView();
            Czas = new ColumnHeader();
            Typ = new ColumnHeader();
            Treść = new ColumnHeader();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { Czas, Typ, Treść });
            listView1.Location = new Point(92, 71);
            listView1.Name = "listView1";
            listView1.Size = new Size(505, 241);
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(listView1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private ListView listView1;
        private ColumnHeader Czas;
        private ColumnHeader Typ;
        private ColumnHeader Treść;
    }
}