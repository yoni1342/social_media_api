using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galacticos.Domain.Entities
{
    public class Email
    {
        public string To { get; set; } = null!;
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }
}