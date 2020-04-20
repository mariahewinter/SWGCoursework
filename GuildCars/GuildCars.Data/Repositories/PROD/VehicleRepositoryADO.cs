using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
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
    public class VehicleRepositoryADO : IVehicleRepository
    {
        public Vehicle Add(Vehicle vehicle)
        {
            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                List<Vehicle> vehicles = GetVehicles();

                SqlCommand cmd = new SqlCommand("AddVehicle", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VinNumber", vehicle.VinNumber);
                cmd.Parameters.AddWithValue("@ModelID", vehicle.ModelID);
                cmd.Parameters.AddWithValue("@MakeID", vehicle.MakeID);
                cmd.Parameters.AddWithValue("@BodyStyleID", vehicle.BodyStyleID);
                cmd.Parameters.AddWithValue("@TransmissionID", vehicle.TransmissionID);
                cmd.Parameters.AddWithValue("@ExteriorColor", vehicle.ExteriorColor);
                cmd.Parameters.AddWithValue("@InteriorColor", vehicle.InteriorColor);
                cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                cmd.Parameters.AddWithValue("@Mileage", vehicle.Mileage);
                cmd.Parameters.AddWithValue("@MSRP", vehicle.MSRP);
                cmd.Parameters.AddWithValue("@SalePrice", vehicle.SalePrice);
                cmd.Parameters.AddWithValue("@Description", vehicle.Description);
                cmd.Parameters.AddWithValue("@Picture", vehicle.Picture);
                cmd.Parameters.AddWithValue("@IsFeatured", vehicle.IsFeatured);
                cmd.Parameters.AddWithValue("@IsPurchased", vehicle.IsPurchased);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return vehicle;
        }

        public void Delete(string vinNumber)
        {
            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                List<Vehicle> vehicles = GetVehicles();

                SqlCommand cmd = new SqlCommand("DeleteVehicle", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VinNumber", vinNumber);

                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }

        public Vehicle Edit(Vehicle vehicle)
        {
            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                List<Vehicle> vehicles = GetVehicles();

                SqlCommand cmd = new SqlCommand("EditVehicle", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VinNumber", vehicle.VinNumber);
                cmd.Parameters.AddWithValue("@ModelID", vehicle.ModelID);
                cmd.Parameters.AddWithValue("@MakeID", vehicle.MakeID);
                cmd.Parameters.AddWithValue("@BodyStyleID", vehicle.BodyStyleID);
                cmd.Parameters.AddWithValue("@TransmissionID", vehicle.TransmissionID);
                cmd.Parameters.AddWithValue("@ExteriorColor", vehicle.ExteriorColor);
                cmd.Parameters.AddWithValue("@InteriorColor", vehicle.InteriorColor);
                cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                cmd.Parameters.AddWithValue("@Mileage", vehicle.Mileage);
                cmd.Parameters.AddWithValue("@MSRP", vehicle.MSRP);
                cmd.Parameters.AddWithValue("@SalePrice", vehicle.SalePrice);
                cmd.Parameters.AddWithValue("@Description", vehicle.Description);
                cmd.Parameters.AddWithValue("@Picture", vehicle.Picture);
                cmd.Parameters.AddWithValue("@IsFeatured", vehicle.IsFeatured);
                cmd.Parameters.AddWithValue("@IsPurchased", vehicle.IsPurchased);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return vehicle;
        }

        public List<Vehicle> GetFeaturedVehicles()
        {
            List<Vehicle> featuredVehicles = GetVehicles().Where(v => v.IsFeatured == true).ToList();

            return featuredVehicles;
        }

        public Vehicle GetVehicleByVIN(string vinNumber)
        {
            Vehicle vehicle = null;

            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesSelectByVinNumber", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VinNumber", vinNumber);
                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        vehicle = new Vehicle();
                        vehicle.VinNumber = dr["VinNumber"].ToString();
                        vehicle.ModelID = (int)dr["ModelID"];
                        vehicle.MakeID = (int)dr["MakeID"];
                        vehicle.BodyStyleID = (int)dr["BodyStyleID"];
                        vehicle.TransmissionID = (int)dr["TransmissionID"];
                        vehicle.ExteriorColor = (int)dr["ExteriorColor"];
                        vehicle.InteriorColor = (int)dr["InteriorColor"];
                        vehicle.Year = (int)dr["Year"];
                        vehicle.Mileage = (int)dr["Mileage"];
                        vehicle.MSRP = (decimal)dr["MSRP"];
                        vehicle.SalePrice = (decimal)dr["SalePrice"];
                        vehicle.Description = dr["Description"].ToString();
                        vehicle.Picture = dr["Picture"].ToString();
                        vehicle.IsFeatured = (bool)dr["IsFeatured"];
                        vehicle.IsPurchased = (bool)dr["IsPurchased"];

                    }
                }

            }
            return vehicle;
        }

        public List<Vehicle> GetVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesSelectAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Vehicle currentRow = new Vehicle();
                        currentRow.VinNumber = dr["VinNumber"].ToString();
                        currentRow.ModelID = (int)dr["ModelID"];
                        currentRow.MakeID = (int)dr["MakeID"];
                        currentRow.BodyStyleID = (int)dr["BodyStyleID"];
                        currentRow.TransmissionID = (int)dr["TransmissionID"];
                        currentRow.ExteriorColor = (int)dr["ExteriorColor"];
                        currentRow.InteriorColor = (int)dr["InteriorColor"];
                        currentRow.Year = (int)dr["Year"];
                        currentRow.Mileage = (int)dr["Mileage"];
                        currentRow.MSRP = (decimal)dr["MSRP"];
                        currentRow.SalePrice = (decimal)dr["SalePrice"];
                        currentRow.Description = dr["Description"].ToString();
                        currentRow.Picture = dr["Picture"].ToString();
                        currentRow.IsFeatured = (bool)dr["IsFeatured"];
                        currentRow.IsPurchased = (bool)dr["IsPurchased"];

                        vehicles.Add(currentRow);
                    }
                }

            }

            return vehicles;

        }


        public List<Vehicle> GetVehiclesBySearchParameters(string searchTerm, decimal priceMin, decimal priceMax, int yearMin, int yearMax, int mileage)
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetVehiclesByParameters", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                cmd.Parameters.AddWithValue("@PriceMin", priceMin);
                cmd.Parameters.AddWithValue("@PriceMax", priceMax);
                cmd.Parameters.AddWithValue("@YearMin", yearMin);
                cmd.Parameters.AddWithValue("@YearMax", yearMax);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Vehicle currentRow = new Vehicle();
                        currentRow.VinNumber = dr["VinNumber"].ToString();
                        currentRow.ModelID = (int)dr["ModelID"];
                        currentRow.MakeID = (int)dr["MakeID"];
                        currentRow.BodyStyleID = (int)dr["BodyStyleID"];
                        currentRow.TransmissionID = (int)dr["TransmissionID"];
                        currentRow.ExteriorColor = (int)dr["ExteriorColor"];
                        currentRow.InteriorColor = (int)dr["InteriorColor"];
                        currentRow.Year = (int)dr["Year"];
                        currentRow.Mileage = (int)dr["Mileage"];
                        currentRow.MSRP = (decimal)dr["MSRP"];
                        currentRow.SalePrice = (decimal)dr["SalePrice"];
                        currentRow.Description = dr["Description"].ToString();
                        currentRow.Picture = dr["Picture"].ToString();
                        currentRow.IsFeatured = (bool)dr["IsFeatured"];
                        currentRow.IsPurchased = (bool)dr["IsPurchased"];

                        vehicles.Add(currentRow);
                    }
                }

            }
            if (mileage <= 1000 && mileage > 0) // return new vehicles
            {
                vehicles.RemoveAll(v => v.Mileage >= 1000);
            }
            else if (mileage >= 1000) // return used vehicles
            {
                vehicles.RemoveAll(v => v.Mileage <= 1000);
            }


            // if mileage was -1, it's the sales or admin page and they want ALL vechicle types
            // only return the first 20

            return vehicles.Take(20).ToList();

        }
    }
}