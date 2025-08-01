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
        
        public SucursalService(ISucursalRepository repo)
        {
            _repo = repo;
        }

        public void Registrar(Sucursal sucursal)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"SucursalService.Registrar: Iniciando registro de sucursal '{sucursal.NomComercial}'");

                // Validaciones de negocio
                if (string.IsNullOrWhiteSpace(sucursal.NomComercial))
                    throw new ArgumentException("El nombre comercial es obligatorio.");

                if (string.IsNullOrWhiteSpace(sucursal.Telefono))
                    throw new ArgumentException("El teléfono es obligatorio.");

                if (string.IsNullOrWhiteSpace(sucursal.Correo))
                    throw new ArgumentException("El correo es obligatorio.");

                if (string.IsNullOrWhiteSpace(sucursal.Direccion))
                    throw new ArgumentException("La dirección es obligatoria.");

                if (sucursal.IdEmpresa <= 0)
                    throw new ArgumentException("Debe seleccionar una empresa.");

                if (sucursal.IdColonia <= 0)
                    throw new ArgumentException("Debe seleccionar una colonia.");

                // Verificar si la sucursal ya existe
                var sucursalesExistentes = _repo.GetAll();
                if (sucursalesExistentes.Any(s => s.NomComercial.ToLower() == sucursal.NomComercial.ToLower() && s.IdEmpresa == sucursal.IdEmpresa))
                    throw new ArgumentException($"La sucursal '{sucursal.NomComercial}' ya existe para esta empresa.");

                System.Diagnostics.Debug.WriteLine($"SucursalService.Registrar: Validaciones pasadas, registrando sucursal");

                // Persistir
                _repo.Add(sucursal);
                
                System.Diagnostics.Debug.WriteLine($"SucursalService.Registrar: Sucursal '{sucursal.NomComercial}' registrada exitosamente con ID: {sucursal.IdSucursal}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SucursalService.Registrar: Error al registrar sucursal: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<Sucursal> ObtenerTodas()
            => _repo.GetAll();

        public Sucursal ObtenerPorId(int id)
            => _repo.GetById(id);

        public IEnumerable<Sucursal> ObtenerPorEmpresa(int idEmpresa)
            => _repo.GetAll().Where(s => s.IdEmpresa == idEmpresa);

        public void Actualizar(Sucursal sucursal)
        {
            if (string.IsNullOrWhiteSpace(sucursal.NomComercial))
                throw new ArgumentException("El nombre comercial es obligatorio.");

            if (string.IsNullOrWhiteSpace(sucursal.Telefono))
                throw new ArgumentException("El teléfono es obligatorio.");

            if (string.IsNullOrWhiteSpace(sucursal.Correo))
                throw new ArgumentException("El correo es obligatorio.");

            if (string.IsNullOrWhiteSpace(sucursal.Direccion))
                throw new ArgumentException("La dirección es obligatoria.");

            _repo.Update(sucursal);
        }

        public void Eliminar(int id)
        {
            var sucursal = _repo.GetById(id);
            if (sucursal == null)
                throw new ArgumentException("La sucursal no existe.");

            _repo.Delete(sucursal);
        }
    }
}