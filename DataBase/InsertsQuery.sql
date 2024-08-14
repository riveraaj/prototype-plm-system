----------------- Insert Default Values -----------------

USE projectplmdb;
GO

-------------- Role --------------

INSERT INTO [role] VALUES ('Administrador'), 
						   ('Desarrollador'), 
						   ('Revisor'), 
						   ('Usuario');			

-------------- Category Idea --------------

INSERT INTO category_idea VALUES ('Innovaci�n'),
								  ('Mejora de Producto Existente'), 
								  ('Nuevas Funcionalidades'), 
								  ('Eficiencia Operacional'), 
								  ('Sostenibilidad'), 
								  ('Reducci�n de Costos'), 
								  ('Seguridad'), 
								  ('Experiencia del Usuario');