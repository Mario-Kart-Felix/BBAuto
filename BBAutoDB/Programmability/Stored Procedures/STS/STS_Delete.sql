CREATE PROCEDURE [dbo].[STS_Delete]
@idCar int
AS
BEGIN
	DELETE FROM STS WHERE car_id=@idCar
END
GO
