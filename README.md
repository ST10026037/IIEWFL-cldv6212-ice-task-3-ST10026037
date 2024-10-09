# Fibonacci Sequence Console Application with Azure Integration

## Objective
Develop a console application that generates the Fibonacci sequence up to 233, stores the sequence in Azure Queue Storage, processes the queued messages, and saves the results to Azure File Storage.

## Applications

### Application 1: Fibonacci Sequence Generator
- Create a simple console application that generates the Fibonacci sequence up to 233.
- Store each Fibonacci number as a separate message in Azure Queue Storage.

### Application 2: Message Processor
- Develop a second console application that retrieves and processes the messages from Azure Queue Storage.
- Write the processed Fibonacci sequence numbers into a single text file named `<YourName>-<YourSurname>.txt`.
- Save this text file to Azure File Storage.

## Instructions
1. Clone the repository to your local machine.
2. Open the solution in your preferred IDE (e.g., Visual Studio).
3. Build the solution to ensure all dependencies are installed.
4. Configure the Azure connection strings for Queue and File Storage in the application settings.
5. Run the Fibonacci Sequence Generator application to generate the sequence and store it in Azure Queue Storage.
6. Execute the Message Processor application to retrieve the messages and save the results to Azure File Storage.

## Notes
- Ensure that your Azure account is set up and you have the necessary permissions to access Queue and File Storage.
- Make sure to handle exceptions and errors gracefully in your applications.
