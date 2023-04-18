using MANAGERMENT.HOTEL.BL.BaseBL;
using MANAGERMENT.HOTEL.Common.Entities.DTO;
using MANAGERMENT.HOTEL.Common.Entities.Model;
using MANAGERMENT.HOTEL.DL.BaseDL;
using MANAGERMENT.HOTEL.DL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MANAGERMENT.HOTEL.DL.Service.RoomService;
using MANAGERMENT.HOTEL.DL.Service.StatusService;

namespace MANAGERMENT.HOTEL.BL.Service.RoomService
{
    public class RoomBL : BaseBL<Room>, IRoomBL
    {
        #region Field

        /// <summary>
        /// Đối tượng IRoomDL
        /// </summary>
        private IRoomDL _roomDL;
        private IStatusDL _statusDL;

        #endregion

        public RoomBL(IRoomDL roomDL, IStatusDL statusDL) : base(roomDL)
        {
            _roomDL = roomDL;
            _statusDL = statusDL;
        }

        public PagingResult GetRoomsByFilterAndPaging(int pageSize, int pageIndex, string? textFilter, int? status)
        {
            textFilter = textFilter?.Trim();
            // Lấy kết quả trả về từ Employee DL
            return _roomDL.GetRoomsByFilterAndPaging(pageSize, pageIndex, textFilter, status);
        }
    }
}
