using System.Collections;

namespace  Luftborn.Application.Responses.Wrappers
{
    public class DataSourceResult
    {
        public IEnumerable Data { get; set; }
        public int TotalItems { get; set; }
        public int PageIndex { get; set; }
    }
}
