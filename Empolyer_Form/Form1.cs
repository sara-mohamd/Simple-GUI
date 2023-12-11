using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Text;
using System.Web;

namespace Empolyer_Form
{
    public partial class Form1 : Form

    {
        List<Employer> emp = new List<Employer>();
        public Form1()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region Variables
            string name = textBox1.Text;
            string gender = comboBox1.Text;
            string bd = textBox5.Text;
            string fav_club = textBox3.Text;
            string comp = textBox2.Text;
            string job_title = textBox4.Text;
            #endregion

            string strcon = "Data Source=DESKTOP-EF3HF78;Initial Catalog=\"Task 4 DataFormat\";Integrated Security=True; TrustServerCertificate = True";
            SqlConnection con1 = new SqlConnection(strcon);
            string q1 = "INSERT INTO Empolyer(Name, Gender, BD, Fav_Club) VALUES ('" + name + "', '" + gender + "', '" + bd + "', '" + fav_club + "'); SELECT SCOPE_IDENTITY();";
            string q2 = "INSERT INTO Experiance(Company, Job_Title, Emp_ID) VALUES ('" + comp + "', '" + job_title + "', @EmpID);";

            SqlCommand com1 = new SqlCommand(q1, con1);
            SqlCommand com2 = new SqlCommand(q2, con1);
            try
            {

                con1.Open();
                var empid = com1.ExecuteScalar();
                com2.Parameters.AddWithValue("@EmpID", empid);
                com2.BeginExecuteNonQuery();
            }
            catch (Exception es)
            {
                Console.WriteLine("Exception type: " + es.GetType());
                Console.WriteLine("Exception message: " + es.Message);
            }
            finally { con1.Close(); }
            MessageBox.Show("Successfully Added");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //List<Employer> emp = new List<Employer>();

            string strcon = "Data Source=DESKTOP-EF3HF78;Initial Catalog=\"Task 4 DataFormat\";Integrated Security=True; TrustServerCertificate = True";
            SqlConnection con1 = new SqlConnection(strcon);

            string q3 = "SELECT em.Name, em.Gender, em.BD, em.Fav_Club, ex.Company, ex.Job_Title FROM Empolyer em INNER JOIN Experiance ex ON em.Emp_ID = ex.Emp_ID";
            con1.Open();
            SqlCommand com3 = new SqlCommand(q3, con1);
            SqlDataReader read = com3.ExecuteReader();
            while (read.Read())
            {
                Employer temp = new Employer();

                temp.Name = read[0].ToString();
                temp.Gender = read[1].ToString();
                temp.Birth = read[2].ToString();
                temp.FavClub = read[3].ToString();
                temp.Company = read[4].ToString();
                temp.Job = read[5].ToString();

                emp.Add(temp);
            }
            con1.Close();
            MessageBox.Show("Read done");

            string json = JsonConvert.SerializeObject(emp.ToArray());
            File.WriteAllText("../../../Emp.json", json);
            MessageBox.Show("JSON file Created");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = File.ReadAllText("../../../Emp.json");
            richTextBox1.Text = s;
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string s = File.ReadAllText("../../../Emp.json");
            Employer[] temp = JsonConvert.DeserializeObject<Employer[]>(s);
            StringBuilder sb = new StringBuilder();

            foreach (Employer e1 in temp)
                sb.AppendLine($"Name: {e1.Name}, Gender: {e1.Gender}, Birth Date: {e1.Birth}, Fav_Club: {e1.FavClub}, Company: {e1.Company}, Job: {e1.Job}\n");
            richTextBox2.Text = sb.ToString();
        }
    }
}