using Dapper;
using MANAGERMENT.HOTEL.Common.Constants;
using MANAGERMENT.HOTEL.Common.Entities.DTO;
using MANAGERMENT.HOTEL.Common.Entities.Model;
using MANAGERMENT.HOTEL.DL.BaseDL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGERMENT.HOTEL.DL.Service.RoomService
{
    public class RoomDL : BaseDL<Room>, IRoomDL
    {
        public PagingResult GetRoomsByFilterAndPaging(int pageSize, int pageIndex, string? textFilter, int? status)
        {
            // Chuẩn bị Stored procedure đầu vào
            string storedProceduceName = string.Format(ProcedureName.Filter, typeof(Room).Name);

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();

            parameters.Add("p_PageIndex", pageIndex);
            parameters.Add("p_PageSize", pageSize);
            parameters.Add("p_TextFilter", textFilter);
            parameters.Add("p_TotalRecords", direction: ParameterDirection.Output);
            parameters.Add("p_Status", status);


            dynamic records;
            int totalRecords;

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Gọi vào DB
                var multi = mySqlConnection.QueryMultiple(storedProceduceName, parameters, commandType: CommandType.StoredProcedure);
                records = multi.Read().ToList();
                totalRecords = parameters.Get<int>("p_TotalRecords");
                //totalRecords = multi.ReadFirstOrDefault<int>();
            }
            return new PagingResult
            {
                Data = records,
                CurrentPage = pageIndex,
                CurrentPageRecords = records.Count,
                TotalRecords = totalRecords,
                TotalPage = totalRecords / pageSize + 1
            };
        }
    }
}
