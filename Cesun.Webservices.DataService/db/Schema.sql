CREATE TABLE [dbo].[Temblors](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Longitud] [float] NOT NULL,
	[Latitud] [float] NOT NULL,
	[Magnitud] [float] NOT NULL,
	[Profundidad] [float] NOT NULL,
 CONSTRAINT [PK_Temblors_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

insert into temblors(fecha, latitud, longitud, magnitud, profundidad) values('2010-01-15 09:05', -31.902, -175.74, 5.0, 10);
insert into temblors(fecha, latitud, longitud, magnitud, profundidad) values('2010-01-15 09:06', -31.945, -177.371, 5.4, 10);
insert into temblors(fecha, latitud, longitud, magnitud, profundidad) values('2010-01-15 09:15', 18.407, -72.958, 4.6, 10);
insert into temblors(fecha, latitud, longitud, magnitud, profundidad) values('2010-01-15 09:19', 36.030, -117.846, 4.4, 3);
insert into temblors(fecha, latitud, longitud, magnitud, profundidad) values('2010-01-15 09:25', 2.415, 126.199, 4.8, 94);
insert into temblors(fecha, latitud, longitud, magnitud, profundidad) values('2010-01-15 09:27', 7.182, 126.000, 5.3, 28);
