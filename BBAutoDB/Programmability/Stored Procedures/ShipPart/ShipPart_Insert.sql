CREATE PROCEDURE [dbo].[ShipPart_Insert]
@id int,
@idCar int,
@idDriver int,
@name nvarchar(50),
@dateRequestText nvarchar(50),
@dateSentText nvarchar(50),
@file nvarchar(500)
AS
BEGIN
	Declare @dateRequest datetime
	
	if (@dateRequestText = '')
		SET @dateRequest = null
	else
		SET @dateRequest = CAST(@dateRequestText as datetime)
		
	Declare @dateSent datetime
	
	if (@dateSentText = '')
		SET @dateSent = null
	else
		SET @dateSent = CAST(@dateSentText as datetime)
		
	if (@id = 0)
	begin
		INSERT INTO ShipPart VALUES(@idCar, @idDriver, @name, @dateRequest, @dateSent, @file)
		
		SET @id = SCOPE_IDENTITY()
	end
	else
	begin
		UPDATE ShipPart SET driver_id=@idDriver, shipPart_name=@name, shipPart_dateRequest=@dateRequest,
			shipPart_dateSent=@dateSent, shipPart_file=@file
		WHERE shipPart_id=@id
	end
	
	SELECT @id
END
GO
