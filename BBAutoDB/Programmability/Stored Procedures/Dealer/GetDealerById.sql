create procedure dbo.GetDealerById
  @id int
as
  select
    d.Id,
    d.Name,
    d.Contacts
  from
    dbo.Dealer d
  where
    d.Id = @id
