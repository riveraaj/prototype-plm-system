----------------- Stored Procedures -----------------

USE projectplmdb;
GO

CREATE PROCEDURE get_idea (@ideaName VARCHAR(100),
						   @date_creation VARCHAR(11))
AS
BEGIN
	
	SELECT
		i.idea_id,
		i.[name],
		i.[description],
		i.date_creation,
		CASE 
            WHEN i.[status] = 'P' THEN 'Pendiente'
            WHEN i.[status] = 'A' THEN 'Aprobado'
            WHEN i.[status] = 'R' THEN 'Rechazado'
        END AS status,
		c.[description] AS category,
		(p.[name] + ' ' + p.last_name + ' ' +p.second_lastname) AS full_name
	FROM idea i
	JOIN category_idea c ON i.category_idea_id = c.category_idea_id
	JOIN user_employee u ON i.user_employee_id = u.user_employee_id
	JOIN person p ON u.person_id = p.person_id
	WHERE(((ISNULL(@ideaName, '')<>'' AND i.[name] LIKE '%'+ @ideaName +'%') 
				OR ISNULL(@ideaName, '')='')
			AND
			((ISNULL(@date_creation, '')<>'' AND CAST(i.date_creation AS VARCHAR) LIKE '%'+ @date_creation +'%') 
				OR ISNULL(@date_creation, '')=''));

END
GO

CREATE PROCEDURE get_idea_for_product_proposal
AS
BEGIN
	
	SELECT
		i.idea_id,
		i.[name]
	FROM idea i
	LEFT JOIN product_proposal pp ON i.idea_id = pp.idea_id
	WHERE i.[status] = 'A' 
		  AND pp.idea_id IS NULL;

END
GO

CREATE PROCEDURE create_idea (@name VARCHAR(100), 
							  @description VARCHAR(300), 
							  @user_employee_id INT, 
							  @category_idea_id TINYINT)
AS
BEGIN

	DECLARE @message_id INT;
	DECLARE @message VARCHAR(MAX);

	BEGIN TRY

		BEGIN TRANSACTION;

			--Check if user exists
			IF NOT EXISTS (SELECT 1 FROM user_employee WHERE user_employee_id = @user_employee_id)
				BEGIN
					SET @message = 'El usuario no existe.';
					THROW 50000, @message, 1;
				END

			--Check if category idea exists
			IF NOT EXISTS (SELECT 1 FROM category_idea WHERE category_idea_id = @category_idea_id)
				BEGIN
					SET @message = 'La categoría no existe';
					THROW 50000, @message, 1;
				END

			--Check if an idea with that name exists	
			IF EXISTS (SELECT 1 FROM idea WHERE [name] = @name)
				BEGIN
					SET @message = 'Ya existe una idea con este nombre.';
					THROW 50000, @message, 1;
				END

			--Create idea
			INSERT INTO idea VALUES (@name, @description, GETDATE(), 'P', @user_employee_id, @category_idea_id);

			SET @message_id = 0;
			SET @message = 'Idea creada correctamente';
	
		COMMIT TRANSACTION;

	END TRY
	BEGIN CATCH

		-- Error handling
		IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
	
		-- If the error was triggered by THROW, the controlled error message is maintained.
		IF ERROR_NUMBER() = 50000 SET @message_id = -1;

		ELSE
			BEGIN
				SET @message_id = -2;
				SET @message = ERROR_MESSAGE();
			END

	END CATCH

	--Return message
	SELECT @message_id AS Message_ID, @message AS [Message];
END
GO

CREATE PROCEDURE update_idea (@idea_id INT, 
							  @status CHAR(1), 
							  @user_employee_id INT)
