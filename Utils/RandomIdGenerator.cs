namespace PharmaApi.Utils
{
    public static class RandomIdGenerator
    {
        private static readonly Random _random = new Random();

        public static int GenerateRandomId()
        {
            return _random.Next(100000, 999999);
        }
    }

}
