using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Enrollment_System_2
{
    public partial class Login : Form
    {
        
        enrollmentDataContext db = new enrollmentDataContext();
        private Timer timer;

        public Login()
        {
            InitializeComponent();

            InitializeTimer();

            string Datenow = DateTime.Now.Date.ToString("D");
            date.Text = Datenow;
        }

        private void InitializeTimer()
        {
            // Create a timer with an interval of 1000 milliseconds (1 second)
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;

            // Start the timer
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the label with the current time
            time.Text = DateTime.Now.ToString("h:mm:ss tt"); // Use "h" for 12-hour format
        }
        private void lognbtn_Click(object sender, EventArgs e)
        {
            string usern = username.Text;
            string pass = password.Text;

            password.PasswordChar = '*';

            // check if username and password are not empty
            if (string.IsNullOrWhiteSpace(usern) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Please fill out both username and password fields.", "Incomplete Information");
                return;
            }

            var resultUser = db.selectId(usern, pass).SingleOrDefault();

            if (resultUser != null)
            {
                int userId = resultUser.id;
                user userLog = new user(userId, username.Text);

                user.CurrentUser.User = userLog;

                Face fcform = new Face();
                fcform.Show();
                this.Hide();
            }
            else
            {          
              MessageBox.Show("Invalid username and password! Please contact your administrator.", "Authentication Failed");
                   
            }


            //string Username = username.Text;
            //string Password = password.Text;

            //password.PasswordChar = '*';

            //var exists = db.ad_login(Username, Password).SingleOrDefault();

            //if (exists != null)
            //{

            //    //Face fcform = new Face();
            //    //fcform.Show();
            //    //this.Hide();
            //}
            //else

            //{
            //    MessageBox.Show("Account doesn't exists! Please contact your administrator", "Message");
            //}
        }
    }
}
