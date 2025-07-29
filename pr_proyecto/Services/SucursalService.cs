using System;
using System.Collections.Generic;
using System.Linq;
using pr_proyecto.Models;
using pr_proyecto.Repository;

namespace pr_proyecto.Services
{
    public class SucursalService : ISucursalService
    {
        private readonly ISucursalRepository _repo;
        public SucursalService(ISucursalRepository repo) => _repo = repo;

        public void Registrar(Sucursal sucursal)
        {
            if (string.IsNullOrWhiteSpace(sucursal.NomComercial))
                throw new ArgumentException("El nombre comercial es obligatorio.");

            if (_repo.GetAll().Any(s => s.NomComercial == sucursal.NomComercial 
                                        && s.IdEmpresa == sucursal.IdEmpresa))
                throw new ArgumentException("Ya existe una sucursal con ese nombre en esta empresa.");

            _repo.Add(sucursal);
        }

        public IEnumerable<Sucursal> ObtenerTodos() => _repo.GetAll();
    }
}