
CREATE FUNCTION [dbo].[fn_GetAllAccountLedger] (
	@SearchText VARCHAR(MAX) = N'',
	@fromDate VARCHAR(MAX) = '',
	@toDate VARCHAR(MAX) = ''
	)
RETURNS TABLE
AS
RETURN

SELECT 
p.AccountGroupId AS AccountGroupId,
p.AccountNo AS AccountNo,
p.LedgerTypeId AS LedgerTypeId,
p.LedgerName AS LedgerName,
p.FirstName AS FirstName,
p.LastName AS LastName,
p.DateOfBirth AS DateOfBirth,
p.Mobile As Mobile,
p.MobileNoDialCode AS MobileNoDialCode,
p.Email AS Email
FROM AccountLedgers AS P

WHERE  P.Deleted=0 
AND (
		@SearchText = N''
		)
		AND (@fromDate = '')
		AND (@toDate = '')