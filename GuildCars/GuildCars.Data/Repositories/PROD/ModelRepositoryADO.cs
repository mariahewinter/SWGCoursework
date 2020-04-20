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
    public class ModelRepositoryADO : IModelRepository
    {
        public List<Model> GetModels()
        {
            List<Model> models = new List<Model>();

            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelsSelectAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Model currentRow = new Model();
                        currentRow.ModelID = (int)dr["ModelID"];
                        currentRow.ModelName = dr["ModelName"].ToString();
                        currentRow.MakeID = (int)dr["MakeID"];
                        currentRow.DateAdded = Convert.ToDateTime(dr["DateAdded"]);
                        currentRow.UserID = dr["UserID"].ToString();


                        models.Add(currentRow);
                    }
                }

            }

            return models;
        }

        public List<Model> GetModelsByMakeID(int makeID)
        {
            List<Model> models = new List<Model>();

            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelsSelectByMakeID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MakeID", makeID);
                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Model currentRow = new Model();
                        currentRow.ModelID = (int)dr["ModelID"];
                        currentRow.ModelName = dr["ModelName"].ToString();
                        currentRow.MakeID = (int)dr["MakeID"];


                        models.Add(currentRow);
                    }
                }

            }

            return models;
        }
    

        public void AddModel(Model model)
        {
            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                List<Model> models = GetModels();

                SqlCommand cmd = new SqlCommand("ModelsAdd", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                model.ModelID = models.Max(c => c.ModelID) + 1;

                cmd.Parameters.AddWithValue("@MakeID", model.MakeID);
                cmd.Parameters.AddWithValue("@ModelName", model.ModelName);
                cmd.Parameters.AddWithValue("@ModelID", model.ModelID);
                cmd.Parameters.AddWithValue("@DateAdded", model.DateAdded);
                cmd.Parameters.AddWithValue("@UserId", model.UserID);


                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


    }
}
