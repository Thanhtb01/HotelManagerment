using MANAGERMENT.HOTEL.API.Controller;
using MANAGERMENT.HOTEL.BL.BaseBL;
using MANAGERMENT.HOTEL.Common.Entities.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MANAGERMENT.HOTEL.API.Controllers.Service
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class PricesController : BasesController<Price>
    {
        public PricesController(IBaseBL<Price> baseBL) : base(baseBL)
        {

        }
    }
}
