using System.Data.SqlClient;

namespace gameing
{
    public partial class loginp : Form
    {
        private static String ID;
        private string PIC;
        private SqlConnection sqlConnection;

        public loginp()
        {
            InitializeComponent();
            textBox3.PasswordChar = '*';
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ACER\source\repos\db\game.mdf;Integrated Security=True;Connect Timeout=30");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Player Id cannot be empty");
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Player Name cannot be empty");
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "Password cannot be empty");
                return;
            }

            string query = "SELECT * FROM PLAYER_INFO WHERE PLAYER_NAME=@playerName AND PLAYER_PASS=@playerPass AND PLAYER_ID=@playerId";
            SqlCommand command = new SqlCommand(query, sqlConnection);
            command.Parameters.AddWithValue("@playerName", textBox2.Text.Trim());
            command.Parameters.AddWithValue("@playerPass", textBox3.Text.Trim());
            command.Parameters.AddWithValue("@playerId", textBox1.Text.Trim());
            sqlConnection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                ID = textBox1.Text;
                PIC = display();
                homep form2 = new homep(ID, PIC);
                form2.Tag = this;
                form2.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Player not found.");
            }

            reader.Close();
            sqlConnection.Close();
        }

        private string display()
        {
            string query = "SELECT PLAYER_PIC FROM PLAYER_INFO WHERE PLAYER_ID=@playerId";
            SqlCommand command = new SqlCommand(query, sqlConnection);
            command.Parameters.AddWithValue("@playerId", textBox1.Text.Trim());
            sqlConnection.Open();
            string pic = command.ExecuteScalar()?.ToString();
            sqlConnection.Close();
            return pic;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clp form2 = new clp();
            form2.Tag = this;
            form2.Show();
            Hide();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Player Id cannot be empty");
            }
            else
            {
                errorProvider1.SetError(textBox1, "");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Player Name cannot be empty");
            }
            else
            {
                errorProvider1.SetError(textBox2, "");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "Password cannot be empty");
            }
            else
            {
                errorProvider1.SetError(textBox3, "");
            }
        }
    }
}
