using CafeBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace CafeBot.Dialogs
{
    [Serializable]
    public class CompletedDialog : IDialog<Delivery>
    {
        private Delivery delivery;

        public CompletedDialog(Delivery d)
        {
            delivery = d;
        }

        public async Task StartAsync(IDialogContext context)
        {
            string response = $"Thank you for your order";
            await context.PostAsync(response);
            if (delivery.Order.DeliveryOption == DeliveryOption.Pickup)
            {
                response = $"It will be ready for collection in 20 minutes";
                await context.PostAsync(response);
            }
            else
            {
                response = $"It will be delivered to {delivery.Address} within the next 45 minutes";
                await context.PostAsync(response);
            }
            context.Done(delivery);
        }
    }
}