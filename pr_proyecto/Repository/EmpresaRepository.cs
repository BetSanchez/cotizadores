using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using pr_proyecto.Data;
using pr_proyecto.Models;

namespace pr_proyecto.Repository
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly AppDbContext _context;

        public EmpresaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Empresa empresa)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"EmpresaRepository.Add: Agregando empresa '{empresa.Nombre}' al contexto");
                _context.Empresas.Add(empresa);
                System.Diagnostics.Debug.WriteLine($"EmpresaRepository.Add: Empresa agregada al contexto, intentando guardar cambios");
                var result = _context.SaveChanges();
                System.Diagnostics.Debug.WriteLine($"EmpresaRepository.Add: SaveChanges completado. Filas afectadas: {result}");
                System.Diagnostics.Debug.WriteLine($"EmpresaRepository.Add: ID de la empresa recién creada: {empresa.IdEmpresa}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"EmpresaRepository.Add: Error al agregar empresa: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"EmpresaRepository.Add: Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public void Update(Empresa empresa)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"EmpresaRepository.Update: Actualizando empresa '{empresa.Nombre}' con ID: {empresa.IdEmpresa}");
                _context.Entry(empresa).State = EntityState.Modified;
                var result = _context.SaveChanges();
                System.Diagnostics.Debug.WriteLine($"EmpresaRepository.Update: SaveChanges completado. Filas afectadas: {result}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"EmpresaRepository.Update: Error al actualizar empresa: {ex.Message}");
                throw;
            }
        }

        public void Delete(Empresa empresa)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"EmpresaRepository.Delete: Eliminando empresa '{empresa.Nombre}' con ID: {empresa.IdEmpresa}");
                _context.Empresas.Remove(empresa);
                var result = _context.SaveChanges();
                System.Diagnostics.Debug.WriteLine($"EmpresaRepository.Delete: SaveChanges completado. Filas afectadas: {result}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"EmpresaRepository.Delete: Error al eliminar empresa: {ex.Message}");
                throw;
            }
        }

        public Empresa GetById(int id)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"EmpresaRepository.GetById: Buscando empresa con ID: {id}");
                var empresa = _context.Empresas.Find(id);
                System.Diagnostics.Debug.WriteLine($"EmpresaRepository.GetById: Empresa encontrada: {(empresa != null ? empresa.Nombre : "No encontrada")}");
                return empresa;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"EmpresaRepository.GetById: Error al buscar empresa: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<Empresa> GetAll()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"EmpresaRepository.GetAll: Obteniendo todas las empresas");
                var empresas = _context.Empresas.ToList();
                System.Diagnostics.Debug.WriteLine($"EmpresaRepository.GetAll: Empresas encontradas: {empresas.Count}");
                return empresas;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"EmpresaRepository.GetAll: Error al obtener empresas: {ex.Message}");
                throw;
            }
        }
    }
}