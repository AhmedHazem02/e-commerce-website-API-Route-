using Talabat.APIs.DTOs;

namespace Talabat.APIs.Helper
{
    public class Pagination<T>
    {
       
        public Pagination(int pagesize, int pageIndex, IReadOnlyList<T> data,int Count)
        {
            PageSize = pagesize;
            PageIndex = pageIndex;
            Data = data;
            PageCount = Count;
        }

        public int PageSize {  get; set; }
       
        public int PageIndex {  get; set; }
        public int PageCount { get; set; }

        public IReadOnlyList<T> Data { get; set; }
        
    }
}
