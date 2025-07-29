using System;
using System.Collections.Generic;
using System.Linq;
using pr_proyecto.Models;
using pr_proyecto.Repository;

namespace pr_proyecto.Services
{
    public class ContactoService : IContactoService
    {
        private readonly IContactoRepository _repo;
        public ContactoService(IContactoRepository repo) => _repo = repo;

        public void Registrar(Contacto contacto)
        {
            if (string.IsNullOrWhiteSpace(contacto.Nombre))
                throw new ArgumentException("El nombre del contacto es obligatorio.");

            // podrías validar correo único en la sucursal, etc.
            _repo.Add(contacto);
        }

        public IEnumerable<Contacto> ObtenerTodos() => _repo.GetAll();
    }
}