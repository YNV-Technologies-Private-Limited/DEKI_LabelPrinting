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
    public partial class FormItemsSync : Form
    {
        string APIUrl = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["APIUrl"]);
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LocalCon"].ConnectionString;
        string username = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["APIUser"]);// "administrator";
        string password = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["APIPassword"]);//"CMk95*@$46@";
        public FormItemsSync()
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
                        var response = await client.GetAsync($"{APIUrl.TrimEnd('/')}/ItemListVendorPortal");
                        lblMsg.Text = "Getting Response from Server .....";
                        lblMsg.Refresh();
                        Application.DoEvents();


                        response.EnsureSuccessStatusCode();
                        string json = await response.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var itemsResponse = JsonSerializer.Deserialize<ItemListVendorPortalResponse>(json, options);
                        lblMsg.Text = "Processing Server Response .....";
                        lblMsg.Refresh();
                        Application.DoEvents();

                        int iItems = itemsResponse.Items.Count;
                        Application.DoEvents();
                        progressBar1.Style = ProgressBarStyle.Continuous;
                        progressBar1.Maximum = iItems;
                        progressBar1.Value = 1;
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            if (conn.State == ConnectionState.Closed) conn.Open();
                            int iRecord = 0;
                            foreach (var item in itemsResponse.Items)
                            {
                                string InsertStatement = @"if NOT Exists(select [No] From Items Where [No]=@No) BEGIN 
                                                INSERT INTO [Items]([No],[No_2],[Description],[Description_2],[Item_Category_Code]
                                                    ,[Item_Type],[Item_Tracking_Code],[Alternative_Item_No],[Base_Unit_of_Measure],[Sales_Unit_of_Measure]
                                                 ,[Purch_Unit_of_Measure],[GST_Credit],[GST_Group_Code],[HSN_SAC_Code],[Gen_Prod_Posting_Group],[Inventory_Posting_Group],[Net_Weight]) 
                    OUTPUT INSERTED.RowID 
                                                VALUES (@No,@No_2,@Description,@Description_2,@Item_Category_Code,@Item_Type,@Item_Tracking_Code
                                                    ,@Alternative_Item_No,@Base_Unit_of_Measure,@Sales_Unit_of_Measure,@Purch_Unit_of_Measure,@GST_Credit
                                                    ,@GST_Group_Code,@HSN_SAC_Code,@Gen_Prod_Posting_Group,@Inventory_Posting_Group,@Net_Weight);
                                                END
                                    ELSE BEGIN select ROWID From Items Where [No]=@No END";

                                using (SqlCommand cmd = new SqlCommand(InsertStatement, conn))
                                {
                                    cmd.Parameters.AddWithValue("@No", item.No);
                                    cmd.Parameters.AddWithValue("@No_2", item.No_2);
                                    cmd.Parameters.AddWithValue("@Description", item.Description);
                                    cmd.Parameters.AddWithValue("@Description_2", item.Description_2);
                                    cmd.Parameters.AddWithValue("@Item_Category_Code", item.Item_Category_Code);
                                    cmd.Parameters.AddWithValue("@Item_Type", item.Item_Type);
                                    cmd.Parameters.AddWithValue("@Item_Tracking_Code", item.Item_Tracking_Code);
                                    cmd.Parameters.AddWithValue("@Alternative_Item_No", item.Alternative_Item_No);
                                    cmd.Parameters.AddWithValue("@Base_Unit_of_Measure", item.Base_Unit_of_Measure);
                                    cmd.Parameters.AddWithValue("@Sales_Unit_of_Measure", item.Sales_Unit_of_Measure);
                                    cmd.Parameters.AddWithValue("@Purch_Unit_of_Measure", item.Purch_Unit_of_Measure);
                                    cmd.Parameters.AddWithValue("@GST_Credit", item.GST_Credit);
                                    cmd.Parameters.AddWithValue("@GST_Group_Code", item.GST_Group_Code);
                                    cmd.Parameters.AddWithValue("@HSN_SAC_Code", item.HSN_SAC_Code);
                                    cmd.Parameters.AddWithValue("@Gen_Prod_Posting_Group", item.Gen_Prod_Posting_Group);
                                    cmd.Parameters.AddWithValue("@Inventory_Posting_Group", item.Inventory_Posting_Group);
                                    cmd.Parameters.AddWithValue("@Net_Weight", item.Net_Weight);
                                    Console.WriteLine($"Item Record ID:-  {cmd.ExecuteScalar()}");
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Sync Items", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }

    public class ItemListVendorPortalResponse
    {
        [JsonPropertyName("@odata.context")]
        public string ODataContext { get; set; }

        [JsonPropertyName("value")]
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        [JsonPropertyName("@odata.etag")]
        public string ODataEtag { get; set; }

        public string No { get; set; }
        public string No_2 { get; set; }
        public string Description { get; set; }
        public string Description_2 { get; set; }
        public string Item_Category_Code { get; set; }
        public string Item_Type { get; set; }
        public string Item_Tracking_Code { get; set; }
        public string Alternative_Item_No { get; set; }
        public string Base_Unit_of_Measure { get; set; }
        public string Sales_Unit_of_Measure { get; set; }
        public string Purch_Unit_of_Measure { get; set; }
        public string GST_Credit { get; set; }
        public string GST_Group_Code { get; set; }
        public string HSN_SAC_Code { get; set; }
        public string Gen_Prod_Posting_Group { get; set; }
        public string Inventory_Posting_Group { get; set; }

        public decimal Net_Weight { get; set; }
    }

}
