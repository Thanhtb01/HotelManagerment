using MANAGERMENT.HOTEL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGERMENT.HOTEL.Common.Entities.Model
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public string OrderCode { get; set; }

        public OrderType OrderType { get; set; }

        public double PaymentEstimate { get; set; }

        public double PaymentReality { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public int UsePoint { get; set; }

        public OrderStatus Status { get; set; }

        public Guid CustomerId { get; set; }


        /// <summary>
        /// N
        /// </summary>
        public Guid EmployeeId { get; set; }

        public string Description { get; set; }

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
