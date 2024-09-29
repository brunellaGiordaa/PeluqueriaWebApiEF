using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PeluqueriaDLL.Data.Models;

namespace PeluqueriaDLL.Repositories
{
    public interface IServicioRepository
    {
        List<TServicio> GetAll();
        List<TServicio> GetAllFilteredBy(List<Expression<Func<TServicio, bool>>> filtros);
        bool Update(TServicio servicio, int id);
        bool Delete(int id);
        bool Save(TServicio servicio);
    }
}
