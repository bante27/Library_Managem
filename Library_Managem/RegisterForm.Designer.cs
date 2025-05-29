namespace Library_Managem
{
    partial class RegisterForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.Register_Showpassword = new System.Windows.Forms.CheckBox();
            this.Register_Signin = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.Register_Button = new System.Windows.Forms.Button();
            this.Register_Password = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Register_Username = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Register_Email = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 64);
            this.panel1.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(701, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            // 
            // Register_Showpassword
            // 
            this.Register_Showpassword.AutoSize = true;
            this.Register_Showpassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Register_Showpassword.Location = new System.Drawing.Point(454, 323);
            this.Register_Showpassword.Name = "Register_Showpassword";
            this.Register_Showpassword.Size = new System.Drawing.Size(140, 24);
            this.Register_Showpassword.TabIndex = 21;
            this.Register_Showpassword.Text = "Show password";
            this.Register_Showpassword.UseVisualStyleBackColor = true;
            this.Register_Showpassword.CheckedChanged += new System.EventHandler(this.Register_Showpassword_CheckedChanged);
            // 
            // Register_Signin
            // 
            this.Register_Signin.BackColor = System.Drawing.SystemColors.Highlight;
            this.Register_Signin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Register_Signin.FlatAppearance.BorderSize = 0;
            this.Register_Signin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Cyan;
            this.Register_Signin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Register_Signin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Register_Signin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Register_Signin.Location = new System.Drawing.Point(67, 424);
            this.Register_Signin.Name = "Register_Signin";
            this.Register_Signin.Size = new System.Drawing.Size(458, 28);
            this.Register_Signin.TabIndex = 20;
            this.Register_Signin.Text = "SIGN IN";
            this.Register_Signin.UseVisualStyleBackColor = false;
            this.Register_Signin.Click += new System.EventHandler(this.SignupBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(88, 401);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 20);
            this.label5.TabIndex = 19;
            this.label5.Text = "RegistarAccout";
            // 
            // Register_Button
            // 
            this.Register_Button.BackColor = System.Drawing.SystemColors.Highlight;
            this.Register_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Register_Button.FlatAppearance.BorderSize = 0;
            this.Register_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Cyan;
            this.Register_Button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Register_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Register_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Register_Button.Location = new System.Drawing.Point(79, 355);
            this.Register_Button.Name = "Register_Button";
            this.Register_Button.Size = new System.Drawing.Size(446, 28);
            this.Register_Button.TabIndex = 18;
            this.Register_Button.Text = "Register";
            this.Register_Button.UseVisualStyleBackColor = false;
            this.Register_Button.Click += new System.EventHandler(this.Register_Button_Click);
            // 
            // Register_Password
            // 
            this.Register_Password.Location = new System.Drawing.Point(79, 297);
            this.Register_Password.Name = "Register_Password";
            this.Register_Password.PasswordChar = '*';
            this.Register_Password.Size = new System.Drawing.Size(446, 20);
            this.Register_Password.TabIndex = 17;
            this.Register_Password.TextChanged += new System.EventHandler(this.Register_Password_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(75, 274);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 20);
            this.label4.TabIndex = 16;
            this.label4.Text = "Password";
            // 
            // Register_Username
            // 
            this.Register_Username.Location = new System.Drawing.Point(79, 251);
            this.Register_Username.Name = "Register_Username";
            this.Register_Username.Size = new System.Drawing.Size(446, 20);
            this.Register_Username.TabIndex = 15;
            this.Register_Username.TextChanged += new System.EventHandler(this.Register_username_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(75, 228);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(244, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Register Form";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Library_Managem.Properties.Resources.download;
            this.pictureBox1.Location = new System.Drawing.Point(216, 69);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(198, 85);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(75, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 20);
            this.label6.TabIndex = 22;
            this.label6.Text = "EmailAccount";
            // 
            // Register_Email
            // 
            this.Register_Email.Location = new System.Drawing.Point(79, 205);
            this.Register_Email.Name = "Register_Email";
            this.Register_Email.Size = new System.Drawing.Size(446, 20);
            this.Register_Email.TabIndex = 23;
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 506);
            this.Controls.Add(this.Register_Email);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Register_Showpassword);
            this.Controls.Add(this.Register_Signin);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Register_Button);
            this.Controls.Add(this.Register_Password);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Register_Username);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RegisterForm";
            this.Text = "RegisterForm";
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox Register_Showpassword;
        private System.Windows.Forms.Button Register_Signin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Register_Button;
        private System.Windows.Forms.TextBox Register_Password;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Register_Username;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Register_Email;
    }
}