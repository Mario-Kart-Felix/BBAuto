CREATE PROCEDURE [dbo].[Region_Select]
@all int = 0
AS
BEGIN
	if (@all = 1)
		SELECT region_id, region_name 'Название' FROM Region
		UNION
		SELECT 0, '(все)'
		ORDER BY 'Название'
	else
		SELECT region_id, region_name 'Название'
		FROM Region
		ORDER BY 'Название'
END
GO
