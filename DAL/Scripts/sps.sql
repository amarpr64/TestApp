create procedure usp_getproduct
@ProductId int
As
begin
	Select * from [dbo].[Products] where ProductId=@ProductId
end

Go
create function udf_getproduct(
@ProductId int)
returns Table
As
return(
	Select * from [dbo].[Products] where ProductId=@ProductId)