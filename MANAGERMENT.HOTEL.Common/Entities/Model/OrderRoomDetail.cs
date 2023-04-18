﻿using MANAGERMENT.HOTEL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGERMENT.HOTEL.Common.Entities.Model
{
    public class OrderRoomDetail
    {
        public Guid OrderDetailId { get; set; }

        public Guid OrderId { get; set; }

        public double Price { get; set; }

        public Status Status { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public string Description { get; set; }

        public Guid EmployeeId { get; set; }

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
