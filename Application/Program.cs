using MailKit.Net.Smtp;
using MimeKit;
using Scriban;

internal class Program
{
    /// <summary>
    /// Solves the application "Bubblicious Number" problem and sends the submission email.
    /// </summary>
    public static async Task Main(
        string myName,
        string myAddress,
        string smtpServer,
        int port,
        string authUser,
        string authPass,
        string repo,
        string gitHubUrl)
    {
        // Solve the problem
        // Note: the first three "Bubblicious Number"s are given by the prompt
        int count = 3; // 11, 43, 59
        for (int i = 60; i < 100_000; i++)
        {
            if (Bubblicious.IsBubblicious(i))
            {
                count++;
            }
        }

        // Create the email message
        var text = await File.ReadAllTextAsync("Application.txt").ConfigureAwait(false);
        var template = Template.Parse(text);
        text = template.Render(new { Name = myName, Repository = repo, Githuburl = gitHubUrl, Answer = count});

        // Send the application email
        var msg = new MimeMessage();
        var builder = new BodyBuilder();
        builder.HtmlBody = text;
        builder.Attachments.Add("Application.pdf");
        msg.Body = builder.ToMessageBody();
        msg.To.Add(new MailboxAddress("Nectarine Team",$"{count}@hellonectarine.com"));
        msg.Subject = "Nectarine Application";
        msg.From.Add(new MailboxAddress(myName, myAddress));
        using var client = new SmtpClient();
        await client.ConnectAsync(smtpServer, port, false, default).ConfigureAwait(false);
        await client.AuthenticateAsync(authUser, authPass, default).ConfigureAwait(false);
        await client.SendAsync(msg, default).ConfigureAwait(false);
        await client.DisconnectAsync(true, default).ConfigureAwait(false);
    }
}
