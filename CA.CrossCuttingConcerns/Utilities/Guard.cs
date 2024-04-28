namespace CA.CrossCuttingConcerns.Utilities
{
    public static class Guard
    {
        public static void ThrowIfNull<TExceptionType>(object obj, string message) where TExceptionType : Exception
        {
            if (obj is not null)
            {
                return;
            }

            if (!(Activator.CreateInstance(typeof(TExceptionType), message) is Exception exceptionObj))
            {
                throw new ArgumentNullException(message);
            }

            throw exceptionObj;
        }

        public static void ThrowByCondition<TExceptionType>(bool condition, string message) where TExceptionType : Exception
        {
            if (!condition)
            {
                return;
            }

            if (!(Activator.CreateInstance(typeof(TExceptionType), message) is Exception exceptionObj))
            {
                throw new ArgumentException(message);
            }

            throw exceptionObj;
        }

        public static void DoIfNull(object obj, Action action)
        {
            if (obj is not null)
            {
                return;
            }

            action();
        }

        public static void DoByCondition(bool condition, Action action)
        {
            if (!condition)
            {
                return;
            }

            action();
        }
    }
}
