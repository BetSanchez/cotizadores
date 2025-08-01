// pr_proyecto.Repositories/ContactoRepository.cs
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using pr_proyecto.Data;
using pr_proyecto.Models;

namespace pr_proyecto.Repository
{
    public class ContactoRepository : IContactoRepository
    {
        private readonly AppDbContext _context;

        public ContactoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Contacto contacto)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"ContactoRepository.Add: Agregando contacto '{contacto.Nombre} {contacto.ApPaterno}' al contexto");
                _context.Contactos.Add(contacto);
                System.Diagnostics.Debug.WriteLine($"ContactoRepository.Add: Contacto agregado al contexto, intentando guardar cambios");
                var result = _context.SaveChanges();
                System.Diagnostics.Debug.WriteLine($"ContactoRepository.Add: SaveChanges completado. Filas afectadas: {result}");
                System.Diagnostics.Debug.WriteLine($"ContactoRepository.Add: ID del contacto recién creado: {contacto.IdContacto}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ContactoRepository.Add: Error al agregar contacto: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"ContactoRepository.Add: Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public void Update(Contacto contacto)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"ContactoRepository.Update: Actualizando contacto '{contacto.Nombre} {contacto.ApPaterno}' con ID: {contacto.IdContacto}");
                _context.Entry(contacto).State = EntityState.Modified;
                var result = _context.SaveChanges();
                System.Diagnostics.Debug.WriteLine($"ContactoRepository.Update: SaveChanges completado. Filas afectadas: {result}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ContactoRepository.Update: Error al actualizar contacto: {ex.Message}");
                throw;
            }
        }

        public void Delete(Contacto contacto)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"ContactoRepository.Delete: Eliminando contacto '{contacto.Nombre} {contacto.ApPaterno}' con ID: {contacto.IdContacto}");
                _context.Contactos.Remove(contacto);
                var result = _context.SaveChanges();
                System.Diagnostics.Debug.WriteLine($"ContactoRepository.Delete: SaveChanges completado. Filas afectadas: {result}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ContactoRepository.Delete: Error al eliminar contacto: {ex.Message}");
                throw;
            }
        }

        public Contacto GetById(int id)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"ContactoRepository.GetById: Buscando contacto con ID: {id}");
                var contacto = _context.Contactos.Find(id);
                System.Diagnostics.Debug.WriteLine($"ContactoRepository.GetById: Contacto encontrado: {(contacto != null ? $"{contacto.Nombre} {contacto.ApPaterno}" : "No encontrado")}");
                return contacto;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ContactoRepository.GetById: Error al buscar contacto: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<Contacto> GetAll()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"ContactoRepository.GetAll: Obteniendo todos los contactos");
                var contactos = _context.Contactos.ToList();
                System.Diagnostics.Debug.WriteLine($"ContactoRepository.GetAll: Contactos encontrados: {contactos.Count}");
                return contactos;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ContactoRepository.GetAll: Error al obtener contactos: {ex.Message}");
                throw;
            }
        }
    }
}