using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RoutingScheduler
{
    internal class Program
    {
       static List<RoutingLine> List_RoutingLine = new List<RoutingLine>();
        public static string RoutingLines
        {
            get
            {
                return "RoutingLines";
            }
        }
        public static async Task Main(string[] args)
        {
            try
            {
                string APIUrl = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["APIUrl"]);
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LocalCon"].ConnectionString;
                string username = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["APIUser"]);// "administrator";
                string password = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["APIPassword"]);//"CMk95*@$46@";

                var handler = new HttpClientHandler
                {
                    Credentials = new System.Net.NetworkCredential(username, password) // domain is optional
                };
                using (HttpClient client = new HttpClient(handler))
                {
                    try
                    {
                        var response = await client.GetAsync($"{APIUrl.TrimEnd('/')}/{RoutingLines}");
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(responseBody))
                        {

                            string cmdStr = @"INSERT INTO [RoutingLine]([Routing_No],[Version_Code],[Operation_No],[Previous_Operation_No],[Next_Operation_No]
           ,[Type],[No],[Skip],[Standard_Task_Code],[Routing_Link_Code],[Description],[Setup_Time],[Setup_Time_Unit_of_Meas_Code]
           ,[Run_Time],[Run_Time_Unit_of_Meas_Code],[Wait_Time],[Wait_Time_Unit_of_Meas_Code],[Move_Time]
           ,[Move_Time_Unit_of_Meas_Code],[Fixed_Scrap_Quantity],[Scrap_Factor_Percent],[Minimum_Process_Time]
           ,[Maximum_Process_Time],[Concurrent_Capacities],[Send_Ahead_Quantity],[Unit_Cost_per],[Lot_Size]
           ,[Work_Center_Group_Code] ,[CreatedDate])

     VALUES (@Routing_No,@Version_Code,@Operation_No,@Previous_Operation_No,@Next_Operation_No,@Type
           ,@No,@Skip,@Standard_Task_Code,@Routing_Link_Code,@Description,@Setup_Time
		   ,@Setup_Time_Unit_of_Meas_Code,@Run_Time,@Run_Time_Unit_of_Meas_Code,@Wait_Time,@Wait_Time_Unit_of_Meas_Code
           ,@Move_Time,@Move_Time_Unit_of_Meas_Code
           ,@Fixed_Scrap_Quantity,@Scrap_Factor_Percent
            ,@Minimum_Process_Time,@Maximum_Process_Time
            ,@Concurrent_Capacities ,@Send_Ahead_Quantity,@Unit_Cost_per,@Lot_Size,@Work_Center_Group_Code,GetDate())";

                            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                            var pOrders = JsonSerializer.Deserialize<ODataResponse<RoutingLine>>(responseBody, options);
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                try
                                {
                                    foreach (var item in pOrders.Value)
                                    {
                                        if (item is RoutingLine)
                                        {
                                            try
                                            {
                                                using (SqlCommand command = new SqlCommand(cmdStr, connection))
                                                {
                                                    #region --Add Parameters --
                                                    command.Parameters.AddWithValue("@Routing_No", item.Routing_No);
                                                    command.Parameters.AddWithValue("@Version_Code", item.Version_Code);
                                                    command.Parameters.AddWithValue("@Operation_No", item.Operation_No);
                                                    command.Parameters.AddWithValue("@Previous_Operation_No", item.Previous_Operation_No);
                                                    command.Parameters.AddWithValue("@Next_Operation_No", item.Next_Operation_No);
                                                    command.Parameters.AddWithValue("@Type", item.Type);
                                                    command.Parameters.AddWithValue("@No", item.No);
                                                    command.Parameters.AddWithValue("@Skip", item.Skip);
                                                    command.Parameters.AddWithValue("@Standard_Task_Code", item.Standard_Task_Code);
                                                    command.Parameters.AddWithValue("@Routing_Link_Code", item.Routing_Link_Code);
                                                    command.Parameters.AddWithValue("@Description", item.Description);
                                                    command.Parameters.AddWithValue("@Setup_Time", item.Setup_Time);


                                                    command.Parameters.AddWithValue("@Setup_Time_Unit_of_Meas_Code", item.Setup_Time_Unit_of_Meas_Code);
                                                    command.Parameters.AddWithValue("@Run_Time", item.Run_Time);
                                                    command.Parameters.AddWithValue("@Run_Time_Unit_of_Meas_Code", item.Run_Time_Unit_of_Meas_Code);
                                                    command.Parameters.AddWithValue("@Wait_Time", item.Wait_Time);
                                                    command.Parameters.AddWithValue("@Wait_Time_Unit_of_Meas_Code", item.Wait_Time_Unit_of_Meas_Code);
                                                    command.Parameters.AddWithValue("@Move_Time", item.Move_Time);
                                                    command.Parameters.AddWithValue("@Move_Time_Unit_of_Meas_Code", item.Move_Time_Unit_of_Meas_Code);
                                                    command.Parameters.AddWithValue("@Fixed_Scrap_Quantity", item.Fixed_Scrap_Quantity);
                                                    command.Parameters.AddWithValue("@Scrap_Factor_Percent", item.Scrap_Factor_Percent);
                                                    command.Parameters.AddWithValue("@Minimum_Process_Time", item.Minimum_Process_Time);
                                                    command.Parameters.AddWithValue("@Maximum_Process_Time", item.Maximum_Process_Time);
                                                    command.Parameters.AddWithValue("@Concurrent_Capacities", item.Concurrent_Capacities);
                                                    command.Parameters.AddWithValue("@Send_Ahead_Quantity", item.Send_Ahead_Quantity);
                                                    command.Parameters.AddWithValue("@Unit_Cost_per", item.Unit_Cost_per);
                                                    command.Parameters.AddWithValue("@Lot_Size", item.Lot_Size);
                                                    command.Parameters.AddWithValue("@Work_Center_Group_Code", item.Work_Center_Group_Code);

                                                    #endregion

                                                    command.CommandType = System.Data.CommandType.Text;
                                                    if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
                                                    command.ExecuteNonQuery();
                                                }
                                                RoutingLine routingLine = (RoutingLine)item;
                                                List_RoutingLine.Add(routingLine);
                                            }
                                            catch (Exception ex)
                                            {
                                                writeLog($"Error in inserting values of Routing No {item.Routing_No}\n\n{ex.Message}");
                                            }
                                        }
                                    }
                                }
                                catch (Exception exp)
                                {
                                    writeLog(exp.Message);
                                }
                                finally
                                {
                                    if (connection.State == System.Data.ConnectionState.Open) connection.Close();
                                }
                            };
                        }
                    }
                    catch (Exception ex)
                    {
                        writeLog(ex.Message);
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            catch(Exception ex)
            {
                writeLog(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }

        static void writeLog(string message)
        {
            try
            {
                if (!File.Exists($"Schedular_Log_{DateTime.Today.ToString("ddMMMyyyy")}.txt"))
                {
                    File.Create($"Schedular_Log_{DateTime.Today.ToString("ddMMMyyyy")}.txt").Dispose();
                }
                File.WriteAllText($"Schedular_Log_{DateTime.Today.ToString("ddMMMyyyy")}.txt", message);
            }
            catch (Exception ex) { }
        }
    }

    public class ODataResponse<T>
    {
        [JsonPropertyName("@odata.context")]
        public string ODataContext { get; set; }

        [JsonPropertyName("value")]
        public List<T> Value { get; set; }
    }

    public class RoutingLine
    {
        [JsonPropertyName("@odata.etag")]
        public string ETag { get; set; }

        public string Routing_No { get; set; }
        public string Version_Code { get; set; }
        public string Operation_No { get; set; }
        public string Previous_Operation_No { get; set; }
        public string Next_Operation_No { get; set; }
        public string Type { get; set; }
        public string No { get; set; }
        public bool Skip { get; set; }
        public string Standard_Task_Code { get; set; }
        public string Routing_Link_Code { get; set; }
        public string Description { get; set; }
        public decimal Setup_Time { get; set; }
        public string Setup_Time_Unit_of_Meas_Code { get; set; }
        public decimal Run_Time { get; set; }
        public string Run_Time_Unit_of_Meas_Code { get; set; }
        public decimal Wait_Time { get; set; }
        public string Wait_Time_Unit_of_Meas_Code { get; set; }
        public decimal Move_Time { get; set; }
        public string Move_Time_Unit_of_Meas_Code { get; set; }
        public decimal Fixed_Scrap_Quantity { get; set; }
        public decimal Scrap_Factor_Percent { get; set; }
        public decimal Minimum_Process_Time { get; set; }
        public decimal Maximum_Process_Time { get; set; }
        public int Concurrent_Capacities { get; set; }
        public decimal Send_Ahead_Quantity { get; set; }
        public decimal Unit_Cost_per { get; set; }
        public decimal Lot_Size { get; set; }
        public string Work_Center_Group_Code { get; set; }
    }
}
