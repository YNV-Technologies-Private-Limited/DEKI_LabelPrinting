using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEKI_LabelPrinting
{
    public partial class FormLogin : Form
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LocalCon"].ConnectionString;
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateUser())
                {
                    FormWending wending = new FormWending();
                    this.Hide();
                    if (wending.ShowDialog() == DialogResult.OK)
                    {
                        txtLogin.Text = txtPassword.Text = string.Empty;
                        this.Show();
                        txtLogin.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid User Id or Password.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        bool ValidateUser()
        {
            string cmdStr = @"SELECT ID,USER_NAME,[PASSWORD],[ROLE],[CODE] FROM tblUser Where [USER_NAME]=@USER_NAME and [PASSWORD]=@PASSWORD";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(cmdStr, connection))
                    {
                        command.Parameters.AddWithValue("@USER_NAME", txtLogin.Text);
                        command.Parameters.AddWithValue("@PASSWORD", txtPassword.Text);
                        command.CommandType = System.Data.CommandType.Text;
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable tbl = new DataTable();
                        adapter.Fill(tbl);
                        if (tbl != null)
                        {
                            if (tbl.Rows.Count > 0)
                            {
                                DEKI_LabelPrinting.Model.User.ID = Convert.ToInt32(tbl.Rows[0]["ID"]);
                                DEKI_LabelPrinting.Model.User.USER_NAME = Convert.ToString(tbl.Rows[0]["USER_NAME"]);
                                DEKI_LabelPrinting.Model.User.ROLE = Convert.ToString(tbl.Rows[0]["ROLE"]);
                                DEKI_LabelPrinting.Model.User.CODE = Convert.ToString(tbl.Rows[0]["CODE"]);
                                return true;
                            }
                        }
                        if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception exp)
                {
                    writeLog(exp.Message);
                    MessageBox.Show(exp.Message, "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open) connection.Close();
                }
            };

            return false;
        }

        static void writeLog(string message)
        {
            try
            {
                if (!File.Exists($"Log_{DateTime.Today.ToString("ddMMMyyyy")}.txt"))
                {
                    File.Create($"Log_{DateTime.Today.ToString("ddMMMyyyy")}.txt").Dispose();
                }
                File.WriteAllText($"Log_{DateTime.Today.ToString("ddMMMyyyy")}.txt", message);
            }
            catch (Exception ex) { }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtLogin.Text))
                {
                    if (string.IsNullOrEmpty(txtPassword.Text))
                    {
                        txtPassword.Focus();
                    }
                    else
                    {
                        btnLogin_Click(sender, e);
                    }
                }
            }
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtLogin.Text))
                {
                    txtLogin.Focus(); return;
                }
                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    btnLogin_Click(sender, e);
                }
            }
        }
    }
}
