using System;
using System.Collections.Generic;
using System.Linq;
using pr_proyecto.Models;
using pr_proyecto.Repository;

namespace pr_proyecto.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _repo;
        
        public EmpresaService(IEmpresaRepository repo)
        {
            _repo = repo;
        }

        public void Registrar(Empresa empresa)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"EmpresaService.Registrar: Iniciando registro de empresa '{empresa.Nombre}'");

                // Validaciones de negocio
                if (string.IsNullOrWhiteSpace(empresa.Nombre))
                    throw new ArgumentException("El nombre de la empresa es obligatorio.");

                // Verificar si la empresa ya existe
                var empresasExistentes = _repo.GetAll();
                if (empresasExistentes.Any(e => e.Nombre.ToLower() == empresa.Nombre.ToLower()))
                    throw new ArgumentException($"La empresa '{empresa.Nombre}' ya existe.");

                System.Diagnostics.Debug.WriteLine($"EmpresaService.Registrar: Validaciones pasadas, registrando empresa");

                // Persistir
                _repo.Add(empresa);
                
                System.Diagnostics.Debug.WriteLine($"EmpresaService.Registrar: Empresa '{empresa.Nombre}' registrada exitosamente con ID: {empresa.IdEmpresa}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"EmpresaService.Registrar: Error al registrar empresa: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<Empresa> ObtenerTodas()
            => _repo.GetAll();

        public Empresa ObtenerPorId(int id)
            => _repo.GetById(id);

        public void Actualizar(Empresa empresa)
        {
            if (string.IsNullOrWhiteSpace(empresa.Nombre))
                throw new ArgumentException("El nombre de la empresa es obligatorio.");

            _repo.Update(empresa);
        }

        public void Eliminar(int id)
        {
            var empresa = _repo.GetById(id);
            if (empresa == null)
                throw new ArgumentException("La empresa no existe.");

            _repo.Delete(empresa);
        }
    }
}