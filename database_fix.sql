-- Script para corregir la estructura de la base de datos
USE cotizadores_proteo;

-- 1. Corregir la tabla usuarios - hacer campos opcionales
ALTER TABLE usuarios 
MODIFY COLUMN carrera VARCHAR(100) NULL,
MODIFY COLUMN direccion VARCHAR(200) NULL;

-- 2. Agregar la clave foránea faltante en cotizaciones
ALTER TABLE cotizaciones 
ADD CONSTRAINT fk_cotizaciones_usuario_vendedor 
FOREIGN KEY (id_usuario_vendedor) REFERENCES usuarios(id_usuario);

-- 3. Insertar datos de prueba para roles si no existen
INSERT IGNORE INTO roles (id_rol, nombre) VALUES 
(1, 'Administrador'),
(2, 'Vendedor'),
(3, 'Cliente');

-- 4. Insertar datos de prueba para empresas si no existen
INSERT IGNORE INTO empresas (id_empresa, nombre) VALUES 
(1, 'Empresa Demo');

-- 5. Insertar datos de prueba para países si no existen
INSERT IGNORE INTO paises (id_pais, nombre) VALUES 
(1, 'México');

-- 6. Insertar datos de prueba para estados si no existen
INSERT IGNORE INTO estados (id_estado, nombre, id_pais) VALUES 
(1, 'Jalisco', 1);

-- 7. Insertar datos de prueba para municipios si no existen
INSERT IGNORE INTO municipios (id_municipio, nombre, id_estado) VALUES 
(1, 'Guadalajara', 1);

-- 8. Insertar datos de prueba para colonias si no existen
INSERT IGNORE INTO colonias (id_colonia, nombre, ciudad, id_municipio, asentamiento, codigo_postal) VALUES 
(1, 'Centro', 'Guadalajara', 1, 'Centro Histórico', '44100');

-- Verificar que las correcciones se aplicaron correctamente
SELECT 'Tabla usuarios corregida' as status;
DESCRIBE usuarios;

SELECT 'Roles disponibles:' as status;
SELECT * FROM roles;

SELECT 'Empresas disponibles:' as status;
SELECT * FROM empresas; 