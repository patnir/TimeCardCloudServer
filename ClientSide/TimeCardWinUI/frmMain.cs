using System;
using System.Collections.Generic;
using System.Windows.Forms;

public partial class frmMain : Form
{
    private static string mAppName = "Time Card";
    private clsTimeLogList mTimeLogList = new clsTimeLogList();
    private TimeLogDate mBeginDate;
    private TimeLogDate mEndDate;

    public static void Main()
    {
        frmMain main = new frmMain();
        Application.Run(main);
    }

    public frmMain()
    {
        InitializeComponent();
        Text = mAppName;
        mTimeLogList.UserID = Environment.UserName;
    }

    private void frmMain_Load(object sender, EventArgs e)
    {
        DateTime now = DateTime.Now;
        txtBeginDate.Text = now.ToShortDateString();
        txtEndDate.Text = now.ToShortDateString();
        mBeginDate = new TimeLogDate(now.Year, now.Month, now.Day);
        mEndDate = new TimeLogDate(now.Year, now.Month, now.Day);

        refreshView();
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            frmMaintenance maintenanceDialog = new frmMaintenance("Add", null);

            if (maintenanceDialog.ShowDialog() == DialogResult.OK)
            {
                bool ensureVisible = true;

                addEntryToListView(maintenanceDialog.mEntry, ensureVisible);
            }
        }
        catch (Exception ex)
        {
            msgException(ex);
        }
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        if (lvwMain.SelectedIndices.Count == 0)
        {
            messageBoxOK("An entry must be selected before it can be updated.");
            return;
        }

        clsTimeLogEntry selectedEntry = (clsTimeLogEntry)lvwMain.SelectedItems[0].Tag;

        frmMaintenance addUpdate = new frmMaintenance("Update", selectedEntry);

        if (addUpdate.ShowDialog() == DialogResult.OK)
        {
            int itemIndex = lvwMain.SelectedItems[0].Index; 
            lvwMain.Items[itemIndex] = new ListViewItem(selectedEntry.EmployeeID);

            lvwMain.Items[itemIndex].SubItems.Add(selectedEntry.DateWorked.ToShortDateString());
            lvwMain.Items[itemIndex].SubItems.Add(selectedEntry.HoursWorked.ToString("0.00"));

            if (selectedEntry.Billable == true)
            {
                lvwMain.Items[itemIndex].SubItems.Add("Yes");
            }
            else
            {
                lvwMain.Items[itemIndex].SubItems.Add("No");
            }

            lvwMain.Items[itemIndex].SubItems.Add(selectedEntry.Description);
        }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (lvwMain.SelectedIndices.Count == 0)
            {
                messageBoxOK("An entry must be selected before it can be deleted.");
                return;
            }

            if (messageBoxYesNo("Are you sure you want to delete this entry?") == DialogResult.No)
            {
                return;
            }

            clsTimeLogEntry selectedEntry = (clsTimeLogEntry)lvwMain.SelectedItems[0].Tag;

            mTimeLogList.Delete(selectedEntry);

            lvwMain.Items.Remove(lvwMain.SelectedItems[0]);
        }
        catch (Exception ex)
        {
            msgException(ex);
        }
    }

    private void btnAbout_Click(object sender, EventArgs e)
    {
        frmAbout about = new frmAbout();
        about.ShowDialog(this);
    }

    private void lvwMain_DoubleClick(object sender, System.EventArgs e)
    {
        btnUpdate.PerformClick();
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtBeginDate.Text == "" && TimeLogDate.TryParse(txtBeginDate.Text, out mBeginDate) == false)
            {
                mBeginDate = new TimeLogDate(1, 1, 1);
            }
            else
            {
                mBeginDate = TimeLogDate.Parse(txtBeginDate.Text);
            }

            if (txtEndDate.Text == "" && TimeLogDate.TryParse(txtEndDate.Text, out mEndDate) == false)
            {
                mEndDate = new TimeLogDate(9999, 12, 31);
            }
            else
            {
                mEndDate = TimeLogDate.Parse(txtEndDate.Text);
            }
        }
        catch
        {
            messageBoxOK("Must enter a valid date or leave it empty.");
        }

        refreshView();
    }

    private void refreshView()
    {
        List<clsTimeLogEntry> timeLogEntries = new List<clsTimeLogEntry>();
        double totalBillableHours = 0;
        double totalUnbillableHours = 0;

        lvwMain.Items.Clear();

        try
        {
            timeLogEntries = mTimeLogList.GetAllEntries(mBeginDate, mEndDate);
        }
        catch (clsTooManyRecordsException ex)
        {
            messageBoxOK(ex.Message);
        }
        catch (Exception ex)
        {
            messageBoxOK(ex.Message);
        }

        foreach (clsTimeLogEntry entry in timeLogEntries)
        {
            bool ensureVisible = false;
            addEntryToListView(entry, ensureVisible);

            if (entry.Billable == true)
            {
                totalBillableHours += entry.HoursWorked;
            }
            else
            {
                totalUnbillableHours += entry.HoursWorked;
            }
        }
        refreshTotalHours();
    }

    private void addEntryToListView(clsTimeLogEntry entry, bool ensureVisible)
    {
        ListViewItem item = new ListViewItem(entry.EmployeeID);
        item.SubItems.Add(entry.DateWorked.ToShortDateString());
        item.SubItems.Add(entry.HoursWorked.ToString("0.00"));

        if (entry.Billable == true)
        {
            item.SubItems.Add("Yes");
        }
        else
        {
            item.SubItems.Add("No");
        }

        item.SubItems.Add(entry.Description);

        item.Tag = entry;

        lvwMain.Items.Add(item);

        if (ensureVisible == true)
        {
            int index = lvwMain.Items.IndexOf(item);
            lvwMain.Items[index].EnsureVisible();
            lvwMain.Items[index].Selected = true;
        }
    }

    private void refreshTotalHours()
    {
        double totalBillableHours = 0;
        double totalUnbillableHours = 0;

        double totalHours = 0;

        foreach (ListViewItem item in lvwMain.Items)
        {
            string check = item.SubItems[3].Text;
            if (item.SubItems[3].Text == "Yes")
            {
                totalBillableHours += double.Parse(item.SubItems[2].Text);
            }
            else
            {
                totalUnbillableHours += double.Parse(item.SubItems[2].Text);
            }
        }
        totalHours = totalBillableHours + totalUnbillableHours;

        lblTotalHours.Text = "Total: " + totalHours.ToString(".00");
        lblTotalBillableHours.Text = "Billable: " + totalBillableHours.ToString(".00");
        lblTotalUnbillableHours.Text = "Unbillable: " + totalUnbillableHours.ToString(".00");
    }

    public static void messageBoxOK(string msg)
    {
        MessageBox.Show(msg, mAppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public static void msgException(Exception ex)
    {
        MessageBox.Show(ex.Message, mAppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public static DialogResult messageBoxYesNo(string msg)
    {
        return MessageBox.Show(msg, mAppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
    }
}

