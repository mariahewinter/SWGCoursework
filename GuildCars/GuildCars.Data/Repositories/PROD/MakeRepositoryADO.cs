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
    public class MakeRepositoryADO : IMakeRepository
    {
        public List<Make> GetMakes()
        {
            List<Make> makes = new List<Make>();

            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MakesSelectAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        Make currentRow = new Make();
                        currentRow.MakeID = (int)dr["MakeID"];
                        currentRow.MakeName = dr["MakeName"].ToString();
                        currentRow.DateAdded = Convert.ToDateTime(dr["DateAdded"]);
                        currentRow.UserID = dr["UserID"].ToString();

                        makes.Add(currentRow);
                    }
                }

            }

            return makes;
        }

        public Make AddMake(Make make)
        {
            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                List<Make> makes = GetMakes();

                SqlCommand cmd = new SqlCommand("MakesAdd", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                make.MakeID = makes.Max(c => c.MakeID) + 1;

                cmd.Parameters.AddWithValue("@MakeID", make.MakeID);
                cmd.Parameters.AddWithValue("@MakeName", make.MakeName);
                cmd.Parameters.AddWithValue("@DateAdded", make.DateAdded);
                cmd.Parameters.AddWithValue("@UserId", make.UserID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return make;
        }

    }
}
