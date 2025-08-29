using DEKI_LabelPrinting.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace DEKI_LabelPrinting
{
    public partial class FormWending : Form
    {
        string APIUrl = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["APIUrl"]);
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LocalCon"].ConnectionString;
        string username = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["APIUser"]);// "administrator";
        string password = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["APIPassword"]);//"CMk95*@$46@";
        int dataSplitIndex = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["dataSplitIndex"]);
        DateTime ProdOrderCutOffDate = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["ProdOrderCutOffDate"]);
        List<RelProdOrder> List_ProductionOrders = new List<RelProdOrder>();
        List<RoutingLine> List_RoutingLine = new List<RoutingLine>();
        List<string> ProductionOrderNos = new List<string>();
        bool FormLoadEventCompleted = false;
        SerialPort _serialPort = new SerialPort();
        long Entry_No = 0; string DocumentNo = string.Empty;
        List<GridItemClass> PktsList = new List<GridItemClass>();
        List<CapLedEntryModel> List_CapLedEntryModel = new List<CapLedEntryModel>();

        double tolerancePercent = 10.0;
        public string ProdOrders
        {
            get
            {
                return "RelProdOrders";
            }
        }
        public string RoutingLines
        {
            get
            {
                return "RoutingLines";
            }
        }

        public string UpdateCLE { get { return "UpdateCLE"; } }
        public FormWending()
        {
            InitializeComponent();
            this.FormClosing += FormWending_FormClosing;
        }

        private void FormWending_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                timerGetWeight.Stop();
                timerGetWeight.Dispose();
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                if (null != _serialPort)
                {
                    _serialPort.Dispose();
                }
            }
            catch { }
        }

        private void OpenSerialPort()
        {
            try
            {
                _serialPort.PortName = Convert.ToString(ConfigurationManager.AppSettings["PortName"]); // Change to your COM port
                                                                                                       //_serialPort.BaudRate = 9600;
                _serialPort.BaudRate = Convert.ToInt32(ConfigurationManager.AppSettings["BaudRate"]); ;
                _serialPort.Parity = Parity.None;
                _serialPort.DataBits = Convert.ToInt32(ConfigurationManager.AppSettings["DataBits"]); //8;
                _serialPort.StopBits = StopBits.One;
                _serialPort.Handshake = Handshake.None;
                _serialPort.Encoding = System.Text.Encoding.ASCII;

                //_serialPort.DataReceived += SerialPort_DataReceived;


                _serialPort.Open();
                timerGetWeight.Enabled = true;   // start timer
                writeLog($"SerialPort Open Successfully");
            }
            catch (Exception ex)
            {
                timerGetWeight.Enabled = false;  // stop timer
                if ((_serialPort != null) && (_serialPort.IsOpen)) { _serialPort.Close(); }
                writeLog($"Error in Open Serial Port {ex.Message}");
                MessageBox.Show($"Error opening serial port: {ex.Message}");
            }
        }
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                writeLog($"SerialPort_DataReceived - {_serialPort.ReadLine()}");
                string data = _serialPort.ReadLine(); // Or ReadExisting()
                this.Invoke((MethodInvoker)(() =>
                {
                    if (data.Contains(',')) data = data.Split(',')[dataSplitIndex];

                    lblWeight.Text = data.Trim(); // Display on UI thread
                }));
            }
            catch (Exception ex)
            {
                writeLog($"SerialPort_DataReceived Error - {ex.Message}");
                this.Invoke((MethodInvoker)(() =>
                {
                    MessageBox.Show("Error reading data: " + ex.Message);
                }));
            }
        }

        void writeLog(string message)
        {
            try
            {
                if (!File.Exists($"Log_{DateTime.Today.ToString("ddMMMyyyy")}"))
                {
                    File.Create($"Log_{DateTime.Today.ToString("ddMMMyyyy")}").Dispose();
                }
                File.WriteAllText($"Log_{DateTime.Today.ToString("ddMMMyyyy")}", message);
            }
            catch (Exception ex) { }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void FormWending_Load(object sender, EventArgs e)
        {
            PktsList = new List<GridItemClass>();
            if (string.IsNullOrEmpty($"{APIUrl.TrimEnd('/')}/{ProdOrders}"))
            {
                MessageBox.Show("API Url is not configured.\nContact application administrator.", "Config Missing", MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
                return;
            }
            try
            {
                ProdOrderCutOffDate = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["ProdOrderCutOffDate"]);
                LoadProductionNos(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in Loading Details from ERP.\n\n{ex.Message}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            OpenSerialPort();

            LoadCapLedEntries();

            FormLoadEventCompleted = true;
        }
        void LoadProductionNos(bool IsForceLoad)
        {
            ProgressForm progressForm = new ProgressForm();
            Task.Run(async () =>
            {
                await LoadProductionOrderNos(IsForceLoad);
                await LoadRoutingLines();
                //pf.Invoke(() => pf.Close());
                progressForm.Invoke(new Action(() => { progressForm.Close(); }));

                bindControl();
            });
            progressForm.ShowDialog();
        }
        void bindControl()
        {
            cbProductionNo.Invoke((Action)(() =>
            {
                cbProductionNo.DataSource = List_ProductionOrders;
                cbProductionNo.DisplayMember = "No";     // if object list
                cbProductionNo.ValueMember = "No";         // if needed

                //cbProductionNo.ValueMember = "No";
                //cbProductionNo.DisplayMember = "No";

                var autoSource = new AutoCompleteStringCollection();
                autoSource.AddRange(ProductionOrderNos.ToArray());

                cbProductionNo.AutoCompleteCustomSource = autoSource;
                cbProductionNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbProductionNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }));
        }

        async Task LoadProductionOrderNos(bool IsForceLoad)
        {
            List_ProductionOrders = new List<RelProdOrder>();
            RelProdOrder prodOrder = new RelProdOrder() { No = "--Select--", Quantity = 0, Source_No = "", Status = "Dummy", Description = "" };
            List_ProductionOrders.Add(prodOrder);

            //if (!IsForceLoad)
            //{
            //    try
            //    {
            //        ProductionOrderNos = new List<string>();
            //        using (SqlConnection conn = new SqlConnection(connectionString))
            //        {
            //            string InsertStatement = @"select [Status],[No],[Description],[Source_No],[Routing_No]
            //                                                        ,[Quantity],[Due_Date],[Assigned_User_ID],Creation_Date,LotNo
            //                                    From ProductionOrder Where ([IsCompleted]=0 OR [IsCompleted] IS NULL) AND Due_Date>=@Due_Date";

            //            using (SqlCommand cmd = new SqlCommand(InsertStatement, conn))
            //            {
            //                cmd.Parameters.AddWithValue("@Due_Date", ProdOrderCutOffDate.ToString("dd-MMM-yyyy"));
            //                if (conn.State == ConnectionState.Closed) conn.Open();
            //                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //                DataTable dt = new DataTable();
            //                adapter.Fill(dt);
            //                foreach (DataRow dr in dt.Rows)
            //                {
            //                    RelProdOrder order = new RelProdOrder()
            //                    {
            //                        Status = Convert.ToString(dr["Status"]),
            //                        No = Convert.ToString(dr["No"]),
            //                        Description = Convert.ToString(dr["Description"]),
            //                        Source_No = Convert.ToString(dr["Source_No"]),
            //                        Routing_No = Convert.ToString(dr["Routing_No"]),
            //                        Quantity = Convert.ToInt32(dr["Quantity"]),
            //                        Due_Date = Convert.ToDateTime(dr["Due_Date"]),
            //                        Creation_Date = Convert.ToDateTime(dr["Creation_Date"]),
            //                        Assigned_User_ID = Convert.ToString(dr["Assigned_User_ID"]),
            //                        LOTNo= Convert.ToString(dr["LotNo"])
            //                    };
            //                    List_ProductionOrders.Add(order);
            //                    ProductionOrderNos.Add(order.No);
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception exp) { MessageBox.Show(exp.Message); }
            //}
            //else
            //{
                var handler = new HttpClientHandler
                {
                    Credentials = new System.Net.NetworkCredential(username, password) // domain is optional
                };
                using (HttpClient client = new HttpClient(handler))
                {
                    try
                    {
                        var response = await client.GetAsync($"{APIUrl.TrimEnd('/')}/{ProdOrders}");
                        string json = await response.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(json))
                        {
                            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                            var pOrders = JsonSerializer.Deserialize<ODataResponse<RelProdOrder>>(json, options);
                            int iRecord = 0;

                            foreach (var item in pOrders.Value)
                            {
                                if (item is object)
                                {
                                    RelProdOrder order = (RelProdOrder)item;
                                    //if (order.Status.Equals("Released", StringComparison.CurrentCultureIgnoreCase))
                                    //{
                                    //    using (SqlConnection conn = new SqlConnection(connectionString))
                                    //    {
                                    //        if (conn.State == ConnectionState.Closed) conn.Open();

                                    //        string InsertStatement = @"if NOT Exists(select [No] From ProductionOrder Where [No]=@No) BEGIN 
                                    //            INSERT INTO [ProductionOrder]([Status],[No],[Description],[Source_No],[Routing_No]
                                    //                                ,[Quantity],[Due_Date],[Assigned_User_ID],Creation_Date,LOTNo) 
                                    // OUTPUT INSERTED.RowID 
                                    //            VALUES (@Status,@No,@Description,@Source_No,@Routing_No
                                    //                ,@Quantity,@Due_Date,@Assigned_User_ID,@Creation_Date,@LOTNo);
                                    //END
                                    //ELSE BEGIN Update ProductionOrder SET [Description]=@Description,[Source_No]=@Source_No,[Routing_No]=@Routing_No
                                    //           ,[Quantity]=@Quantity,[Due_Date]=@Due_Date,[Assigned_User_ID]=@Assigned_User_ID Where [No]=@No END";

                                    //        using (SqlCommand cmd = new SqlCommand(InsertStatement, conn))
                                    //        {
                                    //            cmd.Parameters.AddWithValue("@No", order.No);
                                    //            cmd.Parameters.AddWithValue("@Status", order.Status);
                                    //            cmd.Parameters.AddWithValue("@Description", order.Description);
                                    //            cmd.Parameters.AddWithValue("@Source_No", order.Source_No);
                                    //            cmd.Parameters.AddWithValue("@Routing_No", order.Routing_No);
                                    //            cmd.Parameters.AddWithValue("@Quantity", order.Quantity);
                                    //            cmd.Parameters.AddWithValue("@Due_Date", order.Due_Date);
                                    //            cmd.Parameters.AddWithValue("@Assigned_User_ID", order.Assigned_User_ID);
                                    //            cmd.Parameters.AddWithValue("@Creation_Date", order.Creation_Date);
                                    //            cmd.Parameters.AddWithValue("@LOTNo", order.LOTNo);
                                    //            Console.WriteLine($"Order Record ID:-  {cmd.ExecuteScalar()}");
                                    //        }
                                    //        iRecord += 1;
                                    //        Application.DoEvents();
                                    //    }
                                        List_ProductionOrders.Add(order);
                                        ProductionOrderNos.Add(order.No);
                                    //}
                                }
                            }
                            //LoadProductionOrderNos(false);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            //}
        }

        async Task LoadRoutingLines()
        {
            //var handler = new HttpClientHandler
            //{
            //    Credentials = new System.Net.NetworkCredential(username, password) // domain is optional
            //};
            //using (HttpClient client = new HttpClient(handler)) {
            try
            {
                //var response = await client.GetAsync($"{APIUrl.TrimEnd('/')}/{RoutingLines}");
                //string json = await response.Content.ReadAsStringAsync();
                //if (!string.IsNullOrEmpty(json)) {

                //var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                //var pOrders = JsonSerializer.Deserialize<ODataResponse<RoutingLine>>(json, options);
                //foreach (var item in pOrders.Value) {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string InsertStatement = @"SELECT Routing_No, [Operation_No] ,[Work_Center_Group_Code] FROM [RoutingLine]  order by Routing_No";

                    using (SqlCommand cmd = new SqlCommand(InsertStatement, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (null != dt)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                string _Routing_No = Convert.ToString(dr["Routing_No"]);
                                string _Operation_No = Convert.ToString(dr["Operation_No"]);
                                string _Work_Center_Group_Code = Convert.ToString(dr["Work_Center_Group_Code"]);
                                RoutingLine routingLine = new RoutingLine()
                                {
                                    Work_Center_Group_Code = _Work_Center_Group_Code,
                                    Routing_No = _Routing_No,
                                    Operation_No = _Operation_No,
                                };
                                List_RoutingLine.Add(routingLine);
                            }
                        }
                    }
                }
                //}
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            //}
        }

        async Task LoadCapLedEntries()
        {
            var handler = new HttpClientHandler
            {
                Credentials = new System.Net.NetworkCredential(username, password)
            };
            using (HttpClient client = new HttpClient(handler)) {
                try
                {
                    var response = await client.GetAsync($"{APIUrl.TrimEnd('/')}/CapLedEntryAPI");
                    string json = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(json))
                    {
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var entries = JsonSerializer.Deserialize<ODataResponse<CapLedEntryModel>>(json, options);
                        List_CapLedEntryModel = new List<CapLedEntryModel>();
                        foreach (var entry in entries.Value)
                        {
                            List_CapLedEntryModel.Add(entry);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void pbRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                cbProductionNo.Enabled = cbWorkCenterGroup.Enabled = true;
                LoadProductionNos(true);
                PktsList = new List<GridItemClass>();
                cbWorkCenterGroup.Enabled = true;
                txtQuantity.Text = lblNetWeight.Text = lblItemWeight.Text = "0.00";
                dgvWeight.DataSource = PktsList;
                txtItemNo.Text = txtProductionDate.Text = txtRoutingNo.Text = txtLotNo.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbProductionNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FormLoadEventCompleted) return;

            if (cbProductionNo.SelectedIndex <= 0) return;

            RelProdOrder relProdOrder = (RelProdOrder)cbProductionNo.SelectedItem;
            if ((null != relProdOrder))
            {
                cbWorkCenterGroup.Enabled = txtLotNo.Enabled = cbWorkCenterGroup.Enabled = txtItemNo.Enabled = true;
                //txtItemNo.Text = relProdOrder.Description;
                //txtItemNo.Enabled = false;
                //txtLotNo.BackColor = Color.White;
                txtItemNo.Text = relProdOrder.Source_No;
                txtItemNo.Enabled = false;
                txtRoutingNo.Text = relProdOrder.Routing_No;
                txtRoutingNo.Enabled = false;
                txtProductionDate.Text = relProdOrder.Creation_Date.ToString("dd/MM/yyyy");
                txtProductionDate.Enabled = false;
                //txtQuantity.Text = Convert.ToString(relProdOrder.Quantity);
                //txtQuantity.Enabled = false;
                txtLotNo.Text = relProdOrder.LOTNo;
                txtLotNo.Enabled = false;

                txtRoutingNo.BackColor = txtOperationNo.BackColor = txtProductionDate.BackColor = txtQuantity.BackColor = txtItemNo.BackColor = txtLotNo.BackColor = Color.White;
                txtRoutingNo.ForeColor = txtOperationNo.ForeColor = txtProductionDate.ForeColor = Color.Black;
                getItemNetWeight(relProdOrder.Source_No);

                //List<string> routings=from c as List_RoutingLine.ase
                //txtWorkCenterGroup.Text=relProdOrder.
                BindWorkCenterGroup();
            }
        }
        decimal getOrderNoQty()
        {
            decimal Qty = 0;
            if (cbWorkCenterGroup.SelectedIndex <= 0) { return 0; }
            if (List_CapLedEntryModel.Count <= 0) { return 0; }

            //get Qty from CapLedEntryAPI table on basis of [DocumentNo] and [OperationNo]
            var vrOututQty = List_CapLedEntryModel.Where(r => r.OrderNo == cbProductionNo.Text
            && r.WorkCenterGroupCode==cbWorkCenterGroup.Text);
            if (null != vrOututQty)
            {
                if (vrOututQty.Count() > 0)
                {
                    CapLedEntryModel obj=(CapLedEntryModel)vrOututQty.FirstOrDefault();
                    //txtQuantity.Text = getOrderNoQty().ToString();

                    this.Entry_No = obj.Entry_No;
                    DocumentNo = obj.DocumentNo;
                    txtQuantity.Text = obj.OutputQuantity.ToString();
                    txtQuantity.Enabled = false;
                }
            }


            //Check if user has already completed the Packing 
            //string chkPkg = @"SELECT 'Result' = [OutputQuantity] FROM [CapLedEntryAPI] Where OrderNo=@OrderNo and WorkCenterGroupCode=@WorkCenterGroupCode";
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    try
            //    {
            //        using (SqlCommand command = new SqlCommand(chkPkg, connection))
            //        {
            //            command.Parameters.AddWithValue("@OrderNo", cbProductionNo.Text);
            //            command.Parameters.AddWithValue("@WorkCenterGroupCode", cbWorkCenterGroup.Text);
            //            command.CommandType = System.Data.CommandType.Text;
            //            if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
            //            Qty = Convert.ToDecimal(command.ExecuteScalar());
            //        }
            //    }
            //    catch { }
            //}
            return Qty;
        }
        void BindWorkCenterGroup()
        {
            bool pkgCompleted = false;
            //Check if user has already completed the Packing 
            string chkPkg = @"SELECT 'Result' = Count([WorkCenterGroup]) FROM [tbl_ProdOrder_WorkCenterGroup] Where Order_No=@Order_No AND [IsCompleted]=1 AND [WorkCenterGroup]='Packing'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(chkPkg, connection))
                    {
                        command.Parameters.AddWithValue("@Order_No", cbProductionNo.Text);
                        command.CommandType = System.Data.CommandType.Text;
                        if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
                        pkgCompleted = Convert.ToBoolean(command.ExecuteScalar());
                    }
                }
                catch { }
            }
            if (pkgCompleted)
            {
                MessageBox.Show($"{cbProductionNo.Text} already been completed.\nSelect another Order No to continue.", ""
                , MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbProductionNo.SelectedIndex = 0;
                cbWorkCenterGroup.DataSource = null;
                return;
            }

            //Check if Work Center Group is in DB Table -> tbl_ProdOrder_WorkCenterGroup
            string cmdStr = @"With tbl as(SELECT [WorkCenterGroup]='--Select--',[RowNo]=0 UNION 
                    SELECT [WorkCenterGroup],[RowNo]=(SELECT [Operation_No] FROM [RoutingLine]
                                Where Work_Center_Group_Code=[tbl_ProdOrder_WorkCenterGroup].[WorkCenterGroup]
                And [Routing_No]=@Routing_No)
FROM [tbl_ProdOrder_WorkCenterGroup] Where Order_No=@Order_No AND [IsCompleted]=0) Select WorkCenterGroup, RowNo From tbl Order by RowNo";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(cmdStr, connection))
                    {
                        command.Parameters.AddWithValue("@Order_No", cbProductionNo.Text);
                        command.Parameters.AddWithValue("@Routing_No", txtRoutingNo.Text);
                        command.CommandType = System.Data.CommandType.Text;
                        if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        cbWorkCenterGroup.DataSource = null;
                        if (dt.Rows.Count > 1)
                        {
                            cbWorkCenterGroup.DataSource = dt;
                            cbWorkCenterGroup.DisplayMember = "WorkCenterGroup";
                        }
                        else
                        {
                            var matchedRoutes = List_RoutingLine.Where(r => r.Routing_No == txtRoutingNo.Text).ToList().Distinct().ToList();
                            //cbWorkCenterGroup.DataSource = matchedRoutes;
                            //cbWorkCenterGroup.DisplayMember = "Work_Center_Group_Code";
                            InsertOrderWorkCenterGroup(matchedRoutes);
                            BindWorkCenterGroup();
                        }
                        cbWorkCenterGroup.DropDownStyle = ComboBoxStyle.DropDownList;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open) connection.Close();
                }
            }
        }
        void InsertOrderWorkCenterGroup(List<RoutingLine> Routings)
        {
            for (int i = 0; i < Routings.Count; i++)
            {
                string cmdStr = @"if NOT Exists(select [RowID] From tbl_ProdOrder_WorkCenterGroup Where [Order_No]=@Order_No And WorkCenterGroup=@WorkCenterGroup) 
                                    BEGIN 
                                        INSERT INTO [tbl_ProdOrder_WorkCenterGroup]([Order_No],[WorkCenterGroup],[Created],[CreatedBy])
                                        VALUES (@Order_No,@WorkCenterGroup,GetDate(),@CreatedBy);
                                    END
                                    ELSE BEGIN select ROWID From tbl_ProdOrder_WorkCenterGroup Where [Order_No]=@Order_No And WorkCenterGroup=@WorkCenterGroup END";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        var routingLine = Routings[i];
                        using (SqlCommand command = new SqlCommand(cmdStr, connection))
                        {
                            command.Parameters.AddWithValue("@Order_No", cbProductionNo.Text);
                            command.Parameters.AddWithValue("@WorkCenterGroup", routingLine.Work_Center_Group_Code);
                            command.Parameters.AddWithValue("@CreatedBy", DEKI_LabelPrinting.Model.User.USER_NAME);
                            command.CommandType = System.Data.CommandType.Text;
                            if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
                            int iRow = command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (connection.State == System.Data.ConnectionState.Open) connection.Close();
                    }
                }
            }
        }

        void getItemNetWeight(string ItemNo)
        {
            string cmdStr = @"SELECT [Net_Weight] FROM [Items] Where [No]=@NO";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(cmdStr, connection))
                    {
                        command.Parameters.AddWithValue("@NO", ItemNo);
                        command.CommandType = System.Data.CommandType.Text;
                        if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
                        lblItemWeight.Text = Convert.ToString(command.ExecuteScalar());
                        double ItemGrodsWeight = 0;
                        double Qty = 0;
                        if (double.TryParse(lblItemWeight.Text, out ItemGrodsWeight))
                        {
                            if (double.TryParse(txtQuantity.Text, out Qty))
                            {
                                double grams = 0;
                                try
                                {
                                    grams = (ItemGrodsWeight * Qty);
                                    double kilograms = grams / 1000;
                                    lblGrossWeight.Text = kilograms.ToString();
                                    Application.DoEvents();
                                    lblGrossWeight.Refresh();
                                }
                                catch { }
                                finally
                                {
                                    Application.DoEvents();
                                    Application.DoEvents();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open) connection.Close();
                }
            }
        }

        private void cbProductionNo_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Tab) || (e.KeyCode == Keys.Enter))
            {
                if (cbProductionNo.Text.Contains("--Select--")) return;
                try
                {
                    RelProdOrder relProdOrder = (RelProdOrder)cbProductionNo.SelectedItem;
                    if ((null != relProdOrder))
                    {
                        txtItemNo.Text = relProdOrder.Source_No;
                        txtLotNo.Text = relProdOrder.LOTNo;
                        txtRoutingNo.Text = relProdOrder.Routing_No;
                        
                        BindWorkCenterGroup();
                        //var matchedRoutes = List_RoutingLine.Where(r => r.Routing_No == txtRoutingNo.Text).Distinct().ToList();
                        //cbWorkCenterGroup.DataSource = null;
                        //cbWorkCenterGroup.DataSource = matchedRoutes;
                        //cbWorkCenterGroup.DisplayMember = "Work_Center_Group_Code";
                        //cbWorkCenterGroup.Items.Insert(0, "-- Select --");
                        //cbWorkCenterGroup.DropDownStyle = ComboBoxStyle.DropDownList;
                    }
                }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        double prNetWeight = 0;
        private void btnGetData_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtLotNo.Text)) { MessageBox.Show("Order LOT No can not be blank."); return; }
                if (cbProductionNo.SelectedIndex <= 0) { MessageBox.Show("Select Production Order No to continue."); return; }
                if (cbWorkCenterGroup.SelectedIndex <= 0) { MessageBox.Show("Select Work center group to continue."); cbWorkCenterGroup.Enabled = true; cbWorkCenterGroup.Focus(); return; }
                if (string.IsNullOrEmpty(txtQuantity.Text)) { MessageBox.Show("Order Quanity must be greater than 0."); return; }
                if (!string.IsNullOrEmpty(txtQuantity.Text))
                {
                    cbWorkCenterGroup.Enabled = false;
                    int Qty = 0;
                    if (int.TryParse(txtQuantity.Text, out Qty))
                    {
                        if (Qty <= 0)
                        {
                            MessageBox.Show("Order Quanity must be greater than 0."); return;
                        }
                    }
                }
                double iWeight = 0;
                double.TryParse(lblWeight.Text, out iWeight);

                double iGrossWeight = 0;
                double.TryParse(lblGrossWeight.Text, out iGrossWeight);
                double toleranceValue = (iGrossWeight * tolerancePercent) / 100.0;

                double minWeight = iGrossWeight - toleranceValue;
                double maxWeight = iGrossWeight + toleranceValue;

                //if (iWeight > iGrossWeight)
                if (iWeight > maxWeight)
                {
                    MessageBox.Show($"Packet Weight '{iWeight}' can not be more then Gross Weight {iGrossWeight}.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                timerGetWeight.Enabled = false;
                txtItemNo.Enabled = cbWorkCenterGroup.Enabled = txtLotNo.Enabled = false;
                int SrnO = PktsList.Count + 1;
                double NetWeight = 0;
                //foreach (GridItemClass pkt in PktsList)
                //{
                //    NetWeight += pkt.PacketWeight;
                //}
                ////decimal.TryParse(lblNetWeight.Text, out NetWeight);
                //if (NetWeight > iGrossWeight)
                //{
                //    MessageBox.Show($"Net Weight '{NetWeight}' can not be more then Gross Weight {iGrossWeight}.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                
                if (iWeight > 0)
                {
                    PktsList.Add(new GridItemClass { SrNo = SrnO, PacketWeight = iWeight });
                    foreach (GridItemClass pkt in PktsList)
                    {
                        NetWeight += pkt.PacketWeight;
                    }
                    if (NetWeight > iGrossWeight)
                    {
                        MessageBox.Show($"Net Weight '{NetWeight}' can not be more then Gross Weight {iGrossWeight}.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        PktsList.RemoveAt(SrnO - 1);
                        lblNetWeight.Text = prNetWeight.ToString();
                        return;
                    }
                    InsertPackingLine(iWeight);
                }
                dgvWeight.DataSource = null;
                dgvWeight.DataSource = PktsList;
                dgvWeight.Columns[0].HeaderText = "Sr. No";
                dgvWeight.Columns[1].HeaderText = "Weight";
                dgvWeight.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvWeight.Refresh();

                cbWorkCenterGroup.Enabled = false;
                lblNetWeight.Text = NetWeight.ToString();
                prNetWeight = NetWeight;
                cbProductionNo.Enabled = false;
                lblNetWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                timerGetWeight.Enabled = true;
            }
        }

        long HeaderRowID = 0;
        void InsertPackingLine(double PktWeight)
        {
            string cmdStr = @"if NOT Exists(select [RowID] From tbl_Packing_Header Where [Production_No]=@Production_No And ROUTING_NO=@Routing_No
                                                AND Operation_No=@Operation_No) 
                                    BEGIN
                                    INSERT INTO [tbl_Packing_Header]([Production_No],[Production_Date],[ROUTING_NO]
                                                                    ,[WORK_CENTER_GROUP],[Operation_No],[Quantity]
                                                                ,[Item_No],[Lot_No],[Item_netWeight],[Gross_Weight],[Created],[CreatedBy])
                          OUTPUT INSERTED.RowID 
                                    VALUES (@Production_No,@Production_Date,@ROUTING_NO,@WORK_CENTER_GROUP
                                                    ,@Operation_No,@Quantity,@Item_No,@Lot_No,@Item_netWeight,@Gross_Weight,GETDAte(),@CreatedBy)
                                    END
                                    ELSE BEGIN 
                                        Select ROWID From tbl_Packing_Header Where [Production_No]=@Production_No And ROUTING_NO=@Routing_No
                                                AND Operation_No=@Operation_No END";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(cmdStr, connection))
                    {
                        #region --Add Parameters ---

                        string[] drDate = txtProductionDate.Text.Split('/');
                        if (Convert.ToString(drDate[2]).Equals("0001"))
                        {
                            drDate[2] = "1900";
                        }
                        DateTime ProductionDate = DateTime.ParseExact($"{drDate[0]}/{drDate[1]}/{drDate[2]}", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //DateTime ProductionDate = DateTime.ParseExact(txtProductionDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        command.Parameters.AddWithValue("@Production_Date", ProductionDate);

                        command.Parameters.AddWithValue("@Production_No", cbProductionNo.Text);
                        command.Parameters.AddWithValue("@ROUTING_NO", txtRoutingNo.Text);
                        command.Parameters.AddWithValue("@WORK_CENTER_GROUP", cbWorkCenterGroup.Text);
                        command.Parameters.AddWithValue("@Operation_No", txtOperationNo.Text);
                        command.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
                        command.Parameters.AddWithValue("@Item_No", txtItemNo.Text);
                        command.Parameters.AddWithValue("@Lot_No", txtLotNo.Text);
                        command.Parameters.AddWithValue("@Item_netWeight", lblNetWeight.Text);
                        command.Parameters.AddWithValue("@Gross_Weight", lblGrossWeight.Text);
                        command.Parameters.AddWithValue("@CreatedBy", User.USER_NAME);
                        #endregion

                        command.CommandType = System.Data.CommandType.Text;
                        if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
                        HeaderRowID = Convert.ToInt64(command.ExecuteScalar());
                        if (HeaderRowID > 0)
                        {
                            string str = "INSERT INTO [tbl_Packing_Line]([Header_RowID],[Pkt_Weight],[Created],[CreatedBy])  VALUES(@Header_RowID,@Pkt_Weight,GetDate(),@CreatedBy)";
                            using (SqlCommand commandline = new SqlCommand(str, connection))
                            {
                                #region --Add Parameters --
                                commandline.Parameters.AddWithValue("@Header_RowID", HeaderRowID);
                                commandline.Parameters.AddWithValue("@Pkt_Weight", PktWeight);
                                commandline.Parameters.AddWithValue("@CreatedBy", User.USER_NAME);
                                #endregion

                                commandline.CommandType = System.Data.CommandType.Text;
                                commandline.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open) connection.Close();
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                decimal grossWeight = 0; // Example value
                decimal netWeight = 0;   // Example value
                decimal.TryParse(lblGrossWeight.Text, out grossWeight);
                decimal.TryParse(lblNetWeight.Text, out netWeight);

                bool isWithinTolerance = IsWithinTolerance(grossWeight, netWeight, 0.05m);
                string workCenterGroup = cbWorkCenterGroup.Text;
                bool isPacking = false;
                if (dgvWeight.Rows.Count > 0)
                {
                    if ((netWeight > 0) && (grossWeight > 0))
                    {
                        if (DialogResult.Yes == MessageBox.Show("Do you want to submit the weighing data to ERP?", "Confirmation", MessageBoxButtons.YesNo
                        , MessageBoxIcon.Question))
                        {
                            //if (isWithinTolerance)
                            //if (netWeight <= grossWeight)
                            //{
                            string cmdStr = @"Update [tbl_ProdOrder_WorkCenterGroup] SET [IsCompleted]=1  Where [Order_No]=@Order_No AND [WorkCenterGroup]=@WorkCenterGroup";

                            if (cbWorkCenterGroup.Text.Trim().Equals("Packing", StringComparison.CurrentCultureIgnoreCase))
                            {
                                cmdStr += "; Update [ProductionOrder] set IsCompleted=1 Where [No]=@Order_No;";
                                isPacking = true;
                            }
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                try
                                {
                                    using (SqlCommand command = new SqlCommand(cmdStr, connection))
                                    {
                                        #region --Add Parameters --
                                        command.Parameters.AddWithValue("@Order_No", cbProductionNo.Text);
                                        command.Parameters.AddWithValue("@WorkCenterGroup", cbWorkCenterGroup.Text);
                                        #endregion

                                        command.CommandType = System.Data.CommandType.Text;
                                        if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
                                        int iRowUpdate = Convert.ToInt32(command.ExecuteNonQuery());
                                        if (iRowUpdate > 0)
                                        {
                                            string str = "Update [tbl_Packing_Header] SET [IsCompleted]=1 Where RowID=@RowID";
                                            using (SqlCommand commandline = new SqlCommand(str, connection))
                                            {
                                                #region --Add Parameters --
                                                commandline.Parameters.AddWithValue("@RowID", HeaderRowID);
                                                #endregion
                                                commandline.CommandType = System.Data.CommandType.Text;
                                                commandline.ExecuteNonQuery();

                                                PktsList = new List<GridItemClass>();
                                                cbWorkCenterGroup.Enabled = true;

                                                Application.DoEvents();
                                                updateEPR();
                                                dgvWeight.DataSource = PktsList;
                                                lblNetWeight.Text = "0.00";
                                                Application.DoEvents();

                                                BindWorkCenterGroup();
                                                Application.DoEvents();
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    if (connection.State == System.Data.ConnectionState.Open) connection.Close();
                                    if (isPacking)
                                    {
                                        LoadProductionNos(false);
                                    }
                                }
                            }

                            //}
                            //else
                            //{
                            //MessageBox.Show(isWithinTolerance ? "Weight is within tolerance." : "Weight is out of tolerance.");
                            //MessageBox.Show($"{netWeight} is l","", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        static bool IsWithinTolerance(decimal gross, decimal net, decimal tolerance)
        {
            // Calculate the absolute difference
            decimal difference = Math.Abs(gross - net);

            // Calculate allowed difference based on tolerance percentage
            decimal allowedDifferenceGross = gross * tolerance;
            decimal allowedDifferenceNet = net * tolerance;

            // Pass if difference is within tolerance of EITHER value
            return difference <= allowedDifferenceGross || difference <= allowedDifferenceNet;
        }
        private void btnGetData_MouseHover(object sender, EventArgs e)
        {
            btnGetData.BackgroundImage = global::DEKI_LabelPrinting.Properties.Resources.dg;
            //btnGetData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            btnGetData.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        }

        private void btnGetData_MouseLeave(object sender, EventArgs e)
        {
            this.btnGetData.BackgroundImage = global::DEKI_LabelPrinting.Properties.Resources.lg;
            //this.btnGetData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGetData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void btnGetData_Enter(object sender, EventArgs e)
        {
            btnGetData_MouseHover(sender, e);
        }

        private void btnGetData_Leave(object sender, EventArgs e)
        {
            btnGetData_MouseLeave(sender, e);
        }

        private void cbWorkCenterGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtOperationNo.Text = string.Empty;
            txtQuantity.Text = "0";
            if (cbWorkCenterGroup.SelectedIndex <= 0) return;
            string cmdStr = @"SELECT [Operation_No] FROM [RoutingLine] Where Work_Center_Group_Code=@Work_Center_Group_Code And [Routing_No]=@Routing_No";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand(cmdStr, connection))
                        {
                            #region --Add Parameters --
                            command.Parameters.AddWithValue("@Work_Center_Group_Code", cbWorkCenterGroup.Text);
                            command.Parameters.AddWithValue("@Routing_No", txtRoutingNo.Text);
                            #endregion

                            command.CommandType = System.Data.CommandType.Text;
                            if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
                            txtOperationNo.Text = Convert.ToString(command.ExecuteScalar());
                            txtOperationNo.Enabled = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (connection.State == System.Data.ConnectionState.Open) connection.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            getOrderNoQty();
            getItemNetWeight(txtItemNo.Text);
        }

        private void FormWending_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnGetData_Click(sender, e);
            }
        }

        private void FormWending_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                btnSubmit_Click(sender, e);
            }
        }

        private void pbSettings_Click(object sender, EventArgs e)
        {
            //Settings
            FormItemsSync itemSync = new FormItemsSync();
            itemSync.ShowDialog();
        }

        private void pbSyncvOrders_Click(object sender, EventArgs e)
        {
            FormCapLedEntryAPI fnr_CapLedEntryAPI = new FormCapLedEntryAPI();
            //FormRelProdOrdersSync itemSync = new FormRelProdOrdersSync();
            fnr_CapLedEntryAPI.ShowDialog();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (dgvWeight.Rows.Count > 0)
            {
                if (DialogResult.Yes == MessageBox.Show("Do you want to Delete the Weighing Entries?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    PktsList = new List<GridItemClass>();
                    cbProductionNo.Enabled = txtItemNo.Enabled = cbWorkCenterGroup.Enabled = txtLotNo.Enabled = true;
                    dgvWeight.DataSource = null;
                    lblNetWeight.Text = "0.00";
                    if (HeaderRowID > 0)
                    {
                        string cmdStr = @"Delete From tbl_Packing_Line Where Header_RowID=@Header_RowID;
                                            Delete from tbl_Packing_Header Where ROWID=@Header_RowID";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            try
                            {
                                using (SqlCommand command = new SqlCommand(cmdStr, connection))
                                {
                                    #region --Add Parameters --
                                    command.Parameters.AddWithValue("@Header_RowID", HeaderRowID);
                                    #endregion

                                    command.CommandType = System.Data.CommandType.Text;
                                    if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
                                    command.ExecuteNonQuery();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (connection.State == System.Data.ConnectionState.Open) connection.Close();
                            }
                        }
                    }
                }
            }
        }

        async Task updateEPR()
        {
            var handler = new HttpClientHandler
            {
                Credentials = new System.Net.NetworkCredential(username, password) // domain is optional
            };
            using (HttpClient client = new HttpClient(handler))
            {
                try
                {
                    var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    UpdateLedEntryModel entry = new UpdateLedEntryModel()
                    {
                         OrderNo = cbProductionNo.Text
                        , DocumentNo = this.DocumentNo
                        , OperationNo = txtOperationNo.Text
                        , WorkCenterGroupCode = cbWorkCenterGroup.Text
                        , GrossWeight = Convert.ToDecimal(lblGrossWeight.Text)
                        , NetWeight = Convert.ToDecimal(lblNetWeight.Text)
                        ,OutputQuantity=Convert.ToInt32(txtQuantity.Text)
                    };
                    var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(entry), Encoding.UTF8, "application/json");
                    ////var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync($"{APIUrl.TrimEnd('/')}/{UpdateCLE}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Record inserted in ERP successfully!");
                    }
                    else
                    {
                        MessageBox.Show($"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}", "", MessageBoxButtons.OK);
                    }
                }
                catch { }
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtQuantity.Text))
            {
                double Qty = 0;
                if (double.TryParse(txtQuantity.Text, out Qty))
                {
                    getItemNetWeight(txtItemNo.Text);
                }
            }
        }

        private void timerGetWeight_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_serialPort != null && _serialPort.IsOpen && _serialPort.BytesToRead > 0)
                {
                    string data = _serialPort.ReadExisting();  // read available data
                                                               //txtData.AppendText(data + Environment.NewLine);

                    data = data.Replace("\u0002", "").Replace("\u0003", "").Trim();
                    writeLog($"SerialPort_DataReceived _serialPort ReadLine - {_serialPort.ReadLine()}");
                    //string data = _serialPort.ReadLine(); // Or ReadExisting()
                    writeLog($"SerialPort_DataReceived _serialPort ReadExisting - {data}");
                    //this.Invoke((MethodInvoker)(() => {
                    if (data.Contains(',')) data = data.Split(',')[dataSplitIndex];
                        
                    lblWeight.Text = data.Trim(); // Display on UI thread
                    //}));
                }
            }
            catch (Exception ex)
            {
                writeLog($"SerialPort_DataReceived (1170)- {ex.Message}");
                //timerGetWeight.Enabled = false;
                //MessageBox.Show("Read error: " + ex.Message);
            }

        }
    }
    public class UpdateLedEntryModel
    {
        public string OrderNo { get; set; }
        public string DocumentNo { get; set; }
        public string WorkCenterGroupCode { get; set; }
        public string OperationNo { get; set; }
        public decimal NetWeight { get; set; }
        public decimal GrossWeight { get; set; }
        public int OutputQuantity { get; set; }
    }
    public class CapLedEntryModel
    {
        public long Entry_No { get; set; }
        public string OrderNo { get; set; }
        public string DocumentNo { get; set; }
        public string WorkCenterGroupCode { get; set; }
        public string OperationNo { get; set; }
        public decimal NetWeight { get; set; }
        public decimal GrossWeight { get; set; }
        public int OutputQuantity { get; set; }
    }
    public class GridItemClass
    {
        public int SrNo { get; set; }
        public double PacketWeight { get; set; }
    }
}
