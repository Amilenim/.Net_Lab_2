using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using static System.Net.WebRequestMethods;

namespace Net_Lab_2
{
    public static class BotActions {

        public static async Task GetDuckImageOrGif(ITelegramBotClient botClient, long chatId, CancellationToken token)
        {
            var root = "https://random-d.uk/api/random";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(root);

                        if (response.IsSuccessStatusCode)
                        {
                            string jsonResult = await response.Content.ReadAsStringAsync();

                            var duckData = JsonConvert.DeserializeObject<dynamic>(jsonResult);

                            string imageUrl = duckData.url;
                            Console.WriteLine($"Завантажена свiтлина: {imageUrl}");
                            if (imageUrl != null)
                            {
                                await botClient.SendPhotoAsync(chatId,
                                    photo: InputFile.FromUri(imageUrl),
                                    caption: $"<b>DuckDuck!</b>\n<a href = '{imageUrl}'>Джерело</a>",
                                    parseMode: ParseMode.Html,
                                    cancellationToken: token);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Ошибка при загрузке изображения: {response.StatusCode}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                }
            }
        }
    }
}

