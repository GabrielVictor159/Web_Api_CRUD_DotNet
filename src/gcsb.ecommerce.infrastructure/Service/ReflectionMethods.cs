using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Interfaces.Services;

namespace gcsb.ecommerce.infrastructure.Service
{
    public class ReflectionMethods : IReflectionMethods
    {
         public void ReplaceDifferentAttributes<T>(T source, T destination)
        {
            var sourceType = source!.GetType();
            var destinationType = destination!.GetType();
            var sourceProperties = sourceType.GetProperties();
            var destinationProperties = destinationType.GetProperties();
            foreach (var sourceProperty in sourceProperties)
            {
                var destinationProperty = destinationProperties.FirstOrDefault(p => p.Name == sourceProperty.Name);
                if (destinationProperty != null && destinationProperty.PropertyType == sourceProperty.PropertyType)
                {
                    var sourceValue = sourceProperty.GetValue(source);
                    var destinationValue = destinationProperty.GetValue(destination);
                    if (!Equals(sourceValue, destinationValue))
                    {
                        destinationProperty.SetValue(destination, sourceValue);
                    }
                }
             }
        }
    }
}