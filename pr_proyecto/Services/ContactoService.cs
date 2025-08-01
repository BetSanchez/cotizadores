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
        
        public ContactoService(IContactoRepository repo)
        {
            _repo = repo;
        }

        public void Registrar(Contacto contacto)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"ContactoService.Registrar: Iniciando registro de contacto '{contacto.Nombre} {contacto.ApPaterno}'");

                // Validaciones de negocio
                if (string.IsNullOrWhiteSpace(contacto.Nombre))
                    throw new ArgumentException("El nombre es obligatorio.");

                if (string.IsNullOrWhiteSpace(contacto.ApPaterno))
                    throw new ArgumentException("El apellido paterno es obligatorio.");

                if (string.IsNullOrWhiteSpace(contacto.Correo))
                    throw new ArgumentException("El correo es obligatorio.");

                if (string.IsNullOrWhiteSpace(contacto.Telefono))
                    throw new ArgumentException("El teléfono es obligatorio.");

                if (string.IsNullOrWhiteSpace(contacto.Puesto))
                    throw new ArgumentException("El puesto es obligatorio.");

                if (contacto.IdSucursal <= 0)
                    throw new ArgumentException("Debe seleccionar una sucursal.");

                // Verificar si el contacto ya existe
                var contactosExistentes = _repo.GetAll();
                if (contactosExistentes.Any(c => c.Correo.ToLower() == contacto.Correo.ToLower() && c.IdSucursal == contacto.IdSucursal))
                    throw new ArgumentException($"Ya existe un contacto con el correo '{contacto.Correo}' en esta sucursal.");

                System.Diagnostics.Debug.WriteLine($"ContactoService.Registrar: Validaciones pasadas, registrando contacto");

                // Persistir
                _repo.Add(contacto);
                
                System.Diagnostics.Debug.WriteLine($"ContactoService.Registrar: Contacto '{contacto.Nombre} {contacto.ApPaterno}' registrado exitosamente con ID: {contacto.IdContacto}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ContactoService.Registrar: Error al registrar contacto: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<Contacto> ObtenerTodos()
            => _repo.GetAll();

        public Contacto ObtenerPorId(int id)
            => _repo.GetById(id);

        public IEnumerable<Contacto> ObtenerPorSucursal(int idSucursal)
            => _repo.GetAll().Where(c => c.IdSucursal == idSucursal);

        public void Actualizar(Contacto contacto)
        {
            if (string.IsNullOrWhiteSpace(contacto.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(contacto.ApPaterno))
                throw new ArgumentException("El apellido paterno es obligatorio.");

            if (string.IsNullOrWhiteSpace(contacto.Correo))
                throw new ArgumentException("El correo es obligatorio.");

            if (string.IsNullOrWhiteSpace(contacto.Telefono))
                throw new ArgumentException("El teléfono es obligatorio.");

            if (string.IsNullOrWhiteSpace(contacto.Puesto))
                throw new ArgumentException("El puesto es obligatorio.");

            _repo.Update(contacto);
        }

        public void Eliminar(int id)
        {
            var contacto = _repo.GetById(id);
            if (contacto == null)
                throw new ArgumentException("El contacto no existe.");

            _repo.Delete(contacto);
        }
    }
}