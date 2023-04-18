using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGERMENT.HOTEL.Common.Entities.DTO
{
    public class PagingResult
    {
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// Trang hiện tại
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Số bản ghi trong trang hiện tại
        /// </summary>
        public int CurrentPageRecords { get; set; }

        /// <summary>
        /// Danh sách nhân viên
        /// </summary>
        public List<dynamic> Data { get; set; }
    }
}
