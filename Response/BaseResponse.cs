namespace BanDongHoSolution.Response
{
    public class BaseResponse<T>
    {
        public bool _success { get; set; }
        public string _Message { get; set; }
        public T _Data { get; set; }

        public BaseResponse(bool success, string message, T data)
        {
            _success = success;
            _Message = message;
            _Data = data;
        }
    }
}
