using MySql.Data.MySqlClient;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        string server = "127.0.0.1";
        int port = 3306;
        string database = "csc313";
        string id = "root";
        string pw = "";
        string connectionAddress = "";
        BindingSource dbgridBindingSource = new BindingSource();
        public Form1()
        {
            InitializeComponent();
            connectionAddress = string.Format("Server={0};Port={1};Database={2};Username={3};Password={4};",server,port,database,id,pw);

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int sel_idx = e.RowIndex;
            this.txtboxVin.Text = dataGridView1.Rows[sel_idx].Cells["vin"].Value.ToString();
            this.txtboxPlate.Text = dataGridView1.Rows[sel_idx].Cells["license_plate"].Value.ToString();
            this.txtboxMake.Text = dataGridView1.Rows[sel_idx].Cells["make"].Value.ToString();
            this.txtboxModel.Text = dataGridView1.Rows[sel_idx].Cells["model"].Value.ToString();
            this.txtboxYear.Text = dataGridView1.Rows[sel_idx].Cells["year"].Value.ToString();
            dbgridBindingSource.ResetBindings(false);
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            selectTable();
        }

        private void selectTable()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionAddress))
                {
                    conn.Open();
                    string selectQuery = string.Format("select * from cars");

                    MySqlCommand command = new MySqlCommand(selectQuery, conn);
                    MySqlDataReader table = command.ExecuteReader();
                    dbgridBindingSource.DataSource = table;

                    //dataGridView1.Rows.Clear();
                    dataGridView1.DataSource = dbgridBindingSource;
                    changeHeader();
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void changeHeader()
        {
            dataGridView1.Columns[0].HeaderText = "VIN";
            dataGridView1.Columns[1].HeaderText = "LICENSE PLATE";
            dataGridView1.Columns[2].HeaderText = "MAKER";
            dataGridView1.Columns[3].HeaderText = "MODEL";
            dataGridView1.Columns[4].HeaderText = "YEAR";

        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionAddress))
                {
                    conn.Open();
                    string insertQuery = string.Format("insert into cars (vin,license_plate,make,model,year) " + "values ('{0}','{1}','{2}','{3}','{4}');",
                        txtboxVin.Text, txtboxPlate.Text, txtboxMake.Text, txtboxModel.Text, txtboxYear.Text);

                    MySqlCommand command = new MySqlCommand(insertQuery, conn);
                    if (command.ExecuteNonQuery() != 1 ) {
                        MessageBox.Show("Failed to insert data!");
                    }

                    txtboxVin.Text = "";
                    txtboxPlate.Text = "";
                    txtboxMake.Text = "";
                    txtboxModel.Text = "";
                    txtboxYear.Text = "";

                    selectTable();
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionAddress))
                {
                    conn.Open();

                    var vin = txtboxVin.Text;
                    var plate = txtboxPlate.Text;
                    var make = txtboxMake.Text;
                    var model = txtboxModel.Text;
                    var year = txtboxYear.Text;
                    // string insertQuery = string.Format("insert into cars (vin,license_plate,make,model,year) " + "values ('{0}','{1}','{2}','{3}','{4}');",
                    //     txtboxVin.Text, txtboxPlate.Text, txtboxMake.Text, txtboxModel.Text, txtboxYear.Text);
                    string updateQuery = "update cars set vin=@vin,license_plate=@plate, make=@make, model=@model, year=@year where vin=@vin";


                    using (MySqlCommand command = new MySqlCommand(updateQuery, conn)) {
                        command.Parameters.AddWithValue("vin", vin);
                        command.Parameters.AddWithValue("plate", plate);
                        command.Parameters.AddWithValue("make", make);
                        command.Parameters.AddWithValue("model", model);
                        command.Parameters.AddWithValue("year", year);
                        if (command.ExecuteNonQuery() != 1)
                        {
                            MessageBox.Show("Failed to update data!");
                        }
                    }

                    txtboxVin.Text = "";
                    txtboxPlate.Text = "";
                    txtboxMake.Text = "";
                    txtboxModel.Text = "";
                    txtboxYear.Text = "";

                    selectTable();
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            //DELETE FROM Customers WHERE CustomerName='Alfreds Futterkiste';
            try
            {
                var vin = txtboxVin.Text;
                using (MySqlConnection conn = new MySqlConnection(connectionAddress))
                {
                    conn.Open();
                    string insertQuery = string.Format("delete from cars where vin='{0}';",
                        txtboxVin.Text, txtboxPlate.Text, txtboxMake.Text, txtboxModel.Text, txtboxYear.Text);

                    MySqlCommand command = new MySqlCommand(insertQuery, conn);
                    if (command.ExecuteNonQuery() != 1)
                    {
                        MessageBox.Show("Failed to insert data!");
                    }

                    txtboxVin.Text = "";
                    txtboxPlate.Text = "";
                    txtboxMake.Text = "";
                    txtboxModel.Text = "";
                    txtboxYear.Text = "";

                    selectTable();
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    
    }
}