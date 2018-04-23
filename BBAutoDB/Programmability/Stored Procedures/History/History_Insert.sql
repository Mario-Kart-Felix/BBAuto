CREATE PROCEDURE [dbo].[History_Insert]
@Comment nvarchar(50),
@id int,
@Event nvarchar(50),
@file nvarchar(MAX)
AS
BEGIN
	if (@file = '')
		insert into History values(@Comment, @id, CURRENT_TIMESTAMP, @Event, null)
	else
		insert into History values(@Comment, @id, CURRENT_TIMESTAMP, @Event, @file)
END
GO
