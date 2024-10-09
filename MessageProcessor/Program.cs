using Azure.Storage.Files.Shares;
using Azure.Storage.Queues;
using System.Text;

namespace MessageProcessor
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=hopehlongwanesa;AccountKey=F+VR0Ut91jSryb3DLGj7Kw8SCWCVjhLRA6TqtATqvWbtxtOLn48YmUM5efrPpqCtZNTgwue/tpIE+AStpkmt+Q==;EndpointSuffix=core.windows.net";
            string queueName = "fibonacci-queue";
            QueueClient queueClient = new QueueClient(connectionString, queueName);

            var fileContent = new StringBuilder();

            try
            {
                await queueClient.GetPropertiesAsync();
                Console.WriteLine("Queue exists. Processing messages...");

                while (true)
                {
                    var response = await queueClient.ReceiveMessageAsync();

                    if (response.Value != null)
                    {
                        fileContent.AppendLine(response.Value.MessageText);
                        await queueClient.DeleteMessageAsync(response.Value.MessageId, response.Value.PopReceipt);
                        Console.WriteLine($"Processed: {response.Value.MessageText}");
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Azure.RequestFailedException ex) when (ex.Status == 404)
            {
                Console.WriteLine("Queue does not exist.");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return;
            }

            string fileName = "Omphile-Hlongwane.txt";
            File.WriteAllText(fileName, fileContent.ToString());
            Console.WriteLine($"File {fileName} created.");

            await UploadToAzureFileStorage(fileName);
        }

        static async Task UploadToAzureFileStorage(string fileName)
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=hopehlongwanesa;AccountKey=F+VR0Ut91jSryb3DLGj7Kw8SCWCVjhLRA6TqtATqvWbtxtOLn48YmUM5efrPpqCtZNTgwue/tpIE+AStpkmt+Q==;EndpointSuffix=core.windows.net";
            string shareName = "fibonaccifiles";
            string directoryName = "output";

            ShareClient share = new ShareClient(connectionString, shareName);
            await share.CreateIfNotExistsAsync();
            ShareDirectoryClient directory = share.GetDirectoryClient(directoryName);
            await directory.CreateIfNotExistsAsync();

            ShareFileClient file = directory.GetFileClient(fileName);

            if (File.Exists(fileName))
            {
                long fileLength = new FileInfo(fileName).Length;

                if (fileLength > 0)
                {
                    using FileStream stream = File.OpenRead(fileName);
                    await file.CreateAsync(fileLength); 
                    await file.UploadAsync(stream); 
                    Console.WriteLine($"File uploaded to Azure File Storage: {fileName}");
                }
                else
                {
                    Console.WriteLine($"The file {fileName} is empty and cannot be uploaded.");
                }
            }
            else
            {
                Console.WriteLine($"The file {fileName} does not exist.");
            }
        }
    }
}
