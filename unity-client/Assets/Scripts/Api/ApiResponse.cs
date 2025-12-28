using System;

namespace Api
{
    [Serializable]
    public class ApiResponse<T>
    {
        public bool success;
        public string code;
        public string message;
        public T data;
    }
}
