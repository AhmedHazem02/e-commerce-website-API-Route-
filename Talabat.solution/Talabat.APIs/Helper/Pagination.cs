using Talabat.APIs.DTOs;

namespace Talabat.APIs.Helper
{
    public class Pagination<T>
    {
       
        public Pagination(int pagesize, int pageIndex, IReadOnlyList<T> data,int count)
        {
            PageSize = pagesize;
            PageIndex = pageIndex;
            Data = data;
            Count = count;
        }

        public int PageSize {  get; set; }
       
        public int PageIndex {  get; set; }
        public int Count { get; set; }

        public IReadOnlyList<T> Data { get; set; }
        
    }
}
