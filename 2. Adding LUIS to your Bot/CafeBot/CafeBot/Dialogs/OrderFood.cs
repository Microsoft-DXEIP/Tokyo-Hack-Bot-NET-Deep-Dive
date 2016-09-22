using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using CafeBot.Models;

namespace CafeBot.Dialogs
{
    [LuisModel("cd2e0839-5ab8-4b9a-a18b-94067381a0ec", "c130f20fea484b28bd0c3220ca2b0a11")]
    [Serializable]
    public class OrderFood : LuisDialog<Command>
    {
        public const string Entity_datetimeDate = "builtin.datetime.date";
        public const string Entity_datetimeTime = "builtin.datetime.time";
        public const string Entity_number = "builtin.number";

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry I did not understand: " + string.Join(", ", result.Intents.Select(i => i.Intent));
            await context.PostAsync(message);
            Command c = new Command() { CommandType = CommandType.None };
            context.Done(c);
        }

        [LuisIntent("OrderFood")]
        public async Task Order(IDialogContext context, LuisResult result)
        {
            Command cmd = new Command() { CommandType = CommandType.OrderFood };
            EntityRecommendation time;
            EntityRecommendation date;

            if (result.TryFindEntity(Entity_datetimeTime, out time))
            {
                cmd.Time = time.Entity;
            }

            if (result.TryFindEntity(Entity_datetimeDate, out date))
            {
                cmd.Date = date.Entity;
            }

            context.Done(cmd);
        }

        [LuisIntent("BookTable")]
        public async Task Booking(IDialogContext context, LuisResult result)
        {
            Command cmd = new Command() { CommandType = CommandType.BookTable };

            EntityRecommendation date;
            EntityRecommendation time;
            EntityRecommendation guests;

            if (result.TryFindEntity(Entity_datetimeTime, out time))
            {
                cmd.Time = time.Entity;
            }

            if (result.TryFindEntity(Entity_datetimeDate, out date))
            {
                cmd.Date = date.Entity;
            }

            if (result.TryFindEntity(Entity_number, out guests))
            {
                cmd.Guests = int.Parse(guests.Entity);
            }

            context.Done(cmd);
        }

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            await context.PostAsync("I am the Contoso Cafe bot, I can help you order food for pick up or delivery.");

            context.Wait(MessageReceivedAsync);
        }
    }
}