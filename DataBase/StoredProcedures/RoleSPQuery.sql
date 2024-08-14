----------------- Stored Procedures -----------------

USE projectplmdb;
GO

CREATE PROCEDURE get_role
AS
BEGIN

	SELECT 
		role_id,
		[description]
	FROM [role] 

END
GO

----------------- Testing -----------------
get_role