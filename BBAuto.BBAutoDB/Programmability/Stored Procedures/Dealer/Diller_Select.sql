CREATE PROCEDURE [dbo].[Diller_Select]
AS
BEGIN
	SELECT diller_id, diller_name 'Название', diller_contacts 'Контакты'
	FROM Diller
	ORDER BY 'Название'
END
GO
