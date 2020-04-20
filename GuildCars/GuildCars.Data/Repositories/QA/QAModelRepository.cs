using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.QA
{
    public class QAModelRepository : IModelRepository
    {
        public static List<Model> _models = new List<Model>()
        {
            new Model() {ModelID=1, ModelName="RAV4", MakeID=1, DateAdded= new DateTime(2020, 04, 10), UserID="test@test.com"},
            new Model() {ModelID=2, ModelName="Camry", MakeID=1, DateAdded= new DateTime(2020, 04, 10), UserID="test@test.com"},
            new Model() {ModelID=3, ModelName="Land Cruiser", MakeID=1, DateAdded= new DateTime(2020, 04, 10), UserID="test@test.com"},
            new Model() {ModelID=4, ModelName="GT-R", MakeID=2, DateAdded= new DateTime(2020, 04, 10), UserID="test@test.com"},
            new Model() {ModelID=5, ModelName="Sentra", MakeID=2, DateAdded= new DateTime(2020, 04, 10), UserID="test@test.com"},
            new Model() {ModelID=6, ModelName="Frontier", MakeID=2, DateAdded= new DateTime(2020, 04, 10), UserID="test@test.com"},
            new Model() {ModelID=7, ModelName="Civic", MakeID=3, DateAdded= new DateTime(2020, 04, 10), UserID="test@test.com"},
            new Model() {ModelID=8, ModelName="CR-V", MakeID=3, DateAdded= new DateTime(2020, 04, 10), UserID="test@test.com"},
            new Model() {ModelID=9, ModelName="Odyssey", MakeID=3, DateAdded= new DateTime(2020, 04, 10), UserID="test@test.com"}
        };

        public List<Model> GetModels()
        {
            return _models;
        }

        public List<Model> GetModelsByMakeID(int makeID)
        {
            return _models.Where(m => m.MakeID == makeID).ToList();
        }


        public void AddModel(Model model)
        {
            if (model.ModelID == 0)
            {
                model.ModelID = _models.Max(m => m.ModelID) + 1;
            }
            else if (_models.Contains(model))
            {
                _models.RemoveAll(m => m.ModelID == model.ModelID);
            }

            _models.Add(model);
        }

    }
}
