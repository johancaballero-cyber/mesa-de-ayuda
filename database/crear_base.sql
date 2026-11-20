
CREATE DATABASE IF NOT EXISTS mesa_ayuda_db
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE mesa_ayuda_db;

CREATE TABLE IF NOT EXISTS categorias (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS tickets (
    id INT AUTO_INCREMENT PRIMARY KEY,
    titulo VARCHAR(150) NOT NULL,
    descripcion TEXT NOT NULL,
    categoria_id INT NOT NULL,
    estado ENUM('Abierto', 'Cerrado') NOT NULL DEFAULT 'Abierto',
    fecha_creacion DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    fecha_cierre DATETIME NULL,

    CONSTRAINT fk_ticket_categoria
        FOREIGN KEY (categoria_id)
        REFERENCES categorias(id),

    CONSTRAINT chk_titulo
        CHECK (CHAR_LENGTH(TRIM(titulo)) > 0),

    CONSTRAINT chk_descripcion
        CHECK (CHAR_LENGTH(TRIM(descripcion)) > 0)
);

INSERT IGNORE INTO categorias (nombre)
VALUES
    ('Hardware'),
    ('Software'),
    ('Redes'),
    ('Otros');
  
  
  