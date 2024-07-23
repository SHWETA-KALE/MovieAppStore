using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviestoreApp.Exceptions
{
    internal class MovieStoreIsEmptyException:Exception
    {
        public MovieStoreIsEmptyException(string Message) : base(Message) { }
    }
}
