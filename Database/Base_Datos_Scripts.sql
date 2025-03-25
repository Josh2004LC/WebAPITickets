create table Roles
(
ro_identificador int primary key identity(1,1),
ro_decripcion varchar(125) NOT NULL,
ro_fecha_adicion datetime default getdate() not null,
ro_adicionado_por varchar(10) not null,
ro_fecha_modificacion datetime default getdate(),
ro_modificado_por varchar(10),
)

insert into roles values ('Soporte', GETDATE(), 'admin', null, null),
					   	('Analista', GETDATE(), 'admin', null, null);

drop table usuarios
CREATE TABLE Usuarios (
    us_identificador    INT PRIMARY KEY IDENTITY(1,1),
    us_nombre_completo  NVARCHAR(150) NOT NULL,
    us_correo           NVARCHAR(150) NOT NULL,
    us_clave            NVARCHAR(255) NOT NULL,
    us_ro_identificador INT FOREIGN KEY REFERENCES Roles(ro_identificador),
    us_estado           NVARCHAR(1) not null,
    us_fecha_adicion    DATETIME DEFAULT GETDATE() NOT NULL,
    us_adicionado_por   NVARCHAR(10) NOT NULL,
    us_fecha_modificacion DATETIME,
    us_modificado_por   NVARCHAR(10)
);

insert into Usuarios values ('Joshua Leiton', 'joshualeiton@gmail.com',
'123', 1, 'A', getdate(), 'admin', null, null)
insert into Usuarios values ('Johel Leiton', 'johelleiton@gmail.com',
'123', 2, 'A', getdate(), 'admin', null, null)

drop table tiquetes
CREATE TABLE Tiquetes (
    ti_identificador   INT PRIMARY KEY IDENTITY(1,1),
    ti_asunto         NVARCHAR(150) NOT NULL,
    ti_categoria      NVARCHAR(150) NOT NULL,
    ti_us_id_asigna   INT FOREIGN KEY REFERENCES Usuarios(us_identificador),
    ti_urgencia       NVARCHAR(150) NOT NULL,
    ti_importancia    NVARCHAR(150) NOT NULL,
    ti_estado        NVARCHAR(1) NOT NULL,
	ti_solucion           nvarchar(255),
    ti_fecha_adicion  DATETIME DEFAULT GETDATE() NOT NULL,
    ti_adicionado_por NVARCHAR(10) NOT NULL,
    ti_fecha_modificacion DATETIME,
    ti_modificado_por NVARCHAR(10)
);

INSERT INTO Tiquetes (ti_asunto, ti_categoria, ti_us_id_asigna, ti_urgencia, ti_importancia, ti_estado, ti_fecha_adicion, ti_adicionado_por)
VALUES ('Problema de red', 'Redes', 2, 'Alta', 'Alta', 'A', GETDATE(), 'admin');

INSERT INTO Tiquetes (ti_asunto, ti_categoria, ti_us_id_asigna, ti_urgencia, ti_importancia, ti_estado, ti_fecha_adicion, ti_adicionado_por)
VALUES ('Problema de compu', 'Hardware', 2, 'Alta', 'Alta', 'A', GETDATE(), 'admin');

