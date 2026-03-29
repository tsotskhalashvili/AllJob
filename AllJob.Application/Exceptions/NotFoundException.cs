namespace AllJob.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message)
        {
            
        }


        public NotFoundException(string name, object key)
            : base($"{name} with Id '{key}' was not found")
        {

            
        }

    }
}
    