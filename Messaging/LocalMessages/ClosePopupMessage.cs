namespace Messaging.LocalMessages
{
    public class ClosePopupMessage
    {
        public WeakReference? Sender { get; set; }

        public ClosePopupMessage(object? sender)
        {
            Sender = new WeakReference(sender);
        }
    }
}
