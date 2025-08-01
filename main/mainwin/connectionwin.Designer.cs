namespace mainwin
{
    partial class connectionwin
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
            this.labelConnection = new System.Windows.Forms.Label();
            this.loginBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.connectionButton = new System.Windows.Forms.Button();
            this.loginLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.portBox = new System.Windows.Forms.TextBox();
            this.hostBox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.hostLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelConnection
            // 
            this.labelConnection.AutoSize = true;
            this.labelConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelConnection.Location = new System.Drawing.Point(88, 9);
            this.labelConnection.Name = "labelConnection";
            this.labelConnection.Size = new System.Drawing.Size(188, 31);
            this.labelConnection.TabIndex = 0;
            this.labelConnection.Text = "Подключение";
            // 
            // loginBox
            // 
            this.loginBox.AccessibleDescription = "";
            this.loginBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.loginBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.loginBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.loginBox.Location = new System.Drawing.Point(141, 122);
            this.loginBox.Name = "loginBox";
            this.loginBox.Size = new System.Drawing.Size(195, 19);
            this.loginBox.TabIndex = 1;
            this.loginBox.Text = "Введите логин";
            this.loginBox.Enter += new System.EventHandler(this.loginBox_Enter);
            this.loginBox.Leave += new System.EventHandler(this.loginBox_Leave);
            // 
            // passwordBox
            // 
            this.passwordBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.passwordBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.passwordBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.passwordBox.Location = new System.Drawing.Point(141, 147);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(195, 19);
            this.passwordBox.TabIndex = 2;
            this.passwordBox.Text = "Введите пароль";
            this.passwordBox.Enter += new System.EventHandler(this.passwordBox_Enter);
            this.passwordBox.Leave += new System.EventHandler(this.passwordBox_Leave);
            // 
            // connectionButton
            // 
            this.connectionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connectionButton.Location = new System.Drawing.Point(33, 172);
            this.connectionButton.Name = "connectionButton";
            this.connectionButton.Size = new System.Drawing.Size(303, 74);
            this.connectionButton.TabIndex = 3;
            this.connectionButton.Text = "Подключиться";
            this.connectionButton.UseVisualStyleBackColor = true;
            this.connectionButton.Click += new System.EventHandler(this.connectionButton_Click);
            // 
            // loginLabel
            // 
            this.loginLabel.AutoSize = true;
            this.loginLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.0012F, System.Drawing.FontStyle.Bold);
            this.loginLabel.Location = new System.Drawing.Point(17, 118);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(118, 25);
            this.loginLabel.TabIndex = 4;
            this.loginLabel.Text = " Логин    :";
            this.loginLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.passwordLabel.Location = new System.Drawing.Point(28, 141);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(106, 25);
            this.passwordLabel.TabIndex = 5;
            this.passwordLabel.Text = "Пароль :";
            this.passwordLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // portBox
            // 
            this.portBox.AccessibleDescription = "";
            this.portBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.portBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.portBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.portBox.Location = new System.Drawing.Point(141, 97);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(195, 19);
            this.portBox.TabIndex = 1;
            this.portBox.Text = "Введите порт";
            this.portBox.Enter += new System.EventHandler(this.portBox_Enter);
            this.portBox.Leave += new System.EventHandler(this.portBox_Leave);
            // 
            // hostBox
            // 
            this.hostBox.AccessibleDescription = "";
            this.hostBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.hostBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.hostBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.hostBox.Location = new System.Drawing.Point(141, 70);
            this.hostBox.Name = "hostBox";
            this.hostBox.Size = new System.Drawing.Size(195, 19);
            this.hostBox.TabIndex = 1;
            this.hostBox.Text = "Введите название хоста";
            this.hostBox.Enter += new System.EventHandler(this.hostBox_Enter);
            this.hostBox.Leave += new System.EventHandler(this.hostBox_Leave);
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.portLabel.Location = new System.Drawing.Point(28, 93);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(107, 25);
            this.portLabel.TabIndex = 4;
            this.portLabel.Text = "Порт     :";
            this.portLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // hostLabel
            // 
            this.hostLabel.AutoSize = true;
            this.hostLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hostLabel.Location = new System.Drawing.Point(29, 66);
            this.hostLabel.Name = "hostLabel";
            this.hostLabel.Size = new System.Drawing.Size(105, 25);
            this.hostLabel.TabIndex = 4;
            this.hostLabel.Text = "Хост     :";
            this.hostLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // connectionwin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 259);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.hostLabel);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.loginLabel);
            this.Controls.Add(this.connectionButton);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.hostBox);
            this.Controls.Add(this.portBox);
            this.Controls.Add(this.loginBox);
            this.Controls.Add(this.labelConnection);
            this.MaximizeBox = false;
            this.Name = "connectionwin";
            this.Text = "Подключение";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelConnection;
        private System.Windows.Forms.TextBox loginBox;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Button connectionButton;
        private System.Windows.Forms.Label loginLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox portBox;
        private System.Windows.Forms.TextBox hostBox;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Label hostLabel;
    }
}