CREATE FUNCTION [dbo].[DateTime_To_Date]
	(@date datetime)
RETURNS nvarchar(50)
AS
BEGIN
	Declare @result nvarchar(50)
	SELECT @result = CASE WHEN DAY(@date) < 10 THEN '0' ELSE '' END
		+ CAST(DAY(@date) as nvarchar(50)) + '.'
		+ CASE WHEN MONTH(@date) < 10 THEN '0' ELSE '' END
		+ CAST(MONTH(@date) as nvarchar(50))
		+ '.' + CAST(YEAR(@date) as nvarchar(50));
		
	RETURN @result
END
GO
