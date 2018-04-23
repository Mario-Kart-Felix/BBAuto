CREATE PROCEDURE [dbo].[STS_Insert]
@idCar int,
@number nvarchar(50),
@date datetime,
@giveOrg nvarchar(100),
@file nvarchar(200)
AS
BEGIN
	if (@idCar = 0)
		return
		
	Declare @count int
	SELECT @count=COUNT(car_id) FROM STS WHERE car_id=@idCar
	
	if (@count = 0)
		INSERT INTO STS VALUES(@idCar, @number, @date, @giveOrg, @file)
	else
		UPDATE STS
		SET sts_number=@number, sts_date=@date, sts_giveOrg=@giveOrg, sts_file=@file
		WHERE car_id=@idCar
END
GO
