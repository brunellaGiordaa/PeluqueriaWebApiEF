using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PeluqueriaDLL.Data.Models;

namespace PeluqueriaDLL.Repositories
{
    public class TurnoRepository : ITurnoRepository
    {
        private PeluqueriaDbContext _context;

        public TurnoRepository(PeluqueriaDbContext context)
        {
            _context = context;
        }

        public bool Create(TTurno turno)
        {
            var turnos = GetAll();
            if(turnos.Any(tur => tur.Fecha == turno.Fecha && tur.Hora == turno.Hora))
            {
                throw new Exception("Ya existe un turno en esa misma fecha y hora");
            }
            turno.EstaActivo = "S";
            _context.TTurnos.Add(turno);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var turnoDelete = GetById(id);

            if (turnoDelete!= null)
            {
                turnoDelete.EstaActivo = "N";

                _context.TTurnos.Update(turnoDelete);
                return _context.SaveChanges() > 0;
            }
            return false;
        }

        public List<TTurno> GetAll()
        {
            return _context.TTurnos.ToList();
        }

        public TTurno GetById(int id)
        {
            return _context.TTurnos.Find(id);
        }

        public List<TTurno> GetFilteredBy(List<Expression<Func<TTurno, bool>>> filtros)
        {
            var query = _context.TTurnos.AsQueryable();
            foreach (var filtro in filtros)
            {
                query = query.Where(filtro);
            }
            return query.ToList();
        }

        public bool Update(int id, TTurno turno)
        {
            var turnoUpdate = GetById(id);

            if(turnoUpdate != null)
            {
                turnoUpdate.Fecha = turno.Fecha;
                turnoUpdate.Hora = turno.Hora;
                turnoUpdate.Cliente = turno.Cliente;
                if(turnoUpdate.EstaActivo != turno.EstaActivo)
                {
                    throw new Exception("No se puede dar de baja al actualizar(?");
                }

                _context.TTurnos.Update(turnoUpdate);
                return _context.SaveChanges() > 0;
            }
            return false;
        }
    }
}
