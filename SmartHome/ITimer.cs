using System;

namespace SmartHome
{
    public interface ITimer
    {
        event SmartHome.OperationDoneDelegate OperationDone;
    
        bool IsRunning
        {
            get;
        }

        void SetTimer(TimeSpan time);

        void Start();

        void Stop();
    }
}
