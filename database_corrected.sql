CREATE DATABASE IF NOT EXISTS cotizadores_proteo;
USE cotizadores_proteo;

CREATE TABLE paises (
  id_pais INT AUTO_INCREMENT NOT NULL,
  nombre VARCHAR(100) NOT NULL,
  PRIMARY KEY (id_pais)
);

CREATE TABLE estados (
  id_estado INT AUTO_INCREMENT NOT NULL,
  nombre VARCHAR(100) NOT NULL,
  id_pais INT NOT NULL,
  PRIMARY KEY (id_estado),
  FOREIGN KEY (id_pais) REFERENCES paises(id_pais)
);

CREATE TABLE municipios (
  id_municipio INT AUTO_INCREMENT NOT NULL,
  nombre VARCHAR(100) NOT NULL,
  id_estado INT NOT NULL,
  PRIMARY KEY (id_municipio),
  FOREIGN KEY (id_estado) REFERENCES estados(id_estado)
);

CREATE TABLE colonias (
  id_colonia INT AUTO_INCREMENT NOT NULL,
  nombre VARCHAR(100) NOT NULL,
  ciudad VARCHAR(100) NOT NULL,
  id_municipio INT NOT NULL,
  asentamiento VARCHAR(100),
  codigo_postal VARCHAR(10),
  PRIMARY KEY (id_colonia),
  FOREIGN KEY (id_municipio) REFERENCES municipios(id_municipio)
);

CREATE TABLE empresas (
  id_empresa INT AUTO_INCREMENT NOT NULL,
  nombre VARCHAR(100) NOT NULL,
  PRIMARY KEY (id_empresa)
);

CREATE TABLE roles (
  id_rol INT AUTO_INCREMENT NOT NULL,
  nombre VARCHAR(100) NOT NULL,
  PRIMARY KEY (id_rol)
);

CREATE TABLE usuarios (
  id_usuario INT AUTO_INCREMENT NOT NULL,
  nombre VARCHAR(100) NOT NULL,
  ap_paterno VARCHAR(100) NOT NULL,
  ap_materno VARCHAR(100),
  correo VARCHAR(100) NOT NULL,
  telefono VARCHAR(15) NOT NULL,
  tel_empresa VARCHAR(15),
  carrera VARCHAR(100) NOT NULL,
  direccion VARCHAR(200) NOT NULL,
  fecha_activacion DATE NOT NULL,
  fecha_ingreso DATE,
  fecha_termino DATE,
  nom_usuario VARCHAR(50) NOT NULL,
  contraseña VARCHAR(100) NOT NULL,
  estatus BOOLEAN NOT NULL,
  id_rol INT NOT NULL,
  PRIMARY KEY (id_usuario),
  FOREIGN KEY (id_rol) REFERENCES roles(id_rol)
);

CREATE TABLE sucursales (
  id_sucursal INT AUTO_INCREMENT NOT NULL,
  nom_comercial VARCHAR(100) NOT NULL,
  razon_social VARCHAR(150),
  giro VARCHAR(100),
  telefono VARCHAR(15) NOT NULL,
  correo VARCHAR(100) NOT NULL,
  direccion VARCHAR(200) NOT NULL,
  metros DOUBLE,
  id_empresa INT NOT NULL,
  id_colonia INT NOT NULL,
  PRIMARY KEY (id_sucursal),
  FOREIGN KEY (id_empresa) REFERENCES empresas(id_empresa),
  FOREIGN KEY (id_colonia) REFERENCES colonias(id_colonia)
);

CREATE TABLE contactos (
  id_contacto INT AUTO_INCREMENT NOT NULL,
  nombre VARCHAR(100) NOT NULL,
  ap_paterno VARCHAR(100) NOT NULL,
  ap_materno VARCHAR(100),
  correo VARCHAR(100) NOT NULL,
  telefono VARCHAR(15) NOT NULL,
  puesto VARCHAR(100) NOT NULL,
  id_sucursal INT NOT NULL,
  PRIMARY KEY (id_contacto),
  FOREIGN KEY (id_sucursal) REFERENCES sucursales(id_sucursal)
);

