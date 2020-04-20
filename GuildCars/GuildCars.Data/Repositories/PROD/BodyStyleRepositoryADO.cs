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
    public class BodyStyleRepositoryADO : IBodyStyleRepository
    {
        public List<BodyStyle> GetBodyStyles()
        {
            List<BodyStyle> bodyStyles = new List<BodyStyle>();

            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("BodyStylesSelectAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        BodyStyle currentRow = new BodyStyle();
                        currentRow.BodyStyleID = (int)dr["BodyStyleID"];
                        currentRow.BodyStyleName = dr["BodyStyleName"].ToString();

                        bodyStyles.Add(currentRow);
                    }
                }

            }

            return bodyStyles;
        }
    }
}
