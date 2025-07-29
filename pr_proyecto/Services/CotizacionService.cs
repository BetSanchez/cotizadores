using System;
using System.Collections.Generic;
using System.Linq;
using pr_proyecto.Models;
using pr_proyecto.Repository;

namespace pr_proyecto.Services
{
    public class CotizacionService : ICotizacionService
    {
        private readonly ICotizacionRepository _repo;
        public CotizacionService(ICotizacionRepository repo) => _repo = repo;

        public void Registrar(Cotizacion cotizacion)
        {
            if (cotizacion.IdSucursal <= 0)
                throw new ArgumentException("Debe asignar una sucursal válida.");

            _repo.Add(cotizacion);
        }

        public IEnumerable<Cotizacion> ObtenerTodos() => _repo.GetAll();
    }
}