-- Script para diagnosticar y corregir problemas con registro de vendedores
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
    fecha_activacion,
    carrera,
    direccion
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

-- 9. Verificar foreign keys en la tabla usuarios
SELECT 'Verificando foreign keys en usuarios:' as status;
SELECT 
    TABLE_NAME,
    COLUMN_NAME,
    CONSTRAINT_NAME,
    REFERENCED_TABLE_NAME,
    REFERENCED_COLUMN_NAME
FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
WHERE TABLE_SCHEMA = 'cotizadores_proteo'
AND TABLE_NAME = 'usuarios'
ORDER BY COLUMN_NAME;

-- 10. Verificar índices en la tabla usuarios
SELECT 'Verificando índices en tabla usuarios:' as status;
SHOW INDEX FROM usuarios;

-- 11. Verificar restricciones NOT NULL
SELECT 'Verificando restricciones NOT NULL:' as status;
SELECT 
    COLUMN_NAME,
    IS_NULLABLE,
    COLUMN_DEFAULT,
    DATA_TYPE
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_SCHEMA = 'cotizadores_proteo' 
AND TABLE_NAME = 'usuarios'
AND IS_NULLABLE = 'NO'
ORDER BY ORDINAL_POSITION;

-- 12. Probar inserción de un vendedor de prueba
SELECT 'Probando inserción de vendedor de prueba:' as status;
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
    'Vendedor',
    'Prueba',
    'Sistema',
    'vendedor@test.com',
    '1234567890',
    'Ventas',
    'Dirección de prueba',
    NOW(),
    'vendedor_test',
    'VmVuZGVkb3IyMDI0', -- Base64 de "Vendedor2024"
    true,
    2
);

-- 13. Verificar que el vendedor de prueba se creó correctamente
SELECT 'Verificando vendedor de prueba:' as status;
SELECT 
    id_usuario,
    nombre,
    nom_usuario,
    estatus,
    id_rol,
    carrera,
    direccion
FROM usuarios 
WHERE nom_usuario = 'vendedor_test';

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

-- 15. Verificar que no hay problemas de codificación
SELECT 'Verificando codificación de caracteres:' as status;
SHOW VARIABLES LIKE 'character_set%';
SHOW VARIABLES LIKE 'collation%';

-- 16. Verificar que la tabla usuarios tiene la codificación correcta
SELECT 'Verificando codificación de tabla usuarios:' as status;
SELECT 
    TABLE_NAME,
    TABLE_COLLATION
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_SCHEMA = 'cotizadores_proteo' 
AND TABLE_NAME = 'usuarios';

-- 17. Probar consulta de autenticación para el vendedor
SELECT 'Probando autenticación del vendedor:' as status;
SELECT 
    u.id_usuario,
    u.nombre,
    u.nom_usuario,
    u.estatus,
    r.nombre as rol_nombre
FROM usuarios u
LEFT JOIN roles r ON u.id_rol = r.id_rol
WHERE u.nom_usuario = 'vendedor_test' 
AND u.contraseña = 'VmVuZGVkb3IyMDI0' 
AND u.estatus = true;

-- 18. Verificar que no hay registros duplicados
SELECT 'Verificando duplicados en usuarios:' as status;
SELECT 
    nom_usuario,
    COUNT(*) as cantidad
FROM usuarios 
GROUP BY nom_usuario 
HAVING COUNT(*) > 1;

SELECT 
    correo,
    COUNT(*) as cantidad
FROM usuarios 
GROUP BY correo 
HAVING COUNT(*) > 1; 