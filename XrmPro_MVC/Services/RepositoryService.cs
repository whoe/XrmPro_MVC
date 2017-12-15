using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XrmPro_MVC.Models;

namespace XrmPro_MVC.Services
{
    public class RepositoryService : IRepositoryService
    {

        private Dictionary<int, StudentModel> students = new Dictionary<int, StudentModel>();

        bool IRepositoryService.Delete(int id)
        {
            if (!students.ContainsKey(id))
                return false;

            students.Remove(id);
            return true;
        }

        StudentModel IRepositoryService.Load(int id)
        {
            if (students.ContainsKey(id))
                return students[id];

            return null;
        }

        IEnumerable<StudentModel> IRepositoryService.LoadAll()
        {
            return students.Select(pair => pair.Value);
        }

        bool IRepositoryService.Save(StudentModel model)
        {
            if (students.ContainsKey(model.Id))
                return false;

            students[model.Id] = model;
            return true;
        }
    }
}
