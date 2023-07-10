using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.Boundaries
{
    public interface IOutputPort<T>
    {
        void Standard(T output);
        void Error(string message);
        void NotFound(string message);
    }
}