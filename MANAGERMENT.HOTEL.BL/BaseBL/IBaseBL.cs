using MANAGERMENT.HOTEL.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGERMENT.HOTEL.BL.BaseBL
{
    public interface IBaseBL<T>
    {
        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        List<T> GetRecords();

        /// <summary>
        /// Hàm lấy danh sách bản ghi theo bộ lọc và phân trang
        /// </summary>
        /// <param name="pageSize">số bản ghi trong một trang</param>
        /// <param name="pageIndex"></param>
        /// <param name="textFilter"></param>
        /// <returns>Đối tượng PagingResult
        /// - Tổng số bản ghi
        /// - Tổng số trang
        /// - Danh sách bản ghi trên 1 trang
        /// - Số bản ghi trong trang hiện tại
        /// - Trang hiện tại
        /// </returns>
        PagingResult GetRecordsByFilterAndPaging(int pageSize, int pageIndex, string? textFilter);

        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="recordId">id của bản ghi muốn lấy</param>
        /// <returns>Bản ghi thỏa mãn</returns>
        T GetRecordById(Guid recordId);

        /// <summary>
        /// Hàm thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">Bản ghi muốn thêm mới</param>
        /// <returns>
        /// Nếu validate trả về Success (Không có lỗi Front-end)
        /// -> Trả về số bản ghi bị ảnh hưởng
        ///     1: Thêm mới bản ghi thành công
        ///     0: THêm mới bản ghi thất bại (Lỗi back-end)
        /// Nếu validate thất bại (Có lỗi Front-end)
        /// -> Trả về đối tượng ValidateResult (Gồm có ErrorRessult)
        /// </returns>
        /// CreatedBy:QCThanh(08/02/2023)
        dynamic InsertRecord(T record);

        /// <summary>
        /// Thay đổi thông tin của 1 bản ghi
        /// </summary>
        /// <param name="recordId">Id của bản ghi muốn thay đổi</param>
        /// <param name="record">Thông tin mới của bản ghi</param>
        /// <returns>
        /// Nếu validate trả về Success (Không có lỗi Front-end)
        /// -> Trả về số bản ghi bị ảnh hưởng
        ///     1: Sửa bản ghi thành công
        ///     0: Sửa bản ghi thất bại (Lỗi back-end)
        /// Nếu validate thất bại (Có lỗi Front-end)
        /// -> Trả về đối tượng ValidateResult (Gồm có ErrorRessult)
        /// </returns>
        dynamic UpdateRecord(Guid recordId, T record);

        /// <summary>
        /// Xóa 1 bản ghi theo Id
        /// </summary>
        /// <param name="recordId">Id bản ghi muốn xóa</param>
        /// <returns>
        /// Trả về số bản ghi bị ảnh hưởng
        ///     1: Xóa bản ghi thành công
        ///     0: Xóa bản ghi thất bại
        /// </returns>
        int DeleteRecord(Guid recordId);

        /// <summary>
        /// Xóa nhiều bản ghi theo id
        /// </summary>
        /// <param name="ids">Danh sách Id</param>
        ///     1: Xóa bản ghi thành công
        ///     0: Xóa bản ghi thất bại</returns>
        int DeleteMultiple(IEnumerable<Guid> ids);

        /// <summary>
        /// Hàm lấy ra mã mới
        /// </summary>
        /// <returns></returns>
        string GetNewCode();
    }
}
