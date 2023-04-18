using Dapper;
using MANAGERMENT.HOTEL.Common.Constants;
using MANAGERMENT.HOTEL.Common.Entities.DTO;
using MANAGERMENT.HOTEL.Common.Entities.Model;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGERMENT.HOTEL.DL.BaseDL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<T> GetRecords()
        {
            List<T> records = new List<T>();

            // Chuẩn bị Stored procedure đầu vào
            string storedProceduceName = String.Format(ProcedureName.GetAll, typeof(T).Name);

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Gọi vào DB
                records = mySqlConnection.Query<T>(storedProceduceName, parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return records;
        }

        /// <summary>
        /// Hàm lấy danh sách bản ghi theo bộ lọc và phân trang
        /// </summary>
        /// <param name="pageSize">số bản ghi trong một trang</param>
        /// <param name="pageIndex"></param>
        /// <param name="textFilter"></param>
        /// <returns>Đối tượng PagingResult
        /// - Tổng số bản ghi
        /// - Tổng số trang
        /// - Danh sách bản ghi trên 1 trang
        /// - Số bản ghi trong trang hiện tại
        /// - Trang hiện tại
        /// </returns>
        public PagingResult GetRecordsByFilterAndPaging(int pageSize, int pageIndex, string? textFilter)
        {
            // Chuẩn bị Stored procedure đầu vào
            string storedProceduceName = String.Format(ProcedureName.Filter, typeof(T).Name);

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();

            parameters.Add("p_PageIndex", pageIndex);
            parameters.Add("p_PageSize", pageSize);
            parameters.Add("p_TextFilter", textFilter);
            parameters.Add("p_TotalRecords", direction: ParameterDirection.Output);

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

        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="recordId">id của bản ghi muốn lấy</param>
        /// <returns>Bản ghi thỏa mãn</returns>
        public T GetRecordById(Guid recordId)
        {
            T record;

            // Chuẩn bị Stored Procedure đầu vào
            string storedProcedureName = String.Format(ProcedureName.GetById, typeof(T).Name);

            // Chuẩn bị tham số đầu vào cho Stored
            var parameters = new DynamicParameters();
            parameters.Add($"p_{typeof(T).Name}Id", recordId);

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Gọi vào DB
                record = mySqlConnection.QueryFirstOrDefault<T>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }

            return record;
        }

        /// <summary>
        /// Hàm thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">bản ghi muốn thêm mới</param>
        /// <returns>
        /// Trả về số bản ghi bị ảnh hưởng
        ///     1: Thêm mới bản ghi thành công
        ///     0: THêm mới bản ghi thất bại
        /// </returns>
        /// CreatedBy:QCThanh(08/02/2023)
        public int InsertRecord(T record)
        {
            // Chuẩn bị stored procedure
            string storedProcedureName = String.Format(ProcedureName.Insert, typeof(T).Name);

            // Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                parameters.Add($"p_{property.Name}", property.GetValue(record));
            }
            parameters.Add($"p_{typeof(T).Name}Id", Guid.NewGuid());
            parameters.Add($"p_ModifiedDate", DateTime.Now);
            parameters.Add($"p_CreatedDate", DateTime.Now);
            int numberOfEffectedRows = 0;

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Gọi vào DB
                numberOfEffectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            return numberOfEffectedRows;
        }

        /// <summary>
        /// Hàm thay đổi thông tin 1 bản ghi
        /// </summary>
        /// <param name="record">bản ghi muốn thay đổi</param>
        /// <returns>
        /// Trả về số bản ghi bị ảnh hưởng
        ///     1: Thay đổi bản ghi thành công
        ///     0: Thay đổi bản ghi thất bại
        /// </returns>
        /// CreatedBy:QCThanh(08/02/2023)
        public int UpdateRecord(Guid recordId, T record)
        {
            // Chuẩn bị stored procedure
            string storedProcedureName = String.Format(ProcedureName.Update, typeof(T).Name);

            // Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                parameters.Add($"p_{property.Name}", property.GetValue(record));
            }
            parameters.Add($"p_{typeof(T).Name}Id", recordId);
            parameters.Add($"p_ModifiedDate", DateTime.Now);

            int numberOfEffectedRows = 0;

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Gọi vào DB
                numberOfEffectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            return numberOfEffectedRows;
        }

        /// <summary>
        /// Xóa 1 bản ghi theo Id
        /// </summary>
        /// <param name="recordId">Id bản ghi muốn xóa</param>
        /// <returns>
        /// Trả về số bản ghi bị ảnh hưởng
        ///     1: Xóa bản ghi thành công
        ///     0: Xóa bản ghi thất bại
        /// </returns>
        public int DeleteRecord(Guid recordId)
        {
            // Số bản ghi bị ảnh hưởng
            int numberOfEffectedRow = 0;

            // Chuẩn bị Stored Procedure đầu vào
            string storedProcedureName = String.Format(ProcedureName.Delete, typeof(T).Name);

            var parameters = new DynamicParameters();
            parameters.Add($"p_{typeof(T).Name}Id", recordId);

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                //Gọi đến DB
                numberOfEffectedRow = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            return numberOfEffectedRow;
        }

        /// <summary>
        /// Xóa nhiều bản ghi theo id
        /// </summary>
        /// <param name="ids">Danh sách Id</param>
        ///     >0: Xóa bản ghi thành công
        ///     0: Xóa bản ghi thất bại</returns>
        public int DeleteMultiple(IEnumerable<Guid> ids)
        {
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        string storedProcedureName = String.Format(ProcedureName.Delete, typeof(T).Name);
                        foreach (var id in ids)
                        {
                            var parameters = new DynamicParameters();
                            parameters.Add($"p_{typeof(T).Name}Id", id);
                            var numberOfEffectedRow = mySqlConnection.Execute(storedProcedureName, parameters, transaction, commandType: CommandType.StoredProcedure);
                        }
                        transaction.Commit();
                        return ids.Count();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Hàm lấy ra mã lớn nhất
        /// </summary>
        /// <returns></returns>
        public string GetMaxCode()
        {
            string maxCode = "";

            string storedProcedureName = String.Format(ProcedureName.GetMaxCode, typeof(T).Name);

            try
            {
                using(var cnn = new MySqlConnection(DatabaseContext.ConnectionString)) 
                { 
                    maxCode = cnn.QueryFirstOrDefault<string>(storedProcedureName, commandType: CommandType.StoredProcedure);
                    return maxCode;
                }
                
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return maxCode;
            }
        }

        /// <summary>
        /// Hàm lấy ra bản ghi theo mã
        /// </summary>
        /// <param name="recordCode">mã</param>
        /// <returns></returns>
        public T GetByCode(string recordCode)
        {
            T record;
            var parameters = new DynamicParameters();
            string typeRecord = typeof(T).Name;
            parameters.Add($"p_{typeRecord}Code", recordCode);
            string sql = $"SELECT * FROM  {typeRecord}  e WHERE e.{typeRecord}Code = p_{typeRecord}Code;";
            using (var cnn = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Gọi vào DB
                record = cnn.QueryFirstOrDefault<T>(sql, param: parameters);
            }
            return record;
        }

        /// <summary>
        /// Hàm lấy 1 ra nhân viên có:
        ///     Id không tồn tại trong danh sách
        ///     Code tồn tại trong danh sách
        ///    -Khi thêm mới: recordId là null nên sẽ kiểm tra recordCode mới có bị trùng lặp không
        ///    -Khi sửa: lấy ra bản ghi có id trùng với id truyền vào và mã bằng với mã truyền vào -> Trả về Null: Chp phép 
        /// </summary>
        /// <param name="recordId">id</param>
        /// <param name="recordCode">mã</param>
        /// <returns></returns>
        public T GetRecordDuplicate(Guid? recordId, string recordCode)
        {
            T record;
            var parameters = new DynamicParameters();
            string typeRecord = typeof(T).Name;
            parameters.Add($"p_{typeRecord}Code", recordCode);
            parameters.Add($"p_{typeRecord}Id", recordId);

            string storedProcedureName = $"Proc_{typeRecord}_GetDuplicate";

            using (var cnn = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                record = cnn.QueryFirstOrDefault<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return record;
        }
    }
}
