using System;
using Foundation;
using UIKit;
using WindowsAzure.Messaging;

namespace XamSnap.iOS
{
    public class AppleNotificationService : INotificationService
    {
        private SBNotificationHub hub;
        private string userName;

        public void Start(string userName)
        {
            this.userName = userName;

            var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null);

            UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
        }

        public void SetToken(object deviceToken)
        {
            if (hub == null)
            {
                hub = new SBNotificationHub("Endpoint=sb://xamsnap.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=ZqbSdT8i+2YBpNNxmgMNAOrnoGPCBcr+/Hs9gecyNTQ=", "xamsnap");
            }

            var tags = new NSSet(userName);
            hub.RegisterNativeAsync((NSData)deviceToken, tags, (errorCallback) =>
            {
                if (errorCallback != null)
                    Console.WriteLine("RegisterNativeAsync error: " + errorCallback);
            });
        }
    }
}
