
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Firebase.Messaging;
using SI_Master.Managers;
using SI_Master.Services.PushService;
using Xamarin.Forms;

namespace SI_Master.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    class PushMessagingService: FirebaseMessagingService
    {
        private const int NOTIFICATION_ID = 10;

        private IPushService pushService = DependencyService.Get<IPushService>();
        private IAuthManager authManager = DependencyService.Get<IAuthManager>();

        public override async void OnNewToken(string token)
        {
            base.OnNewToken(token);
            await authManager.MemorizeFCMTken(token);
        }

        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            var notification = message.GetNotification();

            var data = message.Data;

            if (notification != null)
            {
                ShowNotification(notification.Title, notification.Body);
            }
            if (data != null)
            {
                pushService.OnDataPushReceived(data);
            }
        }

        private void ShowNotification(string title, string subtitle)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this,
                                                          NOTIFICATION_ID,
                                                          intent,
                                                          PendingIntentFlags.OneShot);

            var notificationBuilder = new NotificationCompat.Builder(this, MainActivity.CHANNEL_ID)
                                      .SetSmallIcon(Resource.Mipmap.icon)
                                      .SetContentTitle(title)
                                      .SetContentText(subtitle)
                                      .SetPriority((int)NotificationPriority.Max)
                                      .SetAutoCancel(true)
                                      .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(NOTIFICATION_ID, notificationBuilder.Build());
        }
    }
}