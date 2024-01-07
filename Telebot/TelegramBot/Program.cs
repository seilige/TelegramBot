using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot;



using CancellationTokenSource cts = new();
DTO.botClient = new TelegramBotClient(token: "6750885550:AAF-OPm3CfxXWSpt4KhZVHaNvCkGtMnQJDg");

ReceiverOptions receiverOptions = new()
{
    AllowedUpdates = Array.Empty<UpdateType>()
};

DTO.botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await DTO.botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

cts.Cancel();



static async Task HandleCallBackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
{
    await logicTelegram.editMessage(callbackQuery);
    return;
}

static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if (update.Type == UpdateType.CallbackQuery)
    {
        await HandleCallBackQuery(botClient, update.CallbackQuery);
        return;
    }

    if (update.Message == null)
    {
        return;
    }

            if (update.Message.Text == "/start")
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Choose commands: /inline | /keyboard");
            }

            if (update.Message.Text == "/keyboard")
            {
                ReplyKeyboardMarkup keyBoard = new
                (
                    new[]
                    {
                        new KeyboardButton[]{"Choose user", "/inline"}
                    }
                )
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Choose: ", replyMarkup: keyBoard);
                return;
            }

            if (update.Message.Text == "/inline")
            {
                DTO.mainIndex = 0;
                DTO.emojiList.Clear();
                DTO.diag1.Clear();
                DTO.diag2.Clear();
                DTO.vert1.Clear();
                DTO.vert2.Clear();

                for (int i = 1; i <= Math.Pow(DTO.countFeilds, 2); i++)
                {
                    DTO.emojiList.Add($"{i}", "none");
                }

                var a = helpLogic.Buttons(DTO.countFeilds, DTO.emojiList);
                var message = await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Choose: ", replyMarkup: a);
                DTO.lastIndexOfMessage = message.MessageId;
            }
        }



        static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

