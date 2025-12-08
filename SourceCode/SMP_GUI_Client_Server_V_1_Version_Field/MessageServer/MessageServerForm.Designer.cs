using System.Media;

namespace SMPServer
{
    partial class FormSmpServer
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
            this.buttonStartServer = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioAll = new System.Windows.Forms.RadioButton();
            this.radioButtonPriorityHigh = new System.Windows.Forms.RadioButton();
            this.radioButtonPriorityMedium = new System.Windows.Forms.RadioButton();
            this.radioButtonPriorityLow = new System.Windows.Forms.RadioButton();
            this.textBoxMessages = new System.Windows.Forms.TextBox();
            this.buttonShowMessages = new System.Windows.Forms.Button();
            this.buttonShowUsers = new System.Windows.Forms.Button();
            this.textBoxMessageType = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPortNumber = new System.Windows.Forms.TextBox();
            this.textBoxServerIPAddress = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMessagePriority = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelLastUserID = new System.Windows.Forms.Label();
            this.textBoxLastUserID = new System.Windows.Forms.TextBox();
            this.labelLastPassword = new System.Windows.Forms.Label();
            this.textBoxLastPassword = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStartServer
            // 
            this.buttonStartServer.Location = new System.Drawing.Point(20, 10);
            this.buttonStartServer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonStartServer.Name = "buttonStartServer";
            this.buttonStartServer.Size = new System.Drawing.Size(131, 39);
            this.buttonStartServer.TabIndex = 3;
            this.buttonStartServer.Text = "Start Server";
            this.buttonStartServer.UseVisualStyleBackColor = true;
            this.buttonStartServer.Click += new System.EventHandler(this.buttonStartServer_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.textBoxMessages);
            this.groupBox1.Controls.Add(this.buttonShowMessages);
            this.groupBox1.Location = new System.Drawing.Point(11, 229);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(510, 273);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Messages";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioAll);
            this.groupBox3.Controls.Add(this.radioButtonPriorityHigh);
            this.groupBox3.Controls.Add(this.radioButtonPriorityMedium);
            this.groupBox3.Controls.Add(this.radioButtonPriorityLow);
            this.groupBox3.Location = new System.Drawing.Point(7, 23);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(124, 190);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Priority";
            // 
            // radioAll
            // 
            this.radioAll.AutoSize = true;
            this.radioAll.Checked = true;
            this.radioAll.Location = new System.Drawing.Point(9, 152);
            this.radioAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioAll.Name = "radioAll";
            this.radioAll.Size = new System.Drawing.Size(43, 20);
            this.radioAll.TabIndex = 3;
            this.radioAll.TabStop = true;
            this.radioAll.Text = "All";
            this.radioAll.UseVisualStyleBackColor = true;
            // 
            // radioButtonPriorityHigh
            // 
            this.radioButtonPriorityHigh.AutoSize = true;
            this.radioButtonPriorityHigh.Location = new System.Drawing.Point(9, 111);
            this.radioButtonPriorityHigh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButtonPriorityHigh.Name = "radioButtonPriorityHigh";
            this.radioButtonPriorityHigh.Size = new System.Drawing.Size(56, 20);
            this.radioButtonPriorityHigh.TabIndex = 2;
            this.radioButtonPriorityHigh.Text = "High";
            this.radioButtonPriorityHigh.UseVisualStyleBackColor = true;
            // 
            // radioButtonPriorityMedium
            // 
            this.radioButtonPriorityMedium.AutoSize = true;
            this.radioButtonPriorityMedium.Location = new System.Drawing.Point(9, 70);
            this.radioButtonPriorityMedium.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButtonPriorityMedium.Name = "radioButtonPriorityMedium";
            this.radioButtonPriorityMedium.Size = new System.Drawing.Size(76, 20);
            this.radioButtonPriorityMedium.TabIndex = 1;
            this.radioButtonPriorityMedium.Text = "Medium";
            this.radioButtonPriorityMedium.UseVisualStyleBackColor = true;
            // 
            // radioButtonPriorityLow
            // 
            this.radioButtonPriorityLow.AutoSize = true;
            this.radioButtonPriorityLow.Location = new System.Drawing.Point(9, 30);
            this.radioButtonPriorityLow.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButtonPriorityLow.Name = "radioButtonPriorityLow";
            this.radioButtonPriorityLow.Size = new System.Drawing.Size(52, 20);
            this.radioButtonPriorityLow.TabIndex = 0;
            this.radioButtonPriorityLow.Text = "Low";
            this.radioButtonPriorityLow.UseVisualStyleBackColor = true;
            // 
            // textBoxMessages
            // 
            this.textBoxMessages.Location = new System.Drawing.Point(157, 36);
            this.textBoxMessages.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxMessages.Multiline = true;
            this.textBoxMessages.Name = "textBoxMessages";
            this.textBoxMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxMessages.Size = new System.Drawing.Size(333, 168);
            this.textBoxMessages.TabIndex = 6;
            // 
            // buttonShowMessages
            // 
            this.buttonShowMessages.Location = new System.Drawing.Point(218, 220);
            this.buttonShowMessages.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonShowMessages.Name = "buttonShowMessages";
            this.buttonShowMessages.Size = new System.Drawing.Size(182, 38);
            this.buttonShowMessages.TabIndex = 5;
            this.buttonShowMessages.Text = "Show Messages";
            this.buttonShowMessages.UseVisualStyleBackColor = true;
            this.buttonShowMessages.Click += new System.EventHandler(this.buttonShowMessages_Click);
        // SMP 3.0 Implementation
            // 
            // buttonShowUsers
            // 
            this.buttonShowUsers.Location = new System.Drawing.Point(20, 57);
            this.buttonShowUsers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonShowUsers.Name = "buttonShowUsers";
            this.buttonShowUsers.Size = new System.Drawing.Size(131, 38);
            this.buttonShowUsers.TabIndex = 5;
            this.buttonShowUsers.Text = "Registrations";
            this.buttonShowUsers.UseVisualStyleBackColor = true;
            this.buttonShowUsers.Click += new System.EventHandler(this.buttonShowUsers_Click);
            // 
            // textBoxRegistrations
            // 
            this.textBoxRegistrations.Location = new System.Drawing.Point(12, 12);
            this.textBoxRegistrations.Multiline = true;
            this.textBoxRegistrations.Name = "textBoxMessages";
            this.textBoxRegistrations.Size = new System.Drawing.Size(320, 250);
            this.textBoxRegistrations.TabIndex = 6;
            // 
            // radioButtonUserIDs
            // 
            this.radioButtonUserIDs.AutoSize = true;
            this.radioButtonUserIDs.Checked = true;
            this.radioButtonUserIDs.Location = new System.Drawing.Point(10, 285);
            this.radioButtonUserIDs.Name = "radioButtonUserIDs";
            this.radioButtonUserIDs.Size = new System.Drawing.Size(63, 24);
            this.radioButtonUserIDs.TabIndex = 0;
            this.radioButtonUserIDs.Text = "User IDs";
            this.radioButtonUserIDs.UseVisualStyleBackColor = true;
            // 
            // radioButtonUserIDsandPasswords
            // 
            this.radioButtonUserIDsandPasswords.AutoSize = true;
            this.radioButtonUserIDsandPasswords.Location = new System.Drawing.Point(122, 285);
            this.radioButtonUserIDsandPasswords.Name = "radioButtonUserIDsandPasswords";
            this.radioButtonUserIDsandPasswords.Size = new System.Drawing.Size(63, 24);
            this.radioButtonUserIDsandPasswords.TabIndex = 0;
            this.radioButtonUserIDsandPasswords.Text = "User IDs and Passwords";
            this.radioButtonUserIDsandPasswords.UseVisualStyleBackColor = true;
            // 
            // buttonShowRegistrations
            // 
            this.buttonShowRegistrations.Location = new System.Drawing.Point(12, 330);
            this.buttonShowRegistrations.Name = "buttonShowRegistrations";
            this.buttonShowRegistrations.Size = new System.Drawing.Size(320, 42);
            this.buttonShowRegistrations.TabIndex = 3;
            this.buttonShowRegistrations.Text = "Show Registrations";
            this.buttonShowRegistrations.UseVisualStyleBackColor = true;
            this.buttonShowRegistrations.Click += new System.EventHandler(this.buttonShowRegistrations_Click);
        // End of SMP 3.0 Implementation
            // 
            // textBoxMessageType
            // 
            this.textBoxMessageType.Location = new System.Drawing.Point(126, 63);
            this.textBoxMessageType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxMessageType.Multiline = true;
            this.textBoxMessageType.Name = "textBoxMessageType";
            this.textBoxMessageType.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxMessageType.Size = new System.Drawing.Size(112, 25);
            this.textBoxMessageType.TabIndex = 9;
            this.textBoxMessageType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBoxPortNumber);
            this.groupBox2.Controls.Add(this.textBoxServerIPAddress);
            this.groupBox2.Location = new System.Drawing.Point(168, 10);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(353, 93);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port Number";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server IP Address";
            // 
            // textBoxPortNumber
            // 
            this.textBoxPortNumber.Location = new System.Drawing.Point(142, 63);
            this.textBoxPortNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxPortNumber.Name = "textBoxPortNumber";
            this.textBoxPortNumber.Size = new System.Drawing.Size(191, 22);
            this.textBoxPortNumber.TabIndex = 1;
            this.textBoxPortNumber.Text = "50400";
            this.textBoxPortNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxServerIPAddress
            // 
            this.textBoxServerIPAddress.Location = new System.Drawing.Point(142, 30);
            this.textBoxServerIPAddress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxServerIPAddress.Name = "textBoxServerIPAddress";
            this.textBoxServerIPAddress.Size = new System.Drawing.Size(191, 22);
            this.textBoxServerIPAddress.TabIndex = 0;
            this.textBoxServerIPAddress.Text = "0.0.0.0";
            this.textBoxServerIPAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.textBoxMessagePriority);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.textBoxMessageType);
            this.groupBox4.Controls.Add(this.labelLastUserID);
            this.groupBox4.Controls.Add(this.textBoxLastUserID);
            this.groupBox4.Controls.Add(this.labelLastPassword);
            this.groupBox4.Controls.Add(this.textBoxLastPassword);
            this.groupBox4.Location = new System.Drawing.Point(11, 115);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(510, 101);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Last Received Message";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(244, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Message Priority";
            // 
            // textBoxMessagePriority
            // 
            this.textBoxMessagePriority.Location = new System.Drawing.Point(372, 63);
            this.textBoxMessagePriority.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxMessagePriority.Multiline = true;
            this.textBoxMessagePriority.Name = "textBoxMessagePriority";
            this.textBoxMessagePriority.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxMessagePriority.Size = new System.Drawing.Size(118, 25);
            this.textBoxMessagePriority.TabIndex = 11;
            this.textBoxMessagePriority.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Message Type";
            // 
            // labelLastUserID
            // 
            this.labelLastUserID.AutoSize = true;
            this.labelLastUserID.Location = new System.Drawing.Point(12, 34);
            this.labelLastUserID.Name = "labelLastUserID";
            this.labelLastUserID.Size = new System.Drawing.Size(52, 16);
            this.labelLastUserID.TabIndex = 20;
            this.labelLastUserID.Text = "User ID";
            // 
            // textBoxLastUserID
            // 
            this.textBoxLastUserID.Location = new System.Drawing.Point(126, 31);
            this.textBoxLastUserID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxLastUserID.Name = "textBoxLastUserID";
            this.textBoxLastUserID.Size = new System.Drawing.Size(112, 22);
            this.textBoxLastUserID.TabIndex = 21;
            // 
            // labelLastPassword
            // 
            this.labelLastPassword.AutoSize = true;
            this.labelLastPassword.Location = new System.Drawing.Point(244, 34);
            this.labelLastPassword.Name = "labelLastPassword";
            this.labelLastPassword.Size = new System.Drawing.Size(67, 16);
            this.labelLastPassword.TabIndex = 22;
            this.labelLastPassword.Text = "Password";
            // 
            // textBoxLastPassword
            // 
            this.textBoxLastPassword.Location = new System.Drawing.Point(372, 31);
            this.textBoxLastPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxLastPassword.Name = "textBoxLastPassword";
            this.textBoxLastPassword.Size = new System.Drawing.Size(118, 22);
            this.textBoxLastPassword.TabIndex = 23;
            // 
            // FormSmpServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
