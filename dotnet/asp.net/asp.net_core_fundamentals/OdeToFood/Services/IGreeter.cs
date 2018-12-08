using Microsoft.Extensions.Configuration;

namespace OdeToFood.Services
{
    public interface IGreeter
    {
        string GetMessageOfTheDay();
    }

    public class Greeter : IGreeter
    {
        IConfiguration _configuration;
        public Greeter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetMessageOfTheDay() => _configuration["Greeting"];
    }
}