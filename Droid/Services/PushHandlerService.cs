using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Content;
using Android.Support.V7.App;
using Android.Util;
using Gcm.Client;
using WindowsAzure.Messaging;

namespace XamSnap.Droid
{
    [Service]
    public class PushHandlerService : GcmServiceBase
    {
        private const string Title = "XamSnap";
        private NotificationHub _hub;

        public PushHandlerService() : base(Constants.ProjectId) { }

        protected override void OnRegistered(Context context, string registrationId)
        {
            Log.Verbose(PushBroadcastReceiver.TAG, "GCM Registered: " + registrationId);

            _hub = new NotificationHub(Constants.HubName, Constants.ConnectionString, context);
            try
            {
                _hub.RegisterTemplate(registrationId, "Android", "{\"data\":{\"message\":\"$(message)\"}}", "test");
            }
            catch (Exception ex)
            {
                Log.Error(PushBroadcastReceiver.TAG, ex.Message);
            }
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            Log.Info(PushBroadcastReceiver.TAG, "GCM Message Received!");

            string message = intent.Extras.GetString("message");
            if (!string.IsNullOrEmpty(message))
            {
                //Create notification
                var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

                //Create an intent to show UI
                var uiIntent = new Intent(this, typeof(LoginActivity));

                ////Create the notification
                //var notification = new Notification(Android.Resource.Drawable.SymActionEmail, Title);

                ////Auto-cancel will remove the notification once the user touches it
                //notification.Flags = NotificationFlags.AutoCancel;

                ////Set the notification info
                ////we use the pending intent, passing our ui intent over, which will get called
                ////when the notification is tapped.
                //notification.SetLatestEventInfo(this, Title, message, );
                ////notification.ContentIntent = 

                ////Show the notification
                //notificationManager.Notify(1, notification);

                var notification = new NotificationCompat.Builder(this)
                    .SetContentIntent(PendingIntent.GetActivity(this, 0, uiIntent, 0))
                    .SetSmallIcon(Android.Resource.Drawable.SymActionEmail)
                    .SetAutoCancel(true)
                    .SetContentTitle(Title)
                    .SetContentText(message)
                    .Build();
                notificationManager.Notify(1, notification);
            }
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Log.Verbose(PushBroadcastReceiver.TAG, "GCM Unregistered: " + registrationId);

            _hub.UnregisterTemplate("Android");
        }

        protected override bool OnRecoverableError(Context context, string errorId)
        {
            Log.Warn(PushBroadcastReceiver.TAG, "Recoverable Error: " + errorId);

            return base.OnRecoverableError(context, errorId);
        }

        protected override void OnError(Context context, string errorId)
        {
            Log.Error(PushBroadcastReceiver.TAG, "GCM Error: " + errorId);
        }
    }
}
