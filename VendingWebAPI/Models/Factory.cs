using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace VendingWebAPI.Models
{
    public static class Factory
    {
        public static IItemRepository Create()
        {
            string mode = WebConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "Database":
                    return new ItemRepository();
                case "Sample":
                    return new SampleRepository();
                default:
                    throw new Exception("Mode value in app config is not valid.");
            }

            throw new NotImplementedException();

        }
    }
}