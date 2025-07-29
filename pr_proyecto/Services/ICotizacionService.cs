// pr_proyecto.Services/ICotizacionService.cs
using System.Collections.Generic;
using pr_proyecto.Models;

namespace pr_proyecto.Services
{
    public interface ICotizacionService
    {
        void Registrar(Cotizacion cotizacion);
        IEnumerable<Cotizacion> ObtenerTodos();
    }
}