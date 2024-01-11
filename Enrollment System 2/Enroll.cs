using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Enrollment_System_2
{
    public partial class Enroll : Form
    {
        public Point mouseLocation;
        string mis, status;
        int studid;
        enrollmentDataContext db = new enrollmentDataContext();
        SqlConnection conn = new SqlConnection(@"Data Source = CLARK-KEINDRICK\SQLEXPRESS; Initial Catalog = ENROLLMENT_DB; Integrated security=True;");
        public Enroll()
        {
            InitializeComponent();
            enrData.DataSource = db.enr_view();
            updateBTN.Enabled = false;
            deleteBTN.Enabled = false;
            fill_MIS();
            fill_Stud();
        }

        private void Classes_Load(object sender, EventArgs e)
        {
            enrData.DataSource = db.enr_view();
            timer1.Start();
            fill_MIS();
            fill_Stud();
        }

        private void fill_MIS()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT MIS_CODE FROM CLASS", conn);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                CBmis.Items.Add(dr["MIS_CODE"].ToString());
            }
            conn.Close();
        }

        private void fill_Stud()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT STUD_ID FROM STUDENT", conn);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                CBid.Items.Add(dr["STUD_ID"].ToString());
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

        private void btnSub_Click(object sender, EventArgs e)
        {
            Subjects sub = new Subjects();

            sub.Show();
            this.Hide();
        }

        private void btnClass_Click(object sender, EventArgs e)
        {
            Classes cls = new Classes();

            cls.Show();

            this.Hide();
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

        private void btnProg_Click(object sender, EventArgs e)
        {
            Programs prog = new Programs();

            prog.Show();

            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbldate.Text = DateTime.Now.ToLongDateString();
            lbltime.Text = DateTime.Now.ToLongTimeString();
        }

        private void searchbox__TextChanged(object sender, EventArgs e)
        {
            enrData.DataSource = db.enr_search(searchbox.Texts);
        }

        private void searchbox_Leave(object sender, EventArgs e)
        {
            enrData.DataSource = db.enr_view();
        }

        private void CBmis_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            mis = CBmis.SelectedItem.ToString();
        }

        private void CBid_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            studid = int.Parse(CBid.SelectedItem.ToString());
        }

        private void submitBTN_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime datenow = DateTime.Now;
                db.enr_save(mis, studid, status, datenow);
                MessageBox.Show("Save Successfully!", "OK");
                enrData.DataSource = db.enr_view();
                CBmis.Texts = "MIS CODE";
                updateBTN.Enabled = false;
                deleteBTN.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exit Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CBstat_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            status = CBstat.SelectedItem.ToString();
        }

        private void deleteBTN_Click(object sender, EventArgs e)
        {
            db.enr_delete(mis, studid);
            MessageBox.Show("Successfully Deleted!", "OK");
            enrData.DataSource = db.enr_view();
            CBmis.Texts = "MIS CODE";
            updateBTN.Enabled = false;
            deleteBTN.Enabled = false;
        }

        private void updateBTN_Click(object sender, EventArgs e)
        {
            try
            {
                db.enr_update(mis, studid, status);
                MessageBox.Show("Update Successfully!", "OK");
                enrData.DataSource = db.enr_view();
                CBmis.Texts = "MIS CODE";
                updateBTN.Enabled = false;
                deleteBTN.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exit Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Enroll_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void Enroll_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        private void enrData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (enrData.CurrentRow != null)
            {
                updateBTN.Enabled = true;
                deleteBTN.Enabled = true;

                mis = CBmis.Texts = enrData.CurrentRow.Cells[0].Value.ToString();
                CBid.Texts = enrData.CurrentRow.Cells[1].Value.ToString();
                CBstat.Texts = enrData.CurrentRow.Cells[2].Value.ToString();

                studid = int.Parse(enrData.CurrentRow.Cells[1].Value.ToString());

            }
            else
            {
                MessageBox.Show("Row is Empty!", "Exit Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
