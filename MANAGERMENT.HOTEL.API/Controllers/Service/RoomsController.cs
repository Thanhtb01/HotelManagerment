using MANAGERMENT.HOTEL.API.Controller;
using MANAGERMENT.HOTEL.BL.BaseBL;
using MANAGERMENT.HOTEL.BL.Service.RoomService;
using MANAGERMENT.HOTEL.Common.Entities.DTO;
using MANAGERMENT.HOTEL.Common.Entities.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MANAGERMENT.HOTEL.API.Controllers.Service
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class RoomsController : BasesController<Room>
    {
        #region Field

        private IRoomBL _roomBL;

        #endregion

        #region Constructor

        public RoomsController(IRoomBL roomBL) : base(roomBL)
        {
            _roomBL = roomBL;
        }

        #endregion

        #region Constructor

        [HttpGet]
        [Route("filter1")]
        public IActionResult GetRoomsByFilterAndPaging(
                [FromQuery] int pageSize = 10,
                [FromQuery] int pageIndex = 1,
                [FromQuery] string textFilter = "",
                [FromQuery] int status = -1
            )
        {
            // Try catch Exception
            try
            {
                dynamic result;
                if(status != -1)
                {
                    result = _roomBL.GetRoomsByFilterAndPaging(pageSize, pageIndex, textFilter, status);
                }
                else
                {
                    result = _roomBL.GetRoomsByFilterAndPaging(pageSize, pageIndex, textFilter, null);
                }
                if (result.TotalRecords <= 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                // Xử lý kết quả trả về
                return StatusCode(StatusCodes.Status200OK, result);

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Common.Enums.ErrorCode.Exception,
                    DevMsg = ex.Message,
                    //UserMsg = Resource.UserMsg_Exception
                });
            }
        }

        #endregion
    }
}
