using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using FluentValidator;

namespace gcsb.ecommerce.application.Interfaces.Services
{
    public interface INotificationService
    {
        List<Notification> Notifications { get; set; }
        bool HasNotifications { get; }
        void AddNotification(string key, string message);
        void AddNotifications(ValidationResult? validationResult);
    }
}