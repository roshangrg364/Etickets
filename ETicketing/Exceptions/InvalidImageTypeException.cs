namespace ETicketing.Exceptions
{
    public class InvalidImageTypeException:Exception
    {
        public InvalidImageTypeException(string message ="Invalid Image Type."):base(message)
        {

        }
    }
}
