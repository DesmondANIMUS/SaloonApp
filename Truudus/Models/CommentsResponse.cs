using System.Collections.Generic;

namespace Truudus.Models
{    
        public class Response
    {
        public string SalonName { get; set; }
        public string EndUsername { get; set; }
        public string Comment { get; set; }
        public string Star { get; set; }
    }

    public class CommentsResponse
    {
        public List<Response> response { get; set; }
    }
}
