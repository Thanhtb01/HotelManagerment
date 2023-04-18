using MANAGERMENT.HOTEL.API.Controller;
using MANAGERMENT.HOTEL.BL.BaseBL;
using MANAGERMENT.HOTEL.Common.Entities.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MANAGERMENT.HOTEL.API.Controllers.Service
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class CustomersController : BasesController<Customer>
    {
        #region Constructor

        public CustomersController(IBaseBL<Customer> baseBL) : base(baseBL)
        {

        }

        #endregion
    }
}
