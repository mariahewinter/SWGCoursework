using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using VendingWebAPI.Models;

namespace VendingWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ItemController : ApiController
    {
        //VendingManager manager = Factory.Create();

        public IItemRepository manager = Factory.Create();
        

        [Route("items/all")]
        [AcceptVerbs("GET")]
        public IHttpActionResult All()
        {
            return Ok(manager.GetAll());
          
        }

        [Route("items/get/{itemId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Get(int itemId)
        {
            Item item = manager.Get(itemId);

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(item);
            }
        }

        [Route("money/{amount}/item/{itemId}")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Update(int itemId, decimal amount)
        {
            Change change = new Change();
            Item item = manager.Get(itemId);

            if (item == null)
            {
                return BadRequest("Invalid item.");
            }
            else if (item.quantity == 0)
            {
                return new System.Web.Http.Results.ResponseMessageResult(
                    Request.CreateErrorResponse((HttpStatusCode)422,
                    new HttpError("Out of Stock!!!")
                    ));
            }
            else if (item.price > amount)
            {
                decimal moneyNeeded = item.price - amount;

                return new System.Web.Http.Results.ResponseMessageResult(
                 Request.CreateErrorResponse((HttpStatusCode)422,
                new HttpError($"Please deposit: {moneyNeeded}.")
                ));

            }


            if (item.price < amount)
            {
                decimal convertToChange = amount - item.price;
                int remainingCents = Convert.ToInt32(convertToChange * 100);

                while (remainingCents >= 25)
                {
                    remainingCents -= 25;
                    change.quarters++;
                }
                while (remainingCents >= 10)
                {
                    remainingCents -= 10;
                    change.dimes++;
                }
                while (remainingCents >= 05)
                {
                    remainingCents -= 05;
                    change.nickels++;
                }
                while (remainingCents > 0)
                {
                    remainingCents -= 01;
                    change.pennies++;
                }
            }


            item.quantity = item.quantity - 1;

            manager.Update(item);
            return Ok(change);
        }
    }
}
