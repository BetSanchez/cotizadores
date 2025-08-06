-- Script para diagnosticar y corregir problemas de login
USE cotizadores_proteo;

-- 1. Verificar que la base de datos existe y está accesible
SELECT 'Verificando acceso a la base de datos:' as status;
SELECT DATABASE() as current_database;

-- 2. Verificar que las tablas necesarias existen
SELECT 'Verificando existencia de tablas:' as status;
SHOW TABLES;

-- 3. Verificar estructura de la tabla usuarios
SELECT 'Verificando estructura de tabla usuarios:' as status;
DESCRIBE usuarios;

-- 4. Verificar que existen roles
SELECT 'Verificando roles disponibles:' as status;
SELECT * FROM roles;

-- 5. Verificar usuarios existentes (sin mostrar contraseñas)
SELECT 'Verificando usuarios existentes:' as status;
SELECT 
    id_usuario,
    nombre,
    ap_paterno,
    ap_materno,
    correo,
    nom_usuario,
    estatus,
    id_rol,
    fecha_activacion
FROM usuarios;

-- 6. Verificar permisos del usuario de conexión
SELECT 'Verificando permisos del usuario proteo:' as status;
SHOW GRANTS FOR 'proteo'@'localhost';

-- 7. Verificar configuración de MySQL
SELECT 'Verificando configuración de MySQL:' as status;
SHOW VARIABLES LIKE 'max_connections';
SHOW VARIABLES LIKE 'wait_timeout';
SHOW VARIABLES LIKE 'interactive_timeout';

-- 8. Verificar estado de las conexiones
SELECT 'Verificando conexiones activas:' as status;
SHOW PROCESSLIST;

-- 9. Crear un usuario de prueba si no existe
SELECT 'Creando usuario de prueba si no existe:' as status;
INSERT IGNORE INTO usuarios (
    nombre, 
    ap_paterno, 
    ap_materno, 
    correo, 
    telefono, 
    carrera, 
    direccion, 
    fecha_activacion, 
    nom_usuario, 
    contraseña, 
    estatus, 
    id_rol
) VALUES (
    'Usuario',
    'Prueba',
    'Sistema',
    'admin@test.com',
    '1234567890',
    'Administrador',
    'Dirección de prueba',
    NOW(),
    'admin',
    'QWRtaW4yMDI0', -- Base64 de "Admin2024"
    true,
    1
);

-- 10. Verificar que el usuario de prueba se creó correctamente
SELECT 'Verificando usuario de prueba:' as status;
SELECT 
    id_usuario,
    nombre,
    nom_usuario,
    estatus,
    id_rol
FROM usuarios 
WHERE nom_usuario = 'admin';

-- 11. Verificar foreign keys
SELECT 'Verificando foreign keys:' as status;
SELECT 
    TABLE_NAME,
    COLUMN_NAME,
    CONSTRAINT_NAME,
    REFERENCED_TABLE_NAME,
    REFERENCED_COLUMN_NAME
FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
WHERE REFERENCED_TABLE_SCHEMA = 'cotizadores_proteo'
ORDER BY TABLE_NAME, COLUMN_NAME;

-- 12. Verificar índices en la tabla usuarios
SELECT 'Verificando índices en tabla usuarios:' as status;
SHOW INDEX FROM usuarios;

-- 13. Probar consulta de autenticación
SELECT 'Probando consulta de autenticación:' as status;
SELECT 
    u.id_usuario,
    u.nombre,
    u.nom_usuario,
    u.estatus,
    r.nombre as rol_nombre
FROM usuarios u
LEFT JOIN roles r ON u.id_rol = r.id_rol
WHERE u.nom_usuario = 'admin' 
AND u.contraseña = 'QWRtaW4yMDI0' 
AND u.estatus = true;

-- 14. Verificar configuración de Entity Framework
SELECT 'Verificando configuración de EF:' as status;
SELECT 
    TABLE_SCHEMA,
    TABLE_NAME,
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE,
    COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_SCHEMA = 'cotizadores_proteo' 
AND TABLE_NAME = 'usuarios'
ORDER BY ORDINAL_POSITION; 