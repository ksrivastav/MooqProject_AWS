namespace Order.Core.Specs
{
    public class OrderSpecs
    {
        public string? orderByColumn { get; set; }
        public string? sortOrder { get; set; }
        public string? searchColumn {  get; set; }
        public string? searchValue { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
