namespace mainwin
{
    partial class mainwin
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.databaseGridView = new System.Windows.Forms.DataGridView();
            this.connectButton = new System.Windows.Forms.Button();
            this.replicationButton = new System.Windows.Forms.Button();
            this.playbackButton = new System.Windows.Forms.Button();
            this.comboDataBaseBox = new System.Windows.Forms.ComboBox();
            this.comboTableBox = new System.Windows.Forms.ComboBox();
            this.refreshButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.databaseGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // databaseGridView
            // 
            this.databaseGridView.AllowUserToAddRows = false;
            this.databaseGridView.AllowUserToDeleteRows = false;
            this.databaseGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.databaseGridView.Location = new System.Drawing.Point(12, 12);
            this.databaseGridView.Name = "databaseGridView";
            this.databaseGridView.ReadOnly = true;
            this.databaseGridView.Size = new System.Drawing.Size(480, 471);
            this.databaseGridView.TabIndex = 0;
            // 
            // connectButton
            // 
            this.connectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.connectButton.Location = new System.Drawing.Point(512, 12);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(200, 97);
            this.connectButton.TabIndex = 1;
            this.connectButton.Text = "Подключиться к серверу";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectionButton_Click);
            // 
            // replicationButton
            // 
            this.replicationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.replicationButton.Location = new System.Drawing.Point(512, 310);
            this.replicationButton.Name = "replicationButton";
            this.replicationButton.Size = new System.Drawing.Size(200, 97);
            this.replicationButton.TabIndex = 2;
            this.replicationButton.Text = "Создать резервную копию";
            this.replicationButton.UseVisualStyleBackColor = true;
            this.replicationButton.Click += new System.EventHandler(this.replicationButton_Click);
            // 
            // playbackButton
            // 
            this.playbackButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.playbackButton.Location = new System.Drawing.Point(512, 413);
            this.playbackButton.Name = "playbackButton";
            this.playbackButton.Size = new System.Drawing.Size(200, 97);
            this.playbackButton.TabIndex = 3;
            this.playbackButton.Text = "Воспроизвести БД";
            this.playbackButton.UseVisualStyleBackColor = true;
            this.playbackButton.Click += new System.EventHandler(this.playbackButton_Click);
            // 
            // comboDataBaseBox
            // 
            this.comboDataBaseBox.FormattingEnabled = true;
            this.comboDataBaseBox.Location = new System.Drawing.Point(12, 489);
            this.comboDataBaseBox.Name = "comboDataBaseBox";
            this.comboDataBaseBox.Size = new System.Drawing.Size(200, 21);
            this.comboDataBaseBox.TabIndex = 4;
            this.comboDataBaseBox.Text = "База данных";
            this.comboDataBaseBox.SelectedIndexChanged += new System.EventHandler(this.comboDataBaseBox_SelectedIndexChanged);
            // 
            // comboTableBox
            // 
            this.comboTableBox.FormattingEnabled = true;
            this.comboTableBox.Location = new System.Drawing.Point(292, 489);
            this.comboTableBox.Name = "comboTableBox";
            this.comboTableBox.Size = new System.Drawing.Size(200, 21);
            this.comboTableBox.TabIndex = 5;
            this.comboTableBox.Text = "Таблицы БД";
            this.comboTableBox.SelectedIndexChanged += new System.EventHandler(this.comboTableBox_SelectedIndexChanged);
            // 
            // refreshButton
            // 
            this.refreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.refreshButton.Location = new System.Drawing.Point(512, 115);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(200, 97);
            this.refreshButton.TabIndex = 6;
            this.refreshButton.Text = "Обновить сервер";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // mainwin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 522);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.comboTableBox);
            this.Controls.Add(this.comboDataBaseBox);
            this.Controls.Add(this.playbackButton);
            this.Controls.Add(this.replicationButton);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.databaseGridView);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(740, 561);
            this.MinimumSize = new System.Drawing.Size(740, 561);
            this.Name = "mainwin";
            this.Text = "Окно управления";
            ((System.ComponentModel.ISupportInitialize)(this.databaseGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView databaseGridView;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button replicationButton;
        private System.Windows.Forms.Button playbackButton;
        private System.Windows.Forms.ComboBox comboDataBaseBox;
        private System.Windows.Forms.ComboBox comboTableBox;
        private System.Windows.Forms.Button refreshButton;
    }
}

