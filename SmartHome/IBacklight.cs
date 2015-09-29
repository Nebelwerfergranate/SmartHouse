namespace SmartHome
{
    public interface IBacklight
    {
        bool IsHighlighted
        {
            get;
        }

        double LampPower { get; }
    }
}
