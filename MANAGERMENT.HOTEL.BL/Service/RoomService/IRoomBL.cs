using MANAGERMENT.HOTEL.BL.BaseBL;
using MANAGERMENT.HOTEL.Common.Entities.DTO;
using MANAGERMENT.HOTEL.Common.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGERMENT.HOTEL.BL.Service.RoomService
{
    public interface IRoomBL : IBaseBL<Room>
    {
        PagingResult GetRoomsByFilterAndPaging(int pageSize, int pageIndex, string? textFilter, int? status);
    }
}
