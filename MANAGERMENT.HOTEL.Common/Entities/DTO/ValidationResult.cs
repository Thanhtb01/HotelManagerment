using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGERMENT.HOTEL.Common.Entities.DTO
{
    public class ValidationResult
    {
        /// <summary>
        /// Kết quả Validate: 
        ///     - true: có lỗi
        ///     - false: không có lỗi
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Đối tượng ErrorResult nếu có lỗi
        /// </summary>
        public ErrorResult? ErrorResult { get; set; }
    }
}
