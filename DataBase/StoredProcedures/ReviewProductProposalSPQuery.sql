----------------- Stored Procedures -----------------

USE projectplmdb;
GO

CREATE PROCEDURE get_summary_review_product_proposal
AS
BEGIN
    SELECT 
        c.[description] AS category,
        CASE 
            WHEN pp.[status] = 'P' THEN 'Pendiente'
            WHEN pp.[status] = 'A' THEN 'Aprobado'
            WHEN pp.[status] = 'R' THEN 'Rechazado'
        END AS status,
        COUNT(pp.product_proposal_id) AS total_proposal
    FROM 
        product_proposal pp
		JOIN review_product_proposal r ON r.product_proposal_id = pp.product_proposal_id
		JOIN idea i ON pp.idea_id = i.idea_id
        INNER JOIN category_idea c ON i.category_idea_id = c.category_idea_id
    GROUP BY 
        c.[description], 
        pp.[status];
END
GO


CREATE PROCEDURE get_review_product_proposal (@userName VARCHAR(90),
											  @ideaName VARCHAR(100))
AS
BEGIN

	SELECT
		r.review_product_proposal_id,
		r.evaluation_date,
		c.[description] AS category,
		r.justification,
		i.[name],
		CASE 
            WHEN pp.[status] = 'P' THEN 'Pendiente'
            WHEN pp.[status] = 'A' THEN 'Aprobado'
            WHEN pp.[status] = 'R' THEN 'Rechazado'
        END AS status,
		(p.[name] + ' ' + p.last_name + ' ' +p.second_lastname) AS full_name	
	FROM review_product_proposal r
	JOIN product_proposal pp ON r.product_proposal_id = pp.product_proposal_id
	JOIN idea i ON pp.idea_id = i.idea_id
	JOIN category_idea c ON i.category_idea_id = c.category_idea_id
	JOIN user_employee u ON r.user_employee_id = u.user_employee_id
	JOIN person p ON u.person_id = p.person_id
	WHERE(((ISNULL(@userName, '')<>'' AND (p.[name] + ' ' + p.last_name + ' ' + p.second_lastname) LIKE '%'+ @userName +'%') 
				OR ISNULL(@userName, '')='')
			AND
			(ISNULL(@ideaName, '') = ''
            OR (
                (
                    @ideaName LIKE '%Pe%' AND pp.[status] = 'P'
                    OR @ideaName LIKE '%Ap%' AND pp.[status] = 'A'
                    OR @ideaName LIKE '%Re%' AND pp.[status] = 'R'
                )
            )));

END
GO

CREATE PROCEDURE get_review_product_proposal_for_design
AS
BEGIN
	
	SELECT
		r.review_product_proposal_id,
		i.[name]
	FROM review_product_proposal r
	JOIN product_proposal pp ON r.product_proposal_id = pp.product_proposal_id
	JOIN idea i ON pp.idea_id = i.idea_id
	WHERE pp.[status] = 'A'

END
GO

CREATE PROCEDURE create_review_product_proposal (@status CHAR(1), 
												 @user_employee_id INT, 
												 @product_proposal_id INT,
												 @justification VARCHAR(300))
AS
BEGIN

	DECLARE @message_id INT;
	DECLARE @message VARCHAR(MAX);

	BEGIN TRY
	
		BEGIN TRANSACTION

			IF NOT EXISTS(SELECT 1 FROM user_employee WHERE user_employee_id = @user_employee_id)
				BEGIN
					SET @message = 'El usuario no existe.';
					THROW 50000, @message, 1;
				END

			--Check if justification is empty. 
			IF @justification IS NULL OR LEN (@justification) = 0
				BEGIN
					SET @message = 'La justificación no puede estar vacía.';	
					THROW 50000, @message, 1;
				END

			EXECUTE evaluate_product_proposal @product_proposal_id, @status, @message_id OUTPUT, @message OUTPUT

			IF(@message_id <> 0) THROW 50000, @message, 1;

			INSERT INTO review_product_proposal VALUES (GETDATE(), @justification, @product_proposal_id, @user_employee_id);

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
get_review_product_proposal '', '';

get_summary_review_product_proposal '', '';

get_review_product_proposal_for_design

create_review_product_proposal 'C', 100, 100, '';

create_review_product_proposal 'C', 1, 1, '';

create_review_product_proposal 'C', 1, 1, 'Test';

create_review_product_proposal 'A', 1, 1, 'Test';

create_review_product_proposal 'A', 1, 3, 'Test';

create_review_product_proposal 'R', 1, 8, 'Test';