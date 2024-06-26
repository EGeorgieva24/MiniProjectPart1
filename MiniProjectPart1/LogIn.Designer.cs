﻿namespace MiniProjectPart1
{
    partial class LogIn
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
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            label2 = new Label();
            passwordTextBox = new TextBox();
            usernameTextBox = new TextBox();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            button3 = new Button();
            passwordAgainTextBox = new TextBox();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(133, 297);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Login";
            button1.UseVisualStyleBackColor = true;
            button1.Click += LoginButton_Click;
            // 
            // button2
            // 
            button2.Location = new Point(396, 297);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 1;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            button2.Click += cancelButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(133, 127);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 2;
            label1.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(133, 193);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 3;
            label2.Text = "Password";
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(240, 185);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(100, 23);
            passwordTextBox.TabIndex = 4;
            // 
            // usernameTextBox
            // 
            usernameTextBox.Location = new Point(240, 127);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(100, 23);
            usernameTextBox.TabIndex = 5;
            usernameTextBox.TextChanged += usernameTextBox_TextChanged;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(114, 54);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(61, 19);
            radioButton1.TabIndex = 6;
            radioButton1.TabStop = true;
            radioButton1.Text = "Admin";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(331, 54);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(48, 19);
            radioButton2.TabIndex = 7;
            radioButton2.TabStop = true;
            radioButton2.Text = "User";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(260, 298);
            button3.Margin = new Padding(2, 2, 2, 2);
            button3.Name = "button3";
            button3.Size = new Size(78, 20);
            button3.TabIndex = 8;
            button3.Text = "Register";
            button3.UseVisualStyleBackColor = true;
            button3.Click += registerButton_Click;
            // 
            // passwordAgainTextBox
            // 
            passwordAgainTextBox.Location = new Point(240, 245);
            passwordAgainTextBox.Name = "passwordAgainTextBox";
            passwordAgainTextBox.Size = new Size(100, 23);
            passwordAgainTextBox.TabIndex = 10;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(133, 253);
            label3.Name = "label3";
            label3.Size = new Size(0, 15);
            label3.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(106, 248);
            label4.Name = "label4";
            label4.Size = new Size(96, 15);
            label4.TabIndex = 11;
            label4.Text = "Repeat Password";
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(passwordAgainTextBox);
            Controls.Add(label3);
            Controls.Add(button3);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(usernameTextBox);
            Controls.Add(passwordTextBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form3";
            Text = "Form3";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Label label1;
        private Label label2;
        private TextBox passwordTextBox;
        private TextBox usernameTextBox;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private Button button3;
        private TextBox passwordAgainTextBox;
        private Label label3;
        private Label label4;
    }
}