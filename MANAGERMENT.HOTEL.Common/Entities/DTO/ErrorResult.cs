using MANAGERMENT.HOTEL.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGERMENT.HOTEL.Common.Entities.DTO
{
    public class ErrorResult
    {
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public ErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Message thông báo lỗi chi tiết cho Dev front-end 
        /// </summary>
        public string? DevMsg { get; set; }

        /// <summary>
        /// Message thông báo lỗi người dùng 
        /// </summary>
        public string? UserMsg { get; set; }

        /// <summary>
        /// Lỗi chi tiết
        /// </summary>
        public List<string>? MoreInfor { get; set; }
    }
}
