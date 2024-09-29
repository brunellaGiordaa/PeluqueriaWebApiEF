using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PeluqueriaDLL.Data.Models;

namespace PeluqueriaDLL.Repositories
{
    public class ServicioRepository : IServicioRepository
    {
        private PeluqueriaDbContext _context;

        public ServicioRepository(PeluqueriaDbContext context)
        {
            _context = context;
        }
        public bool Delete(int id)
        {
            var servicio = _context.TServicios.Find(id);
            if (servicio != null)
            {
                servicio.EstaActivo = "N";
                _context.TServicios.Update(servicio);

                return _context.SaveChanges() > 0;
            }
            return false;
        }

        public List<TServicio> GetAll()
        {
            return _context.TServicios.Where(x => x.EstaActivo.ToUpper() == "S").ToList();
        }

        public List<TServicio> GetAllFilteredBy(List<Expression<Func<TServicio, bool>>> filtros)
        {
            filtros.Add(servicio => servicio.EstaActivo.ToUpper() == "S");
            var query = _context.TServicios.AsQueryable();
            foreach (var filtro in filtros)
            {
                query = query.Where(filtro);
            }
            return query.ToList();
        }

        public bool Save(TServicio servicio)
        {
            if(servicio.Id == 0)
            {
                var maxId = _context.TServicios.Max(servicio => servicio.Id);
                servicio.Id = maxId + 1;
            }
            _context.TServicios.Add(servicio);
            return _context.SaveChanges() > 0;
        }

        public bool Update(TServicio servicio, int id)
        {
            var servicioUpdate = _context.TServicios.Find(id);
            if (servicioUpdate != null)
            {
                servicioUpdate.Nombre = servicio.Nombre;
                servicioUpdate.EnPromocion = servicio.EnPromocion;
                servicioUpdate.EstaActivo = servicio.EstaActivo;
                servicioUpdate.Costo = servicio.Costo;

                _context.TServicios.Update(servicioUpdate);

                return _context.SaveChanges() > 0;
            }
            return false;
        }
    }
}
