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
    public class ColorRepositoryADO : IColorRepository
    {
        public List<Color> GetColors()
        {
            List<Color> colors = new List<Color>();

            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ColorsSelectAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Color currentRow = new Color();
                        currentRow.ColorID = (int)dr["ColorID"];
                        currentRow.ColorName = dr["ColorName"].ToString();

                        colors.Add(currentRow);
                    }
                }

            }

            return colors;
        }
    }
}
