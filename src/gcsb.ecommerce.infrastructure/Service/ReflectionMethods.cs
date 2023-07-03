using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using gcsb.ecommerce.application.Interfaces.Services;

namespace gcsb.ecommerce.infrastructure.Service
{
    public class ReflectionMethods : IReflectionMethods
    {
        public void ReplaceDifferentAttributes<TSource, TDestination>(TSource source, TDestination destination)
        {
            var sourceType = source!.GetType();
            var destinationType = destination!.GetType();
            var sourceProperties = sourceType.GetProperties();
            var destinationProperties = destinationType.GetProperties();
            
            foreach (var sourceProperty in sourceProperties)
            {
                var destinationProperty = destinationProperties.FirstOrDefault(p => p.Name == sourceProperty.Name);

                if (destinationProperty != null && AreTypesCompatible(destinationProperty.PropertyType, sourceProperty.PropertyType))
                {
                    var sourceValue = sourceProperty.GetValue(source);
                    var destinationValue = destinationProperty.GetValue(destination);

                    if (sourceValue != null && !Equals(sourceValue, destinationValue))
                    {
                        if (IsAttributeEmpty(sourceValue) || IsGuidEmpty(sourceValue))
                        {
                            continue;
                        }

                        var propertyName = destinationProperty.Name;
                        var destinationField = destinationType.GetField($"<{propertyName}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);

                        if (destinationField != null)
                        {
                            destinationField.SetValue(destination, sourceValue);
                        }
                        else
                        {
                            var withMethodName = "With" + propertyName;
                            var withMethod = destinationType.GetMethod(withMethodName, BindingFlags.Instance | BindingFlags.Public);

                            if (withMethod != null)
                            {
                                withMethod.Invoke(destination, new[] { sourceValue });
                            }
                            else
                            {
                                var setMethodName = "Set" + propertyName;
                                var setMethod = destinationType.GetMethod(setMethodName, BindingFlags.Instance | BindingFlags.Public);

                                if (setMethod != null)
                                {
                                    setMethod.Invoke(destination, new[] { sourceValue });
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool AreTypesCompatible(Type destinationType, Type sourceType)
        {
            // Verifica se um dos tipos é DateTime? e o outro é DateTime
            if ((destinationType == typeof(DateTime?) && sourceType == typeof(DateTime))
                || (destinationType == typeof(DateTime) && sourceType == typeof(DateTime?)))
            {
                return true;
            }

            return destinationType == sourceType;
        }

        private bool IsAttributeEmpty(object attributeValue)
        {
            if (attributeValue is string stringValue)
            {
                return string.IsNullOrEmpty(stringValue);
            }
            else
            {
                return false;
            }
        }

        private bool IsGuidEmpty(object attributeValue)
        {
            if (attributeValue is Guid guidValue)
            {
                return guidValue == Guid.Empty;
            }
            else
            {
                return false;
            }
        }
    }
}
