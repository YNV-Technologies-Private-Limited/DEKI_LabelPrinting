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
    public partial class FormCapLedEntryAPI : Form
    {
        string APIUrl = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["APIUrl"]);
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LocalCon"].ConnectionString;
        string username = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["APIUser"]);// "administrator";
        string password = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["APIPassword"]);//"CMk95*@$46@";
        public FormCapLedEntryAPI()
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
                        var response = await client.GetAsync($"{APIUrl.TrimEnd('/')}/CapLedEntryAPI");
                        lblMsg.Text = "Getting Response from Server .....";
                        lblMsg.Refresh();
                        Application.DoEvents();

                        response.EnsureSuccessStatusCode();
                        string json = await response.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var itemsResponse = JsonSerializer.Deserialize<CapLedEntryResponse>(json, options);
                        lblMsg.Text = "Processing Server Response .....";
                        lblMsg.Refresh();
                        Application.DoEvents();

                        int iOrders = itemsResponse.Entries.Count;
                        Application.DoEvents();
                        progressBar1.Style = ProgressBarStyle.Continuous;
                        progressBar1.Maximum = iOrders;
                        progressBar1.Value = 1;
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            if (conn.State == ConnectionState.Closed) conn.Open();
                            int iRecord = 0;
                            foreach (var order in itemsResponse.Entries)
                            {
                                string InsertStatement = @"if NOT Exists(select [DocumentNo] From CapLedEntryAPI Where [DocumentNo]=@DocumentNo) BEGIN 
                                                INSERT INTO [CapLedEntryAPI]([DocumentNo],[OperationNo],[WorkCenterGroupCode],[NetWeight]
                                                                    ,[GrossWeight],OutputQuantity) 
                                     OUTPUT INSERTED.RowID 
                                                VALUES (@DocumentNo,@OperationNo,@WorkCenterGroupCode,@NetWeight,@GrossWeight,@OutputQuantity);
                                    END
                                    ELSE BEGIN select ROWID From CapLedEntryAPI Where [DocumentNo]=@DocumentNo END";

                                using (SqlCommand cmd = new SqlCommand(InsertStatement, conn))
                                {
                                    cmd.Parameters.AddWithValue("@DocumentNo", order.DocumentNo);
                                    cmd.Parameters.AddWithValue("@OperationNo", order.OperationNo);
                                    cmd.Parameters.AddWithValue("@WorkCenterGroupCode", order.WorkCenterGroupCode);
                                    cmd.Parameters.AddWithValue("@NetWeight", order.NetWeight);
                                    cmd.Parameters.AddWithValue("@GrossWeight", order.GrossWeight);
                                    cmd.Parameters.AddWithValue("@OutputQuantity", order.OutputQuantity);
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

    public class CapLedEntryResponse
    {
        [JsonPropertyName("@odata.context")]
        public string ODataContext { get; set; }

        [JsonPropertyName("value")]
        public List<CapLedEntryAPI> Entries { get; set; }
    }

    public class CapLedEntryAPI
    {
        [JsonPropertyName("@odata.etag")]
        public string ODataEtag { get; set; }
        public string DocumentNo { get; set; }
        public string OperationNo { get; set; }
        public string WorkCenterGroupCode { get; set; }
        public decimal NetWeight { get; set; }
        public decimal GrossWeight { get; set; }
        public decimal OutputQuantity { get; set; }
    }

}
