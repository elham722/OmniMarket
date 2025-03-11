namespace OmniMarket.Application.Exceptions
{
    public class UserAlreadyExistsException(string userName) : Exception($"User name '{userName}' already exists.")
    {
    }

    public class EmailAlreadyExistsException(string email) : Exception($"Email '{email}' already exists.")
    {
    }

    public class UserNotFoundException(string email) : Exception($"User with email '{email}' not found.")
    {
    }

    public class InvalidCredentialsException(string email) : Exception($"Credentials for '{email}' are not valid.")
    {
    }

    public class RegistrationFailedException(string errors) : Exception($"Registration failed: {errors}")
    {
    }
}