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
    public class TransmissionTypeRepositoryADO : ITransmissionTypeRepository
    {
        public List<Transmission> GetTransmissions()
        {
            List<Transmission> transmissions = new List<Transmission>();

            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("TransmissionsSelectAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Transmission currentRow = new Transmission();
                        currentRow.TransmissionID = (int)dr["TransmissionID"];
                        currentRow.TransmissionName = dr["TransmissionName"].ToString();

                        transmissions.Add(currentRow);
                    }
                }

            }

            return transmissions;
        }
    }
}
