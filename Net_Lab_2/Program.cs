using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using Net_Lab_2;
class Program
{
    static void Main(string[] args)
    {
        var client = new TelegramBotClient("6706397450:AAHX0OHzhpgSNsIF6UhG1t58b2YGjYJaNNg");
        client.StartReceiving(Update, Error);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"> Бота увiмкнено! ({DateTime.UtcNow} UTC)");

            while (true)
            {
                string exitKeyword = "exit";

                string input = Console.ReadLine();

                if (input.ToLower() == exitKeyword)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"> Бота вимкнено! ({DateTime.UtcNow} UTC)");
                    break;
                }
                else
                {
                    Console.ReadLine();
                }
            }
        }


    static async Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
    {

    }

    static async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
       
        var message = update.Message;

        if (message != null)
        {
            switch (message.Text) 
            {
                case "Give duck!":
                    Console.WriteLine($"({message.Date.ToLocalTime()}) {message.Chat.Id}, {message.Chat.Username}: {message.Text}");
                    await BotActions.GetDuckImageOrGif(botClient, message.Chat.Id, token);
                    break;
                default: botClient.SendTextMessageAsync(message.Chat.Id, "No duck :(");
                    break;
            }
        }
    }
}