using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGERMENT.HOTEL.Common.Enums
{
    public enum Role
    {
        /// <summary>
        /// Quan tri he thong
        /// </summary>
        Administrator = 0,

        /// <summary>
        /// Quan ly
        /// </summary>
        Manager = 1,

        /// <summary>
        /// Nhan vien
        /// </summary>
        Employee = 2,

        /// <summary>
        /// Le tan
        /// </summary>
        Receptionist = 3,

        /// <summary>
        /// Ke toan
        /// </summary>
        Accountant = 4
    }
}
