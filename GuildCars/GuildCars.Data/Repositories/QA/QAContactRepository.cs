using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.QA
{
    public class QAContactRepository : IContactRepository
    {
        public static List<Contact> _contacts = new List<Contact>()
        {
            new Contact() {ContactID = 1, Name = "Mariah Winter", Email = "mw@gmail.com", Message = "Hello, I'm interested in: 1G1AL52F957593553"},
            new Contact() {ContactID = 2, Name = "Jonathan Winter", Message = "Hello, I'm interested in: WBAWC73568E033744", Phone="507-525-2260"},
            new Contact() {ContactID = 3, Name = "Evelyn Dorian", Email = "evdorian@gmail.com", Message = "Hello, I'm interested in: JF1GE6A67BH517070", Phone="507-525-2260"}
        };

        public List<Contact> GetContacts()
        {
            return _contacts;
        }

        public Contact AddContact(Contact contact)
        {
            if (contact.ContactID == 0)
            {
                contact.ContactID = _contacts.Max(m => m.ContactID) + 1;
            }

            if (!string.IsNullOrEmpty(contact.Name) && !string.IsNullOrEmpty(contact.Message) && (!string.IsNullOrEmpty(contact.Phone) || !string.IsNullOrEmpty(contact.Email)))
            {
                _contacts.Add(contact);
                return contact;
            }
            else
            {
                return null;
            }

        }
    }
}
