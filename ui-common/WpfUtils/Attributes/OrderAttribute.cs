namespace WpfUtils.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class OrderAttribute : Attribute
    {
        public int Order { get; private set; } = 999;
        public OrderAttribute(int order)
        {
            Order = order;
        }
    }
}
