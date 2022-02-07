using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailWebTest.Models
{
    public class EmailData
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Recipients { get; set; }
        public string CreateDate { get; set; }
        public string Result { get; set; }
        public string FailedMessage { get; set; }

    }
}
