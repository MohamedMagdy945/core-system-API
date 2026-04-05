namespace AppCoreSystem.Application.Common.Exceptions
{
    public class NotFoundAppException : Exception
    {
        public string EntityName { get; }
        public object? Key { get; }

        public NotFoundAppException(string entityName, object? key = null)
            : base($"{entityName} was not found{(key != null ? $" (Key: {key})" : "")}")
        {
            EntityName = entityName;
            Key = key;
        }
    }
}