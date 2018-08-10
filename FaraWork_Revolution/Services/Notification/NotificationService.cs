using Notifications.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaraWork_Revolution.Services.Notification
{
    public class NotificationService
    {
        public static void SimpleNotification(string title,string message,NotificationType type)
        {
            var notificationManager = new NotificationManager();

            notificationManager.Show(new NotificationContent
            {
                Title = title,
                Message = message,
                Type = type
            });
        }
        
        public static void SimpleNotification(string title, string message, NotificationType type,Task myaction) { }
        public static void NativeNotification(string title, string message,string area)
        {
            NotificationManager _notificationManager = new NotificationManager();
            var content = new NotificationContent { Title = title, Message = message };
            var clickContent = new NotificationContent
            {
                Title = "Clicked!",
                Message = "Window notification was clicked!",
                Type = NotificationType.Success
            };
            _notificationManager.Show(content, area, onClick: () => _notificationManager.Show(clickContent));
        }
    }
}
