﻿namespace Pihalve.Player.Persistence
{
    public interface IAppDataPersister<T>
    {
        void Save(T applicationData, string outputFileName);
        T Load(string inputFileName);
    }
}
