// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.9.2

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CoreBot2.BL;

namespace CoreBot2.Bots
{
    public class DialogAndWelcomeBot<T> : DialogBot<T>
        where T : Dialog
    {
        HttpMethods httpMethods;
        public DialogAndWelcomeBot(ConversationState conversationState, UserState userState, T dialog, ILogger<DialogBot<T>> logger)
            : base(conversationState, userState, dialog, logger)
        {
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, 
            ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            httpMethods = new HttpMethods();
            string strUserInfo = await httpMethods.getUserInfo(userName);
            string[] strSplitResponse = strUserInfo.Replace("[", "").Replace("]", "").Replace("\"", "").Split(",");
            string strName = strSplitResponse[0] + " " + strSplitResponse[1];
            string strBU = strSplitResponse[2];
            string strRoleType = strSplitResponse[3];

            var welcomeText = "Hello " + strName + " , welcome to DMaaS Virtual Agent." + "\n";
            welcomeText += "\n" + "Your role is " + strRoleType + ".\n";
            welcomeText += "\n" + "How can I assist you today?";
            
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }

        private static string ProcessInput(string text)
        {
            const string colorText = "is the best color, I agree.";
            switch (text)
            {
                case "red":
                    {
                        return $"Red {colorText}";
                    }

                case "yellow":
                    {
                        return $"Yellow {colorText}";
                    }

                case "blue":
                    {
                        return $"Blue {colorText}";
                    }

                default:
                    {
                        return "Please select a color from the suggested action choices";
                    }
            }
        }

        private static async Task SendSuggestedActionsAsync_QueueAsync(ITurnContext turnContext,
            CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text("Please select from the below queue..");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = new List<CardAction>()
                    {
                        new CardAction() { Title = "GDW Queue 1", Type = ActionTypes.ImBack, Value = "GDW Queue 1" },
                        new CardAction() { Title = "GDW Queue 2", Type = ActionTypes.ImBack, Value = "GDW Queue 2" },
                        new CardAction() { Title = "GDW Queue 3", Type = ActionTypes.ImBack, Value = "GDW Queue 3" },
                    },
            };
            await turnContext.SendActivityAsync(null, cancellationToken);
        }
    }
}

