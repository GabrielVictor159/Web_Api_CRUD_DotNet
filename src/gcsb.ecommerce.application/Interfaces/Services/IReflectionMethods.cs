using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.Interfaces.Services
{
    public interface IReflectionMethods
    {
        void ReplaceDifferentAttributes<T>(T source, T destination);
    }
}