AS
BEGIN

	DECLARE @message_id INT;
	DECLARE @message VARCHAR(MAX);
	DECLARE @current_status CHAR(1);

	BEGIN TRY

		--Check if @status is 'A' or 'R'
		IF @status NOT IN ('A', 'R')
			BEGIN
				SET @message = 'Estado inválido. Solo se permiten los valores A (Aceptado) o R (Rechazado).';
				THROW 50000, @message, 1;
			END

		BEGIN TRANSACTION;

			--Check if idea exists
			IF NOT EXISTS (SELECT 1 FROM idea WHERE idea_id = @idea_id)
				BEGIN
					SET @message = 'La idea no existe.';
					THROW 50000, @message, 1;
				END

			--Check if user exists
			IF NOT EXISTS (SELECT 1 FROM user_employee WHERE user_employee_id = @user_employee_id)
				BEGIN
					SET @message = 'El usuario no existe.';
					THROW 50000, @message, 1;
				END

			SELECT @current_status = [status] FROM idea WHERE idea_id = @idea_id;

			--Check if @current_status is 'A' or 'R'
			IF @current_status IN ('A', 'R')
				BEGIN
					SET @message = 'La idea ya ha sido evaluada y no puede ser actualizada nuevamente.';
					THROW 50000, @message, 1;
				END

			UPDATE idea SET [status] = @status WHERE idea_id = @idea_id;

			SET @message_id = 0;
			SET @message = 'Idea evaluada correctamente.';
		
		COMMIT TRANSACTION;

	END TRY
	BEGIN CATCH
	
		-- Error handling
		IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
	
		-- If the error was triggered by THROW, the controlled error message is maintained.
		IF ERROR_NUMBER() = 50000 SET @message_id = -1;

		ELSE
			BEGIN
				SET @message_id = -2;
				SET @message = ERROR_MESSAGE();
			END

	END CATCH

	--Return message
	SELECT @message_id AS Message_ID, @message AS [Message];
END
GO

CREATE PROCEDURE delete_idea (@idea_id INT)
AS
BEGIN

	DECLARE @message_id INT;
	DECLARE @message VARCHAR(MAX);

	BEGIN TRY

		BEGIN TRANSACTION;

			--Check if idea exists
			IF NOT EXISTS (SELECT 1 FROM idea WHERE idea_id = @idea_id)
				BEGIN
					SET @message = 'La idea no existe.';
					THROW 50000, @message, 1;
				END

			--Check if idea has a product proposal
			IF EXISTS (SELECT 1 FROM product_proposal WHERE idea_id = @idea_id)
				BEGIN
					SET @message = 'No se puede eliminar la idea porque tiene propuestas asociadas.';
					THROW 50000, @message, 1;
				END

			--Delete idea
			DELETE idea WHERE idea_id = @idea_id;

			SET @message_id = 0;
			SET @message = 'Idea eliminada correctamente.';

		COMMIT TRANSACTION;

	END TRY
	BEGIN CATCH
	
		-- Error handling
		IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
	
		-- If the error was triggered by THROW, the controlled error message is maintained.
		IF ERROR_NUMBER() = 50000 SET @message_id = -1;

		ELSE
			BEGIN
				SET @message_id = -2;
				SET @message = ERROR_MESSAGE();
			END

	END CATCH

	--Return message
	SELECT @message_id AS Message_ID, @message AS [Message];
END
GO

----------------- Testing -----------------

get_idea '','';

get_idea '','07';

get_idea 'Celu','';

get_idea_for_product_proposal;

create_idea 'Celular cuántico', 'Un autómata celular cuántico, o ACC, es un modelo abstracto de la computación cuántica, ideado en analogía de los modelos convencionales de la autómata celular introducidos por von Neumann.',
			10, 1

create_idea 'Celular cuántico', 'Un autómata celular cuántico, o ACC, es un modelo abstracto de la computación cuántica, ideado en analogía de los modelos convencionales de la autómata celular introducidos por von Neumann.',
			1, 200

create_idea 'Celular cuántico', 'Un autómata celular cuántico, o ACC, es un modelo abstracto de la computación cuántica, ideado en analogía de los modelos convencionales de la autómata celular introducidos por von Neumann.',
			1, 1

create_idea 'Celular cuántico', 'Es el procesador que está dentro de los teléfonos más ambiciosos que han llegado al mercado en los últimos meses como el Galaxy S24 Ultra o el Xiaomi 14 Ultra.',
			2, 1

create_idea 'Qualcomm', 'Es el procesador que está dentro de los teléfonos más ambiciosos que han llegado al mercado en los últimos meses como el Galaxy S24 Ultra o el Xiaomi 14 Ultra.',
			2, 1

create_idea 'Test', 'Es el procesador que está dentro de los teléfonos más ambiciosos que han llegado al mercado en los últimos meses como el Galaxy S24 Ultra o el Xiaomi 14 Ultra.',
			2, 1

create_idea 'TestTwo', 'Es el procesador que está dentro de los teléfonos más ambiciosos que han llegado al mercado en los últimos meses como el Galaxy S24 Ultra o el Xiaomi 14 Ultra.',
			2, 1

update_idea 1, 'C', 1;

update_idea 100, 'A', 1;

update_idea 4, 'A', 100;

update_idea 4, 'A', 1;

update_idea 4, 'R', 1;

update_idea 5, 'R', 1;

update_idea 7, 'A', 1;

delete_idea 100;

delete_idea 4;