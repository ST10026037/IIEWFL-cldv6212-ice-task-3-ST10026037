using Azure.Storage.Queues;

namespace FibonacciSequenceGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var fibonacciNumbers = GenerateFibonacci(233);

            string connectionString = "DefaultEndpointsProtocol=https;AccountName=hopehlongwanesa;AccountKey=F+VR0Ut91jSryb3DLGj7Kw8SCWCVjhLRA6TqtATqvWbtxtOLn48YmUM5efrPpqCtZNTgwue/tpIE+AStpkmt+Q==;EndpointSuffix=core.windows.net";
            string queueName = "fibonacci-queue";
            QueueClient queueClient = new QueueClient(connectionString, queueName);

            queueClient.CreateIfNotExists();

            if (queueClient.Exists())
            {
                foreach (var number in fibonacciNumbers)
                {
                    queueClient.SendMessage(number.ToString());
                    Console.WriteLine($"Sent: {number}");
                }
            }
        }

        static List<int> GenerateFibonacci(int limit)
        {
            List<int> fibonacci = new List<int> { 0, 1 };
            int nextNumber = 1;

            while (nextNumber <= limit)
            {
                fibonacci.Add(nextNumber);
                nextNumber = fibonacci[^1] + fibonacci[^2];
            }

            return fibonacci;
        }
    }
}
