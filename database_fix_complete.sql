-- Script completo para corregir la base de datos y insertar datos necesarios
USE cotizadores_proteo;

-- 1. Verificar que las tablas existen
SELECT 'Verificando existencia de tablas:' as status;
SHOW TABLES;

-- 2. Corregir la tabla usuarios - hacer campos opcionales si es necesario
-- (Comentado porque según el CREATE TABLE original, estos campos son NOT NULL)
-- ALTER TABLE usuarios MODIFY COLUMN carrera VARCHAR(100) NULL;
-- ALTER TABLE usuarios MODIFY COLUMN direccion VARCHAR(200) NULL;

-- 3. Agregar la clave foránea faltante en cotizaciones si no existe
-- Verificar si ya existe la foreign key
SELECT 'Verificando foreign key en cotizaciones:' as status;
SELECT 
    CONSTRAINT_NAME,
    COLUMN_NAME,
    REFERENCED_TABLE_NAME,
    REFERENCED_COLUMN_NAME
FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
WHERE TABLE_SCHEMA = 'cotizadores_proteo' 
AND TABLE_NAME = 'cotizaciones' 
AND COLUMN_NAME = 'id_usuario_vendedor';

-- Si no existe, agregarla
-- ALTER TABLE cotizaciones ADD CONSTRAINT fk_cotizaciones_usuario_vendedor FOREIGN KEY (id_usuario_vendedor) REFERENCES usuarios(id_usuario);

-- 4. Insertar datos de prueba para roles si no existen
SELECT 'Insertando datos de prueba para roles:' as status;
INSERT IGNORE INTO roles (id_rol, nombre) VALUES 
(1, 'Administrador'),
(2, 'Vendedor'),
(3, 'Cliente');

-- 5. Insertar datos de prueba para empresas si no existen
SELECT 'Insertando datos de prueba para empresas:' as status;
INSERT IGNORE INTO empresas (id_empresa, nombre) VALUES 
(1, 'Empresa Demo');

-- 6. Insertar datos de prueba para países si no existen
SELECT 'Insertando datos de prueba para países:' as status;
INSERT IGNORE INTO paises (id_pais, nombre) VALUES 
(1, 'México');

-- 7. Insertar datos de prueba para estados si no existen
SELECT 'Insertando datos de prueba para estados:' as status;
INSERT IGNORE INTO estados (id_estado, nombre, id_pais) VALUES 
(1, 'Jalisco', 1);

-- 8. Insertar datos de prueba para municipios si no existen
SELECT 'Insertando datos de prueba para municipios:' as status;
INSERT IGNORE INTO municipios (id_municipio, nombre, id_estado) VALUES 
(1, 'Guadalajara', 1);

-- 9. Insertar datos de prueba para colonias si no existen
SELECT 'Insertando datos de prueba para colonias:' as status;
INSERT IGNORE INTO colonias (id_colonia, nombre, ciudad, id_municipio, asentamiento, codigo_postal) VALUES 
(1, 'Centro', 'Guadalajara', 1, 'Centro Histórico', '44100');

-- 10. Verificar que las correcciones se aplicaron correctamente
SELECT 'Verificando estructura de tabla usuarios:' as status;
DESCRIBE usuarios;

SELECT 'Roles disponibles:' as status;
SELECT * FROM roles;

SELECT 'Empresas disponibles:' as status;
SELECT * FROM empresas;

SELECT 'Paises disponibles:' as status;
SELECT * FROM paises;

SELECT 'Estados disponibles:' as status;
SELECT * FROM estados;

SELECT 'Municipios disponibles:' as status;
SELECT * FROM municipios;

SELECT 'Colonias disponibles:' as status;
SELECT * FROM colonias;

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

-- 12. Verificar permisos del usuario
SELECT 'Verificando permisos del usuario:' as status;
SHOW GRANTS FOR 'proteo'@'localhost';

-- 13. Verificar configuración de MySQL
SELECT 'Configuración de MySQL:' as status;
SHOW VARIABLES LIKE 'sql_mode';
SHOW VARIABLES LIKE 'character_set%';
SHOW VARIABLES LIKE 'collation%';

-- 14. Verificar que las tablas tienen índices apropiados
SELECT 'Verificando índices:' as status;
SHOW INDEX FROM roles;
SHOW INDEX FROM empresas;
SHOW INDEX FROM usuarios;
SHOW INDEX FROM sucursales;
SHOW INDEX FROM contactos;

-- 15. Resumen final
SELECT 'Resumen de datos en la base de datos:' as status;
SELECT 'Roles:' as tabla, COUNT(*) as total FROM roles
UNION ALL
SELECT 'Empresas:' as tabla, COUNT(*) as total FROM empresas
UNION ALL
SELECT 'Paises:' as tabla, COUNT(*) as total FROM paises
UNION ALL
SELECT 'Estados:' as tabla, COUNT(*) as total FROM estados
UNION ALL
SELECT 'Municipios:' as tabla, COUNT(*) as total FROM municipios
UNION ALL
SELECT 'Colonias:' as tabla, COUNT(*) as total FROM colonias
UNION ALL
SELECT 'Usuarios:' as tabla, COUNT(*) as total FROM usuarios
UNION ALL
SELECT 'Sucursales:' as tabla, COUNT(*) as total FROM sucursales
UNION ALL
SELECT 'Contactos:' as tabla, COUNT(*) as total FROM contactos; 