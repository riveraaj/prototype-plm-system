----------------- Stored Procedures -----------------

USE projectplmdb;
GO

CREATE PROCEDURE get_user_employee (@personName VARCHAR(90), 
									@identification VARCHAR(10))
AS
BEGIN	

	SELECT 
		u.user_employee_id, 
		u.email, 
		r.[description] AS [role], 
	    (p.[name] + ' ' + p.last_name + ' ' +p.second_lastname) AS full_name, 
		p.[address],
		p.phone_number
	FROM user_employee u 
	JOIN person p ON u.person_id = p.person_id 
	JOIN [role] r ON u.role_id = r.role_id
	WHERE (((ISNULL(@personName, '')<>'' AND (p.[name] + ' ' + p.last_name + ' ' +p.second_lastname) LIKE '%'+ @personName +'%') 
				OR ISNULL(@personName, '')='')
			AND
			((ISNULL(@identification, '')<>'' AND CAST(u.person_id AS VARCHAR) LIKE '%'+ @identification +'%') 
				OR ISNULL(@identification, '')=''));

END
GO

CREATE PROCEDURE get_by_id_user_employee (@user_employee_id INT)
AS
BEGIN
 
    DECLARE @message_id INT;
    DECLARE @message VARCHAR(MAX);

	--Check if the user exists
	IF NOT EXISTS (SELECT 1 FROM user_employee WHERE user_employee_id = @user_employee_id)
		BEGIN
			SET @message_id = -1;
			SET @message = 'Usuario no encontrado';

			--Return message
			SELECT @message_id AS Message_ID, @message AS [Message];
			RETURN;
		END

		SELECT 
				u.user_employee_id, 
				u.email,
				u.role_id,
				p.[address],
				p.phone_number
			FROM user_employee u 
			JOIN person p ON u.person_id = p.person_id
			WHERE user_employee_id = @user_employee_id;

END
GO

CREATE PROCEDURE create_user_employee (@person_id INT, 
									   @name VARCHAR(30), 
									   @last_name VARCHAR(30),
									   @second_lastname VARCHAR(30), 
									   @address VARCHAR(300), 
									   @birthday DATE,
									   @phone_number INT,
									   @password VARCHAR(300), 
									   @email VARCHAR(150), 
									   @role_id TINYINT)
AS
BEGIN

	DECLARE @message_id INT;
	DECLARE @message VARCHAR(MAX);

	BEGIN TRY

		BEGIN TRANSACTION;
	
			--Check if the email exists
			IF EXISTS(SELECT 1 FROM user_employee WHERE email = @email)
				BEGIN
					SET @message = 'El correo proporcionado ya está asociado a otro empleado.';
					THROW 50000, @message, 1;
				END

			--Check if the role exists
			IF NOT EXISTS(SELECT 1 FROM [role] WHERE role_id = @role_id)
				BEGIN
					SET @message = 'El rol no existe.';
					THROW 50000, @message, 1;
				END

			--Create person
			EXECUTE create_person @person_id, @name, @last_name, @second_lastname, @address, @birthday, @phone_number, @message_id OUTPUT, @message OUTPUT;

			IF @message_id <> 0 THROW 50000, @message, 1;
						
			--Create user
			INSERT INTO user_employee VALUES (@password, @email, 1, @role_id, @person_id);

			SET @message_id = 0;
			SET @message = 'Usuario ingresado correctamente';			

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

CREATE PROCEDURE update_user_employee (@user_employee_id INT, 
									   @address VARCHAR(300), 
							           @phone_number INT, 
									   @email VARCHAR(150), 
									   @role_id TINYINT)
