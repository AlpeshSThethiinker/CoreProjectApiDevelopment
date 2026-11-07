
CREATE OR ALTER PROCEDURE [dbo].[sp_GetAllAccountLedger]
    @GetAll BIT,
    @page_size INT,
    @page_num INT,
    @totalRows INT = 0,
    @totalPages INT = 0,
    @SearchText VARCHAR(MAX) = '',
    @OrderBy VARCHAR(MAX) = 'NULL',
    @fromDate VARCHAR(MAX) = '',
    @toDate VARCHAR(MAX) = ''
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

    -- Validate pagination
    IF (@page_size <= 0 OR @page_num <= 0)
    BEGIN
        THROW 50001, 'Page size and page number must be greater than zero.', 1;
    END;

    -- Get all records
    IF (@GetAll = 1)
    BEGIN
        SELECT
            *,
            (SELECT COUNT(*)
             FROM dbo.fn_GetAllAccountLedger(
                 @SearchText, @fromDate, @toDate
             )) AS total_rows,
            1 AS total_pages
        FROM dbo.fn_GetAllAccountLedger(
            @SearchText, @fromDate, @toDate
        );

        RETURN;
    END;

    -- Get total records
    SELECT @totalRows = COUNT(*)
    FROM dbo.fn_GetAllAccountLedger(
        @SearchText, @fromDate, @toDate
    );

    -- Calculate total pages
    SET @totalPages = CEILING(
        CAST(@totalRows AS DECIMAL(18,2)) / @page_size
    );

    -- Default sorting
    IF (
        @OrderBy IS NULL
        OR LTRIM(RTRIM(@OrderBy)) = ''
        OR UPPER(LTRIM(RTRIM(@OrderBy))) = 'NULL'
    )
    BEGIN
        SET @OrderBy = 'AccountGroupId DESC';
    END;

    -- Validate sorting column and direction
    DECLARE @SortColumn VARCHAR(50);
    DECLARE @SortDirection VARCHAR(4);

    SET @SortDirection =
        CASE
            WHEN UPPER(RIGHT(LTRIM(RTRIM(@OrderBy)), 4)) = 'DESC'
                THEN 'DESC'
            ELSE 'ASC'
        END;

    SET @SortColumn =
        CASE
            WHEN CHARINDEX(' ', LTRIM(RTRIM(@OrderBy))) > 0
                THEN LEFT(
                    LTRIM(RTRIM(@OrderBy)),
                    CHARINDEX(' ', LTRIM(RTRIM(@OrderBy))) - 1
                )
            ELSE LTRIM(RTRIM(@OrderBy))
        END;

    IF @SortColumn NOT IN (
        'AccountGroupId',
        'AccountNo',
        'LedgerTypeId',
        'LedgerName',
        'FirstName',
        'LastName',
        'DateOfBirth',
        'Mobile',
        'Email'
    )
    BEGIN
        SET @SortColumn = 'AccountGroupId';
        SET @SortDirection = 'DESC';
    END;

    -- Dynamic pagination query
    DECLARE @QUERY NVARCHAR(MAX);

    SET @QUERY = N'
        SELECT *,
            @totalRows AS total_rows,
            @totalPages AS total_pages
        FROM dbo.fn_GetAllAccountLedger(
            @SearchText,
            @fromDate,
            @toDate
        )
        ORDER BY ' + QUOTENAME(@SortColumn) + N' ' + @SortDirection + N'
        OFFSET @Offset ROWS
        FETCH NEXT @PageSize ROWS ONLY
        OPTION (RECOMPILE);';

    EXEC sp_executesql
        @QUERY,
        N'@SearchText VARCHAR(MAX),
          @fromDate VARCHAR(MAX),
          @toDate VARCHAR(MAX),
          @totalRows INT,
          @totalPages INT,
          @Offset INT,
          @PageSize INT',
        @SearchText = @SearchText,
        @fromDate = @fromDate,
        @toDate = @toDate,
        @totalRows = @totalRows,
        @totalPages = @totalPages,
        @Offset = (@page_size * (@page_num - 1)),
        @PageSize = @page_size;
END;
GO
