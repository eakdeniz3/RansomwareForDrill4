using System.Collections.Generic;
using System.Xml.Serialization;

namespace RFD.Entities.Common.Model
{
    [XmlRoot("ApiResponse")]
    public class ApiResponse<T>: Response
    {
        public T Data { get; set; }

        public bool IsSucceeded { get; set; }

        public string[] Message { get; set; }

        public static ApiResponse<T> Success(T data)
        {
            return new ApiResponse<T> { Data = data,  IsSucceeded = true };
        }

        public static ApiResponse<T> Fail(string[] errorMessages)
        {
            return new ApiResponse<T> { Message = errorMessages,IsSucceeded=false};
        }
        public static ApiResponse<T> Fail(string errorMessage)
        {

            return new ApiResponse<T> { Message = new string[] { errorMessage }, IsSucceeded = false };
        }

        public static ApiResponse<T> Fail(List<string> errorMessage)
        {
            return new ApiResponse<T> { Message = errorMessage.ToArray(), IsSucceeded = false };
        }

    }
}
