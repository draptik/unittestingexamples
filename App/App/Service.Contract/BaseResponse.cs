namespace App.Service.Contract
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Success = true;
        }

        public string Message { get; set; }
        public bool Success { get; set; }
    }
}