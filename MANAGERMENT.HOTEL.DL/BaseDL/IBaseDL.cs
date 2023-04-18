using MANAGERMENT.HOTEL.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGERMENT.HOTEL.DL.BaseDL
{
    public interface IBaseDL<T>
    {
        /// <summary>
        /// Lấy danh sách tất cả nhân viên
        /// </summary>
        /// <returns>Danh sách nhân viên</returns>
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
        /// <param name="record">bản ghi muốn thêm mới</param>
        /// <returns>
        /// Trả về số bản ghi bị ảnh hưởng
        ///     1: Thêm mới bản ghi thành công
        ///     0: THêm mới bản ghi thất bại
        /// </returns>
        /// CreatedBy:QCThanh(08/02/2023)
        int InsertRecord(T record);

        /// <summary>
        /// Hàm thay đổi thông tin 1 bản ghi
        /// </summary>
        /// <param name="record">bản ghi muốn thay đổi</param>
        /// <returns>
        /// Trả về số bản ghi bị ảnh hưởng
        ///     1: Thay đổi bản ghi thành công
        ///     0: Thay đổi bản ghi thất bại
        /// </returns>
        /// CreatedBy:QCThanh(08/02/2023)
        int UpdateRecord(Guid recordId, T record);

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
        /// Hàm lấy ra mã lớn nhất
        /// </summary>
        /// <returns></returns>
        string GetMaxCode();

        /// <summary>
        /// Hàm lấy ra bản ghi theo mã
        /// </summary>
        /// <param name="recordCode">mã</param>
        /// <returns></returns>
        T GetByCode(string recordCode);

        /// <summary>
        /// Hàm lấy 1 ra nhân viên có:
        ///     Id không tồn tại trong danh sách
        ///     Code tồn tại trong danh sách
        ///    -Khi thêm mới: recordId là null nên sẽ kiểm tra recordCode mới có bị trùng lặp không
        ///    -Khi sửa: lấy ra bản ghi có id trùng với id truyền vào và mã bằng với mã truyền vào -> Trả về Null: Chp phép 
        /// </summary>
        /// <param name="recordId">id</param>
        /// <param name="recordCode">mã</param>
        /// <returns></returns>
        T GetRecordDuplicate(Guid? recordId, string recordCode);
    }
}
