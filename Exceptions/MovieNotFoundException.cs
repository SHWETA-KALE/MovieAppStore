using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviestoreApp.Exceptions
{
    internal class MovieNotFoundException:Exception
    {
        public MovieNotFoundException(string message) : base(message) { }
    }
}
