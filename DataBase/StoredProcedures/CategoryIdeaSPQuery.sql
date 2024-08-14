----------------- Stored Procedures -----------------

USE projectplmdb;
GO

CREATE PROCEDURE get_category_idea
AS
BEGIN

	SELECT 
		category_idea_id,
		[description]
	FROM category_idea 

END
GO

----------------- Testing -----------------

get_category_idea