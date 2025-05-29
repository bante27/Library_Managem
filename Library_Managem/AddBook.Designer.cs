namespace Library_Managem
{
    partial class AddBook
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.addBooks_booktitle = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.addBooks_delbtn = new System.Windows.Forms.Button();
            this.addBooks_bt = new System.Windows.Forms.Button();
            this.addbooks_states = new System.Windows.Forms.ComboBox();
            this.addBooks_published = new System.Windows.Forms.DateTimePicker();
            this.addBooks_pictere = new System.Windows.Forms.Panel();
            this.addBooks_clbtn = new System.Windows.Forms.Button();
            this.addBooks_upbtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.addBooks_authore = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.addBooks_issueid = new System.Windows.Forms.TextBox();
            this.addBooks_title = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridViewAllBooks = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.addBooks_booktitle.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // addBooks_booktitle
            // 
            this.addBooks_booktitle.Controls.Add(this.label5);
            this.addBooks_booktitle.Controls.Add(this.addBooks_delbtn);
            this.addBooks_booktitle.Controls.Add(this.addBooks_bt);
            this.addBooks_booktitle.Controls.Add(this.addbooks_states);
            this.addBooks_booktitle.Controls.Add(this.addBooks_published);
            this.addBooks_booktitle.Controls.Add(this.addBooks_pictere);
            this.addBooks_booktitle.Controls.Add(this.addBooks_clbtn);
            this.addBooks_booktitle.Controls.Add(this.addBooks_upbtn);
            this.addBooks_booktitle.Controls.Add(this.label7);
            this.addBooks_booktitle.Controls.Add(this.addBooks_authore);
            this.addBooks_booktitle.Controls.Add(this.label6);
            this.addBooks_booktitle.Controls.Add(this.label4);
            this.addBooks_booktitle.Controls.Add(this.label3);
            this.addBooks_booktitle.Controls.Add(this.addBooks_issueid);
            this.addBooks_booktitle.Controls.Add(this.addBooks_title);
            this.addBooks_booktitle.Controls.Add(this.label1);
            this.addBooks_booktitle.Location = new System.Drawing.Point(24, 15);
            this.addBooks_booktitle.Name = "addBooks_booktitle";
            this.addBooks_booktitle.Size = new System.Drawing.Size(238, 451);
            this.addBooks_booktitle.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 296);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 20);
            this.label5.TabIndex = 20;
            this.label5.Text = "statues";
            // 
            // addBooks_delbtn
            // 
            this.addBooks_delbtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.addBooks_delbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addBooks_delbtn.Location = new System.Drawing.Point(115, 357);
            this.addBooks_delbtn.Name = "addBooks_delbtn";
            this.addBooks_delbtn.Size = new System.Drawing.Size(75, 32);
            this.addBooks_delbtn.TabIndex = 19;
            this.addBooks_delbtn.Text = "Delete";
            this.addBooks_delbtn.UseVisualStyleBackColor = false;
            this.addBooks_delbtn.Click += new System.EventHandler(this.addBooks_delbtn_Click);
            // 
            // addBooks_bt
            // 
            this.addBooks_bt.BackColor = System.Drawing.SystemColors.Highlight;
            this.addBooks_bt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addBooks_bt.Location = new System.Drawing.Point(3, 374);
            this.addBooks_bt.Name = "addBooks_bt";
            this.addBooks_bt.Size = new System.Drawing.Size(75, 32);
            this.addBooks_bt.TabIndex = 18;
            this.addBooks_bt.Text = "Add";
            this.addBooks_bt.UseVisualStyleBackColor = false;
            this.addBooks_bt.Click += new System.EventHandler(this.addBooks_btn_Click);
            // 
            // addbooks_states
            // 
            this.addbooks_states.FormattingEnabled = true;
            this.addbooks_states.Location = new System.Drawing.Point(102, 298);
            this.addbooks_states.Name = "addbooks_states";
            this.addbooks_states.Size = new System.Drawing.Size(121, 21);
            this.addbooks_states.TabIndex = 17;
            this.addbooks_states.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // addBooks_published
            // 
            this.addBooks_published.Location = new System.Drawing.Point(102, 199);
            this.addBooks_published.Name = "addBooks_published";
            this.addBooks_published.Size = new System.Drawing.Size(121, 20);
            this.addBooks_published.TabIndex = 0;
            // 
            // addBooks_pictere
            // 
            this.addBooks_pictere.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.addBooks_pictere.Location = new System.Drawing.Point(49, 18);
            this.addBooks_pictere.Name = "addBooks_pictere";
            this.addBooks_pictere.Size = new System.Drawing.Size(126, 101);
            this.addBooks_pictere.TabIndex = 16;
            // 
            // addBooks_clbtn
            // 
            this.addBooks_clbtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.addBooks_clbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addBooks_clbtn.Location = new System.Drawing.Point(115, 409);
            this.addBooks_clbtn.Name = "addBooks_clbtn";
            this.addBooks_clbtn.Size = new System.Drawing.Size(75, 28);
            this.addBooks_clbtn.TabIndex = 15;
            this.addBooks_clbtn.Text = "Clear";
            this.addBooks_clbtn.UseVisualStyleBackColor = false;
            this.addBooks_clbtn.Click += new System.EventHandler(this.addBooks_clbtn_Click);
            // 
            // addBooks_upbtn
            // 
            this.addBooks_upbtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.addBooks_upbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addBooks_upbtn.Location = new System.Drawing.Point(14, 409);
            this.addBooks_upbtn.Name = "addBooks_upbtn";
            this.addBooks_upbtn.Size = new System.Drawing.Size(75, 32);
            this.addBooks_upbtn.TabIndex = 14;
            this.addBooks_upbtn.Text = "Update";
            this.addBooks_upbtn.UseVisualStyleBackColor = false;
            this.addBooks_upbtn.Click += new System.EventHandler(this.addBooks_upbtn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(16, 232);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "Authore";
            // 
            // addBooks_authore
            // 
            this.addBooks_authore.Location = new System.Drawing.Point(102, 232);
            this.addBooks_authore.Name = "addBooks_authore";
            this.addBooks_authore.Size = new System.Drawing.Size(121, 20);
            this.addBooks_authore.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 199);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "Published";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 261);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Book title";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 234);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "states";
            // 
            // addBooks_issueid
            // 
            this.addBooks_issueid.Location = new System.Drawing.Point(105, 164);
            this.addBooks_issueid.Name = "addBooks_issueid";
            this.addBooks_issueid.Size = new System.Drawing.Size(118, 20);
            this.addBooks_issueid.TabIndex = 5;
            // 
            // addBooks_title
            // 
            this.addBooks_title.Location = new System.Drawing.Point(102, 261);
            this.addBooks_title.Name = "addBooks_title";
            this.addBooks_title.Size = new System.Drawing.Size(121, 20);
            this.addBooks_title.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Issue ID";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.dataGridViewAllBooks);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(285, 18);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(372, 462);
            this.panel2.TabIndex = 2;
            // 
            // dataGridViewAllBooks
            // 
            this.dataGridViewAllBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAllBooks.Location = new System.Drawing.Point(3, 48);
            this.dataGridViewAllBooks.Name = "dataGridViewAllBooks";
            this.dataGridViewAllBooks.Size = new System.Drawing.Size(366, 430);
            this.dataGridViewAllBooks.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "All Issue Book";
            // 
            // AddBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.addBooks_booktitle);
            this.Name = "AddBook";
            this.Size = new System.Drawing.Size(677, 499);
            this.Load += new System.EventHandler(this.AddBook_Load);
            this.addBooks_booktitle.ResumeLayout(false);
            this.addBooks_booktitle.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllBooks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel addBooks_booktitle;
        private System.Windows.Forms.Panel addBooks_pictere;
        private System.Windows.Forms.Button addBooks_clbtn;
        private System.Windows.Forms.Button addBooks_upbtn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox addBooks_issueid;
        private System.Windows.Forms.TextBox addBooks_title;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker addBooks_published;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox addbooks_states;
        private System.Windows.Forms.Button addBooks_delbtn;
        private System.Windows.Forms.Button addBooks_bt;
        private System.Windows.Forms.TextBox addBooks_authore;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridViewAllBooks;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}
