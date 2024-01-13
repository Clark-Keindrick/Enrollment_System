using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;

namespace Enrollment_System_2
{
    public partial class Classes : Form
    {
        public Point mouseLocation;
        string shift, mis, clscode;
        string timesched;
        int profid;

        enrollmentDataContext db = new enrollmentDataContext();
        SqlConnection conn = new SqlConnection(@"Data Source = LAPTOP-7VJGOGAD\SQLEXPRESS; Initial Catalog = ENROLLMENT_DB; Integrated security=True;");

        public Classes()
        {
            InitializeComponent();
            clsData.DataSource = db.cls_view();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            dtpsecond.Enabled = false;
            dtpsecond2.Enabled = false;
            dtpthird.Enabled = false;
            dtpthird2.Enabled = false;
            fill_MIS();
            fill_Prof();
        }

        private void Classes_Load(object sender, EventArgs e)
        {
            clsData.DataSource = db.cls_view();
            timer1.Start();
            fill_MIS();
            fill_Prof();
        }

        private void fill_MIS()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT SUB_CODE FROM SUBJECTS", conn);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                CBmis.Items.Add(dr["SUB_CODE"].ToString());
            }
            conn.Close();
        }

        private void fill_Prof()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT PROF_ID FROM PROFESSOR", conn);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                CBprofid.Items.Add(dr["PROF_ID"].ToString());
            }
            conn.Close();
        }

        private void btnDash_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard();

            dash.Show();
            this.Hide();
        }

        private void btnStud_Click(object sender, EventArgs e)
        {
            Student stud = new Student();

            stud.Show();

            this.Hide();
        }

        private void btnProf_Click(object sender, EventArgs e)
        {
            Professor prof = new Professor();

            prof.Show();
            this.Hide();
        }

        private void btnProg_Click(object sender, EventArgs e)
        {
            Programs prog = new Programs();

            prog.Show();

            this.Hide();
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            Subjects sub = new Subjects();

            sub.Show();
            this.Hide();
        }

        private void CBmis_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            mis = CBmis.SelectedItem.ToString();
        }

        private void CBprofid_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            profid = int.Parse(CBprofid.SelectedItem.ToString());
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if ((dtpTime.Enabled && dtpTime2.Enabled == true) && (dtpsecond.Enabled && dtpsecond2.Enabled == true) && (dtpthird.Enabled = dtpthird.Enabled == true))
                {
                    timesched = dtpTime.Text + " - " + dtpTime2.Text + " / " + dtpsecond.Text + " - " + dtpsecond2.Text + " / " + dtpthird.Text + " - " + dtpthird2.Text;
                }
                else if ((dtpTime.Enabled && dtpTime2.Enabled == true) && (dtpsecond.Enabled && dtpsecond2.Enabled == true) && (dtpthird.Enabled = dtpthird.Enabled == false))
                {
                    timesched = dtpTime.Text + " - " + dtpTime2.Text + " / " + dtpsecond.Text + " - " + dtpsecond2.Text;
                }
                else 
                {
                    timesched = dtpTime.Text + " - " + dtpTime2.Text;
                }

                if (RBDay.Checked == true)
                {
                    shift = "DAY";
                }
                else
                {
                    shift = "NIGHT";
                }

                db.cls_update(clscode, timesched, tbdays.Texts, shift, tbroom.Texts, tbSec.Texts, mis, profid);
                MessageBox.Show("Update Successfully!", "OK");
                clsData.DataSource = db.cls_view();
                clear();
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exit Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                db.cls_delete(clscode);
                MessageBox.Show("Successfully Deleted!", "OK");
                clsData.DataSource = db.cls_view();
                clear();
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exit Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void searchbox__TextChanged(object sender, EventArgs e)
        {
            clsData.DataSource = db.cls_search(searchbox.Texts);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if ((dtpTime.Enabled && dtpTime2.Enabled == true) && (dtpsecond.Enabled && dtpsecond2.Enabled == true) && (dtpthird.Enabled = dtpthird.Enabled == true))
                {
                    timesched = dtpTime.Text + " - " + dtpTime2.Text + " / " + dtpsecond.Text + " - " + dtpsecond2.Text + " / " + dtpthird.Text + " - " + dtpthird2.Text;
                }
                else if ((dtpTime.Enabled && dtpTime2.Enabled == true) && (dtpsecond.Enabled && dtpsecond2.Enabled == true) && (dtpthird.Enabled = dtpthird.Enabled == false))
                {
                    timesched = dtpTime.Text + " - " + dtpTime2.Text + " / " + dtpsecond.Text + " - " + dtpsecond2.Text;
                }
                else
                {
                    timesched = dtpTime.Text + " - " + dtpTime2.Text;
                }

                if (RBDay.Checked == true)
                {
                    shift = "DAY";
                }
                else
                {
                    shift = "NIGHT";
                }

                db.cls_save(TBmis.Texts, timesched, tbdays.Texts, shift, tbroom.Texts, tbSec.Texts, mis, profid);
                MessageBox.Show("Save Successfully!", "OK");
                clsData.DataSource = db.cls_view();
                clear();
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exit Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {

            }
        }

        private void clsData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (clsData.CurrentRow != null)
            {
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                clscode = TBmis.Texts = clsData.CurrentRow.Cells[0].Value.ToString();
                mis = CBmis.Texts = clsData.CurrentRow.Cells[1].Value.ToString();
                tbSec.Texts = clsData.CurrentRow.Cells[3].Value.ToString();
                timesched = clsData.CurrentRow.Cells[4].Value.ToString();
                tbdays.Texts = clsData.CurrentRow.Cells[5].Value.ToString();
                shift = clsData.CurrentRow.Cells[6].Value.ToString();
                tbroom.Texts = clsData.CurrentRow.Cells[7].Value.ToString();
                CBprofid.Texts = clsData.CurrentRow.Cells[8].Value.ToString();
                profid = int.Parse(clsData.CurrentRow.Cells[8].Value.ToString());

                if(timesched.Contains("/"))
                {
                    string[] timeString = timesched.Split('/');

                    if(timeString.Length == 3)
                    {
                        string firstdtp, seconddtp, thirddtp;
                        dtpsecond.Enabled = true;
                        dtpsecond2.Enabled = true;
                        dtpthird.Enabled = true;
                        dtpthird2.Enabled = true;

                        for (int i = 0; i < timeString.Length; i++)
                        {
                            timeString[i] = timeString[i].Trim();
                        }

                        firstdtp = timeString[0];
                        seconddtp = timeString[1];
                        thirddtp = timeString[2];

                        string[] dtpsplit = firstdtp.Split('-');

                        if (dtpsplit.Length == 2)
                        {
                            dtpTime.Text = dtpsplit[0].Trim();
                            dtpTime2.Text = dtpsplit[1].Trim();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Time Format", "OK");
                        }

                        string[] dtpsplit2 = seconddtp.Split('-');

                        if (dtpsplit2.Length == 2)
                        {
                            dtpsecond.Text = dtpsplit2[0].Trim();
                            dtpsecond2.Text = dtpsplit2[1].Trim();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Time Format", "OK");
                        }

                        string[] dtpsplit3 = thirddtp.Split('-');

                        if (dtpsplit3.Length == 2)
                        {
                            dtpthird.Text = dtpsplit3[0].Trim();
                            dtpthird2.Text = dtpsplit3[1].Trim();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Time Format", "OK");
                        }

                    }

                    if (timeString.Length == 2)
                    {
                        string firstdtp, seconddtp;
                        dtpsecond.Enabled = true;
                        dtpsecond2.Enabled = true;
                        dtpthird.Enabled = false;
                        dtpthird2.Enabled = false;

                        for (int i = 0; i < timeString.Length; i++)
                        {
                            timeString[i] = timeString[i].Trim();
                        }

                        firstdtp = timeString[0];
                        seconddtp = timeString[1];

                        string[] dtpsplit = firstdtp.Split('-');

                        if (dtpsplit.Length == 2)
                        {
                            dtpTime.Text = dtpsplit[0].Trim();
                            dtpTime2.Text = dtpsplit[1].Trim();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Time Format", "OK");
                        }

                        string[] dtpsplit2 = seconddtp.Split('-');

                        if (dtpsplit2.Length == 2)
                        {
                            dtpsecond.Text = dtpsplit2[0].Trim();
                            dtpsecond2.Text = dtpsplit2[1].Trim();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Time Format", "OK");
                        }
                    }
                }

                else
                {
                    string[] timeString = timesched.Split('-');
                    dtpsecond.Enabled = false;
                    dtpsecond2.Enabled = false;
                    dtpthird.Enabled = false;
                    dtpthird2.Enabled = false;

                    if (timeString.Length == 2)
                    {
                        dtpTime.Text = timeString[0].Trim();
                        dtpTime2.Text = timeString[1].Trim();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Time Format", "OK");
                    }
                }

                if (shift == "DAY")
                {
                    RBDay.Checked = true;
                }
                else if (shift == "NIGHT")
                {
                    RBNight.Checked = true;
                }
                else
                {
                    RBDay.Checked = false;
                    RBNight.Checked = false;
                }

            }
            else
            {
                MessageBox.Show("Row is empty!", "Exit Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            dtpsecond.Enabled = true;
            dtpsecond2.Enabled = true;
        }

        private void btnEnable2_Click(object sender, EventArgs e)
        {
            dtpthird.Enabled = true;
            dtpthird2.Enabled = true;
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            dtpsecond.Enabled = false;
            dtpsecond2.Enabled = false;
        }

        private void btnDisable2_Click(object sender, EventArgs e)
        {
            dtpthird.Enabled = false;
            dtpthird2.Enabled = false;
        }

        private void searchbox_Leave(object sender, EventArgs e)
        {
            clsData.DataSource = db.cls_view();
        }

        private void btnEnroll_Click(object sender, EventArgs e)
        {
            Enroll enrl = new Enroll();

            enrl.Show();

            this.Hide();
        }

        private void Classes_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void Classes_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        private void settings_Click(object sender, EventArgs e)
        {
            Personal personalForm = new Personal();
            personalForm.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbldate.Text = DateTime.Now.ToLongDateString();
            lbltime.Text = DateTime.Now.ToLongTimeString();
        }

        public void clear()
        {
            TBmis.Texts = string.Empty;
            tbdays.Texts = string.Empty;
            tbroom.Texts = string.Empty;
            tbSec.Texts = string.Empty;
            dtpTime.Text = string.Empty;
            dtpTime2.Text = string.Empty;
            CBprofid.Texts = "PROF ID";
            CBmis.Texts = "COURSE CODE";
            RBDay.Checked = false;
            RBNight.Checked = false;
            dtpsecond.Enabled = false;
            dtpsecond2.Enabled = false;
            dtpthird.Enabled = false;
            dtpthird2.Enabled = false;
        }
    }
}
