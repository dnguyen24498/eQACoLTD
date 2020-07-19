using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Utilities.Exceptions
{
    public class eQAException:Exception
    {
        public eQAException()
        {

        }

        public eQAException(string message):base(message)
        {

        }

        public eQAException(string message,Exception inner):base(message,inner)
        {

        }
    }
}
