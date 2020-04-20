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
    public class SpecialRepositoryADO : ISpecialRepository
    {
        public List<Special> GetSpecials()
        {
            List<Special> specials = new List<Special>();

            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SpecialsSelectAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Special currentRow = new Special();
                        currentRow.SpecialID = (int)dr["SpecialID"];
                        currentRow.SpecialTitle = dr["SpecialTitle"].ToString();
                        currentRow.SpecialDescription = dr["SpecialDescription"].ToString();

                        specials.Add(currentRow);
                    }
                }

            }

            return specials;
        }

        public Special Add(Special special)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
                {
                    List<Special> specials = GetSpecials();

                    SqlCommand cmd = new SqlCommand("SpecialsAdd", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    special.SpecialID = specials.Max(c => c.SpecialID) + 1;

                    cmd.Parameters.AddWithValue("@SpecialID", special.SpecialID);
                    cmd.Parameters.AddWithValue("@SpecialTitle", special.SpecialTitle);
                    cmd.Parameters.AddWithValue("@SpecialDescription", special.SpecialDescription);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    return special;
                }
            }
            catch
            {
                return null;
            }
        }

        public void Delete(Special special)
        {
            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SpecialsDelete");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SpecialID", special.SpecialID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