<<<<<<< HEAD
            this.ClientSize = new System.Drawing.Size(610, 666);
            this.Location = new System.Drawing.Point(15, 15);
            // start of 2.0 controls implementation
            this.groupBox4.Controls.Add(this.labelLastUserID);
            this.groupBox4.Controls.Add(this.textBoxLastUserID);
            this.groupBox4.Controls.Add(this.labelLastPassword);
            this.groupBox4.Controls.Add(this.textBoxLastPassword);
            // end of 2.0 controls implementation
=======
            this.ClientSize = new System.Drawing.Size(542, 533);
>>>>>>> 108dab5673aafae846e84d60388d3f3da4a8071f
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonShowUsers);
            this.Controls.Add(this.buttonStartServer);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSmpServer";
            this.ShowIcon = false;
            this.Text = "Message Server Version 2.0";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ///
            // FormSmpRegistrations
            ///
            this.registrationsForm.Controls.Add(this.textBoxRegistrations);
            this.registrationsForm.Controls.Add(this.radioButtonUserIDs);
            this.registrationsForm.Controls.Add(this.radioButtonUserIDsandPasswords);
            this.registrationsForm.Controls.Add(this.buttonShowRegistrations);
            this.registrationsForm.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.registrationsForm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.registrationsForm.ClientSize = new System.Drawing.Size(350, 400);
            this.registrationsForm.MaximizeBox = false;
            this.registrationsForm.MinimizeBox = false;
            this.registrationsForm.Name = "FormSmpRegistrations";
            this.registrationsForm.ShowIcon = false;
            this.registrationsForm.Text = "Registrations";
            this.registrationsForm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonStartServer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioAll;
        private System.Windows.Forms.RadioButton radioButtonPriorityHigh;
        private System.Windows.Forms.RadioButton radioButtonPriorityMedium;
        private System.Windows.Forms.RadioButton radioButtonPriorityLow;
        private System.Windows.Forms.TextBox textBoxMessages;
        private System.Windows.Forms.Button buttonShowMessages;
        // SMP 3.0 Implementation
        private System.Windows.Forms.Button buttonShowUsers;
        private System.Windows.Forms.Form registrationsForm;
        private System.Windows.Forms.TextBox textBoxRegistrations;
        private System.Windows.Forms.RadioButton radioButtonUserIDs;
        private System.Windows.Forms.RadioButton radioButtonUserIDsandPasswords;
        private System.Windows.Forms.Button buttonShowRegistrations;
        // End of SMP 3.0 Implementation
        private System.Windows.Forms.TextBox textBoxMessageType;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPortNumber;
        private System.Windows.Forms.TextBox textBoxServerIPAddress;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxMessagePriority;
        private System.Windows.Forms.Label labelLastUserID;
        private System.Windows.Forms.TextBox textBoxLastUserID;
        private System.Windows.Forms.Label labelLastPassword;
        private System.Windows.Forms.TextBox textBoxLastPassword;
    }
}

