public partial class frmMaintenance
{
    #region Windows Form Designer generated code


    private void InitializeComponent()
    {
        this.cboHoursWorked = new System.Windows.Forms.ComboBox();
        this.txtEmployeeID = new System.Windows.Forms.TextBox();
        this.txtDescription = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.chkBillable = new System.Windows.Forms.CheckBox();
        this.btnCancel = new System.Windows.Forms.Button();
        this.btnOK = new System.Windows.Forms.Button();
        this.txtDateWorked = new System.Windows.Forms.TextBox();
        this.SuspendLayout();
        // 
        // cboHoursWorked
        // 
        this.cboHoursWorked.Items.AddRange(new object[] {
            "0.25",
            "0.50",
            "0.75",
            "1.00",
            "1.25",
            "1.50",
            "1.75",
            "2.00",
            "2.25",
            "2.50",
            "2.75",
            "3.00",
            "3.25",
            "3.50",
            "3.75",
            "4.00"});
        this.cboHoursWorked.Location = new System.Drawing.Point(85, 44);
        this.cboHoursWorked.Name = "cboHoursWorked";
        this.cboHoursWorked.Size = new System.Drawing.Size(112, 21);
        this.cboHoursWorked.TabIndex = 16;
        // 
        // txtEmployeeID
        // 
        this.txtEmployeeID.Location = new System.Drawing.Point(85, 12);
        this.txtEmployeeID.Name = "txtEmployeeID";
        this.txtEmployeeID.Size = new System.Drawing.Size(112, 20);
        this.txtEmployeeID.TabIndex = 12;
        // 
        // txtDescription
        // 
        this.txtDescription.Location = new System.Drawing.Point(85, 80);
        this.txtDescription.Multiline = true;
        this.txtDescription.Name = "txtDescription";
        this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.txtDescription.Size = new System.Drawing.Size(328, 76);
        this.txtDescription.TabIndex = 19;
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(13, 80);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(63, 13);
        this.label4.TabIndex = 18;
        this.label4.Text = "D&escription:";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(213, 16);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(33, 13);
        this.label3.TabIndex = 13;
        this.label3.Text = "&Date:";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(13, 48);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(38, 13);
        this.label2.TabIndex = 15;
        this.label2.Text = "&Hours:";
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(13, 16);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(70, 13);
        this.label1.TabIndex = 11;
        this.label1.Text = "&Employee ID:";
        // 
        // chkBillable
        // 
        this.chkBillable.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.chkBillable.Location = new System.Drawing.Point(213, 40);
        this.chkBillable.Name = "chkBillable";
        this.chkBillable.Size = new System.Drawing.Size(72, 24);
        this.chkBillable.TabIndex = 17;
        this.chkBillable.Text = "&Billable:";
        // 
        // btnCancel
        // 
        this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.btnCancel.Location = new System.Drawing.Point(341, 164);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new System.Drawing.Size(75, 23);
        this.btnCancel.TabIndex = 21;
        this.btnCancel.Text = "Cancel";
        this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
        // 
        // btnOK
        // 
        this.btnOK.Location = new System.Drawing.Point(261, 164);
        this.btnOK.Name = "btnOK";
        this.btnOK.Size = new System.Drawing.Size(75, 23);
        this.btnOK.TabIndex = 20;
        this.btnOK.Text = "&OK";
        this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
        // 
        // txtDateWorked
        // 
        this.txtDateWorked.Location = new System.Drawing.Point(271, 12);
        this.txtDateWorked.Name = "txtDateWorked";
        this.txtDateWorked.Size = new System.Drawing.Size(142, 20);
        this.txtDateWorked.TabIndex = 22;
        // 
        // frmAddUpdate
        // 
        this.AcceptButton = this.btnOK;
        this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
        this.CancelButton = this.btnCancel;
        this.ClientSize = new System.Drawing.Size(426, 195);
        this.Controls.Add(this.txtDateWorked);
        this.Controls.Add(this.cboHoursWorked);
        this.Controls.Add(this.txtEmployeeID);
        this.Controls.Add(this.txtDescription);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.chkBillable);
        this.Controls.Add(this.btnCancel);
        this.Controls.Add(this.btnOK);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.Name = "frmAddUpdate";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Tag = "Fall2011";
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox cboHoursWorked;
    private System.Windows.Forms.TextBox txtEmployeeID;
    private System.Windows.Forms.TextBox txtDescription;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.CheckBox chkBillable;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.TextBox txtDateWorked;
}