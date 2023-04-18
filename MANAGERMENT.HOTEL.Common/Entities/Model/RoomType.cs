using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGERMENT.HOTEL.Common.Entities.Model
{
    public class RoomType
    {
        public Guid RoomTypeId { get; set; }

        public string RoomTypeCode { get; set; }

        public string RoomTypeName { get; set; }

        public int NumOfAdults { get; set; }

        public int NumOfChildren { get; set; }

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
