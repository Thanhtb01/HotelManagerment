using MANAGERMENT.HOTEL.Common.Entities.DTO;
using MANAGERMENT.HOTEL.Common.Entities.Model;
using MANAGERMENT.HOTEL.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGERMENT.HOTEL.DL.Service.RoomService
{
    public interface IRoomDL : IBaseDL<Room>
    {
        PagingResult GetRoomsByFilterAndPaging(int pageSize, int pageIndex, string? textFilter, int? status);
    }
}
