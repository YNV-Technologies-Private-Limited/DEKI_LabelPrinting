using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEKI_LabelPrinting
{
    public partial class FormRelProdOrdersSync : Form
    {
        string APIUrl = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["APIUrl"]);
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LocalCon"].ConnectionString;
        string username = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["APIUser"]);// "administrator";
        string password = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["APIPassword"]);//"CMk95*@$46@";
        public FormRelProdOrdersSync()
        {
            InitializeComponent();
        }

        private async void btnSync_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "Please Wait .....";
            lblMsg.Refresh();
            Application.DoEvents();
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 60;
            try
            {
                var handler = new HttpClientHandler
                {
                    Credentials = new System.Net.NetworkCredential(username, password) // domain is optional
                };
                using (HttpClient client = new HttpClient(handler))
                {
                    //Console.WriteLine($"Fetching Items records.");
                    lblMsg.Text = "Getting Response from Server .....";
                    lblMsg.Refresh();
                    Application.DoEvents();
                    try
                    {
                        var response = await client.GetAsync($"{APIUrl.TrimEnd('/')}/RelProdOrders");
                        lblMsg.Text = "Getting Response from Server .....";
                        lblMsg.Refresh();
                        Application.DoEvents();


                        response.EnsureSuccessStatusCode();
                        string json = await response.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var itemsResponse = JsonSerializer.Deserialize<OrderListResponse>(json, options);
                        lblMsg.Text = "Processing Server Response .....";
                        lblMsg.Refresh();
                        Application.DoEvents();

                        int iOrders = itemsResponse.Orders.Count;
                        Application.DoEvents();
                        progressBar1.Style = ProgressBarStyle.Continuous;
                        progressBar1.Maximum = iOrders;
                        progressBar1.Value = 1;
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            if (conn.State == ConnectionState.Closed) conn.Open();
                            int iRecord = 0;
                            foreach (var order in itemsResponse.Orders)
                            {
                                string InsertStatement = @"if NOT Exists(select [No] From ProductionOrder Where [No]=@No) BEGIN 
                                                INSERT INTO [ProductionOrder]([Status],[No],[Description],[Source_No],[Routing_No]
                                                                    ,[Quantity],[Due_Date],[Assigned_User_ID],Creation_Date) 
                                     OUTPUT INSERTED.RowID 
                                                VALUES (@Status,@No,@Description,@Source_No,@Routing_No
                                                    ,@Quantity,@Due_Date,@Assigned_User_ID,@Creation_Date);
                                    END
                                    ELSE BEGIN select ROWID From ProductionOrder Where [No]=@No END";

                                using (SqlCommand cmd = new SqlCommand(InsertStatement, conn))
                                {
                                    cmd.Parameters.AddWithValue("@No", order.No);
                                    cmd.Parameters.AddWithValue("@Status", order.Status);
                                    cmd.Parameters.AddWithValue("@Description", order.Description);
                                    cmd.Parameters.AddWithValue("@Source_No", order.Source_No);
                                    cmd.Parameters.AddWithValue("@Routing_No", order.Routing_No);
                                    cmd.Parameters.AddWithValue("@Quantity", order.Quantity);
                                    cmd.Parameters.AddWithValue("@Due_Date", order.Due_Date);
                                    cmd.Parameters.AddWithValue("@Assigned_User_ID", order.Assigned_User_ID);
                                    cmd.Parameters.AddWithValue("@Creation_Date", order.Creation_Date);
                                    Console.WriteLine($"Order Record ID:-  {cmd.ExecuteScalar()}");
                                }
                                iRecord += 1;
                                Application.DoEvents();
                                progressBar1.Value = iRecord;
                                Application.DoEvents();
                            }

                            MessageBox.Show($"Sync Completed Successfully.....");
                        }
                        //    Console.WriteLine($"No: {vendor.No}, Name: {vendor.Name}, State: {vendor.State_Code}, GST Type: {vendor.GST_Vendor_Type}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sync Items", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }

    public class OrderListResponse
    {
        [JsonPropertyName("@odata.context")]
        public string ODataContext { get; set; }

        [JsonPropertyName("value")]
        public List<Order> Orders { get; set; }
    }

    public class Order
    {
        [JsonPropertyName("@odata.etag")]
        public string ODataEtag { get; set; }
        public string Status { get; set; }
        public string No { get; set; }
        public string Description { get; set; }
        public string Source_No { get; set; }
        public string Routing_No { get; set; }
        public int Quantity { get; set; }
        public DateTime Due_Date { get; set; }
        public string Assigned_User_ID { get; set; }
        public DateTime Creation_Date { get; set; }
    }

}
