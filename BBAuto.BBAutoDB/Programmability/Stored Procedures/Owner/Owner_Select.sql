CREATE PROCEDURE [dbo].[Owner_Select]
@all int = 0
AS
BEGIN
	if (@all = 1)
	begin
		SELECT owner_id, owner_name 'Название' FROM Owner
		UNION
		SELECT 0, '(все)'	
		ORDER BY 'Название'
	end
	else
	begin
		SELECT owner_id, owner_name 'Название' FROM Owner
	end
END
GO
