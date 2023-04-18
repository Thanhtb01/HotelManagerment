using MANAGERMENT.HOTEL.BL.BaseBL;
using MANAGERMENT.HOTEL.Common.Entities.DTO;
using MANAGERMENT.HOTEL.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MANAGERMENT.HOTEL.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region FieBld

        private IBaseBL<T> _baseBL;
        #endregion

        #region Constructor

        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>
        ///     - Danh sách bản ghi
        ///     - Nếu danh sách trống sẽ trả về mã code 204
        /// </returns>
        [HttpGet]
        public IActionResult GetRecords()
        {
            // try catch exception
            try
            {
                // Xử lý kết quả trả về từ Bl
                var records = _baseBL.GetRecords();

                if (records.Count > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, records);
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Common.Enums.ErrorCode.Exception,
                    DevMsg = ex.Message,
                    UserMsg = Resource.UserMsg_Exception
                });
            }
        }

        /// <summary>
        /// Lấy danh sách nhân viên theo bộ lọc và phân trang
        /// </summary>
        /// <param name="pageSize">Số bản ghi hiển thị</param>
        /// <param name="pageIndex">Số thứ tự trang</param>
        /// <param name="textFilter">Từ khóa tìm kiếm</param>
        /// <returns>Đối tượng PagingResult
        /// - Tổng số bản ghi
        /// - Tổng số trang
        /// - Danh sách nhân viên trên 1 trang
        /// - Số nhân viên trong trang hiện tại
        /// - Trang hiện tại
        /// </returns>
        /// Created By: QCThanh (16/01/2023)
        [HttpGet]
        [Route("filter")]
        public IActionResult GetRecordsByFilterAndPaging(
                [FromQuery] int pageSize = 10,
                [FromQuery] int pageIndex = 1,
                [FromQuery] string textFilter = ""
            )
        {
            // Try catch Exception
            try
            {
                var result = _baseBL.GetRecordsByFilterAndPaging(pageSize, pageIndex, textFilter);

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

        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="recordId">Id của bản ghi</param>
        /// <returns>
        ///     - Trả về mã Code 200 và thông tin bản ghi nếu thành công
        ///     - Trả về mã code 404 nếu không tìm thấy bản ghi thỏa mãn id
        ///     - Trả về mã code 500 lấy bản ghi thất bại ghi gọi vào DL 
        /// </returns>
        /// Created By: QCThanh (16/01/2023)
        [HttpGet]
        [Route("{recordId}")]
        public IActionResult GetRecordById(
                [FromRoute] Guid recordId
            )
        {
            // Try catch exception
            try
            {
                var result = _baseBL.GetRecordById(recordId);

                if (result != null)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ErrorResult
                    {
                        ErrorCode = Common.Enums.ErrorCode.IsInvalid,
                        DevMsg = Resource.DevMsg_NotFound,
                        UserMsg = Resource.UserMsg_NotFound,
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Common.Enums.ErrorCode.Exception,
                    DevMsg = ex.Message,
                    UserMsg = Resource.UserMsg_Exception
                });
            }
        }

        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">Bản ghi cần thêm mới</param>
        /// <returns>
        ///     - Trả về mã code 201 nếu thêm mới thành công
        ///     - Trả về mã code 400 nếu thêm mới thất bại do validate không thành công
        ///     - Trả về mã code 500 nếu thêm mới thất bại khi gọi vào DL
        /// </returns>
        [HttpPost]
        public IActionResult InsertRecord(T record)
        {
            try
            {
                //Lấy kết quả từ BL
                var result = _baseBL.InsertRecord(record);

                // Nếu có lỗi từ Validate
                if (result is ValidationResult)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result.ErrorResult);
                }

                // Xử lý kết quả trả về
                else if (result > 0)
                {
                    // TH thành công
                    return StatusCode(StatusCodes.Status201Created);
                }
                else
                {
                    // TH thất bại
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = Common.Enums.ErrorCode.ExcuteFailed,
                        DevMsg = Resource.DevMsg_InsertFailed,
                        UserMsg = Resource.UserMsg_InsertFailed
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Common.Enums.ErrorCode.Exception,
                    DevMsg = ex.Message,
                    UserMsg = Resource.UserMsg_Exception
                });
            }

        }

        /// <summary>
        /// Sửa 1 bản ghi
        /// </summary>
        /// <param name="recordId">Id bản ghi cần thay đổi</param>
        /// <param name="record">Bản ghi cần thay đổi</param>
        /// <returns>
        ///     - Trả về mã code 404 nếu không tìm thấy bản ghi nào thỏa mãn id nhập vào
        ///     - Trả về mã code 200 nếu sửa thành công
        ///     - Trả về mã code 400 nếu sửa thất bại do validate không thành công
        ///     - Trả về mã code 500 nếu sửa thất bại khi gọi vào DL
        /// </returns>
        [HttpPut]
        [Route("{recordId}")]
        public IActionResult UpdateRecord([FromRoute] Guid recordId, [FromBody] T record)
        {
            try
            {
                //Lấy kết quả từ BL
                var result = _baseBL.UpdateRecord(recordId, record);

                // Nếu có lỗi từ Validate
                if (result is ValidationResult)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result.ErrorResult);
                }

                // Xử lý kết quả trả về
                else if (result > 0)
                {
                    // TH thành công
                    return StatusCode(StatusCodes.Status200OK);
                }
                else
                {
                    // TH không tìm thấy bản ghi nào thỏa mãn id nhập vào
                    if (result < 0)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, new ErrorResult
                        {
                            ErrorCode = Common.Enums.ErrorCode.IsInvalid,
                            DevMsg = Resource.DevMsg_NotFound,
                            UserMsg = Resource.UserMsg_NotFound,
                        });
                    }
                    // TH thất bại
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = Common.Enums.ErrorCode.ExcuteFailed,
                        DevMsg = Resource.DevMsg_UpdateFailed,
                        UserMsg = Resource.DevMsg_UpdateFailed
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Common.Enums.ErrorCode.Exception,
                    DevMsg = ex.Message,
                    UserMsg = Resource.UserMsg_Exception
                });
            }

        }

        /// <summary>
        /// Xóa 1 bản ghi theo id
        /// </summary>
        /// <param name="recordId">Id của bản ghi muốn xóa</param>
        /// <returns>
        ///     - Trả về mã code 404 nếu không tìm thấy bản ghi nào thỏa mãn id nhập vào
        ///     - Trả về mã code 200 nếu xóa thành công
        ///     - Trả về mã code 500 nếu xóa thất bại khi gọi vào DL
        /// </returns>
        [HttpDelete]
        [Route("{recordId}")]
        public IActionResult DeleteRecord([FromRoute] Guid recordId)
        {
            try
            {
                //Lấy kết quả từ BL
                var result = _baseBL.DeleteRecord(recordId);

                // TH thành công
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else
                {
                    // TH không tìm thấy bản ghi nào thỏa mãn id nhập vào
                    if (result < 0)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, new ErrorResult
                        {
                            ErrorCode = Common.Enums.ErrorCode.IsInvalid,
                            DevMsg = Resource.DevMsg_NotFound,
                            UserMsg = Resource.UserMsg_NotFound,
                        });
                    }
                    // TH thất bại
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = Common.Enums.ErrorCode.ExcuteFailed,
                        DevMsg = Resource.DevMsg_DeleteFailed,
                        UserMsg = Resource.UserMsg_DeleteFailed,
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Common.Enums.ErrorCode.Exception,
                    DevMsg = ex.Message,
                    UserMsg = Resource.UserMsg_Exception
                });
            }
        }

        /// <summary>
        /// Hàm xử lý xóa nhiều bản ghi theo danh sách id
        /// </summary>
        /// <param name="ids">danh sách id</param>
        /// <returns>
        ///     mã code 200 nếu xóa thành công
        ///     mã code 500 nếu gắp lỗi
        /// </returns>
        [HttpDelete]
        [Route("deleteMultiple")]
        public IActionResult DeleteMultiple([FromBody] IEnumerable<Guid> ids)
        {
            try
            {
                var result = _baseBL.DeleteMultiple(ids);
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Common.Enums.ErrorCode.ExcuteFailed,
                    DevMsg = Resource.DevMsg_DeleteFailed,
                    UserMsg = Resource.UserMsg_DeleteFailed,
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Common.Enums.ErrorCode.Exception,
                    DevMsg = ex.Message,
                    UserMsg = Resource.UserMsg_Exception
                });
            }
        }
        /// <summary>
        /// Tự sinh mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        [Route("newCode")]
        [HttpGet]
        public IActionResult GetNewCode()
        {
            try
            {
                string newCode = _baseBL.GetNewCode();

                if (!string.IsNullOrEmpty(newCode))
                {
                    return StatusCode(StatusCodes.Status200OK, newCode);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion
    }
}
