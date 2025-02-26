namespace TODO_App
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
            lstTasks = new ListBox();
            btnAdd = new Button();
            btnRemove = new Button();
            lblStatus = new Label();
            txtTask = new TextBox();
            cmbPriority = new ComboBox();
            btnMarkDone = new Button();
            SuspendLayout();
            // 
            // lstTasks
            // 
            lstTasks.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lstTasks.FormattingEnabled = true;
            lstTasks.ItemHeight = 41;
            lstTasks.Location = new Point(22, 31);
            lstTasks.Name = "lstTasks";
            lstTasks.Size = new Size(1291, 209);
            lstTasks.TabIndex = 0;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(43, 366);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(188, 58);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Hinzufügen";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnRemove
            // 
            btnRemove.Location = new Point(261, 366);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(188, 58);
            btnRemove.TabIndex = 2;
            btnRemove.Text = "Entfernen";
            btnRemove.UseVisualStyleBackColor = true;
            btnRemove.Click += btnRemove_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(22, 272);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(97, 41);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "label1";
            // 
            // txtTask
            // 
            txtTask.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtTask.Location = new Point(1335, 31);
            txtTask.Name = "txtTask";
            txtTask.Size = new Size(623, 47);
            txtTask.TabIndex = 4;
            // 
            // cmbPriority
            // 
            cmbPriority.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbPriority.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPriority.FormattingEnabled = true;
            cmbPriority.Location = new Point(1335, 111);
            cmbPriority.Name = "cmbPriority";
            cmbPriority.Size = new Size(623, 49);
            cmbPriority.TabIndex = 5;
            // 
            // btnMarkDone
            // 
            btnMarkDone.Location = new Point(482, 366);
            btnMarkDone.Name = "btnMarkDone";
            btnMarkDone.Size = new Size(188, 58);
            btnMarkDone.TabIndex = 6;
            btnMarkDone.Text = "Erledigt";
            btnMarkDone.UseVisualStyleBackColor = true;
            btnMarkDone.Click += btnMarkDone_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2007, 450);
            Controls.Add(btnMarkDone);
            Controls.Add(cmbPriority);
            Controls.Add(txtTask);
            Controls.Add(lblStatus);
            Controls.Add(btnRemove);
            Controls.Add(btnAdd);
            Controls.Add(lstTasks);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstTasks;
        private Button btnAdd;
        private Button btnRemove;
        private Label lblStatus;
        private TextBox txtTask;
        private ComboBox cmbPriority;
        private Button btnMarkDone;
    }
}
