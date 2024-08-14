----------------- Stored Procedures -----------------

USE projectplmdb;
GO

CREATE PROCEDURE get_design (@name VARCHAR(100),
							 @last_modification VARCHAR(11))
AS
BEGIN
	
	SELECT
		d.design_id,
		d.[name],
		d.last_modification,
		d.current_desing_path,
		CASE 
            WHEN d.[status] = 'P' THEN 'Pendiente'
            WHEN d.[status] = 'A' THEN 'Aprobado'
            WHEN d.[status] = 'R' THEN 'Rechazado'
        END AS status,
		(p.[name] + ' ' + p.last_name + ' ' +p.second_lastname) AS full_name
	FROM design d
	JOIN user_employee u ON d.user_employee_id = u.user_employee_id
	JOIN person p ON u.person_id = p.person_id
	WHERE (((ISNULL(@name, '')<>'' AND d.[name] LIKE '%'+ @name +'%') 
				OR ISNULL(@name, '')='')
			AND
			((ISNULL(@last_modification, '')<>'' AND CAST(d.last_modification AS VARCHAR) LIKE '%'+ @last_modification +'%') 
				OR ISNULL(@last_modification, '')=''));

END
GO

CREATE PROCEDURE create_design (@name VARCHAR(100),
								@current_design_path VARCHAR(300),
								@review_product_proposal_id INT,
								@user_employee_id INT)
AS
BEGIN

	DECLARE @message_id INT;
	DECLARE @message VARCHAR(MAX);

	BEGIN TRY

		BEGIN TRANSACTION;

		--Check if proposal exists
		IF NOT EXISTS (SELECT 1 FROM review_product_proposal r
						JOIN product_proposal pp ON r.product_proposal_id = pp.product_proposal_id
						WHERE review_product_proposal_id = @review_product_proposal_id AND pp.[status] <> 'R')
			BEGIN
				SET @message = 'La propuesta no ha sido evaluada o ha sido rechazada';
				THROW 50000, @message, 1;
			END

		--Check if user exists
		IF NOT EXISTS (SELECT 1 FROM user_employee WHERE user_employee_id = @user_employee_id)
			BEGIN
				SET @message = 'El usuario no existe.';
				THROW 50000, @message, 1;
			END

		--Check if user exists
		IF NOT EXISTS (SELECT 1 FROM user_employee WHERE user_employee_id = @user_employee_id)
			BEGIN
				SET @message = 'El usuario no existe.';
				THROW 50000, @message, 1;
			END

		--Check if name already exists
		IF EXISTS (SELECT 1 FROM design WHERE [name] = @name)
			BEGIN 
				SET @message = 'Ya existe un diseño con ese nombre.';
				THROW 50000, @message, 1;
			END

		--Check if path is empty
		IF @current_design_path IS NULL OR LEN (@current_design_path) = 0
			BEGIN
				SET @message = 'La ruta del archivo no puede estar vacía.';	
				THROW 50000, @message, 1;
			END

		--Check if name is empty
		IF @name IS NULL OR LEN (@name) = 0
			BEGIN 
				SET @message = 'El nombre del diseño no puede estar vacía.';
				THROW 50000, @message, 1;
			END

		--Create design
		INSERT INTO design VALUES (@name, 'P', @current_design_path, GETDATE(), @review_product_proposal_id, @user_employee_id);

		SET @message_id = 0;
		SET @message = 'Diseño creado correctamente.';

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

CREATE PROCEDURE update_design (@design_id INT,
							    @current_design_path VARCHAR(300),
								@user_employee_id INT)
AS
BEGIN

	DECLARE @message_id INT;
	DECLARE @message VARCHAR(MAX);

	BEGIN TRY
	
		BEGIN TRANSACTION

			--Check if design exists
			IF NOT EXISTS (SELECT 1 FROM design WHERE design_id = @design_id)
				BEGIN 
					SET @message = 'El diseño no existe.';	
					THROW 50000, @message, 1;
				END

			--Check if user exists
			IF NOT EXISTS (SELECT 1 FROM user_employee WHERE user_employee_id = @user_employee_id)
				BEGIN 
					SET @message = 'El usuario no existe.';	
					THROW 50000, @message, 1;
				END

			--Check if path is empty
			IF @current_design_path IS NULL OR LEN (@current_design_path) = 0
				BEGIN
					SET @message = 'La ruta del archivo no puede estar vacía.';	
					THROW 50000, @message, 1;
				END

			EXECUTE create_design_history @current_design_path,
										  @design_id, 
										  @user_employee_id, 
										  @message_id OUTPUT, 
										  @message OUTPUT;

			IF (@message_id <> 0) THROW 50000, @message, 1;

			--Update design proposal
			UPDATE design SET current_desing_path = @current_design_path, 
							  last_modification = GETDATE(), 
							  user_employee_id = @user_employee_id 
							  WHERE design_id = @design_id;

			SET @message_id = 0;
			SET @message = 'Diseño actualizado exitosamente.';

		COMMIT TRANSACTION

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

