namespace App.Services.Responses
{

    public class Response<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; }

        public static Response<T> Ok(T data, string message = "") =>
            new Response<T> { Success = true, Data = data, Message = message };

        public static Response<T> Fail(string message) =>
            new Response<T> { Success = false, Data = default, Message = message };
    }
}
