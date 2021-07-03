create procedure usp_getproduct
@id int
As
begin
	Select * from [dbo].[Products] where Id=@id
end

GO
create function fn_getproduct(
@id int)
returns table
As
return (Select * from [dbo].[Products] where Id=@id)