CREATE PROCEDURE evaluate_design (@design_id INT,
							      @status CHAR(1),
								  @message_id INT OUTPUT, 
							      @message VARCHAR(MAX) OUTPUT)
AS
BEGIN

	DECLARE @current_status CHAR(1);

	BEGIN TRY

		--Check if @status is diferent from 'A' or 'R'
		IF @status NOT IN ('A', 'R')
			BEGIN
				SET @message = 'Estado inválido. Solo se permiten los valores A (Aceptado) o R (Rechazado).';
				THROW 50000, @message, 1;
			END

		SELECT @current_status = [status] FROM design WHERE design_id = @design_id;

		--Check if @current_status is 'A' or 'R'
		IF @current_status IN ('A', 'R')
			BEGIN
				SET @message = 'El diseño ya ha sido evaluado y no puede ser actualizada nuevamente.';
				THROW 50000, @message, 1;
			END

		--Evaluate product proposal
		UPDATE design SET [status] = @status WHERE design_id = @design_id;

		SET @message_id = 0;
		SET @message = 'Diseño evaluado correctamente.';

	END TRY
	BEGIN CATCH
		
		-- If the error was triggered by THROW, the controlled error message is maintained.
		IF ERROR_NUMBER() = 50000 SET @message_id = -1;

		ELSE
			BEGIN
				SET @message_id = -2;
				SET @message = ERROR_MESSAGE();
			END

	END CATCH
END
GO

CREATE PROCEDURE delete_design (@design_id INT)
AS
BEGIN

	DECLARE @message_id INT;
	DECLARE @message VARCHAR(MAX);

	BEGIN TRY

		BEGIN TRANSACTION;

			--Check if design exists
			IF NOT EXISTS (SELECT 1 FROM design WHERE design_id = @design_id)
				BEGIN
					SET @message = 'El diseño no existe.';
					THROW 50000, @message, 1;
				END

			--Check that it has no associations with other tables
			IF EXISTS (SELECT 1 FROM review_design WHERE design_id = @design_id)
				BEGIN
					SET @message = 'El diseño no se puede eliminar una vez aprobado.';
					THROW 50000, @message, 1;
				END

			--Check that it has no associations with other tables
			IF EXISTS (SELECT 1 FROM design_comment WHERE design_id = @design_id)
				BEGIN
					SET @message = 'El diseño no se puede eliminar ya que tiene comentarios asociados.';
					THROW 50000, @message, 1;
				END

			--Check that it has no associations with other tables
			IF EXISTS (SELECT 1 FROM design_history WHERE design_id = @design_id)
				BEGIN
					SET @message = 'El diseño no se puede eliminar ya que tiene un historial asociado.';
					THROW 50000, @message, 1;
				END

			--Delete design
			DELETE design WHERE design_id = @design_id;

			SET @message_id = 0;
			SET @message = 'Diseño eliminado correctamente.'

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

get_design '', '';

create_design '', '', 100, 100;

create_design '', '', 2, 100;

create_design '', '', 2, 1;

create_design '', 'D:\Bachillerato en Ingenieria de Software\X Cuatrimestre\Ingenieria de Software IV\Proyecto\PLM\DataBase\StoredProcedures', 2, 1;

create_design 'Test', 'D:\Bachillerato en Ingenieria de Software\X Cuatrimestre\Ingenieria de Software IV\Proyecto\PLM\DataBase\StoredProcedures', 2, 1;

create_design 'Test', 'D:\Bachillerato en Ingenieria de Software\X Cuatrimestre\Ingenieria de Software IV\Proyecto\PLM\DataBase\StoredProcedures', 2, 1;

create_design 'Test', 'D:\Bachillerato en Ingenieria de Software\X Cuatrimestre\Ingenieria de Software IV\Proyecto\PLM\DataBase\StoredProcedures', 2, 1;

update_design 100, '', 100;

update_design 3, '', 100;

update_design 3, 'C:\Test\', 1;

update_design 3, 'C:\Test\Test', 1;

update_design 3, 'C:\Test\React', 1;

delete_design 2;