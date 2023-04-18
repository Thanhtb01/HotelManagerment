using MANAGERMENT.HOTEL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGERMENT.HOTEL.Common.Entities.Model
{
    public class Employee
    {
        [DisplayName("ID nhân viên")]
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid EmployeeId { get; set; }

        [Key]
        [DisplayName("Mã nhân viên")]
        [Required(ErrorMessage = "Thông tin mã nhân viên không được để trống")]
        [RegularExpression(@"NV-[0-9]{5,20}",
         ErrorMessage = "Mã nhân viên không đúng định dạng (NV- )")]
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }

        [DisplayName("Họ tên")]
        [Required(ErrorMessage = "Thông tin họ tên không được để trống")]
        /// <summary>
        /// Họ tên
        /// </summary>
        public string FullName { get; set; }

        [DisplayName("Ngày sinh")]
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        [DisplayName("Giới tính")]
        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender Gender { get; set; }

        [DisplayName("Chức vụ")]
        /// <summary>
        /// Chức vụ
        /// </summary>
        public string Position { get; set; }

        [DisplayName("Địa chỉ")]
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Địa chỉ Email không hợp lệ")]
        /// <summary>
        /// Địa chỉ Email
        /// </summary>
        public string Email { get; set; }

        [DisplayName("ĐT di động")]
        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; }

        [DisplayName("User")]
        [Required(ErrorMessage = "Thông tin tai khoan không được để trống")]
        /// <summary>
        /// Khóa ngoại - Id User
        /// </summary>
        public Guid UserId { get; set; }

        [DisplayName("Lương")]
        /// <summary>
        /// Lương
        /// </summary>
        public double Salary { get; set; }

        [DisplayName("Trạng thái")]
        /// <summary>
        /// Trạng thái làm việc
        /// </summary>
        public WorkingStatus Status { get; set; }

        [DisplayName("Ngày tạo")]
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        [DisplayName("Người tạo")]
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }

        [DisplayName("Ngày sửa")]
        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        [DisplayName("Người sửa")]
        /// <summary>
        /// Người sửa
        /// </summary>
        public string ModifiedBy { get; set; }
    }
}
