<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExportarCotizaciones.aspx.cs" Inherits="pr_proyecto.Views.ExportarCotizaciones" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Exportar Cotizaciones</title>
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
            max-width: 1100px;
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
        .export-btns {
            display: flex;
            gap: 1rem;
            margin-bottom: 2rem;
        }
        .export-btns .btn {
            font-weight: 600;
            font-size: 1rem;
            border-radius: 8px;
            padding: 0.7rem 1.5rem;
            box-shadow: 0 2px 8px rgba(66,153,225,0.08);
            transition: background 0.2s, color 0.2s, transform 0.2s;
        }
        .export-btns .btn-pdf {
            background: #e53e3e;
            color: #fff;
        }
        .export-btns .btn-pdf:hover {
            background: #c53030;
            color: #fff;
            transform: translateY(-2px);
        }
        .export-btns .btn-excel {
            background: #38a169;
            color: #fff;
        }
        .export-btns .btn-excel:hover {
            background: #2f855a;
            color: #fff;
            transform: translateY(-2px);
        }
        .export-btns .btn-csv {
            background: #3182ce;
            color: #fff;
        }
        .export-btns .btn-csv:hover {
            background: #2b6cb0;
            color: #fff;
            transform: translateY(-2px);
        }
        .table-container {
            background: #fff;
            border-radius: 16px;
            overflow: hidden;
            box-shadow: 0 2px 12px rgba(0, 0, 0, 0.06);
            border: 1px solid #e2e8f0;
        }
        .table-title {
            font-size: 1.3rem;
            font-weight: 700;
            color: #1a202c;
            display: flex;
            align-items: center;
            gap: 0.5rem;
            margin: 0;
            padding: 1.5rem 2rem 0.5rem 2rem;
        }
        .data-table {
            width: 100%;
            border-collapse: collapse;
            font-size: 0.97rem;
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
            letter-spacing: 0.5px;
            display: inline-block;
        }
        .amount {
            font-weight: 700;
            color: #1a202c;
            font-size: 1rem;
        }
        .commission {
            font-weight: 600;
            color: #38a169;
            font-size: 1rem;
        }
        @media (max-width: 768px) {
            .container { padding: 1rem; }
            .header { padding: 1rem; }
            .table-title { padding: 1rem 1rem 0.5rem 1rem; }
            .data-table th, .data-table td { padding: 0.7rem 0.5rem; }
        }
    </style>
</head>
<body>
    <div class="container">
        <div class="header">
            <div class="header-title">
                <i class="fas fa-file-export"></i>
                <h1>Exportar Cotizaciones</h1>
            </div>
        </div>
        <div class="export-btns mb-4">
            <button class="btn btn-pdf" type="button"><i class="fas fa-file-pdf"></i> Exportar PDF</button>
            <button class="btn btn-excel" type="button"><i class="fas fa-file-excel"></i> Exportar Excel</button>
            <button class="btn btn-csv" type="button"><i class="fas fa-file-csv"></i> Exportar CSV</button>
        </div>
        <div class="table-container">
            <div class="table-title">
                <i class="fas fa-table"></i>
                Tabla de Cotizaciones
            </div>
            <table class="data-table">
                <thead>
                    <tr>
                        <th>Folio</th>
                        <th>Cliente</th>
                        <th>Descripcion</th>
                        <th>Monto</th>
                        <th>Comision (5%)</th>
                        <th>Fecha</th>
                        <th style="text-align: right; min-width: 170px;">Descargar</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td><span class="table-folio">COT-001</span></td>
                        <td>Juan Perez</td>
                        <td>Sistema de Gestion Empresarial</td>
                        <td><span class="amount">$10,000</span></td>
                        <td><span class="commission">$500</span></td>
                        <td>2024-06-01</td>
                        <td style="text-align: right;">
                            <button class="btn-export-pdf" title="Exportar PDF" style="background:#e53e3e;color:#fff;border:none;border-radius:6px;padding:6px 10px;margin-right:4px;cursor:pointer;"><i class="fas fa-file-pdf"></i></button>
                        </td>
                    </tr>
                    <tr>
                        <td><span class="table-folio">COT-002</span></td>
                        <td>Maria Lopez</td>
                        <td>Aplicacion Web Corporativa</td>
                        <td><span class="amount">$8,000</span></td>
                        <td><span class="commission">$400</span></td>
                        <td>2024-06-02</td>
                        <td style="text-align: right;">
                            <button class="btn-export-pdf" title="Exportar PDF" style="background:#e53e3e;color:#fff;border:none;border-radius:6px;padding:6px 10px;margin-right:4px;cursor:pointer;"><i class="fas fa-file-pdf"></i></button>
                            
                        </td>
                    </tr>
                    <tr>
                        <td><span class="table-folio">COT-003</span></td>
                        <td>Carlos Ruiz</td>
                        <td>Plataforma E-commerce</td>
                        <td><span class="amount">$15,000</span></td>
                        <td><span class="commission">$750</span></td>
                        <td>2024-06-03</td>
                        <td style="text-align: right;">
                            <button class="btn-export-pdf" title="Exportar PDF" style="background:#e53e3e;color:#fff;border:none;border-radius:6px;padding:6px 10px;margin-right:4px;cursor:pointer;"><i class="fas fa-file-pdf"></i></button>
                            
                        </td>
                    </tr>
                    <tr>
                        <td><span class="table-folio">COT-004</span></td>
                        <td>Ana Torres</td>
                        <td>Sistema de Inventarios</td>
                        <td><span class="amount">$12,000</span></td>
                        <td><span class="commission">$600</span></td>
                        <td>2024-06-04</td>
                        <td style="text-align: right;">
                            <button class="btn-export-pdf" title="Exportar PDF" style="background:#e53e3e;color:#fff;border:none;border-radius:6px;padding:6px 10px;margin-right:4px;cursor:pointer;"><i class="fas fa-file-pdf"></i></button>
                            
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</body>
</html>
