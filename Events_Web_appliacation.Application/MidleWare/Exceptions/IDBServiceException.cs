using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events_Web_application.Application.MidleWare.Exceptions
{
    internal interface IDBServiceException
    {
        public string Operation { get; set; }
        public object Value { get; set; }
    }
}
