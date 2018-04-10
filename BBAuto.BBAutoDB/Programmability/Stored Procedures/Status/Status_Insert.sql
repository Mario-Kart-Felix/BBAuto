CREATE PROCEDURE [dbo].[Status_Insert]
@id int,
@Name varchar(50)
AS
BEGIN
	if (@id = 0)
	begin
		Declare @count int
		SELECT @count=count(Status_id) FROM Status WHERE Status_name=@Name
	
		if (@count = 0)
		begin
			INSERT INTO Status VALUES(@Name)
			SELECT 'Добавлен'
		end
		else
			SELECT 'Данный статус уже существует'
	end
	else
	begin
		UPDATE Status SET Status_name=@Name WHERE Status_id=@id
		SELECT 'Обновлен'
	end
END
GO