AS
BEGIN

	DECLARE @message_id INT;
	DECLARE @message VARCHAR(MAX);
	DECLARE @person_id INT;

	BEGIN TRY

		BEGIN TRANSACTION 

			-- Check if the user exists
			IF NOT EXISTS(SELECT 1 FROM user_employee WHERE user_employee_id = @user_employee_id)
				BEGIN
					SET @message = 'El usuario no existe.';
					THROW 50000, @message, 1;
				END

			-- Check if the email is free
			IF EXISTS (SELECT 1 FROM user_employee WHERE email = @email AND user_employee_id <> @user_employee_id)
				BEGIN
					SET @message = 'El correo ya está asignado a otra persona.';
					THROW 50000, @message, 1;
				END

			--Check if the role exists
			IF NOT EXISTS(SELECT 1 FROM role WHERE role_id = @role_id)
				BEGIN
					SET @message = 'El rol no existe.';
					THROW 50000, @message, 1;
				END

			-- Update person
			SELECT @person_id = person_id FROM user_employee WHERE user_employee_id = @user_employee_id;
			EXECUTE update_person @person_id, @address, @phone_number, @message_id OUTPUT, @message OUTPUT;

			IF @message_id <> 0 THROW 50000, @message, 1;

			-- Update user
			UPDATE user_employee SET email = @email, role_id = @role_id WHERE user_employee_id = @user_employee_id;

			SET @message_id = 0;
			SET @message = 'Usuario actualizado correctamente.';
			
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

CREATE PROCEDURE delete_user_employee (@user_employee_id INT)
AS
BEGIN

	DECLARE @message_id INT;
	DECLARE @message VARCHAR(MAX);

	BEGIN TRY

		BEGIN TRANSACTION 

			--Check if the user exists
			IF NOT EXISTS(SELECT 1 FROM user_employee WHERE user_employee_id = @user_employee_id)
				BEGIN
					SET @message = 'El usuario no existe.';
					THROW 50000, @message, 1;
				END
		
			--Disable user 
			UPDATE user_employee SET [active] = 0 WHERE user_employee_id = @user_employee_id;

			SET @message_id = 0; 
			SET @message = 'Usuario desactivado correctamente.';

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

----------------- Testing -----------------

get_user_employee '', '';

get_by_id_user_employee 2;

create_user_employee 118800124, 'Jonathan', 'Rivera', 'Vasquez', 'San Jose - Guadalupe', '07-17-2003', 84432412, '12345', 'jonathandavidr7@gmail.com', 2;
create_user_employee 118800124, 'Jonathan', 'Rivera', 'Vasquez', 'San Jose - Guadalupe', '07-17-2003', 84432412, '12345', 'jonathandavidr7@gmail.com', 1;
create_user_employee 118800124, 'Eddie', 'Rivera', 'Colocho', 'San Jose - Guadalupe', '01-06-1979', 84432412, '12345', 'jonathandavidr7@gmail.com', 1;
create_user_employee 118800124, 'Eddie', 'Rivera', 'Colocho', 'San Jose - Guadalupe', '01-06-1979', 84432412, '12345', 'e.rivera@gmail.com', 1;
create_user_employee 110230629, 'Eddie', 'Rivera', 'Colocho', 'San Jose - Guadalupe', '01-06-1979', 84432412, '12345', 'e.rivera@gmail.com', 1;
create_user_employee 110230629, 'Eddie', 'Rivera', 'Colocho', 'San Jose - Guadalupe', '01-06-1979', 60420779, '12345', 'e.rivera@gmail.com', 1;

update_user_employee 100, 'Guadalupe - San Jose', 60420779, 'e.rivera@gmail.com', 1;
update_user_employee 1, 'Guadalupe - San Jose', 60420779, 'e.rivera@gmail.com', 0;
update_user_employee 1, 'Guadalupe - San Jose', 60420779, 'messias00yt@gmail.com', 0;
update_user_employee 1, 'Guadalupe - San Jose', 60420779, 'messias00yt@gmail.com', 1;
update_user_employee 1, 'Guadalupe - San Jose', 84432412, 'messias00yt@gmail.com', 1;

delete_user_employee 1;