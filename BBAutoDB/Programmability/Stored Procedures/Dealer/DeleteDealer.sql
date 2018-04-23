create procedure dbo.DeleteDealer
  @id int
as
begin
  delete from dbo.Dealer
  where Id = @id
end
