----------------- Stored Procedures -----------------

USE projectplmdb;
GO

CREATE PROCEDURE get_design_history (@name VARCHAR(100),
									 @upload_date VARCHAR(11),
									 @design_id INT)
AS
BEGIN
	
	SELECT
		dh.design_history_id,
		dh.design_path,
		dh.upload_date,
		d.[name],
		(p.[name] + ' ' + p.last_name + ' ' +p.second_lastname) AS full_name
	FROM design_history dh
	JOIN design d ON dh.design_id = d.design_id
	JOIN user_employee u ON dh.user_employee_id = u.user_employee_id
	JOIN person p ON u.person_id = p.person_id
	WHERE (((ISNULL(@name, '')<>'' AND d.[name] LIKE '%'+ @name +'%') 
				OR ISNULL(@name, '')='')
			AND
			((ISNULL(@upload_date, '')<>'' AND CAST(d.last_modification AS VARCHAR) LIKE '%'+ @upload_date +'%') 
				OR ISNULL(@upload_date, '')=''))
			AND dh.design_id = @design_id;

END
GO

CREATE PROCEDURE create_design_history (@design_path VARCHAR(300),
										@design_id INT,
										@user_employee_id INT,
										@message_id INT OUTPUT,
										@message VARCHAR(MAX) OUTPUT)
AS
BEGIN

	BEGIN TRY

		--Check if user exits
		IF NOT EXISTS (SELECT 1 FROM user_employee WHERE user_employee_id = @user_employee_id)
			BEGIN
				SET @message = 'El usuario no existe.';
				THROW 50000, @message, 1;
			END

		--Check if design exits
		IF NOT EXISTS (SELECT 1 FROM design WHERE design_id = @design_id)
			BEGIN
				SET @message = 'El diseño no existe.';
				THROW 50000, @message, 1;
			END

		--Create design history
		INSERT INTO design_history VALUES (@design_path, GETDATE(), @design_id, @user_employee_id);

		SET @message_id = 0;
		SET @message = 'Historial creado correctamente.';

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
END
GO

CREATE PROCEDURE delete_design_history (@design_history_id INT)
AS
BEGIN

	DECLARE @message_id INT;
	DECLARE @message VARCHAR(MAX);

	BEGIN TRY

		BEGIN TRANSACTION;

			--Check if design history exits
			IF NOT EXISTS (SELECT 1 FROM design_history WHERE design_history_id = @design_history_id)
				BEGIN
					SET @message = 'El historial no existe.';
					THROW 50000, @message, 1;
				END

			--Delete design history
			DELETE FROM design_history WHERE design_history_id = @design_history_id;

			SET @message_id = 0;
			SET @message = 'Historial eliminado correctamente.';

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
get_design_history '', '', '';

delete_design_history 100;

delete_design_history 2;