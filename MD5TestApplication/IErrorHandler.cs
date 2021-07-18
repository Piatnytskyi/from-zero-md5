using System;

namespace MD5TestApplication
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
