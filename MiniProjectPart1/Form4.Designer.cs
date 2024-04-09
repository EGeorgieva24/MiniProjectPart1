namespace MiniProjectPart1
{
    partial class Form4
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
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            firstNameTextBox = new TextBox();
            lastnameTextBox = new TextBox();
            label3 = new Label();
            countryTextBox = new TextBox();
            label4 = new Label();
            femaleRadio = new RadioButton();
            maleButton = new RadioButton();
            userIdBox = new TextBox();
            label5 = new Label();
            saveButton = new Button();
            browseButton = new Button();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(90, 87);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(138, 144);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(335, 63);
            label1.Name = "label1";
            label1.Size = new Size(97, 25);
            label1.TabIndex = 1;
            label1.Text = "First Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(335, 131);
            label2.Name = "label2";
            label2.Size = new Size(95, 25);
            label2.TabIndex = 2;
            label2.Text = "Last Name";
            // 
            // firstNameTextBox
            // 
            firstNameTextBox.Location = new Point(448, 57);
            firstNameTextBox.Name = "firstNameTextBox";
            firstNameTextBox.Size = new Size(150, 31);
            firstNameTextBox.TabIndex = 3;
            firstNameTextBox.TextChanged += firstNameTextBox_TextChanged;
            // 
            // lastnameTextBox
            // 
            lastnameTextBox.Location = new Point(448, 125);
            lastnameTextBox.Name = "lastnameTextBox";
            lastnameTextBox.Size = new Size(150, 31);
            lastnameTextBox.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(335, 212);
            label3.Name = "label3";
            label3.Size = new Size(75, 25);
            label3.TabIndex = 5;
            label3.Text = "Country";
            // 
            // countryTextBox
            // 
            countryTextBox.Location = new Point(448, 206);
            countryTextBox.Name = "countryTextBox";
            countryTextBox.Size = new Size(150, 31);
            countryTextBox.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(418, 295);
            label4.Name = "label4";
            label4.Size = new Size(69, 25);
            label4.TabIndex = 7;
            label4.Text = "Gender";
            // 
            // femaleRadio
            // 
            femaleRadio.AutoSize = true;
            femaleRadio.Location = new Point(335, 343);
            femaleRadio.Name = "femaleRadio";
            femaleRadio.Size = new Size(93, 29);
            femaleRadio.TabIndex = 8;
            femaleRadio.TabStop = true;
            femaleRadio.Text = "Female";
            femaleRadio.UseVisualStyleBackColor = true;
            // 
            // maleButton
            // 
            maleButton.AutoSize = true;
            maleButton.Location = new Point(505, 343);
            maleButton.Name = "maleButton";
            maleButton.Size = new Size(75, 29);
            maleButton.TabIndex = 9;
            maleButton.TabStop = true;
            maleButton.Text = "Male";
            maleButton.UseVisualStyleBackColor = true;
            // 
            // userIdBox
            // 
            userIdBox.Location = new Point(661, 145);
            userIdBox.Name = "userIdBox";
            userIdBox.Size = new Size(100, 31);
            userIdBox.TabIndex = 11;
            userIdBox.TextChanged += textBox1_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(651, 97);
            label5.Name = "label5";
            label5.Size = new Size(110, 25);
            label5.TabIndex = 10;
            label5.Text = "Your User ID";
            // 
            // saveButton
            // 
            saveButton.Location = new Point(627, 365);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(112, 34);
            saveButton.TabIndex = 12;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // browseButton
            // 
            browseButton.Location = new Point(116, 276);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(112, 34);
            browseButton.TabIndex = 13;
            browseButton.Text = "Browse";
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += browseButton_Click;
            // 
            // button1
            // 
            button1.Location = new Point(116, 340);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 14;
            button1.Text = "Save Photo";
            button1.UseVisualStyleBackColor = true;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(browseButton);
            Controls.Add(saveButton);
            Controls.Add(userIdBox);
            Controls.Add(label5);
            Controls.Add(maleButton);
            Controls.Add(femaleRadio);
            Controls.Add(label4);
            Controls.Add(countryTextBox);
            Controls.Add(label3);
            Controls.Add(lastnameTextBox);
            Controls.Add(firstNameTextBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Name = "Form4";
            Text = "Form4";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private TextBox firstNameTextBox;
        private TextBox lastnameTextBox;
        private Label label3;
        private TextBox countryTextBox;
        private Label label4;
        private RadioButton femaleRadio;
        private RadioButton maleButton;
        private TextBox userIdBox;
        private Label label5;
        private Button saveButton;
        private Button browseButton;
        private Button button1;
    }
}