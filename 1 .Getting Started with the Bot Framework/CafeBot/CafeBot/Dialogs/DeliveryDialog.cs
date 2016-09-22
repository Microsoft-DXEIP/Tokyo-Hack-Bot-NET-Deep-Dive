using CafeBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace CafeBot.Dialogs
{
    [Serializable]
    public class DeliveryDialog : IDialog<Delivery>
    {
        Delivery delivery;

        public DeliveryDialog(Order o)
        {
            delivery = new Delivery() { Order = o };
        }

        public async Task StartAsync(IDialogContext context)
        {
            if (delivery.Order.DeliveryOption == DeliveryOption.Delivery)
            {
                if (null == delivery.Address)
                {
                    string response = "Please tell me the address where would like this order delivered";

                    await context.PostAsync(response);

                    context.Wait(MessageReceivedAsync);
                }
                else
                {
                    context.Done(delivery);
                }
            }
            else
            {
                context.Done(delivery);
            }
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            IMessageActivity activity = await argument;

            if (null != activity)
            {
                delivery.Address = activity.Text;
                context.Done(delivery);
            }
        }

    }
}