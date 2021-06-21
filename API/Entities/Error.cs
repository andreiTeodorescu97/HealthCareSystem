using System;

namespace API.Entities
{
    public class Error
    {
        public int Id { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string InnerMessage { get; set; }
        public string StackTrace { get; set; }
        public string Route { get; set; }
        public string Section { get; set; }
        public DateTime TimeStamp { get; set; }
        public string caca { get; set; }
    }
}