using Microsoft.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace SudentManagementSystem
{
    public partial class Form1 : Form
    {
        public int StudentID; // used when we delete and updete
        public string Gender; // used when select gender
        public int rowId;
        // databae connection
        // db name: SMSDB
        SqlConnection DBconnection = new SqlConnection("Data Source=.; Initial Catalog=SMSDB; TrustServerCertificate=True; Integrated Security=True ");
        public Form1()
        {
            InitializeComponent();
            DisplayData();
        }
        public void DisplayData()
        {
            SqlDataAdapter sqad = new SqlDataAdapter("select * from Students", DBconnection);
            DataTable dt = new DataTable();
            sqad.Fill(dt);
            dataGridView1.DataSource = dt;
            var dat = dt;
        }
        private void AllClear()
        {
            StudentName.Clear();
            Age.Clear();
            Mobile.Clear();
            Email.Clear();
            male.Checked = false;
            female.Checked = false;
            other.Checked = false;
            StudentClass.SelectedIndex = -1;
            Date.Value = DateTime.Today;
        }

        private void save_Click(object sender, EventArgs e)
        {
            String NameError = NameValidate(StudentName.Text);
            String AgeError = AgeValidate(Age.Text);
            String EmailError = EmailValidate(Email.Text);
            String MobileError = MobileValidate(Mobile.Text);
            int DuplicateEmail = CountDuplicateEmail(Email.Text);
            int DuplicateMobile = CountDuplicateMobile(Email.Text);

            if (NameError != "")
            {
                MessageBox.Show(NameError);
                return;
            }
            else if (AgeError != "")
            {
                MessageBox.Show(AgeError);
                return;
            }
            else if (EmailError != "")
            {
                MessageBox.Show(EmailError);
                return;
            }
            else if (DuplicateEmail > 0)
            {
                MessageBox.Show("This eamil is already used");
                return;
            }
            else if (StudentClass.SelectedIndex == -1)
            {
                MessageBox.Show("Select Your class");
                return;
            }
            else if (MobileError != "")
            {
                MessageBox.Show(MobileError);
                return;
            }
            else if (DuplicateMobile > 0)
            {
                MessageBox.Show("This Mobile is already used");
                return;
            }
            else if (male.Checked != true && female.Checked != true && other.Checked != true)
            {
                MessageBox.Show("Select Your Gender");
                return;
            }
            else if (Date.Value > DateTime.Today)
            {
                MessageBox.Show("You Can Not Set Future Date");
                Date.Value = DateTime.Today;
                return;
            }
            else
            {
                string sqlQuery = "INSERT INTO Students (Name, Age, MobileNumber, Email, Gender, Class, Ad_date) VALUES ('" + StudentName.Text + "' ,'" + Convert.ToInt32(Age.Text) + "','" + Mobile.Text + "','" + Email.Text + "','" + Gender + "','" + StudentClass.SelectedItem + "','" + Date.Value.ToShortDateString() + "')";

                DBconnection.Open();
                SqlCommand cm = new SqlCommand(sqlQuery, DBconnection);
                if (cm.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Saved Successfuly");
                }
                else
                {
                    MessageBox.Show("Error!!!");
                }
                DBconnection.Close();
                AllClear();
                DisplayData();
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            String NameError = NameValidate(StudentName.Text);
            String AgeError = AgeValidate(Age.Text);
            String EmailError = EmailValidate(Email.Text);
            String MobileError = MobileValidate(Mobile.Text);
            int DuplicateEmail = CountDuplicateEmail(Email.Text);
            int DuplicateMobile = CountDuplicateMobile(Email.Text);
            if (NameError != "")
            {
                MessageBox.Show(NameError);
            }
            else if (AgeError != "")
            {
                MessageBox.Show(AgeError);
            }
            else if (EmailError != "")
            {
                MessageBox.Show(EmailError);
            }
            else if (DuplicateEmail > 1)
            {
                MessageBox.Show("This eamil is already used");
            }
            else if (StudentClass.SelectedIndex == -1)
            {
                MessageBox.Show("Select Your class");
            }
            else if (MobileError != "")
            {
                MessageBox.Show(MobileError);
            }
            else if (DuplicateMobile > 1)
            {
                MessageBox.Show("This Mobile is already used");
            }
            else if (Date.Value > DateTime.Today)
            {
                MessageBox.Show("You Can Not Set Future Date");
                Date.Value = DateTime.Today;
            }
            else if (male.Checked != true && female.Checked != true && other.Checked != true)
            {
                MessageBox.Show("Select Your Gender");
            }
            else
            {
                string sqlQuery = "update Students set Name = " + "'" + StudentName.Text +
                                    "' , " + "Age = " + "'" + Convert.ToInt32(Age.Text) + 
                                    "' , MobileNumber = " + "'" + Mobile.Text + 
                                    "' , Email = " + "'" + Email.Text + 
                                    "' , Gender = " + "'" + Gender + 
                                    "' , Class = " + "'" + StudentClass.SelectedItem + 
                                    "' , Ad_date = " + "'" + Date.Value.ToShortDateString() + 
                                    "'   where ID = " + StudentID;
                DBconnection.Open();
                SqlCommand cm = new SqlCommand(sqlQuery, DBconnection);
                if (cm.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Update Successfuly");
                }
                DBconnection.Close();
                AllClear();
                DisplayData();
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            DBconnection.Open();
            int TotalRow = dataGridView1.Rows.Count;
            for (int i = 0; i < TotalRow; i++)
            {
                DataGridViewRow gridr = dataGridView1.Rows[i];
                if (gridr.Selected == true)
                {
                    string sqlQuery = "DELETE FROM Students WHERE ID= '" + dataGridView1.Rows[i].Cells[0].Value + "'";
                    SqlCommand cmd = new SqlCommand(sqlQuery, DBconnection);
                    cmd.ExecuteNonQuery();
                }
            }
            DisplayData();
            MessageBox.Show("Data Deleted.");
            DBconnection.Close();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            AllClear();
        }
        public string NameValidate(string StudenName)
        {
            string NameErrorMessage = string.Empty;
            if (StudenName == "")
            {
                NameErrorMessage = "You must enter name";
            }
            else if (StudenName.Length < 3)
            {
                NameErrorMessage = "Your name is too small";
            }
            else if (StudenName.Length > 50)
            {
                NameErrorMessage = "Your name is too long";
            }
            else
            {
                NameErrorMessage = string.Empty;
            }
            return NameErrorMessage;
        }
        public int CountDuplicateEmail(string email)
        {
            int count = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var gridEmail = row.Cells[4].Value.ToString();
                if (gridEmail == email)
                {
                    count++;
                }
            }
            return count;
        }
        public string EmailValidate(string StudenEmail)
        {
            string EmailErrorMessage = string.Empty;
            StudenEmail= StudenEmail.Trim();
            string email = Email.Text;

            System.Text.RegularExpressions.Regex expr = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (!expr.IsMatch(email))
            {
                EmailErrorMessage = "Invalid Email";
            }
            else if (StudenEmail.Length > 50)
            {
                EmailErrorMessage = "Your Email is too long";
            }
            else
            {
                EmailErrorMessage = string.Empty;
            }
            return EmailErrorMessage;
        }
        
        public string AgeValidate(string StudentAge)
        {
            int StudentAgeInt;
            string AgeErrorMessage = string.Empty;
            string age = StudentAge.Trim();

            if (StudentAge == "")
            {
                AgeErrorMessage = "Age Feild is empty";
            }
            else
            {
                try
                {
                    StudentAgeInt = Int32.Parse(StudentAge);
                    if (StudentAgeInt < 10)
                    {
                        AgeErrorMessage = "Your Are too young for our course";
                    }
                    else if (StudentAgeInt > 40)
                    {
                        AgeErrorMessage = "Your Are too older for our course";
                    }
                    else
                    {
                        AgeErrorMessage = string.Empty;
                    }
                }
                catch (FormatException e)
                {
                    AgeErrorMessage = "Enter the Correct age";                   
                }                
            }         
            return AgeErrorMessage;
        }
        public int CountDuplicateMobile(string mobile)
        {
            int count = 0;
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                var gridMobile = row.Cells[3].Value.ToString();
                if(gridMobile == mobile)
                {
                    count++;
                }
                else
                {
                    count = count;
                }
            }
            return count;
        }

        public string MobileValidate(string StudenMobile)
        {
            string MobileErrorMessage = string.Empty;
            StudenMobile= StudenMobile.Trim();
            if (StudenMobile == "")
            {
                MobileErrorMessage = "Mobile Number required";
            }
            else if (StudenMobile.Length > 20)
            {
                MobileErrorMessage = "Mobile Number is too Long";
            }
            else if (StudenMobile.Length < 11)
            {
                MobileErrorMessage = "Mobile Number is too low";
            }
            else
            {
                MobileErrorMessage = string.Empty;
            }
            return MobileErrorMessage;
        }        

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                StudentID = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                StudentName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                Age.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                Mobile.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                Email.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                string sclass = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                if (sclass == "7th Class")
                {
                    StudentClass.SelectedIndex = 0;
                }
                else if (sclass == "8th Class")
                {
                    StudentClass.SelectedIndex = 1;
                }
                else if (sclass == "9th Class")
                {
                    StudentClass.SelectedIndex = 2;
                }
                else if (sclass == "10th Class")
                {
                    StudentClass.SelectedIndex = 3;
                }
                else
                {
                    StudentClass.SelectedIndex = -1;
                }
                string gendercheck = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                string Dtc = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                string[] subs1 = Dtc.Split(' ');
                string[] subs = subs1[0].Split("/");
                Date.Value = new DateTime(Int16.Parse(subs[2]), Int16.Parse(subs[0]), Int16.Parse(subs[1]));
                if (gendercheck == "Male")
                {
                    male.Checked = true;
                }
                else if (gendercheck == "Female")
                {
                    female.Checked = true;
                }
                else
                {
                    other.Checked = true;
                }
            }
        }

        private void male_CheckedChanged(object sender, EventArgs e)
        {
            Gender = "Male";
        }

        private void female_CheckedChanged(object sender, EventArgs e)
        {
            Gender = "Female";
        }

        private void other_CheckedChanged(object sender, EventArgs e)
        {
            Gender = "Other";
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            string SearchValue = txtSearch.Text;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var SearchName = row.Cells[1].Value.ToString();
                if (!SearchName.Contains(SearchValue))
                {
                    CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[dataGridView1.DataSource];
                    currencyManager1.SuspendBinding();
                    row.Visible = false;
                    currencyManager1.ResumeBinding();
                }
                else
                {
                    CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[dataGridView1.DataSource];
                    currencyManager1.SuspendBinding();
                    row.Visible = true;
                    currencyManager1.ResumeBinding();
                }
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            this.Visible= false;
            Guideline guideline = new Guideline();
            guideline.Show();
        }
    }
}