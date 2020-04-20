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
    public class ContactRepositoryADO : IContactRepository
    {
        public List<Contact> GetContacts()
        {
            List<Contact> contacts = new List<Contact>();

            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactsSelectAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Contact currentRow = new Contact();
                        currentRow.ContactID = (int)dr["ContactID"];
                        currentRow.Name = dr["Name"].ToString();
                        currentRow.Email = dr["Email"].ToString();
                        currentRow.Phone = dr["Phone"].ToString();
                        currentRow.Message = dr["Message"].ToString();

                        contacts.Add(currentRow);
                    }
                }

            }

            return contacts;
        }

        public Contact AddContact(Contact contact)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
                {
                    List<Contact> contacts = GetContacts();

                    SqlCommand cmd = new SqlCommand("ContactsAdd", conn);
                    cmd.CommandType = CommandType.StoredProcedure;


                    contact.ContactID = contacts.Max(c => c.ContactID) + 1;

                    cmd.Parameters.AddWithValue("@ContactID", contact.ContactID);
                    cmd.Parameters.AddWithValue("@Name", contact.Name);
                    cmd.Parameters.AddWithValue("@Email", contact.Email);
                    cmd.Parameters.AddWithValue("@Phone", contact.Phone);
                    cmd.Parameters.AddWithValue("@Message", contact.Message);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return contact;
                }
            }
            catch
            {
                return null;
            }
        }

    }
}
