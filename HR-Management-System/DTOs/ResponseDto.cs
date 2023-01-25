namespace HR_Management_System.DTOs
{
    public class ResponseDto
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Payload { get; set; }

    }
}
