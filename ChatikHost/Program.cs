using System;
using System.ServiceModel;


namespace ChatikHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(Chatik.ServiceChatik)))
            {
                host.Open();
                Console.WriteLine("Host start!!!");
                Console.ReadKey();
            }
        }
    }
}
