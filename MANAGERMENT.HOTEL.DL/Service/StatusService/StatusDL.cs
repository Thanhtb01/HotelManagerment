using Dapper;
using MANAGERMENT.HOTEL.Common.Constants;
using MANAGERMENT.HOTEL.Common.Entities.Model;
using MANAGERMENT.HOTEL.DL.BaseDL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGERMENT.HOTEL.DL.Service.StatusService
{
    public class StatusDL : BaseDL<Status>, IStatusDL
    {
        public Status GetStatusByCode(int statusCode)
        {
            Status record;

            // Chuẩn bị Stored Procedure đầu vào
            string storedProcedureName = "Proc_Status_GetByCode";

            // Chuẩn bị tham số đầu vào cho Stored
            var parameters = new DynamicParameters();
            parameters.Add($"p_StatusCode", statusCode);

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Gọi vào DB
                record = mySqlConnection.QueryFirstOrDefault<Status>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            return record;
        }
    }
}
