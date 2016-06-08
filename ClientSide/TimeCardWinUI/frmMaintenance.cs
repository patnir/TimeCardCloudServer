using System;
using System.Windows.Forms;

public partial class frmMaintenance: Form
{
	private string mMode;
    private clsTimeLogList mTimeLogList = new clsTimeLogList();
    internal clsTimeLogEntry mEntry = new clsTimeLogEntry();

	public frmMaintenance(string mode, clsTimeLogEntry entry)
	{
        InitializeComponent();

        mMode = mode;

		if (mode == "Add")
		{
			Text = "Add Entry";
        }
        else
		{
			Text = "Update Entry";
            mEntry = entry;

			txtEmployeeID.Text = mEntry.EmployeeID;
			txtDateWorked.Text = mEntry.DateWorked.ToShortDateString();
			cboHoursWorked.Text = mEntry.HoursWorked.ToString("#.00");
			chkBillable.Checked = mEntry.Billable;
			txtDescription.Text = mEntry.Description;
		}
	}

	private void btnOK_Click(object sender, System.EventArgs e)
	{
		if (validateForm() == false)
		{
			return;
		}

        mEntry.EmployeeID = txtEmployeeID.Text;
        mEntry.DateWorked = TimeLogDate.Parse(txtDateWorked.Text);
        mEntry.HoursWorked = double.Parse(cboHoursWorked.Text);
        mEntry.Billable = chkBillable.Checked;
        mEntry.Description = txtDescription.Text;

        if (mMode == "Add")
        {
            mTimeLogList.Add(mEntry);
        }
        else
        {
            mTimeLogList.Update(mEntry);
        }

        DialogResult = DialogResult.OK;
	}

	private void btnCancel_Click(object sender, System.EventArgs e)
	{
		DialogResult = DialogResult.Cancel;
	}

	private bool validateForm()
	{		
		if (txtEmployeeID.Text == "")
		{
            frmMain.messageBoxOK("Employee ID is required.");
			txtEmployeeID.Focus();
			return false;
		}

        // Check Date Worked.

        TimeLogDate dateWorked = new TimeLogDate();

        if (TimeLogDate.TryParse(txtDateWorked.Text, out dateWorked) == false)
        {
            frmMain.messageBoxOK("Data Worked must be a valid date.");
            txtDateWorked.Focus();
            return false;
        }

        DateTime dateWorkedDateTime = DateTime.Parse(dateWorked.ToShortDateString());

        TimeSpan diff = DateTime.Now.Date.Subtract(dateWorkedDateTime);

        if (dateWorkedDateTime > DateTime.Now.Date 
			|| diff.TotalDays > 7)
		{
            frmMain.messageBoxOK("Date Worked cannot be > today's date or more than 7 days in the past.");
            txtDateWorked.Focus();
			return false;
		}

		// Check Hours Worked.

		double hoursWorked;
		const string cInvalidHoursWorked = "Hours worked must be in the range .25 through 4.00 and be in .25 hour increments.";

		if (double.TryParse(cboHoursWorked.Text, out hoursWorked) == false)
		{
            frmMain.messageBoxOK(cInvalidHoursWorked);
			cboHoursWorked.Focus();
			return false;
		}

		if (hoursWorked < .25 
			|| hoursWorked > 4
			|| (hoursWorked % .25) != 0)
		{
            frmMain.messageBoxOK(cInvalidHoursWorked);
			cboHoursWorked.Focus();
			return false;
		}
		
		// Check Description.
		
		if (txtDescription.Text.Length < 25)
		{
            frmMain.messageBoxOK("Description is required and must contain at least 25 characters.");
			txtDescription.Focus();
			return false;
		}

		// If we get this far, then everything is OK.
		
		return true;
	}
}
