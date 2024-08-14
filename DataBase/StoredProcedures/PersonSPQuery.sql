----------------- Stored Procedures -----------------

USE projectplmdb;
GO

CREATE PROCEDURE create_person (@person_id INT, 
								@name VARCHAR(30), 
								@last_name VARCHAR(30),
								@second_lastname VARCHAR(30), 
								@address VARCHAR(300), 
								@birthday DATE, 
								@phone_number INT, 
								@message_id INT OUTPUT, 
								@message VARCHAR(MAX) OUTPUT)
AS
BEGIN

	BEGIN TRY

		--Check if the person exists
		IF EXISTS(SELECT 1 FROM person WHERE person_id = @person_id)
			BEGIN
				SET @message = 'La persona ya existe';
				THROW 50000, @message, 1;
			END

		-- Check if the phone number exist
		IF EXISTS (SELECT 1 FROM person WHERE phone_number = @phone_number)
			BEGIN
				SET @message = 'El número celular proporcionado ya está asociado a otra persona.';
				THROW 50000, @message, 1;
			END

		--Create record
		INSERT INTO person VALUES (@person_id, @name, @last_name, @second_lastname, @address, @birthday, @phone_number);

		SET @message_id = 0;
		SET @message = 'Persona ingresada correctamente';

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

CREATE PROCEDURE update_person (@person_id INT, 
								@address VARCHAR(300), 
							    @phone_number INT, 
								@message_id INT OUTPUT, 
								@message VARCHAR(MAX) OUTPUT)
AS
BEGIN

	BEGIN TRY

		-- Check if the person exists
		IF NOT EXISTS(SELECT 1 FROM person WHERE person_id = @person_id)
			BEGIN
				SET @message = 'La persona no existe.';
				THROW 50000, @message, 1;
			END

		-- Check if the phone number is free
		IF EXISTS (SELECT 1 FROM person WHERE phone_number = @phone_number AND person_id <> @person_id)
			BEGIN
				SET @message = 'El número celular ya está asignado a otra persona.';
				THROW 50000, @message, 1;
			END

		-- Update person
		UPDATE person SET [address] = @address, phone_number = @phone_number WHERE person_id = @person_id;

		SET @message_id = 0;
		SET @message = 'Persona actualizada correctamente.';	

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