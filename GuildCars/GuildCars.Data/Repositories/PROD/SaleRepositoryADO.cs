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
    public class SaleRepositoryADO : ISaleRepository
    {
        public List<Sale> GetSales()
        {
            List<Sale> sales = new List<Sale>();

            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SalesSelectAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Sale currentRow = new Sale();
                        currentRow.SaleID = (int)dr["SaleID"];
                        currentRow.UserID = dr["UserID"].ToString();
                        currentRow.VinNumber = dr["VinNumber"].ToString();
                        currentRow.PurchaseTypeID = (int)dr["PurchaseTypeID"];
                        currentRow.PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"]);
                        currentRow.PurchasePrice = (decimal)dr["PurchasePrice"];
                        currentRow.Name = dr["Name"].ToString();
                        currentRow.Email = dr["Email"].ToString();
                        currentRow.Phone = dr["Phone"].ToString();
                        currentRow.Address1 = dr["Address1"].ToString();
                        currentRow.Address2 = dr["Address2"].ToString();
                        currentRow.City = dr["City"].ToString();
                        currentRow.State = dr["State"].ToString();
                        currentRow.Zipcode = dr["Zipcode"].ToString();

                        sales.Add(currentRow);
                    }
                }

            }

            return sales;
        }

        public Sale Add(Sale sale)
        {
            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                List<Sale> sales = GetSales();

                SqlCommand cmd = new SqlCommand("SalesAdd", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                sale.SaleID = sales.Max(c => c.SaleID) + 1;

                cmd.Parameters.AddWithValue("@SaleID", sale.SaleID);
                cmd.Parameters.AddWithValue("@UserID", sale.UserID);
                cmd.Parameters.AddWithValue("@VinNumber", sale.VinNumber);
                cmd.Parameters.AddWithValue("@PurchaseTypeID", sale.PurchaseTypeID);
                cmd.Parameters.AddWithValue("@PurchaseDate", sale.PurchaseDate);
                cmd.Parameters.AddWithValue("@PurchasePrice", sale.PurchasePrice);
                cmd.Parameters.AddWithValue("@Name", sale.Name);
                cmd.Parameters.AddWithValue("@Email", sale.Email);
                cmd.Parameters.AddWithValue("@Phone", sale.Phone);
                cmd.Parameters.AddWithValue("@Address1", sale.Address1);
                cmd.Parameters.AddWithValue("@Address2", sale.Address2);
                cmd.Parameters.AddWithValue("@City", sale.City);
                cmd.Parameters.AddWithValue("@State", sale.State);
                cmd.Parameters.AddWithValue("@Zipcode", sale.Zipcode);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return sale;

        }
    }
}
