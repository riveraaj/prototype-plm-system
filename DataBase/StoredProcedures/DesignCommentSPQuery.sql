----------------- Stored Procedures -----------------

USE projectplmdb;
GO

CREATE PROCEDURE get_by_design_id_comment (@design_id INT)
AS
BEGIN

	SELECT 
		dc.[description],
		(p.[name] + ' ' + p.last_name + ' ' +p.second_lastname) AS full_name
	FROM design_comment dc
	JOIN user_employee u ON dc.user_employee_id = u.user_employee_id
	JOIN person p ON u.person_id = p.person_id
	WHERE dc.design_id = @design_id;

END
GO

CREATE PROCEDURE create_design_comment (@description VARCHAR(300),
										@user_employee_id INT,
										@design_id INT)
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

			--Check if design exists
			IF NOT EXISTS (SELECT 1 FROM design WHERE design_id = @design_id)
				BEGIN
					SET @message = 'El diseño no existe.';
					THROW 50000, @message, 1;
				END

			--Check if description is empty
			IF @description IS NULL OR LEN(@description) = 0
				BEGIN
					SET @message = 'El comentario no puede estar vacío.';
					THROW 50000, @message, 1;
				END
	
			INSERT INTO design_comment VALUES (@description, @user_employee_id, @design_id);

			SET @message_id = 0;
			SET @message = 'Comentario realizado correctamente.';

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

get_by_design_id_comment 3;

create_design_comment '', 1000, 1000;

create_design_comment '', 1, 1000;

create_design_comment '', 1, 3;

create_design_comment 'Test', 1, 3;

create_design_comment 'Test2', 1, 3;

create_design_comment 'Test3', 1, 3;