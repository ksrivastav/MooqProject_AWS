using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Amazon;
using System;
using System.Collections.Generic;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using System.Xml.Linq;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace NotificationLambdaSQS;

public class Function
{
    /// <summary>
    /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
    /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
    /// region the Lambda function is executed in.
    /// </summary>
    public Function()
    {

    }


    /// <summary>
    /// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used 
    /// to respond to SQS messages.
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
    {
        foreach(var message in evnt.Records)
        {
            await ProcessMessageAsync(message, context);
        }
    }

    private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
    {
        context.Logger.LogInformation($"Processed message {message.Body}");

        string senderAddress = "kartikayamsrivastav@outlook.com";
        string receiverAddress  = "kart.mcr@gmail.com";
        string subject = "Order Confirmation";
        string textBody = "Amazon SES Test (.NET)\r\n"
                                        + "This email was sent through Amazon SES "
                                        + "using the AWS SDK for .NET.";


        string htmlBody = @"<html>
<head></head>
<body>
  <h1>Amazon SES Test (AWS SDK for .NET)</h1>
  <p>This email was sent with
    <a href='https://aws.amazon.com/ses/'>Amazon SES</a> using the
    <a href='https://aws.amazon.com/sdk-for-net/'>
      AWS SDK for .NET</a>.</p>"
+ message.ToString()
+ message.Body.ToString()
+@"</body>
</html>";


        var client = new AmazonSimpleEmailServiceClient(RegionEndpoint.USEast1);
        
            var sendRequest = new SendEmailRequest
            {
                Source = senderAddress,
                Destination = new Destination
                {
                    ToAddresses =
                        new List<string> { receiverAddress }
                },
                Message = new Message
                {
                    Subject = new Content(subject),
                    Body = new Body
                    {
                        Html = new Content
                        {
                            Charset = "UTF-8",
                            Data = htmlBody
                        },
                        Text = new Content
                        {
                            Charset = "UTF-8",
                            Data = textBody
                        }
                    }
                }
                // If you are not using a configuration set, comment
                // or remove the following line 
               // ConfigurationSetName = configSet
            };
            try
            {
                Console.WriteLine("Sending email using Amazon SES...");
                var response = await client.SendEmailAsync(sendRequest);
                Console.WriteLine("The email was sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("The email was not sent.");
                Console.WriteLine("Error message: " + ex.Message);

            }
        

       

        // TODO: Do interesting work based on the new message
        await Task.CompletedTask;
    }
}