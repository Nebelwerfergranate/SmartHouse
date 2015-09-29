namespace SmartHome
{
    public interface ITimer
    {
        event SmartHome.OperationDoneDelegate OperationDone;
    
        bool IsRunning
        {
            get;
        }
    
        void TimerSetMinutes(byte minutes);

        void TimerSetSeconds(byte seconds);

        void Start();

        void Stop();
    }
}
