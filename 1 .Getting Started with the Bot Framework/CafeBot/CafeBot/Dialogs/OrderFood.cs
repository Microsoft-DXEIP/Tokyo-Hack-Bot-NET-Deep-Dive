using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace CafeBot.Dialogs
{
    using Microsoft.Bot.Builder.Dialogs;

    [Serializable]
    public class OrderFood : IDialog<object>
    {
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