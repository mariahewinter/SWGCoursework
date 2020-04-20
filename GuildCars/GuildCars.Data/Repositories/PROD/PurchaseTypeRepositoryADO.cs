using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.PROD
{
    public class PurchaseTypeRepositoryADO : IPurchaseTypeRepository
    {
        public List<PurchaseType> GetPurchaseTypes()
        {
            List<PurchaseType> purchaseTypes = new List<PurchaseType>();

            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PurchaseTypesSelectAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        PurchaseType currentRow = new PurchaseType();
                        currentRow.PurchaseTypeID = (int)dr["PurchaseTypeID"];
                        currentRow.PurchaseTypeName = dr["PurchaseTypeName"].ToString();

                        purchaseTypes.Add(currentRow);
                    }
                }

            }

            return purchaseTypes;
        }
    }
}
