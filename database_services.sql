-- Insertar datos de prueba para servicios
INSERT INTO servicios (nombre, descripcion, terminos, incluye, condiciones) VALUES 
('Curso Básico de Programación', 'Curso introductorio a la programación para principiantes', 'Pago anticipado', 'Manual, certificado y coffee break', 'Mínimo 10 personas'),
('Curso Básico de Programación', 'Curso introductorio a la programación para principiantes', 'Pago anticipado', 'Manual, certificado y coffee break', 'Mínimo 10 personas'),
('Curso Avanzado de Desarrollo Web', 'Curso completo de desarrollo web con tecnologías modernas', 'Pago 50% anticipado', 'Material digital y coffee break', 'Solo días hábiles'),
('Consultoría en Tecnología', 'Asesoría especializada en implementación de soluciones tecnológicas', 'Pago mensual', 'Reportes detallados y seguimiento', 'Horario de oficina'),
('Desarrollo de Software a Medida', 'Desarrollo de aplicaciones personalizadas según necesidades del cliente', 'Pago por fases', 'Código fuente y documentación', 'Tiempo estimado según complejidad'),
('Mantenimiento de Sistemas', 'Servicio de mantenimiento preventivo y correctivo de sistemas', 'Contrato anual', 'Monitoreo 24/7 y soporte técnico', 'Respuesta en 4 horas');

-- Insertar datos de prueba para sucursales
INSERT INTO sucursales (id_sucursal, nom_comercial, razon_social, giro, telefono, correo, direccion, metros, id_empresa, id_colonia) VALUES 
(1, 'Sucursal Centro', 'Empresa Demo S.A. de C.V.', 'Tecnología', '3331234567', 'centro@empresademo.com', 'Av. Juárez 123', 150.5, 1, 1),
(2, 'Sucursal Norte', 'Empresa Demo S.A. de C.V.', 'Tecnología', '3331234568', 'norte@empresademo.com', 'Av. Vallarta 456', 200.0, 1, 1),
(3, 'Sucursal Sur', 'Empresa Demo S.A. de C.V.', 'Tecnología', '3331234569', 'sur@empresademo.com', 'Av. López Mateos 789', 180.3, 1, 1);

-- Insertar datos de prueba para usuarios
INSERT INTO usuarios (id_usuario, nombre, ap_paterno, ap_materno, correo, telefono, tel_empresa, carrera, direccion, fecha_activacion, fecha_ingreso, fecha_termino, nom_usuario, contraseña, estatus, id_rol) VALUES 
(1, 'Juan', 'Pérez', 'García', 'juan.perez@empresa.com', '3331234567', '3331234568', 'Ingeniería en Sistemas', 'Calle 123 #456', '2024-01-01', '2024-01-01', NULL, 'jperez', 'password123', 1, 2),
(2, 'María', 'González', 'López', 'maria.gonzalez@empresa.com', '3331234569', '3331234570', 'Ingeniería en Informática', 'Calle 789 #012', '2024-01-01', '2024-01-01', NULL, 'mgonzalez', 'password123', 1, 2),
(3, 'Carlos', 'Rodríguez', 'Martínez', 'carlos.rodriguez@empresa.com', '3331234571', '3331234572', 'Ingeniería en Software', 'Calle 345 #678', '2024-01-01', '2024-01-01', NULL, 'crodriguez', 'password123', 1, 2); 