using MANAGERMENT.HOTEL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGERMENT.HOTEL.Common.Entities.Model
{
    public class Customer
    {
        public Guid CustomerId { get; set; }

        [DisplayName("Mã khách hàng")]
        [Required(ErrorMessage = "Thông tin mã khách hàng không được để trống")]
        public string CustomerCode { get; set; }

        [DisplayName("Họ tên")]
        [Required(ErrorMessage = "Thông tin họ tên không được để trống")]
        public string FullName { get; set; }

        [DisplayName("Giới tính")]
        public Gender Gender { get; set; }

        [DisplayName("Số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; }

        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Địa chỉ Email không hợp lệ")]
        public string Email { get; set; }

        [DisplayName("Số căn cước/ CMND")]
        public string IdentityNumber { get; set; }

        [DisplayName("Ngày cấp")]
        public DateTime IdentityDate { get; set; }

        [DisplayName("Nơi cấp")]
        public string IdentityPlace { get; set; }

        [DisplayName("Tài khoản ngân hàng")]
        public string BankAccount { get; set; }

        [DisplayName("Tên ngân hàng")]
        public string BankName { get; set; }

        [DisplayName("Chi nhánh")]
        public string BankBranch { get; set; }

        [DisplayName("Diểm khách hàng")]
        public int CustomerPoint { get; set; }

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
