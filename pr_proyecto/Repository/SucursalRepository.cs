using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using pr_proyecto.Data;
using pr_proyecto.Models;

namespace pr_proyecto.Repository
{
    public class SucursalRepository : ISucursalRepository
    {
        private readonly AppDbContext _context;

        public SucursalRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Sucursal sucursal)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"SucursalRepository.Add: Agregando sucursal '{sucursal.NomComercial}' al contexto");
                _context.Sucursales.Add(sucursal);
                System.Diagnostics.Debug.WriteLine($"SucursalRepository.Add: Sucursal agregada al contexto, intentando guardar cambios");
                var result = _context.SaveChanges();
                System.Diagnostics.Debug.WriteLine($"SucursalRepository.Add: SaveChanges completado. Filas afectadas: {result}");
                System.Diagnostics.Debug.WriteLine($"SucursalRepository.Add: ID de la sucursal recién creada: {sucursal.IdSucursal}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SucursalRepository.Add: Error al agregar sucursal: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"SucursalRepository.Add: Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public void Update(Sucursal sucursal)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"SucursalRepository.Update: Actualizando sucursal '{sucursal.NomComercial}' con ID: {sucursal.IdSucursal}");
                _context.Entry(sucursal).State = EntityState.Modified;
                var result = _context.SaveChanges();
                System.Diagnostics.Debug.WriteLine($"SucursalRepository.Update: SaveChanges completado. Filas afectadas: {result}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SucursalRepository.Update: Error al actualizar sucursal: {ex.Message}");
                throw;
            }
        }

        public void Delete(Sucursal sucursal)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"SucursalRepository.Delete: Eliminando sucursal '{sucursal.NomComercial}' con ID: {sucursal.IdSucursal}");
                _context.Sucursales.Remove(sucursal);
                var result = _context.SaveChanges();
                System.Diagnostics.Debug.WriteLine($"SucursalRepository.Delete: SaveChanges completado. Filas afectadas: {result}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SucursalRepository.Delete: Error al eliminar sucursal: {ex.Message}");
                throw;
            }
        }

        public Sucursal GetById(int id)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"SucursalRepository.GetById: Buscando sucursal con ID: {id}");
                var sucursal = _context.Sucursales.Find(id);
                System.Diagnostics.Debug.WriteLine($"SucursalRepository.GetById: Sucursal encontrada: {(sucursal != null ? sucursal.NomComercial : "No encontrada")}");
                return sucursal;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SucursalRepository.GetById: Error al buscar sucursal: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<Sucursal> GetAll()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"SucursalRepository.GetAll: Obteniendo todas las sucursales");
                var sucursales = _context.Sucursales.ToList();
                System.Diagnostics.Debug.WriteLine($"SucursalRepository.GetAll: Sucursales encontradas: {sucursales.Count}");
                return sucursales;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SucursalRepository.GetAll: Error al obtener sucursales: {ex.Message}");
                throw;
            }
        }
    }
}