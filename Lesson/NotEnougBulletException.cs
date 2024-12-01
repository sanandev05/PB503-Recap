using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson
{
    public class NotEnougBulletException : Exception
    {
        public NotEnougBulletException(string? message) : base(message)
        {
        }
    }
}
