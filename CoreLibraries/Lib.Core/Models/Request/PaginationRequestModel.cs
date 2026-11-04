namespace Lib.Core.Models.Request
{
    public class PaginationRequestModel
    {
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; } = "ASC";

        public int PageSize { get; set; } = 25;

        public string? SearchText { get; set; }

        public string? OrderBy { get; set; }


        public bool GetAll { get; set; }

        public int Page { get; set; } = 1;

        public string? filters { get; set; }

        public string? total_rows { get; set; }

        public string? total_pages { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public List<Filter>? filterList { get; set; }
        public List<SortList>? sortList { get; set; }
        public List<ExportRequestModel>? exportRequest { get; set; }
    }

    public class ExportRequestModel
    {
        public string? GridName { get; set; }
        public List<ColumnToInclude>? columnToInclude { get; set; }
    }


    public class ColumnToInclude
    {
        public string? ColumnName { get; set; }
        public int ColumnOrder { get; set; }
        public string? DisplayName { get; set; }
    }

    public class Filter
    {
        public string? name { get; set; }
        public string? textFilter { get; set; }
        public List<filterModel>? FilterData { get; set; }
    }

    public class filterModel
    {
        public int id { get; set; }
        public string? name { get; set; }
    }
    public class SortList
    {
        public string? sort { get; set; }
        public string? colId { get; set; }
        public int sortIndex { get; set; }
    }

    public class Filters
    {
        public Filters()
        {
            rules = new List<Rule>();
            groups = new List<Group>();
        }
        public enum GroupOp
        {
            AND = 0,
            OR = 1
        }

        public enum Operations
        {
            eq, // "equal"
            ne, // "not equal"
            lt, // "less"
            le, // "less or equal"
            gt, // "greater"
            ge, // "greater or equal"
            bw, // "between"
            bn, // "does not begin with"
            ew, // "ends with"
            en, // "does not end with"
            cn, // "contains"
            nc  // "does not contain"
        }

        public class Rule
        {
            public string? field { get; set; }
            public Operations op { get; set; }
            public string? data { get; set; }
        }

        public class Group
        {
            public string? groupOp { get; set; }
            public List<Rule>? rules { get; set; }
        }
        public List<Rule> rules { get; set; }
        public List<Group> groups { get; set; }
    }

}
