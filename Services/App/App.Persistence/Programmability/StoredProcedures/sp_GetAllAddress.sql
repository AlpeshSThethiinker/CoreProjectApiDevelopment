CREATE PROCEDURE [dbo].[sp_GetAllAddress] (
	@GetAll BIT,
	@page_size INT,
	@page_num INT,
	@totalRows INT = 0,
	@totalPages INT = 0,
	@SearchText VARCHAR(MAX) = N'',
	@OrderBy VARCHAR(MAX) = 'NULL',
	@fromDate VARCHAR(MAX) = '',
	@toDate VARCHAR(MAX) = ''
	)
AS
BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	 IF (@GetAll = '1')
    BEGIN
        SELECT *,
            0 AS total_rows,
            1 AS total_pages
        FROM [fn_GetAllAddress](@SearchText ,@fromDate ,@toDate) 
    END
    ELSE
    BEGIN
        SELECT @totalRows = COUNT(*)
        FROM [fn_GetAllAddress](@SearchText,@fromDate ,@toDate)

        SET @totalPages = IIF(@totalRows % @page_size = 0, @totalRows / @page_size, @totalRows / @page_size + 1)

        DECLARE @QUERY NVARCHAR(MAX);

        SET @QUERY = 'SELECT * FROM (
            SELECT *, ' + ISNULL(CAST(@totalRows AS VARCHAR(MAX)), '0') + ' AS total_rows,
                ' + ISNULL(CAST(@totalPages AS VARCHAR(MAX)), '0') + ' AS total_pages
            FROM [fn_GetAllAddress](
                ''' + ISNULL(@SearchText, '') + ''',
                ''' + ISNULL(@fromDate, '') + ''',
                ''' + ISNULL(@toDate, '') + '''
            )
        ) AS data
        ORDER BY ' + ISNULL(@OrderBy, 'Id desc') + '
        OFFSET ' + CAST((@page_size * (@page_num - 1)) AS VARCHAR(MAX)) + ' ROWS
        FETCH NEXT ' + CAST(@page_size AS VARCHAR(MAX)) + ' ROWS ONLY OPTION (RECOMPILE)'

        EXEC (@QUERY)
    END
END