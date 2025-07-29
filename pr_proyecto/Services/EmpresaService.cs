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
        public EmpresaService(IEmpresaRepository repo) => _repo = repo;

        public void Registrar(Empresa empresa)
        {
            if (string.IsNullOrWhiteSpace(empresa.Nombre))
                throw new ArgumentException("El nombre de la empresa es obligatorio.");

            if (_repo.GetAll().Any(e => e.Nombre == empresa.Nombre))
                throw new ArgumentException("La empresa ya existe.");

            _repo.Add(empresa);
        }

        public IEnumerable<Empresa> ObtenerTodos() => _repo.GetAll();
    }
}