namespace MiniProjectPart1
{
    partial class Form2
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
            adminButton = new Button();
            comboBoxTowns = new ComboBox();
            comboBoxCountries = new ComboBox();
            button1 = new Button();
            userIdBox = new TextBox();
            label5 = new Label();
            SuspendLayout();
            // 
            // adminButton
            // 
            adminButton.Location = new Point(140, 160);
            adminButton.Name = "adminButton";
            adminButton.Size = new Size(121, 23);
            adminButton.TabIndex = 0;
            adminButton.Text = "Make Changes";
            adminButton.UseVisualStyleBackColor = true;
            adminButton.Click += adminButton_Click;
            // 
            // comboBoxTowns
            // 
            comboBoxTowns.FormattingEnabled = true;
            comboBoxTowns.Location = new Point(291, 96);
            comboBoxTowns.Name = "comboBoxTowns";
            comboBoxTowns.Size = new Size(121, 23);
            comboBoxTowns.TabIndex = 1;
            // 
            // comboBoxCountries
            // 
            comboBoxCountries.FormattingEnabled = true;
            comboBoxCountries.Location = new Point(87, 96);
            comboBoxCountries.Name = "comboBoxCountries";
            comboBoxCountries.Size = new Size(121, 23);
            comboBoxCountries.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(302, 160);
            button1.Name = "button1";
            button1.Size = new Size(121, 23);
            button1.TabIndex = 3;
            button1.Text = "Edit Profile";
            button1.UseVisualStyleBackColor = true;
            button1.Click += editProfile_CLick;
            // 
            // userIdBox
            // 
            userIdBox.Location = new Point(267, 36);
            userIdBox.Margin = new Padding(2);
            userIdBox.Name = "userIdBox";
            userIdBox.Size = new Size(71, 23);
            userIdBox.TabIndex = 13;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(133, 39);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(71, 15);
            label5.TabIndex = 12;
            label5.Text = "Your User ID";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(userIdBox);
            Controls.Add(label5);
            Controls.Add(button1);
            Controls.Add(comboBoxCountries);
            Controls.Add(comboBoxTowns);
            Controls.Add(adminButton);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button adminButton;
        private ComboBox comboBoxTowns;
        private ComboBox comboBoxCountries;
        private Button button1;
        private TextBox userIdBox;
        private Label label5;
    }
}