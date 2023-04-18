using MANAGERMENT.HOTEL.Common;
using MANAGERMENT.HOTEL.Common.Entities.DTO;
using MANAGERMENT.HOTEL.Common.Entities.Model;
using MANAGERMENT.HOTEL.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MANAGERMENT.HOTEL.BL.BaseBL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Constructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<T> GetRecords()
        {
            return _baseDL.GetRecords();
        }

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
        public PagingResult GetRecordsByFilterAndPaging(int pageSize, int pageIndex, string? textFilter)
        {
            textFilter = textFilter?.Trim();

            // Lấy kết quả trả về từ Employee DL
            return _baseDL.GetRecordsByFilterAndPaging(pageSize, pageIndex, textFilter);
        }

        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="recordId">id của bản ghi muốn lấy</param>
        /// <returns>Bản ghi thỏa mãn</returns>
        public T GetRecordById(Guid recordId)
        {
            return _baseDL.GetRecordById(recordId);
        }

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
        /// -> Trả về đối tượng ValidationRecord (Gồm có ErrorRessult)
        /// </returns>
        /// CreatedBy:QCThanh(08/02/2023)
        public dynamic InsertRecord(T record)
        {
            // Validate dữ liệu
            var validationResult = ValidateRequestData(null, record);

            //Nếu validate thành công, không có lỗi -> gọi vào BL
            if (validationResult.IsSuccess)
            {
                var numberOfEffectedRows = _baseDL.InsertRecord(record);
                // Trả về kết quả từ DL
                return numberOfEffectedRows;
            }
            //Nếu có lỗi sẽ trả về Đối tượng ValidationResult
            else
            {
                return validationResult;
            }
        }

        /// <summary>
        /// Thay đổi thông tin của 1 bản ghi
        /// </summary>
        /// <param name="recordId">Id của bản ghi muốn thay đổi</param>
        /// <param name="record">Thông tin mới của bản ghi</param>
        /// <returns>
        /// -1: Nếu không tìm thấy bản ghi muốn sửa
        /// 
        /// Nếu validate trả về Success (Không có lỗi Front-end)
        /// -> Trả về số bản ghi bị ảnh hưởng
        ///     1: Sửa bản ghi thành công
        ///     0: Sửa bản ghi thất bại (Lỗi back-end)
        /// 
        /// Nếu validate thất bại (Có lỗi Front-end)
        /// -> Trả về đối tượng ValidateResult (Gồm có ErrorRessult)
        /// </returns>
        public dynamic UpdateRecord(Guid recordId, T record)
        {
            // Kiểm tra bản ghi có tồn tại hay không
            var recordUpdate = GetRecordById(recordId);
            if (recordUpdate == null)
            {
                return -1;
            }

            // Validate dữ liệu
            var validationResult = ValidateRequestData(recordId, record);

            //Nếu validate thành công, không có lỗi -> gọi vào BL
            if (validationResult.IsSuccess)
            {
                var numberOfEffectedRows = _baseDL.UpdateRecord(recordId, record);
                // Trả về kết quả từ DL
                return numberOfEffectedRows;
            }
            //Nếu có lỗi sẽ trả về Đối tượng ValidationResult
            else
            {
                return validationResult;
            }
        }

        /// <summary>
        /// Xóa 1 bản ghi theo Id
        /// </summary>
        /// <param name="recordId">Id bản ghi muốn xóa</param>
        /// <returns>
        /// Trả về số bản ghi bị ảnh hưởng
        ///     1: Xóa bản ghi thành công
        ///     0: Xóa bản ghi thất bại
        ///     -1: Không tìm thấy bản ghi muốn xóa
        /// </returns>
        public int DeleteRecord(Guid recordId)
        {
            // Kiểm tra bản ghi có tồn tại hay không
            var recordDelete = GetRecordById(recordId);
            if (recordDelete == null)
            {
                return -1;
            }
            var numberOfEffectedRows = _baseDL.DeleteRecord(recordId);
            // Trả về kết quả từ DL
            return numberOfEffectedRows;
        }

        /// <summary>
        /// Xóa nhiều bản ghi theo danh sách id 
        /// </summary>
        /// <param name="ids">danh sách id</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// <exception cref="ArgumentException">Lỗi xảy ra khi danh sách id là null</exception>
        /// <exception cref="Exception">Lỗi bất kỳ trong quá trình xử lý</exception>
        public int DeleteMultiple(IEnumerable<Guid> ids)
        {
            if (ids == null || !ids.Any())
            {
                throw new ArgumentException("Lỗi");
            }
            var numberOfEffectedRows = 0;
            try
            {
                numberOfEffectedRows = _baseDL.DeleteMultiple(ids);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: ", ex);
            }
            return numberOfEffectedRows;
        }


        /// <summary>
        /// Hàm validate dữ liệu record đầu vào
        /// </summary>
        /// <param name="record">thông tin bản ghi cần validate</param>
        /// <param name="idRecordUpdate">Id của bản ghi khi update</param>
        /// <returns>
        /// đối tượng ValidateResult gồm có 
        ///     - IsSuccess: trả về true nếu validate thành công, đầu vào không có lỗi gì, trả về false nếu validate thất bại
        ///     - Đối tượng ErrorResult: (null nếu isSuccess thành công)
        /// </returns>
        /// CreatedBy QCThanh(09/02/2023)
        protected virtual Common.Entities.DTO.ValidationResult ValidateRequestData(Guid? idRecordUpdate, T? record)
        {
            //Gọi hàm validateCustom
            var validateCustom = ValidateCustom(idRecordUpdate, record);

            // Khởi tạo các giá trị mặc định cho đối tượng ValidateResult
            var validationResult = new Common.Entities.DTO.ValidationResult();

            validationResult.IsSuccess = true;

            validationResult.ErrorResult = new ErrorResult();

            validationResult.ErrorResult.MoreInfor = new List<string>();

            if (!validateCustom.IsSuccess)
            {
                validationResult.ErrorResult.MoreInfor = validateCustom.ErrorResult.MoreInfor;
            }

            // Lấy tất cả các property
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(record);

                // Check Required
                var requiredAttribute = (RequiredAttribute)property.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault();

                if (requiredAttribute != null)
                {
                    if (value == null || string.IsNullOrEmpty(value.ToString()))
                    {
                        validationResult.ErrorResult.MoreInfor.Add(requiredAttribute.ErrorMessage);
                    }
                }

                if (property == null)
                {
                    break;
                }

                // Check số điện thoại
                var phoneAttribute = (PhoneAttribute)property.GetCustomAttributes(typeof(PhoneAttribute), false).FirstOrDefault();
                if (phoneAttribute != null)
                {
                    if (value != null)
                    {
                        if (!IsPhoneNumber(value.ToString()))
                        {
                            validationResult.ErrorResult.MoreInfor.Add(phoneAttribute.ErrorMessage);
                        }
                    }
                }

                // Check email
                var emailAttribute = (EmailAddressAttribute)property.GetCustomAttributes(typeof(EmailAddressAttribute), false).FirstOrDefault();
                if (emailAttribute != null)
                {
                    if (value != null)
                    {
                        if (!IsValidEmail(value.ToString()))
                        {
                            validationResult.ErrorResult.MoreInfor.Add(emailAttribute.ErrorMessage);
                        }
                    }
                }
            }

            if (validationResult.ErrorResult.MoreInfor.Count > 0)
            {
                validationResult.IsSuccess = false;
                validationResult.ErrorResult.ErrorCode = Common.Enums.ErrorCode.IsInvalid;
                validationResult.ErrorResult.DevMsg = "Lỗi: ";
                validationResult.ErrorResult.UserMsg = "Lỗi: ";
            }

            return validationResult;
        }

        /// <summary>
        /// Hàm xử lý thêm các validate riêng cho từng đối tượng cụ thể
        /// </summary>
        /// <param name="idRecordUpdate">Id của bản ghi được Update</param>
        /// <param name="record">Thông tin bản ghi cần validate</param>
        /// <returns>
        ///     đối tượng ValidateResult gồm có 
        ///     - IsSuccess: trả về true nếu validate thành công, đầu vào không có lỗi gì, trả về false nếu validate thất bại
        ///     - Đối tượng ErrorResult: (null nếu isSuccess thành công)
        /// </returns>
        protected virtual Common.Entities.DTO.ValidationResult ValidateCustom(Guid? idRecordUpdate, T? record)
        {

            var validationResult = new Common.Entities.DTO.ValidationResult();

            validationResult.IsSuccess = true;

            validationResult.ErrorResult = new ErrorResult();

            validationResult.ErrorResult.MoreInfor = new List<string>();

            ///Viết thêm các hàm validate riêng

            return validationResult;
        }

        /// <summary>
        /// Kiểm tra Email có hợp lệ hay không
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>
        ///     True nếu hợp lệ
        ///     False nếu không hợp lệ
        /// </returns>
        bool IsValidEmail(string? email)
        {
            if (email == null)
            {
                return true;
            }
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra số điện thoại hợp lệ
        /// </summary>
        /// <param name="numberPhone">Số điện thoại</param>
        /// <returns>
        ///     True nếu hợp lệ
        ///     False nếu không hợp lệ
        /// </returns>
        bool IsPhoneNumber(string? numberPhone)
        {
            try
            {
                if (string.IsNullOrEmpty(numberPhone))
                {
                    return true;
                }
                string pattern = @"^((|84|0[9|3|7|8|5])+([0-9]{8})\b)$";
                bool result = Regex.IsMatch(numberPhone, pattern);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Lấy ra mã mới lớn nhất trong db
        /// </summary>
        /// <returns></returns>
        public string GetNewCode()
        {
            string newCode = "";
            string maxCode = _baseDL.GetMaxCode();
            if(!string.IsNullOrEmpty(maxCode))
            {
                //int position = maxCode.IndexOf("-");
                string []arrCode = maxCode.Trim().Split("-");
                newCode = arrCode[0] + "-" + (double.Parse(arrCode[1]) + 1);
                //double newNumberCode = (double.Parse(maxCode.Substring(position + 1)) + 1);
            }
            return newCode;
        }

        /// <summary>
        /// Hàm gọi vào DL kiểm tra mã nhân viên có tồn tại hay chưa
        /// </summary>
        /// <param name="employeeCode">mã nhân viên</param>
        /// <returns>
        ///     true: Mã nhân viên đã tồn tại
        ///     false: Mã nhân viên chưa tồn tại
        /// </returns>
        bool IsDuplicatedCode(Guid? recordId, string recordCode)
        {

            T record = _baseDL.GetRecordDuplicate(recordId, recordCode);

            if (record != null)
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}
