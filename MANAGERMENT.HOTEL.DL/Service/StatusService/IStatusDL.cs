using MANAGERMENT.HOTEL.Common.Entities.Model;
using MANAGERMENT.HOTEL.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGERMENT.HOTEL.DL.Service.StatusService
{
    public interface IStatusDL : IBaseDL<Status>
    {
        Status GetStatusByCode (int statusCode);
    }
}
