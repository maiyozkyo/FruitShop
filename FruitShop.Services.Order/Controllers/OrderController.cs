using FruitShop.Business.Base;
using FruitShop.Business.Order;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Newtonsoft.Json;

namespace FruitShop.Services.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderBusiness IOrderBusiness;
        public OrderController(IOrderBusiness iOrderBusiness)
        {
            IOrderBusiness = iOrderBusiness;
        }

        [HttpPost("test")]
        public async Task<IActionResult> Test(RequestMsg requestMsg)
        {
            string jsonString = "[{\"value\": \"string1\"}, {\"value\": \"string2\"}, {\"value\": \"string3\"}]";
            // Deserialize JSON into a list of objects
            var objects = JsonConvert.DeserializeObject<List<string>>(jsonString);
            return Ok(objects);
        }

        [HttpPost("AddUpdateAsync")]
        public async Task<IActionResult> AddUpdateAsync(RequestMsg requestMsg)
        {
            Type tType = IOrderBusiness.GetType();
            MethodInfo method = tType.GetMethod(requestMsg.Method);
            if (method != null)
            {
                var lstObj = new List<object>();
                
                var lstParams = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(requestMsg.Params);

                var paramInfos = method.GetParameters();
                for (var idx = 0; idx < paramInfos.Length; idx++)
                {
                    var type = paramInfos[idx].ParameterType;
                    if (typeof(string).IsAssignableFrom(type))
                    {
                        lstObj.Add(lstParams[idx]);
                    }
                    else
                    {
                        var jsonObj = JsonConvert.SerializeObject(lstParams[idx]);
                        var obj = JsonConvert.DeserializeObject(jsonObj, type);
                        lstObj.Add(obj);
                    }

                }

                var res = await (dynamic)method.Invoke(IOrderBusiness, lstObj.ToArray());
                return Ok(res);
            }
            return NotFound();
        }
    }
}
