using MailKit.Net.Imap;
using System;
using System.Threading.Tasks;

namespace EmailProject
{
    class Program
    {
        // Our server
        private static string _host = "mail.s4ab.ru";
        private static int _port = 993; // 143;
        private static bool _secure = true; // false;
        private static string _userName = "mail.test@as61.ru";
        private static string _password = "Q1w2e3r4";

        static void Main(string[] args)
        {
            Task.Run(() => MainAsync(args)).Wait();
        }
        static async Task MainAsync(string[] args)
        {
            var i = 0;
            var max = 1000;
            try
            {
                for (; i < max; i++)
                {
                    await Process();
                    DisplayPercent(i / (double)max);
                }
                Console.WriteLine("No error?");
                Console.WriteLine("It's strange.");
                Console.WriteLine("Try again.");
            }
            catch (Exception ex)
            {
                var error = ex.GetBaseException();
                Console.WriteLine("Error raise by step {0}.", i);
                Console.WriteLine(error.Message);
            }
        }
        static async Task Process()
        {
            using (var client = new ImapClient())
            {
                // Connection
                await client.ConnectAsync(_host, _port, _secure);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH");

                
                // Authentication
                await client.AuthenticateAsync(_userName, _password);
            }
        }
        static void DisplayPercent(double percent)
        {
            Console.Clear();
            Console.WriteLine("{0:P2}", percent);
        }
    }
}
