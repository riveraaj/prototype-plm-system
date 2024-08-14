----------------- Stored Procedures -----------------

USE projectplmdb;
GO

CREATE PROCEDURE get_product_proposal (@ideaName VARCHAR(100),
						               @date_creation VARCHAR(11))
AS
BEGIN

	SELECT
		pp.product_proposal_id,
		pp.date_creation,
		CASE 
            WHEN pp.[status] = 'P' THEN 'Pendiente'
            WHEN pp.[status] = 'A' THEN 'Aprobado'
            WHEN pp.[status] = 'R' THEN 'Rechazado'
        END AS status,
		pp.file_path,
		i.[name],
		i.[description],
		(p.[name] + ' ' + p.last_name + ' ' +p.second_lastname) AS full_name	
	FROM product_proposal  pp
	JOIN idea i ON pp.idea_id = i.idea_id
	JOIN user_employee u ON pp.user_employee_id = u.user_employee_id
	JOIN person p ON u.person_id = p.person_id
	WHERE(((ISNULL(@ideaName, '')<>'' AND i.[name] LIKE '%'+ @ideaName +'%') 
				OR ISNULL(@ideaName, '')='')
			AND
			((ISNULL(@date_creation, '')<>'' AND CAST(pp.date_creation AS VARCHAR) LIKE '%'+ @date_creation +'%') 
				OR ISNULL(@date_creation, '')=''));

END
GO

CREATE PROCEDURE create_product_proposal (@idea_id INT,
										  @user_employee_id INT,
										  @file_path VARCHAR(300))
AS
BEGIN
	
	DECLARE @message_id INT;
	DECLARE @message VARCHAR(MAX)

	BEGIN TRY

		BEGIN TRANSACTION;

			--Check if idea exists
			IF NOT EXISTS (SELECT 1 FROM idea WHERE idea_id = @idea_id)
				BEGIN 
					SET @message = 'La idea no existe.';	
					THROW 50000, @message, 1;
				END

			--Check if idea already evaluate
			IF NOT EXISTS (SELECT 1 FROM idea WHERE idea_id = @idea_id AND [status] <> 'P' AND [status] <> 'R')
				BEGIN 
					SET @message = 'La idea no ha sido evaluada o ha sido rechazada.';	
					THROW 50000, @message, 1;
				END
		
			--Check if user exists
			IF NOT EXISTS (SELECT 1 FROM user_employee WHERE user_employee_id = @user_employee_id)
				BEGIN 
					SET @message = 'El usuario no existe.';	
					THROW 50000, @message, 1;
				END

			--Check if path is empty. 
			IF @file_path IS NULL OR LEN (@file_path) = 0
				BEGIN
					SET @message = 'La ruta del archivo no puede estar vacía.';	
					THROW 50000, @message, 1;
				END

			--Create product proposal
			INSERT INTO product_proposal VALUES (GETDATE(), 'P', @file_path, @idea_id, @user_employee_id);

			SET @message_id = 0;
			SET @message = 'Propuesta creada exitosamente.';		

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

CREATE PROCEDURE update_product_proposal (@product_proposal_id INT,
										  @file_path VARCHAR(300),
										  @user_employee_id INT)
AS
BEGIN

	DECLARE @message_id INT;
	DECLARE @message VARCHAR(MAX);

	BEGIN TRY
	
		BEGIN TRANSACTION

			--Check if proposal exists
			IF NOT EXISTS (SELECT 1 FROM product_proposal WHERE product_proposal_id = @product_proposal_id)
				BEGIN 
					SET @message = 'La propuesta no existe.';
					THROW 50000, @message, 1;
				END

			--Check if user exists
			IF NOT EXISTS (SELECT 1 FROM user_employee WHERE user_employee_id = @user_employee_id)
				BEGIN 
					SET @message = 'El usuario no existe.';	
					THROW 50000, @message, 1;
				END

			--Check if path is empty. 
			IF @file_path IS NULL OR LEN (@file_path) = 0
				BEGIN
					SET @message = 'La ruta del archivo no puede estar vacía.';	
					THROW 50000, @message, 1;
				END

			--Update product proposal
			UPDATE product_proposal SET file_path = @file_path, user_employee_id = @user_employee_id WHERE product_proposal_id = @product_proposal_id;

			SET @message_id = 0;
			SET @message = 'Propuesta actualizada exitosamente.';

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

CREATE PROCEDURE evaluate_product_proposal (@product_proposal_id INT,
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
	

		IF NOT EXISTS (SELECT 1 FROM product_proposal WHERE product_proposal_id = @product_proposal_id)
			BEGIN
				SET @message = 'La propuesta no existe.';
				THROW 50000, @message, 1;
			END

		SELECT @current_status = [status] FROM product_proposal WHERE product_proposal_id = @product_proposal_id;

		--Check if @current_status is 'A' or 'R'
		IF @current_status IN ('A', 'R')
			BEGIN
				SET @message = 'La propuesta ya ha sido evaluada y no puede ser actualizada nuevamente.';
				THROW 50000, @message, 1;
			END
	
		--Evaluate product proposal
		UPDATE product_proposal SET [status] = @status WHERE product_proposal_id = @product_proposal_id;

		SET @message_id = 0;
		SET @message = 'Propuesta evaluada correctamente.';

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

CREATE PROCEDURE delete_product_proposal (@product_proposal_id INT)
AS
BEGIN

	DECLARE @message_id INT;
	DECLARE @message VARCHAR(MAX);

	BEGIN TRY

		BEGIN TRANSACTION;

		--Check if proposal exists
		IF NOT EXISTS (SELECT 1 FROM product_proposal WHERE product_proposal_id = @product_proposal_id)
			BEGIN
				SET @message = 'La propuesta no existe.';
				THROW 50000, @message, 1;
			END

		--Check if proposal has already been evaluated
		IF EXISTS (SELECT 1 FROM review_product_proposal WHERE product_proposal_id = @product_proposal_id)
			BEGIN
				SET @message = 'No se puede eliminar la propuesta porque ya fue evaluada.';
				THROW 50000, @message, 1;
			END

		--Delete producto proposal
		DELETE product_proposal WHERE product_proposal_id = @product_proposal_id;

		SET @message_id = 0;
		SET @message = 'Propuesta eliminada correctamente.';

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

get_product_proposal '', '';

get_product_proposal 'Qual', '';

get_product_proposal '', '25';

create_product_proposal 100, 100, '';

create_product_proposal 4, 100, '';

create_product_proposal 4, 2, '';

create_product_proposal 2, 2, '';

create_product_proposal 4, 2, 'D:\Bachillerato en Ingenieria de Software\X Cuatrimestre\Ingenieria de Software IV\Proyecto\PLM\DataBase\StoredProcedures';

create_product_proposal 5, 2, 'D:\Bachillerato en Ingenieria de Software\X Cuatrimestre\Ingenieria de Software IV\Proyecto\PLM\DataBase\StoredProcedures';

create_product_proposal 1, 2, 'D:\Bachillerato en Ingenieria de Software\X Cuatrimestre\Ingenieria de Software IV\Proyecto\PLM\DataBase\StoredProcedures';

update_product_proposal 100, 'C:\ProgramData', 300;

update_product_proposal 1, 'C:\ProgramData', 300;

update_product_proposal 1, '', 1;

update_product_proposal 1, 'C:\ProgramData', 1;

evaluate_product_proposal 100, 'C', '', '';

evaluate_product_proposal 100, 'R', '', '';

evaluate_product_proposal 1, 'R', '', '';

evaluate_product_proposal 1, 'R', '', '';

delete_product_proposal 100;

delete_product_proposal 6;