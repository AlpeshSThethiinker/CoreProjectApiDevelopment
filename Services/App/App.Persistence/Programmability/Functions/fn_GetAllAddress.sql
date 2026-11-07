CREATE OR ALTER FUNCTION [dbo].[fn_GetAllAddress]
(
    @SearchText VARCHAR(MAX) = N'',
    @fromDate VARCHAR(MAX) = '',
    @toDate VARCHAR(MAX) = ''
)
RETURNS TABLE
AS
RETURN
(
    SELECT
        p.Id AS Id,
        p.AccountGroupId AS AccountGroupId,
        p.Line1 AS Line1,
        p.Line2 AS Line2,
        p.Country AS Country,
        p.State AS State,
        p.City AS City,
        p.ZipCode AS ZipCode
    FROM dbo.Addresses AS p
    WHERE p.Deleted = 0
      AND @SearchText = ''
      AND @fromDate = ''
      AND @toDate = ''
);