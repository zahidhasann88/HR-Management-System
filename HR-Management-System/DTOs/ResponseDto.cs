using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_System.DTOs
{
    public class ResponseDto
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public object Payload { get; set; }
    }
}
