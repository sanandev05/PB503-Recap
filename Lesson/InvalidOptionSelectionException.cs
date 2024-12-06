using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson
{
    public class InvalidOptionSelectionException : Exception
    {
        public InvalidOptionSelectionException(string? message) : base(message)
        {
            
        }
    }
}
