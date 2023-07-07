using GalaSoft.MvvmLight.Messaging;

namespace EmailSaveAddin.Helpers
{
    public class MessengerHelper
    {
        public static void BroadcastMessage<T>(T message, object token = null)
        {
            if (token != null)
            {
                Messenger.Default.Send(message, token);
            }
            else
            {
                Messenger.Default.Send(message);
            }

        }
    }
}
