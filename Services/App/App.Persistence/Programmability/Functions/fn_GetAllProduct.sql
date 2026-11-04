
CREATE FUNCTION [dbo].[fn_GetAllProduct] (
	@SearchText VARCHAR(MAX) = N'',
	@fromDate VARCHAR(MAX) = '',
	@toDate VARCHAR(MAX) = ''
	)
RETURNS TABLE
AS
RETURN

SELECT 
P.Id AS Id,
P.SKU AS SKU,
P.Name AS Name,
P.ShortName AS ShortName,
P.Price AS Price,
P.Category AS Category,
P.DeliveryTimeSpan AS DeliveryTimeSpan,
P.ImageUrl AS ImageUrl,
P.Description AS Description
FROM Products AS P

WHERE  P.Deleted=0 
AND (
		@SearchText = N''
		)
		AND (@fromDate = '')
		AND (@toDate = '')