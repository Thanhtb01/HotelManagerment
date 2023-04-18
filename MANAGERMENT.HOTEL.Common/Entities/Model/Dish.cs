﻿using MANAGERMENT.HOTEL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGERMENT.HOTEL.Common.Entities.Model
{
    public class Dish
    {
        public Guid DishId { get; set; }

        public string DishCode { get; set; }

        public string DishName { get; set; }

        public string Image { get; set; }

        public Status Status { get; set; }

        public double Price { get; set; }

        public double PriceReality { get; set; }

        public string Description { get; set; }

        public Guid CategoryId { get; set; }

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
