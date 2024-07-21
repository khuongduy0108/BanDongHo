namespace BanDongHoSolution.Response
{
    public class PaginationResponse<T>:BaseResponse<T>
    {
        public int _totalPages { get; set; }
        public PaginationResponse(bool success, string message, T data,int totalPages) : base(success, message, data)
        {
            _totalPages = totalPages;
        }
    }
}
