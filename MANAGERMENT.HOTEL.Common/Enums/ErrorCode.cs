using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGERMENT.HOTEL.Common.Enums
{
    public enum ErrorCode
    {
        /// <summary>
        /// Lỗi Exception
        /// </summary>
        Exception = 0,

        /// <summary>
        /// Lỗi kết nối
        /// </summary>
        ErrorConnection = 1,

        /// <summary>
        /// Không được để trống
        /// </summary>
        IsRequired = 2,

        /// <summary>
        /// Không hợp lệ
        /// </summary>
        IsInvalid = 3,

        /// <summary>
        /// Xử lý thất bại
        /// </summary>
        ExcuteFailed = 4

    }
}
