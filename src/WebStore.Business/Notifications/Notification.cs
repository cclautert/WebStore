﻿
namespace WebStore.Business.Notifications
{
    public class Notification
    {
        public Notification(string mensage)
        {
            Mensage = mensage;
        }

        public string? Mensage { get; }
    }
}
