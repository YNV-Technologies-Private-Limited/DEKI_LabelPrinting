using System;
using System.Collections.Generic;
using System.Text.Json;

using System.Text.Json.Serialization;

namespace DEKI_LabelPrinting.Model
{
    public static class User
    {
        public static int ID{ get; set; }
        public static string USER_NAME { get; set; }
        public static string PASSWORD { get; set; }
        public static string ROLE { get; set; }
        public static string CODE { get; set; }
        
    }
    public class ODataResponse<T>
    {
        [JsonPropertyName("@odata.context")]
        public string ODataContext { get; set; }

        [JsonPropertyName("value")]
        public List<T> Value { get; set; }
    }

    public class ODataResponseA
    {
        [JsonPropertyName("@odata.context")]
        public string ODataContext { get; set; }

        [JsonPropertyName("value")]
        public List<object> ProdOrders { get; set; }
    }

    public class RelProdOrder
    {
        [JsonPropertyName("@odata.etag")]
        public string ETag { get; set; }

        public string Status { get; set; }
        public string No { get; set; }
        public string Description { get; set; }
        public string Source_No { get; set; }
        public string Routing_No { get; set; }
        public int Quantity { get; set; }
        public DateTime Due_Date { get; set; }
        public string Assigned_User_ID { get; set; }
        public DateTime Creation_Date { get; set; }
        public string LOTNo { get; set; }
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
