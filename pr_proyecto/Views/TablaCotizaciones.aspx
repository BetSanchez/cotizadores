<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TablaCotizaciones.aspx.cs" Inherits="pr_proyecto.Views.TablaCotizaciones" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Tabla de Cotizaciones</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            font-family: 'Inter', 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #f5f7fa 0%, #c3cfe2 100%);
            min-height: 100vh;
            color: #2d3748;
        }
        .container {
            max-width: 1400px;
            margin: 0 auto;
            padding: 2rem 1rem;
        }
        .header {
            background: #fff;
            border-radius: 16px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
            padding: 2rem 2.5rem 1.5rem 2.5rem;
            margin-bottom: 2rem;
            border-left: 5px solid #4299e1;
        }
        .header-title {
            display: flex;
            align-items: center;
            gap: 1rem;
        }
        .header-title i {
            color: #4299e1;
            font-size: 2rem;
        }
        .header-title h1 {
            font-size: 2rem;
            font-weight: 700;
            color: #1a202c;
            margin: 0;
        }
        .stats-summary {
            display: flex;
            gap: 1.5rem;
            margin-top: 1.5rem;
        }
        .stat-card {
            background: #f8fafc;
            padding: 1rem 1.5rem;
            border-radius: 12px;
            text-align: center;
            flex: 1;
        }
        .stat-number {
            font-size: 1.5rem;
            font-weight: 700;
            color: #4299e1;
        }
        .stat-label {
            font-size: 0.9rem;
            color: #718096;
            margin-top: 0.25rem;
        }
        .table-container {
            background: #fff;
            border-radius: 16px;
            overflow: hidden;
            box-shadow: 0 2px 12px rgba(0, 0, 0, 0.06);
            border: 1px solid #e2e8f0;
        }
        .table-header {
            padding: 1.5rem 2rem;
            border-bottom: 1px solid #e2e8f0;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }
        .table-title {
            font-size: 1.3rem;
            font-weight: 700;
            color: #1a202c;
            display: flex;
            align-items: center;
            gap: 0.5rem;
            margin: 0;
        }
        .filter-controls {
            display: flex;
            gap: 1rem;
            align-items: center;
        }
        .filter-select {
            padding: 0.5rem 1rem;
            border: 1px solid #e2e8f0;
            border-radius: 8px;
            font-size: 0.9rem;
        }
        .data-table {
            width: 100%;
            border-collapse: collapse;
            font-size: 0.95rem;
        }
        .data-table thead {
            background: #f8fafc;
        }
        .data-table th {
            padding: 1rem 1.2rem;
            text-align: left;
            font-weight: 700;
            color: #4a5568;
            font-size: 0.85rem;
            text-transform: uppercase;
            letter-spacing: 0.5px;
            border-bottom: 2px solid #e2e8f0;
            white-space: nowrap;
        }
        .data-table td {
            padding: 1rem 1.2rem;
            border-bottom: 1px solid #f1f5f9;
            vertical-align: middle;
        }
        .data-table tbody tr {
            transition: background-color 0.2s ease;
        }
        .data-table tbody tr:hover {
            background: #f8fafc;
        }
        .data-table tbody tr:last-child td {
            border-bottom: none;
        }
        .table-folio {
            background: #4299e1;
            color: #fff;
            font-weight: 600;
            font-size: 0.8rem;
            padding: 6px 12px;
            border-radius: 8px;
            letter-spacing: 0.2px;
            display: inline-block;
            white-space: nowrap;
        }
        .amount {
            font-weight: 700;
            color: #1a202c;
            font-size: 1rem;
        }
        .large-amount {
            color: #38a169;
        }
        .status {
            font-weight: 600;
            border-radius: 8px;
            padding: 4px 12px;
            font-size: 0.85rem;
            display: inline-block;
            text-transform: uppercase;
            letter-spacing: 0.3px;
        }
        .status-APROBADA { background: #38a169; color: #fff; }
        .status-CANCELADA { background: #e53e3e; color: #fff; }
        .status-PENDIENTE { background: #ecc94b; color: #2d3748; }
        .status-REVISION { background: #ed8936; color: #fff; }
        .status-ENVIADA { background: #805ad5; color: #fff; }
        .client-name {
            font-weight: 600;
            color: #2d3748;
        }
        .vendor-name {
            color: #4a5568;
        }
        .date {
            color: #718096;
            font-size: 0.9rem;
        }
        .version-badge {
            background: #e2e8f0;
            color: #4a5568;
            padding: 2px 8px;
            border-radius: 4px;
            font-size: 0.8rem;
            font-weight: 600;
        }
        .pagination {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin: 1.5rem 2rem;
        }
        .pagination-info {
            color: #718096;
            font-size: 0.9rem;
        }
        .pagination-controls {
            display: flex;
            gap: 0.5rem;
        }
        .pagination-btn {
            background: #fff;
            border: 1px solid #4299e1;
            color: #4299e1;
            border-radius: 6px;
            padding: 6px 16px;
            font-weight: 600;
            font-size: 0.9rem;
            transition: all 0.2s;
            cursor: pointer;
        }
        .pagination-btn.active, .pagination-btn:hover {
            background: #4299e1;
            color: #fff;
            transform: translateY(-1px);
        }
        .pagination-btn:disabled {
            opacity: 0.5;
            cursor: not-allowed;
        }
        @media (max-width: 1024px) {
            .stats-summary {
                flex-wrap: wrap;
            }
            .data-table {
                font-size: 0.85rem;
            }
            .data-table th,
            .data-table td {
                padding: 0.75rem 0.5rem;
            }
        }
        @media (max-width: 768px) {
            .container { padding: 1rem 0.5rem; }
            .header { padding: 1rem; }
            .table-header { 
                flex-direction: column; 
                gap: 1rem; 
                align-items: flex-start;
            }
            .filter-controls {
                width: 100%;
                justify-content: space-between;
            }
            .pagination {
                flex-direction: column;
                gap: 1rem;
            }
        }
    </style>
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            const rowsPerPage = 15;
            const table = document.getElementById('cotizaciones-table');
            const tbody = table.querySelector('tbody');
            const rows = Array.from(tbody.querySelectorAll('tr'));
            const totalPages = Math.ceil(rows.length / rowsPerPage);
            const pagination = document.getElementById('pagination-controls');
            const paginationInfo = document.getElementById('pagination-info');
            let currentPage = 1;

            function showPage(page) {
                currentPage = page;
                const startIndex = (page - 1) * rowsPerPage;
                const endIndex = startIndex + rowsPerPage;
                
                rows.forEach((row, idx) => {
                    row.style.display = (idx >= startIndex && idx < endIndex) ? '' : 'none';
                });
                
                updatePaginationInfo();
                renderPagination();
            }

            function updatePaginationInfo() {
                const startItem = ((currentPage - 1) * rowsPerPage) + 1;
                const endItem = Math.min(currentPage * rowsPerPage, rows.length);
                paginationInfo.textContent = `Mostrando ${startItem}-${endItem} de ${rows.length} cotizaciones`;
            }

            function renderPagination() {
                pagination.innerHTML = '';
                
                // Botón anterior
                const prevBtn = document.createElement('button');
                prevBtn.className = 'pagination-btn';
                prevBtn.innerHTML = '<i class="fas fa-chevron-left"></i> Anterior';
                prevBtn.disabled = currentPage === 1;
                prevBtn.onclick = () => currentPage > 1 && showPage(currentPage - 1);
                pagination.appendChild(prevBtn);

                // Números de página
                const maxVisible = 5;
                let startPage = Math.max(1, currentPage - Math.floor(maxVisible / 2));
                let endPage = Math.min(totalPages, startPage + maxVisible - 1);
                
                if (endPage - startPage + 1 < maxVisible) {
                    startPage = Math.max(1, endPage - maxVisible + 1);
                }

                for (let i = startPage; i <= endPage; i++) {
                    const btn = document.createElement('button');
                    btn.className = 'pagination-btn' + (i === currentPage ? ' active' : '');
                    btn.textContent = i;
                    btn.onclick = () => showPage(i);
                    pagination.appendChild(btn);
                }

                // Botón siguiente
                const nextBtn = document.createElement('button');
                nextBtn.className = 'pagination-btn';
                nextBtn.innerHTML = 'Siguiente <i class="fas fa-chevron-right"></i>';
                nextBtn.disabled = currentPage === totalPages;
                nextBtn.onclick = () => currentPage < totalPages && showPage(currentPage + 1);
                pagination.appendChild(nextBtn);
            }

            // Filtros
            const statusFilter = document.getElementById('status-filter');
            const vendorFilter = document.getElementById('vendor-filter');

            function filterTable() {
                const statusValue = statusFilter.value;
                const vendorValue = vendorFilter.value;
                
                rows.forEach(row => {
                    const status = row.cells[6].textContent.trim();
                    const vendor = row.cells[2].textContent.trim();
                    
                    const statusMatch = !statusValue || status.includes(statusValue);
                    const vendorMatch = !vendorValue || vendor === vendorValue;
                    
                    row.style.display = (statusMatch && vendorMatch) ? '' : 'none';
                });
                
                // Recalcular paginación después del filtro
                const visibleRows = rows.filter(row => row.style.display !== 'none');
                // Para simplicidad, resetear a página 1 después de filtrar
                currentPage = 1;
                showPage(1);
            }

            statusFilter.addEventListener('change', filterTable);
            vendorFilter.addEventListener('change', filterTable);

            // Inicializar
            showPage(1);
        });
    </script>
</head>
<body>
    <div class="container">
        <div class="header">
            <div class="header-title">
                <i class="fas fa-file-invoice-dollar"></i>
                <h1>Cotizaciones</h1>
            </div>
            <div class="stats-summary">
                <div class="stat-card">
                    <div class="stat-number">42</div>
                    <div class="stat-label">Total Cotizaciones</div>
                </div>
                <div class="stat-card">
                    <div class="stat-number">28</div>
                    <div class="stat-label">Aprobadas</div>
                </div>
                <div class="stat-card">
                    <div class="stat-number">$2.1M</div>
                    <div class="stat-label">Valor Total</div>
                </div>
                <div class="stat-card">
                    <div class="stat-number">8</div>
                    <div class="stat-label">Pendientes</div>
                </div>
            </div>
        </div>
        
        <div class="table-container">
            <div class="table-header">
                <div class="table-title">
                    <i class="fas fa-list-alt"></i>
                    Registro de Cotizaciones
                </div>
                <div class="filter-controls">
                    <select id="status-filter" class="filter-select">
                        <option value="">Todos los Estados</option>
                        <option value="APROBADA">Aprobadas</option>
                        <option value="PENDIENTE">Pendientes</option>
                        <option value="CANCELADA">Canceladas</option>
                        <option value="REVISION">En Revisión</option>
                        <option value="ENVIADA">Enviadas</option>
                    </select>
                    <select id="vendor-filter" class="filter-select">
                        <option value="">Todos los Vendedores</option>
                        <option value="Roberto Martínez">Roberto Martínez</option>
                        <option value="Ana Patricia López">Ana Patricia López</option>
                        <option value="Carlos E. Rodríguez">Carlos E. Rodríguez</option>
                        <option value="María José Hernández">María José Hernández</option>
                        <option value="Diego Fernández">Diego Fernández</option>
                        <option value="Sofía Ramírez">Sofía Ramírez</option>
                    </select>
                </div>
            </div>
            
            <table class="data-table" id="cotizaciones-table">
                <thead>
                    <tr>
                        <th>Folio</th>
                        <th>Fecha Emision</th>
                        <th>Vendedor</th>
                        <th>Cliente</th>
                        <th>Sucursal</th>
                        <th>Monto Total</th>
                        <th>Estatus</th>
                        <th>Version</th>
                    </tr>
                </thead>
                <tbody>
                    
<tr><td><span class="table-folio">COT-0002</span></td><td><span class="date">14/08/2024</span></td><td><span class="vendor-name">Ana Patricia Lopez</span></td><td><span class="client-name">Grupo Bimbo S.A.B. de C.V.</span></td><td>CDMX Polanco</td><td><span class="amount">$127,850.50</span></td><td><span class="status status-ENVIADA">Enviada</span></td><td><span class="version-badge">1.0</span></td></tr>
<tr><td><span class="table-folio">COT-0003</span></td><td><span class="date">13/08/2024</span></td><td><span class="vendor-name">Carlos E. Rodriguez</span></td><td><span class="client-name">Walmart de Mexico S.A.B. de C.V.</span></td><td>Guadalajara Sur</td><td><span class="amount large-amount">$892,400.00</span></td><td><span class="status status-APROBADA">Aprobada</span></td><td><span class="version-badge">2.0</span></td></tr>
<tr><td><span class="table-folio">COT-0004</span></td><td><span class="date">12/08/2024</span></td><td><span class="vendor-name">Maria Jose Hernandez</span></td><td><span class="client-name">Femsa Comercio, S.A. de C.V.</span></td><td>Tijuana Norte</td><td><span class="amount">$68,920.75</span></td><td><span class="status status-PENDIENTE">Pendiente</span></td><td><span class="version-badge">1.2</span></td></tr>
<tr><td><span class="table-folio">COT-0005</span></td><td><span class="date">11/08/2024</span></td><td><span class="vendor-name">Diego Fernandez</span></td><td><span class="client-name">Grupo Carso, S.A.B. de C.V.</span></td><td>CDMX Santa Fe</td><td><span class="amount large-amount">$1,245,600.00</span></td><td><span class="status status-REVISION">Revision</span></td><td><span class="version-badge">4.0</span></td></tr>
<tr><td><span class="table-folio">COT-0006</span></td><td><span class="date">10/08/2024</span></td><td><span class="vendor-name">Sofia Ramirez</span></td><td><span class="client-name">Techint Engineering & Construction</span></td><td>Veracruz Puerto</td><td><span class="amount">$256,840.25</span></td><td><span class="status status-APROBADA">Aprobada</span></td><td><span class="version-badge">1.0</span></td></tr>
<tr><td><span class="table-folio">COT-0007</span></td><td><span class="date">09/08/2024</span></td><td><span class="vendor-name">Roberto Martinez</span></td><td><span class="client-name">Grupo Televisa, S.A.B.</span></td><td>CDMX Chapultepec</td><td><span class="amount">$145,300.00</span></td><td><span class="status status-CANCELADA">Cancelada</span></td><td><span class="version-badge">2.1</span></td></tr>
<tr><td><span class="table-folio">COT-0008</span></td><td><span class="date">08/08/2024</span></td><td><span class="vendor-name">Ana Patricia Lopez</span></td><td><span class="client-name">Grupo Mexico S.A.B. de C.V.</span></td><td>Hermosillo Centro</td><td><span class="amount large-amount">$678,950.80</span></td><td><span class="status status-APROBADA">Aprobada</span></td><td><span class="version-badge">1.5</span></td></tr>
<tr><td><span class="table-folio">COT-0009</span></td><td><span class="date">07/08/2024</span></td><td><span class="vendor-name">Carlos E. Rodriguez</span></td><td><span class="client-name">Coca-Cola FEMSA, S.A.B. de C.V.</span></td><td>Puebla Industrial</td><td><span class="amount">$98,765.40</span></td><td><span class="status status-PENDIENTE">Pendiente</span></td><td><span class="version-badge">1.0</span></td></tr>
<tr><td><span class="table-folio">COT-0010</span></td><td><span class="date">06/08/2024</span></td><td><span class="vendor-name">Maria Jose Hernandez</span></td><td><span class="client-name">Grupo Alfa, S.A.B. de C.V.</span></td><td>Monterrey San Pedro</td><td><span class="amount">$187,425.60</span></td><td><span class="status status-ENVIADA">Enviada</span></td><td><span class="version-badge">2.0</span></td></tr>
<tr><td><span class="table-folio">COT-0011</span></td><td><span class="date">05/08/2024</span></td><td><span class="vendor-name">Diego Fernandez</span></td><td><span class="client-name">Industrias Penoles, S.A.B. de C.V.</span></td><td>Torreon Norte</td><td><span class="amount large-amount">$534,280.15</span></td><td><span class="status status-APROBADA">Aprobada</span></td><td><span class="version-badge">1.1</span></td></tr>
<tr><td><span class="table-folio">COT-0012</span></td><td><span class="date">04/08/2024</span></td><td><span class="vendor-name">Sofia Ramirez</span></td><td><span class="client-name">Liverpool, S.A.B. de C.V.</span></td><td>CDMX Insurgentes</td><td><span class="amount">$76,540.90</span></td><td><span class="status status-REVISION">Revision</span></td><td><span class="version-badge">3.0</span></td></tr>
<tr><td><span class="table-folio">COT-0013</span></td><td><span class="date">03/08/2024</span></td><td><span class="vendor-name">Roberto Martinez</span></td><td><span class="client-name">Kimberly-Clark de Mexico S.A.B. de C.V.</span></td><td>Queretaro Centro</td><td><span class="amount">$234,650.25</span></td><td><span class="status status-APROBADA">Aprobada</span></td><td><span class="version-badge">1.0</span></td></tr>
<tr><td><span class="table-folio">COT-0014</span></td><td><span class="date">02/08/2024</span></td><td><span class="vendor-name">Ana Patricia Lopez</span></td><td><span class="client-name">Grupo Lala, S.A.B. de C.V.</span></td><td>Leon Industrial</td><td><span class="amount">$156,380.70</span></td><td><span class="status status-PENDIENTE">Pendiente</span></td><td><span class="version-badge">2.2</span></td></tr>
<tr><td><span class="table-folio">COT-0015</span></td><td><span class="date">01/08/2024</span></td><td><span class="vendor-name">Carlos E. Rodriguez</span></td><td><span class="client-name">Arca Continental, S.A.B. de C.V.</span></td><td>Saltillo Sur</td><td><span class="amount large-amount">$445,720.35</span></td><td><span class="status status-APROBADA">Aprobada</span></td><td><span class="version-badge">1.8</span></td></tr>
<tr><td><span class="table-folio">COT-0016</span></td><td><span class="date">31/07/2024</span></td><td><span class="vendor-name">Maria Jose Hernandez</span></td><td><span class="client-name">Banco Santander Mexico, S.A.</span></td><td>CDMX Reforma</td><td><span class="amount">$89,460.50</span></td><td><span class="status status-CANCELADA">Cancelada</span></td><td><span class="version-badge">1.0</span></td></tr>
<tr><td><span class="table-folio">COT-0017</span></td><td><span class="date">30/07/2024</span></td><td><span class="vendor-name">Diego Fernandez</span></td><td><span class="client-name">Grupo Financiero Banorte, S.A.B. de C.V.</span></td><td>Monterrey Valle</td><td><span class="amount">$198,750.80</span></td><td><span class="status status-ENVIADA">Enviada</span></td><td><span class="version-badge">2.5</span></td></tr>
<tr><td><span class="table-folio">COT-0018</span></td><td><span class="date">29/07/2024</span></td><td><span class="vendor-name">Sofia Ramirez</span></td><td><span class="client-name">Controladora Vuela Compania de Aviacion</span></td><td>Toluca Aeropuerto</td><td><span class="amount large-amount">$723,850.40</span></td><td><span class="status status-APROBADA">Aprobada</span></td><td><span class="version-badge">1.3</span></td></tr>
<tr><td><span class="table-folio">COT-0019</span></td><td><span class="date">28/07/2024</span></td><td><span class="vendor-name">Roberto Martinez</span></td><td><span class="client-name">Genomma Lab Internacional, S.A.B. de C.V.</span></td><td>CDMX Del Valle</td><td><span class="amount">$134,290.15</span></td><td><span class="status status-REVISION">Revision</span></td><td><span class="version-badge">4.2</span></td></tr>
<tr><td><span class="table-folio">COT-0020</span></td><td><span class="date">27/07/2024</span></td><td><span class="vendor-name">Ana Patricia Lopez</span></td><td><span class="client-name">Alsea, S.A.B. de C.V.</span></td><td>Cancun Hotel Zone</td><td><span class="amount">$67,835.25</span></td><td><span class="status status-PENDIENTE">Pendiente</span></td><td><span class="version-badge">1.0</span></td></tr>
<tr><td><span class="table-folio">COT-0021</span></td><td><span class="date">26/07/2024</span></td><td><span class="vendor-name">Carlos E. Rodriguez</span></td><td><span class="client-name">Corporativo Fragua, S.A. de C.V.</span></td><td>Guadalajara Zapopan</td><td><span class="amount">$289,560.90</span></td><td><span class="status status-APROBADA">Aprobada</span></td><td><span class="version-badge">2.0</span></td></tr>
<tr><td><span class="table-folio">COT-0022</span></td><td><span class="date">25/07/2024</span></td><td><span class="vendor-name">Maria Jose Hernandez</span></td><td><span class="client-name">Minera Frisco, S.A.B. de C.V.</span></td><td>Zacatecas Centro</td><td><span class="amount large-amount">$612,475.60</span></td><td><span class="status status-APROBADA">Aprobada</span></td><td><span class="version-badge">1.4</span></td></tr>
<tr><td><span class="table-folio">COT-0023</span></td><td><span class="date">24/07/2024</span></td><td><span class="vendor-name">Diego Fernandez</span></td><td><span class="client-name">Compania Minera Autlan, S.A.B. de C.V.</span></td><td>Colima Puerto</td><td><span class="amount">$178,920.35</span></td><td><span class="status status-ENVIADA">Enviada</span></td><td><span class="version-badge">1.1</span></td></tr>
<tr><td><span class="table-folio">COT-0024</span></td><td><span class="date">23/07/2024</span></td><td><span class="vendor-name">Sofia Ramirez</span></td><td><span class="client-name">Organizacion Soriana, S.A.
