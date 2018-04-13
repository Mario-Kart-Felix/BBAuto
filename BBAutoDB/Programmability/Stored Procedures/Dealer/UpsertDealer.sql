create procedure [dbo].[UpsertDealer]
  @id int,
  @Name varchar(100),
  @Contacts varchar(500)
as
begin
  if (@id = 0)
    insert into Dealer([Name], Contacts)
      values (@Name, @Contacts)
  else
    update Dealer
    set [Name] = @Name,
        Contacts = @Contacts
    where Id = @id
end
