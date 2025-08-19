<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Reporte de Cotizaciones</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet" />
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: 'Inter', 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #f5f7fa 0%, #c3cfe2 100%);
            min-height: 100vh;
            color: #2d3748;
            line-height: 1.6;
        }

        .container {
            max-width: 1200px;
            margin: 0 auto;
            padding: 2rem 1rem;
        }

        /* Header Section */
        .header {
            background: #fff;
            border-radius: 16px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
            padding: 2.5rem;
            margin-bottom: 2rem;
            border-left: 5px solid #4299e1;
        }

        .header-title {
            display: flex;
            align-items: center;
            gap: 1rem;
            margin-bottom: 0.5rem;
        }

        .header-title i {
            color: #4299e1;
            font-size: 2rem;
        }

        .header-title h1 {
            font-size: 2.25rem;
            font-weight: 700;
            color: #1a202c;
            margin: 0;
        }

        .header-subtitle {
            color: #718096;
            font-size: 1.1rem;
            margin-left: 3rem;
        }

        /* Stats Section */
        .stats-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
            gap: 1.5rem;
            margin-bottom: 3rem;
        }

        .stat-card {
            background: #fff;
            border-radius: 12px;
            padding: 2rem;
            box-shadow: 0 2px 12px rgba(0, 0, 0, 0.06);
            border: 1px solid #e2e8f0;
            transition: all 0.3s ease;
            position: relative;
            overflow: hidden;
        }

        .stat-card::before {
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            height: 4px;
            background: var(--accent-color);
        }

        .stat-card:hover {
            transform: translateY(-4px);
            box-shadow: 0 8px 25px rgba(0, 0, 0, 0.12);
        }

        .stat-card.primary { --accent-color: #4299e1; }
        .stat-card.purple { --accent-color: #9f7aea; }
        .stat-card.green { --accent-color: #48bb78; }

        .stat-header {
            display: flex;
            align-items: center;
            justify-content: space-between;
            margin-bottom: 1rem;
        }

        .stat-title {
            font-size: 0.9rem;
            font-weight: 600;
            color: #718096;
            text-transform: uppercase;
            letter-spacing: 0.5px;
        }

        .stat-icon {
            width: 40px;
            height: 40px;
            border-radius: 10px;
            display: flex;
            align-items: center;
            justify-content: center;
            color: #fff;
        }

        .stat-card.primary .stat-icon { background: #4299e1; }
        .stat-card.purple .stat-icon { background: #9f7aea; }
        .stat-card.green .stat-icon { background: #48bb78; }

        .stat-value {
            font-size: 2.5rem;
            font-weight: 800;
            color: #1a202c;
            margin-bottom: 0.5rem;
        }

        /* Chart Section */
        .chart-container {
            background: #fff;
            border-radius: 16px;
            padding: 2.5rem;
            margin-bottom: 2rem;
            box-shadow: 0 2px 12px rgba(0, 0, 0, 0.06);
            border: 1px solid #e2e8f0;
        }

        .chart-title {
            font-size: 1.5rem;
            font-weight: 700;
            color: #1a202c;
            margin-bottom: 2rem;
            display: flex;
            align-items: center;
            gap: 0.5rem;
        }

        .chart-title i {
            color: #4299e1;
        }

        .chart-bars {
            display: flex;
            flex-direction: column;
            gap: 1rem;
        }

        .chart-bar {
            display: flex;
            align-items: center;
            gap: 1rem;
        }

        .bar-info {
            min-width: 200px;
            display: flex;
            align-items: center;
            gap: 0.75rem;
        }

        .folio-badge {
            background: #4299e1;
            color: #fff;
            font-weight: 600;
            font-size: 0.8rem;
            padding: 4px 8px;
            border-radius: 6px;
            letter-spacing: 0.5px;
        }

        .client-name {
            font-weight: 600;
            color: #2d3748;
            font-size: 0.95rem;
        }

        .bar-track {
            flex: 1;
            background: #f7fafc;
            height: 12px;
            border-radius: 6px;
            position: relative;
            overflow: hidden;
        }

        .bar-fill {
            height: 100%;
            background: linear-gradient(90deg, #4299e1 0%, #9f7aea 100%);
            border-radius: 6px;
            position: relative;
            transition: width 0.8s ease;
        }

        .bar-value {
            font-weight: 700;
            color: #1a202c;
            min-width: 80px;
            text-align: right;
            font-size: 0.95rem;
        }

        /* Table Section */
        .table-container {
            background: #fff;
            border-radius: 16px;
            overflow: hidden;
            box-shadow: 0 2px 12px rgba(0, 0, 0, 0.06);
            border: 1px solid #e2e8f0;
        }

        .table-header {
            padding: 2rem 2.5rem 1rem;
            border-bottom: 1px solid #e2e8f0;
        }

        .table-title {
            font-size: 1.5rem;
            font-weight: 700;
            color: #1a202c;
            display: flex;
            align-items: center;
            gap: 0.5rem;
            margin: 0;
        }

        .table-title i {
            color: #4299e1;
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
            padding: 1.25rem 1.5rem;
            text-align: left;
            font-weight: 700;
            color: #4a5568;
            font-size: 0.85rem;
            text-transform: uppercase;
            letter-spacing: 0.5px;
            border-bottom: 2px solid #e2e8f0;
        }

        .data-table td {
            padding: 1.25rem 1.5rem;
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
            color: #48bb78;
            font-size: 1rem;
        }

        /* Responsive Design */
        @media (max-width: 768px) {
            .container {
                padding: 1rem;
            }

            .header {
                padding: 1.5rem;
            }

            .header-title h1 {
                font-size: 1.75rem;
            }

            .header-subtitle {
                margin-left: 0;
                margin-top: 0.5rem;
            }

            .stats-grid {
                grid-template-columns: 1fr;
            }

            .chart-container,
            .table-container {
                padding: 1.5rem;
            }

            .bar-info {
                min-width: 150px;
            }

            .data-table {
                font-size: 0.85rem;
            }

            .data-table th,
            .data-table td {
                padding: 1rem 0.75rem;
            }

            .chart-bar {
                flex-direction: column;
                align-items: stretch;
                gap: 0.5rem;
            }

            .bar-track {
                order: 2;
            }

            .bar-value {
                order: 3;
                text-align: left;
            }
        }
    </style>
</head>
<body>
    <div class="container">
        <!-- Header Section -->
        <div class="header">
            <div style="display: flex; justify-content: space-between; align-items: flex-start;">
                <div class="header-title">
                    <i class="fas fa-chart-line"></i>
                    <h1>Reporte de Cotizaciones</h1>
                </div>
                <button class="btn-export-pdf" type="button" style="background: #e53e3e; color: #fff; border: none; border-radius: 8px; padding: 0.7rem 1.5rem; font-weight: 600; font-size: 1rem; box-shadow: 0 2px 8px rgba(66,153,225,0.08); transition: background 0.2s, color 0.2s, transform 0.2s; margin-left: 1rem; cursor: pointer; display: flex; align-items: center; gap: 0.5rem;">
                    <i class="fas fa-file-pdf"></i> Exportar PDF
                </button>
            </div>
            <p class="header-subtitle">Analisis detallado de cotizaciones y comisiones</p>
        </div>

        <!-- Stats Section -->
        <div class="stats-grid">
            <div class="stat-card primary">
                <div class="stat-header">
                    <span class="stat-title">Total Cotizaciones</span>
                    <div class="stat-icon">
                        <i class="fas fa-file-alt"></i>
                    </div>
                </div>
                <div class="stat-value">4</div>
            </div>
            
            <div class="stat-card purple">
                <div class="stat-header">
                    <span class="stat-title">Monto Total</span>
                    <div class="stat-icon">
                        <i class="fas fa-dollar-sign"></i>
                    </div>
                </div>
                <div class="stat-value">$45,000</div>
            </div>
            
            <div class="stat-card green">
                <div class="stat-header">
                    <span class="stat-title">Comision Total (5%)</span>
                    <div class="stat-icon">
                        <i class="fas fa-percentage"></i>
                    </div>
                </div>
                <div class="stat-value">$2,250</div>
            </div>
        </div>

        <!-- Chart Section -->
        <div class="chart-container">
            <h3 class="chart-title">
                <i class="fas fa-chart-bar"></i>
                Distribucion por Monto
            </h3>
            
            <div class="chart-bars">
                <div class="chart-bar">
                    <div class="bar-info">
                        <span class="folio-badge">COT-001</span>
                        <span class="client-name">Juan Perez</span>
                    </div>
                    <div class="bar-track">
                        <div class="bar-fill" style="width: 67%;"></div>
                    </div>
                    <span class="bar-value">$10,000</span>
                </div>
                
                <div class="chart-bar">
                    <div class="bar-info">
                        <span class="folio-badge">COT-002</span>
                        <span class="client-name">Maria Lopez</span>
                    </div>
                    <div class="bar-track">
                        <div class="bar-fill" style="width: 53%;"></div>
                    </div>
                    <span class="bar-value">$8,000</span>
                </div>
                
                <div class="chart-bar">
                    <div class="bar-info">
                        <span class="folio-badge">COT-003</span>
                        <span class="client-name">Carlos Ruiz</span>
                    </div>
                    <div class="bar-track">
                        <div class="bar-fill" style="width: 100%;"></div>
                    </div>
                    <span class="bar-value">$15,000</span>
                </div>
                
                <div class="chart-bar">
                    <div class="bar-info">
                        <span class="folio-badge">COT-004</span>
                        <span class="client-name">Ana Torres</span>
                    </div>
                    <div class="bar-track">
                        <div class="bar-fill" style="width: 80%;"></div>
                    </div>
                    <span class="bar-value">$12,000</span>
                </div>
            </div>
        </div>

        <!-- Table Section -->
        <div class="table-container">
            <div class="table-header">
                <h3 class="table-title">
                    <i class="fas fa-table"></i>
                    Detalle de Cotizaciones
                </h3>
            </div>
            
            <table class="data-table">
                <thead>
                    <tr>
                        <th>Folio</th>
                        <th>Cliente</th>
                        <th>Descripcion</th>
                        <th>Monto</th>
                        <th>Comision (5%)</th>
                        <th style="text-align: right; min-width: 170px;">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td><span class="table-folio">COT-001</span></td>
                        <td>Juan Perez</td>
                        <td>Sistema de Gestion Empresarial</td>
                        <td><span class="amount">$10,000</span></td>
                        <td><span class="commission">$500</span></td>
                        <td style="text-align: right;">
                            <button class="btn-export-pdf" title="Exportar PDF" style="background:#e53e3e;color:#fff;border:none;border-radius:6px;padding:6px 10px;margin-right:4px;cursor:pointer;"><i class="fas fa-file-pdf"></i></button>
                            <button class="btn-export-excel" title="Exportar Excel" style="background:#38a169;color:#fff;border:none;border-radius:6px;padding:6px 10px;margin-right:4px;cursor:pointer;"><i class="fas fa-file-excel"></i></button>
                            <button class="btn-export-csv" title="Exportar CSV" style="background:#3182ce;color:#fff;border:none;border-radius:6px;padding:6px 10px;cursor:pointer;"><i class="fas fa-file-csv"></i></button>
                        </td>
                    </tr>
                    <tr>
                        <td><span class="table-folio">COT-002</span></td>
                        <td>Maria Lopez</td>
                        <td>Aplicacion Web Corporativa</td>
                        <td><span class="amount">$8,000</span></td>
                        <td><span class="commission">$400</span></td>
                        <td style="text-align: right;">
                            <button class="btn-export-pdf" title="Exportar PDF" style="background:#e53e3e;color:#fff;border:none;border-radius:6px;padding:6px 10px;margin-right:4px;cursor:pointer;"><i class="fas fa-file-pdf"></i></button>
                            <button class="btn-export-excel" title="Exportar Excel" style="background:#38a169;color:#fff;border:none;border-radius:6px;padding:6px 10px;margin-right:4px;cursor:pointer;"><i class="fas fa-file-excel"></i></button>
                            <button class="btn-export-csv" title="Exportar CSV" style="background:#3182ce;color:#fff;border:none;border-radius:6px;padding:6px 10px;cursor:pointer;"><i class="fas fa-file-csv"></i></button>
                        </td>
                    </tr>
                    <tr>
                        <td><span class="table-folio">COT-003</span></td>
                        <td>Carlos Ruiz</td>
                        <td>Plataforma E-commerce</td>
                        <td><span class="amount">$15,000</span></td>
                        <td><span class="commission">$750</span></td>
                        <td style="text-align: right;">
                            <button class="btn-export-pdf" title="Exportar PDF" style="background:#e53e3e;color:#fff;border:none;border-radius:6px;padding:6px 10px;margin-right:4px;cursor:pointer;"><i class="fas fa-file-pdf"></i></button>
                            <button class="btn-export-excel" title="Exportar Excel" style="background:#38a169;color:#fff;border:none;border-radius:6px;padding:6px 10px;margin-right:4px;cursor:pointer;"><i class="fas fa-file-excel"></i></button>
                            <button class="btn-export-csv" title="Exportar CSV" style="background:#3182ce;color:#fff;border:none;border-radius:6px;padding:6px 10px;cursor:pointer;"><i class="fas fa-file-csv"></i></button>
                        </td>
                    </tr>
                    <tr>
                        <td><span class="table-folio">COT-004</span></td>
                        <td>Ana Torres</td>
                        <td>Sistema de Inventarios</td>
                        <td><span class="amount">$12,000</span></td>
                        <td><span class="commission">$600</span></td>
                        <td style="text-align: right;">
                            <button class="btn-export-pdf" title="Exportar PDF" style="background:#e53e3e;color:#fff;border:none;border-radius:6px;padding:6px 10px;margin-right:4px;cursor:pointer;"><i class="fas fa-file-pdf"></i></button>
                            <button class="btn-export-excel" title="Exportar Excel" style="background:#38a169;color:#fff;border:none;border-radius:6px;padding:6px 10px;margin-right:4px;cursor:pointer;"><i class="fas fa-file-excel"></i></button>
                            <button class="btn-export-csv" title="Exportar CSV" style="background:#3182ce;color:#fff;border:none;border-radius:6px;padding:6px 10px;cursor:pointer;"><i class="fas fa-file-csv"></i></button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</body>
</html>
