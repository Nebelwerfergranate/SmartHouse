﻿namespace SmartHome
{
    public interface IOpenable
    {
        bool IsOpen { get; }

        void Close();

        void Open();
    }
}
