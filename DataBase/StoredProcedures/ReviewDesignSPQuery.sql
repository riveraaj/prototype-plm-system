----------------- Stored Procedures -----------------

USE projectplmdb;
GO

CREATE PROCEDURE get_review_design (@userName VARCHAR(90),
								    @designName VARCHAR(100))
AS
BEGIN

	SELECT
		r.review_design_id,
		r.evaluation_date,
		r.justification,
		r.user_employee_id,
		d.[name],
		CASE 
            WHEN d.[status] = 'P' THEN 'Pendiente'
            WHEN d.[status] = 'A' THEN 'Aprobado'
            WHEN d.[status] = 'R' THEN 'Rechazado'
        END AS status,
		(p.[name] + ' ' + p.last_name + ' ' +p.second_lastname) AS full_name
	FROM review_design r
	JOIN design d ON r.design_id = d.design_id
	JOIN user_employee u ON r.user_employee_id = u.user_employee_id
	JOIN person p ON u.person_id = p.person_id
	WHERE(((ISNULL(@userName, '')<>'' AND (p.[name] + ' ' + p.last_name + ' ' + p.second_lastname) LIKE '%'+ @userName +'%') 
				OR ISNULL(@userName, '')='')
			AND
			(ISNULL(@designName, '') = ''
            OR (
                (
                    @designName LIKE '%Pe%' AND d.[status] = 'P'
                    OR @designName LIKE '%Ap%' AND d.[status] = 'A'
                    OR @designName LIKE '%Re%' AND d.[status] = 'R'
                )
            )));

END
GO

CREATE PROCEDURE create_review_design (@status CHAR(1), 
									   @user_employee_id INT, 
									   @design_id INT,
									   @justification VARCHAR(300))
AS
BEGIN

	DECLARE @message_id INT;
	DECLARE @message VARCHAR(MAX);

	BEGIN TRY
	
		BEGIN TRANSACTION

			--Check if user exists
			IF NOT EXISTS(SELECT 1 FROM user_employee WHERE user_employee_id = @user_employee_id)
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

			--Check if justification is empty. 
			IF @justification IS NULL OR LEN (@justification) = 0
				BEGIN
					SET @message = 'La justificación no puede estar vacía.';	
					THROW 50000, @message, 1;
				END

			EXECUTE evaluate_design @design_id, @status, @message_id OUTPUT, @message OUTPUT

			IF(@message_id <> 0) THROW 50000, @message, 1;

			INSERT INTO review_design VALUES (GETDATE(), @justification, @design_id, @user_employee_id);

			SET @message_id = 0;
			SET @message = 'Evaluación realizada correctamente.';

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

get_review_design '', 'Re';

create_review_design 'P', 1000, 1000, '';

create_review_design 'P', 2, 1000, '';

create_review_design 'P', 2, 3, '';

create_review_design 'A', 2, 3, 'Test';

create_review_design 'R', 2, 3, 'Test';