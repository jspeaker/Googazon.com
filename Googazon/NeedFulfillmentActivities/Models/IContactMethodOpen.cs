namespace NeedFulfillmentActivities.Models
{
    public interface IContactMethodOpen
    {
        bool IsOpen();
        string Hours();
    }
}