using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PeluqueriaDLL.Data.Models;

namespace PeluqueriaDLL.Repositories
{
    public interface ITurnoRepository
    {
        List<TTurno> GetAll();
        TTurno GetById(int id);
        List<TTurno> GetFilteredBy(List<Expression<Func<TTurno, bool>>> filtros);
        bool Delete(int id);
        bool Update(int id, TTurno turno);
        bool Create (TTurno turno);
    }
}