CREATE TABLE cotizaciones (
  id_cotizacion INT AUTO_INCREMENT NOT NULL,
  folio VARCHAR(50) NOT NULL,
  fecha_emision DATE NOT NULL,
  fecha_vencimiento DATE NOT NULL,
  id_usuario_vendedor INT NOT NULL,
  nota TEXT,
  subtotal DOUBLE NOT NULL,
  iva DOUBLE NOT NULL,
  total DOUBLE NOT NULL,
  estatus CHAR(1) NOT NULL,
  version INT NOT NULL,
  ultima_version INT NOT NULL,
  fecha_guardado DATE NOT NULL,
  comentario TEXT,
  id_usuario INT NOT NULL,
  id_sucursal INT NOT NULL,
  PRIMARY KEY (id_cotizacion),
  FOREIGN KEY (id_usuario) REFERENCES usuarios(id_usuario),
  FOREIGN KEY (id_usuario_vendedor) REFERENCES usuarios(id_usuario),
  FOREIGN KEY (id_sucursal) REFERENCES sucursales(id_sucursal)
);

CREATE TABLE servicios (
  id_servicio INT AUTO_INCREMENT NOT NULL,
  nombre VARCHAR(100) NOT NULL,
  descripcion TEXT,
  terminos TEXT,
  incluye TEXT,
  condiciones TEXT,
  PRIMARY KEY (id_servicio)
);

CREATE TABLE cotizacion_servicios (
  id_cot_servicio INT AUTO_INCREMENT NOT NULL,
  descripcion TEXT NOT NULL,
  cantidad INT NOT NULL,
  valor_unitario DOUBLE NOT NULL,
  subtotal DOUBLE NOT NULL,
  terminos TEXT,
  incluye TEXT,
  condiciones TEXT,
  id_cotizacion INT NOT NULL,
  id_servicio INT NOT NULL,
  PRIMARY KEY (id_cot_servicio),
  FOREIGN KEY (id_cotizacion) REFERENCES cotizaciones(id_cotizacion),
  FOREIGN KEY (id_servicio) REFERENCES servicios(id_servicio)
);

CREATE TABLE plantillas (
  id_plantilla INT AUTO_INCREMENT NOT NULL,
  nombre VARCHAR(100) NOT NULL,
  descripcion TEXT,
  fecha_creacion DATE NOT NULL,
  id_usuario INT NOT NULL,
  PRIMARY KEY (id_plantilla),
  FOREIGN KEY (id_usuario) REFERENCES usuarios(id_usuario)
);

CREATE TABLE plantilla_servicios (
  id_plantilla INT NOT NULL,
  id_servicio INT NOT NULL,
  PRIMARY KEY (id_plantilla, id_servicio),
  FOREIGN KEY (id_plantilla) REFERENCES plantillas(id_plantilla),
  FOREIGN KEY (id_servicio) REFERENCES servicios(id_servicio)
);

CREATE TABLE privilegios (
  id_privilegio INT AUTO_INCREMENT NOT NULL,
  nombre VARCHAR(100) NOT NULL,
  privilegio INT NOT NULL,
  PRIMARY KEY (id_privilegio)
);

CREATE TABLE privilegio_rol (
  id_privilegio_rol INT AUTO_INCREMENT NOT NULL,
  privilegio INT NOT NULL,
  rol INT NOT NULL,
  PRIMARY KEY (id_privilegio_rol),
  FOREIGN KEY (privilegio) REFERENCES privilegios(id_privilegio),
  FOREIGN KEY (rol) REFERENCES roles(id_rol)
);

-- Insertar datos de prueba para roles
INSERT INTO roles (id_rol, nombre) VALUES 
(1, 'Administrador'),
(2, 'Vendedor'),
(3, 'Cliente');

-- Insertar datos de prueba para empresas
INSERT INTO empresas (id_empresa, nombre) VALUES 
(1, 'Empresa Demo');

-- Insertar datos de prueba para países
INSERT INTO paises (id_pais, nombre) VALUES 
(1, 'México');

-- Insertar datos de prueba para estados
INSERT INTO estados (id_estado, nombre, id_pais) VALUES 
(1, 'Jalisco', 1);

-- Insertar datos de prueba para municipios
INSERT INTO municipios (id_municipio, nombre, id_estado) VALUES 
(1, 'Guadalajara', 1);

-- Insertar datos de prueba para colonias
INSERT INTO colonias (id_colonia, nombre, ciudad, id_municipio, asentamiento, codigo_postal) VALUES 
(1, 'Centro', 'Guadalajara', 1, 'Centro Histórico', '44100'); 