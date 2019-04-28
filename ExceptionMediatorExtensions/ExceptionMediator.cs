using System;

namespace ExceptionMediatorLibrary
{
    public static class ExceptionMediator
    {
        public static (Exception exception, TReturn routineResult) Intercept<TReturn>(Func<TReturn> routine) 
        {
            var routineResult = default(TReturn);

            try
            {
                routineResult = routine();

                return (null, routineResult);
            }
            catch(Exception ex)
            {
                return (ex, default);
            }            
        }
    }
}
