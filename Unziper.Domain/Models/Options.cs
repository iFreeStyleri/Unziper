using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unziper.Domain.Models
{
    public class Options
    {
        public string[] Passwords { get; set; }
        public string ExtractDirectory { get; set; }
    }
}
