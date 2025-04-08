CREATE TABLE Roles (
    ro_identificador INT IDENTITY(1,1) PRIMARY KEY,
    ro_decripcion NVARCHAR(255) NOT NULL,
    ro_fecha_adicion DATETIME NOT NULL,
    ro_adicionado_por NVARCHAR(255) NOT NULL,
    ro_fecha_modificacion DATETIME NULL,
    ro_modificado_por NVARCHAR(255) NULL
);

INSERT INTO Roles (ro_decripcion, ro_fecha_adicion, ro_adicionado_por)
VALUES 
  ('Administrador', GETDATE(), 'sistema'),
  ('Usuario', GETDATE(), 'sistema');

CREATE TABLE Categorias (
    ca_identificador INT IDENTITY(1,1) PRIMARY KEY,
    ca_descripcion NVARCHAR(255) NOT NULL,
    ca_fecha_adicion DATETIME NOT NULL,
    ca_adicionado_por NVARCHAR(255) NOT NULL,
    ca_fecha_modificacion DATETIME NULL,
    ca_modificado_por NVARCHAR(255) NULL
);

INSERT INTO Categorias (ca_descripcion, ca_fecha_adicion, ca_adicionado_por)
VALUES 
  ('Software', GETDATE(), 'sistema'),
  ('Hardware', GETDATE(), 'sistema'),
  ('Redes', GETDATE(), 'sistema');

CREATE TABLE Importancias (
    im_identificador INT IDENTITY(1,1) PRIMARY KEY,
    im_descripcion NVARCHAR(255) NOT NULL,
    im_fecha_adicion DATETIME NOT NULL,
    im_adicionado_por NVARCHAR(255) NOT NULL,
    im_fecha_modificacion DATETIME NULL,
    im_modificado_por NVARCHAR(255) NULL
);

INSERT INTO Importancias (im_descripcion, im_fecha_adicion, im_adicionado_por)
VALUES 
  ('Alta', GETDATE(), 'sistema'),
  ('Media', GETDATE(), 'sistema'),
  ('Baja', GETDATE(), 'sistema');

CREATE TABLE Urgencias (
    ur_identificador INT IDENTITY(1,1) PRIMARY KEY,
    ur_descripcion NVARCHAR(255) NOT NULL,
    ur_fecha_adicion DATETIME NOT NULL,
    ur_adicionado_por NVARCHAR(255) NOT NULL,
    ur_fecha_modificacion DATETIME NULL,
    ur_modificado_por NVARCHAR(255) NULL
);

INSERT INTO Urgencias (ur_descripcion, ur_fecha_adicion, ur_adicionado_por)
VALUES 
  ('Urgente', GETDATE(), 'sistema'),
  ('No urgente', GETDATE(), 'sistema'),
  ('Normal', GETDATE(), 'sistema');

CREATE TABLE Usuarios (
    us_identificador INT IDENTITY(1,1) PRIMARY KEY,
    us_nombre_completo NVARCHAR(255) NOT NULL,
    us_correo NVARCHAR(255) NOT NULL,
    us_clave NVARCHAR(255) NOT NULL,
    us_ro_identificador INT NOT NULL,
    us_estado NVARCHAR(50) NOT NULL,
    us_fecha_adicion DATETIME NOT NULL,
    us_adicionado_por NVARCHAR(255) NOT NULL,
    us_fecha_modificacion DATETIME NULL,
    us_modificado_por NVARCHAR(255) NULL,
    CONSTRAINT FK_Usuarios_Roles FOREIGN KEY (us_ro_identificador) REFERENCES Roles(ro_identificador)
);

INSERT INTO Usuarios (us_nombre_completo, us_correo, us_clave, us_ro_identificador, us_estado, us_fecha_adicion, us_adicionado_por)
VALUES 
  ('Juan Perez', 'juan@example.com', '1234', 1, 'A', GETDATE(), 'sistema'),
  ('Maria Lopez', 'maria@example.com', 'abcd', 2, 'A', GETDATE(), 'sistema');

CREATE TABLE Tiquetes (
    ti_identificador INT IDENTITY(1,1) PRIMARY KEY,
    ti_asunto NVARCHAR(255) NOT NULL,
    ti_categoria INT NOT NULL,
    ti_us_id_asigna INT NOT NULL,
    ti_urgencia INT NOT NULL,
    ti_importancia INT NOT NULL,
    ti_estado NVARCHAR(50) NOT NULL,
    ti_solucion NVARCHAR(MAX) NULL,
    ti_fecha_adicion DATETIME NOT NULL,
    ti_adicionado_por NVARCHAR(255) NOT NULL,
    ti_fecha_modificacion DATETIME NULL,
    ti_modificado_por NVARCHAR(255) NULL,
    CONSTRAINT FK_Tiquetes_Usuarios FOREIGN KEY (ti_us_id_asigna) REFERENCES Usuarios(us_identificador),
    CONSTRAINT FK_Tiquetes_Categorias FOREIGN KEY (ti_categoria) REFERENCES Categorias(ca_identificador),
    CONSTRAINT FK_Tiquetes_Urgencias FOREIGN KEY (ti_urgencia) REFERENCES Urgencias(ur_identificador),
    CONSTRAINT FK_Tiquetes_Importancias FOREIGN KEY (ti_importancia) REFERENCES Importancias(im_identificador)
);

INSERT INTO Tiquetes (ti_asunto, ti_categoria, ti_us_id_asigna, ti_urgencia, ti_importancia, ti_estado, ti_solucion, ti_fecha_adicion, ti_adicionado_por)
VALUES 
  ('Error en el sistema', 1, 1, 1, 1, 'Creado', NULL, GETDATE(), 'juan'),
  ('Solicitud de actualización', 2, 2, 2, 2, 'Pendiente', NULL, GETDATE(), 'maria');
