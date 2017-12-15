using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XrmPro_MVC.Models;

namespace XrmPro_MVC.Services
{
    public interface IRepositoryService
    {
        bool Save(StudentModel model);

        StudentModel Load(int id);

        IEnumerable<StudentModel> LoadAll();

        bool Delete(int id);

    }
}
