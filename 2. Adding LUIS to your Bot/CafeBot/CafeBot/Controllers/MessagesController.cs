using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using CafeBot.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using CafeBot.Models;
using System.Collections.Generic;

namespace CafeBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public static IForm<Order> BuildOrderForm()
        {
            return new FormBuilder<Order>()
                    .Message("Welcome to the Contoso Cafe order bot!")
                    .Build();
        }

        internal static IDialog<string> MakeRootDialog()
        {
            return Chain.From(() => new OrderFood())
                .Switch(
                    new Case<Command, IDialog<string>>((cmd) =>
                    {
                        return (cmd.CommandType == CommandType.OrderFood);
                    }, (ctx, cmd) =>
                    {
                        ctx.PostAsync($"Welcome to the Contoso Cafe, are you ready to order?");
                        return Chain.ContinueWith(FormDialog.FromForm(BuildOrderForm),
                            async (c, r) =>
                            {
                                Order o = await r;
                                return Chain.ContinueWith<Delivery, string>(new DeliveryDialog(o),
                                    (async (ct, rs) =>
                                    {
                                        Delivery d = await rs;
                                        return Chain.From(() => new CompletedDialog(d));
                                    }));
                            });
                    }),
                    new Case<Command, IDialog<string>>((cmd) =>
                    {
                        return (cmd.CommandType == CommandType.BookTable);
                    }, (ctx, cmd) =>
                    {
                        string booking = $"Thank you for your booking";
                        if (cmd.Guests > 0)
                        {
                            booking += $" for {cmd.Guests} guests";
                        }
                        if (null != cmd.Time)
                        {
                            booking += $" at {cmd.Time}";
                        }
                        if (null != cmd.Date)
                        {
                            booking += $" {cmd.Date}";
                        }
                        return Chain.Return(booking);
                    }),
                    new DefaultCase<Command, IDialog<string>>((ctx, cmd) =>
                    {
                        return Chain.Return($"I can help you with ordering food or booking a table");
                    })
                )
                .Unwrap()
                .PostToUser()
                ;

        }
    

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, MakeRootDialog);
                //await Conversation.SendAsync(activity, ()=>new OrderFood());
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}