// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.9.2

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using Microsoft.Recognizers.Text.DataTypes.TimexExpression;

using CoreBot2.CognitiveModels;
using CoreBot2.Bots;
using System.Runtime.CompilerServices;
using System.Drawing;
using CoreBot2.BL;
using Newtonsoft.Json;
using System.IO;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Windows.Markup;

namespace CoreBot2.Dialogs
{
    public class MainDialog : ComponentDialog
    {
        private readonly LuisAIRecognizer _luisRecognizer;
        protected readonly ILogger Logger;

        // Dependency injection uses this constructor to instantiate MainDialog
        public MainDialog(LuisAIRecognizer luisRecognizer, FilesDialog filesDialog, ILogger<MainDialog> logger)
            : base(nameof(MainDialog))
        {
            _luisRecognizer = luisRecognizer;
            Logger = logger;

            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(filesDialog);
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                //IntroStepAsync,
                ActStepAsync,
                //FinalStepAsync,
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> IntroStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (!_luisRecognizer.IsConfigured)
            {
                await stepContext.Context.SendActivityAsync(
                    MessageFactory.Text("NOTE: LUIS is not configured. To enable all capabilities, add 'LuisAppId', 'LuisAPIKey' and 'LuisAPIHostName' to the appsettings.json file.", inputHint: InputHints.IgnoringInput), cancellationToken);

                return await stepContext.NextAsync(null, cancellationToken);
            }

            // Use the text provided in FinalStepAsync or the default if it is the first time.
            var messageText = stepContext.Options?.ToString() ?? "Hey, what can I help you with today?\nSay something like \"What is the status of GDW Queue?\"";
            var promptMessage = MessageFactory.Text(messageText, messageText, InputHints.ExpectingInput);
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
        }

        private async Task<DialogTurnResult> ActStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            HttpMethods httpMethods;
            // Call LUIS (Note the TurnContext has the response to the prompt.)
            if (!_luisRecognizer.IsConfigured)
            {
                // LUIS is not configured, we just run the BookingDialog path with an empty BookingDetailsInstance.
                return await stepContext.BeginDialogAsync(nameof(MainDialog), new FileDetails(), cancellationToken);
            }

            // Call LUIS. (Note the TurnContext has the response to the prompt.)
            var luisResult = await _luisRecognizer.RecognizeAsync<FilesMain>(stepContext.Context, cancellationToken);

            //var input = turnContext.Activity.Text?.Trim();

            string textVal = stepContext.Context.Activity.Text;
            if (textVal == "queue")
            {
                //await SendSuggestedActionsAsync_QueueAsync(turnContext, cancellationToken);
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
                await stepContext.Context.SendActivityAsync(reply, cancellationToken);
            }
            else
            {
                switch (luisResult.TopIntent().intent)
                {
                    case FilesMain.Intent.Salutation:
                        var getSalutationText = "Hey, how are you doing today?";
                        //getSalutationText += "You have " + number + " of files pending transfer";
                        var getSalutation = MessageFactory.Text(getSalutationText, getSalutationText, InputHints.IgnoringInput);
                        await stepContext.Context.SendActivityAsync(getSalutation, cancellationToken);
                        break;

                    case FilesMain.Intent.Data_transfer:
                        var getData_TransferText = "TODO: get data transfer status here";
                        var getData_Transfer = MessageFactory.Text(getData_TransferText, getData_TransferText, InputHints.IgnoringInput);
                        await stepContext.Context.SendActivityAsync(getData_Transfer, cancellationToken);
                        break;

                    case FilesMain.Intent.GDW_Queue:
                        var getGWDQueueText = "TODO: get GDW queue status here";

                        var getGWDQueue = MessageFactory.Text(getGWDQueueText, getGWDQueueText, InputHints.IgnoringInput);
                        await stepContext.Context.SendActivityAsync(getGWDQueue, cancellationToken);
                        break;

                    //case FilesMain.Intent.Missing_Data:
                    //    var getMissingDataText = "TODO: get missing data status here";
                    //    var getMissingData = MessageFactory.Text(getMissingDataText, getMissingDataText, InputHints.IgnoringInput);
                    //    await stepContext.Context.SendActivityAsync(getMissingData, cancellationToken);
                    //    break;

                    case FilesMain.Intent.Pending_files:
                        var getPendingFilesText = "TODO: get missing data status here";
                        var getPendingFiles = MessageFactory.Text(getPendingFilesText, getPendingFilesText, InputHints.IgnoringInput);
                        await stepContext.Context.SendActivityAsync(getPendingFiles, cancellationToken);
                        break;

                    case FilesMain.Intent.Outbound_Directory:
                        var getOutboundDirText = "TODO: get missing data status here";
                        var getOutboundDir = MessageFactory.Text(getOutboundDirText, getOutboundDirText, InputHints.IgnoringInput);
                        await stepContext.Context.SendActivityAsync(getOutboundDir, cancellationToken);
                        break;

                    case FilesMain.Intent.Appreciation:
                        var getAppreciationText = "Nice.." + "\n";
                        getAppreciationText += "\n" + "What else can I do for you?";
                        var getAppreciation = MessageFactory.Text(getAppreciationText, getAppreciationText, InputHints.IgnoringInput);
                        await stepContext.Context.SendActivityAsync(getAppreciation, cancellationToken);
                        break;

                    case FilesMain.Intent.Misc:
                        var getMiscText = "Fine.." + "\n";
                        getMiscText += "\n" + "What else can I help with you today?";
                        var getMisc = MessageFactory.Text(getMiscText, getMiscText, InputHints.IgnoringInput);
                        await stepContext.Context.SendActivityAsync(getMisc, cancellationToken);
                        break;

                    case FilesMain.Intent.Nothing:
                        var getNothingText = "Great then, good bye..";
                        var getNothing = MessageFactory.Text(getNothingText, getNothingText, InputHints.IgnoringInput);
                        await stepContext.Context.SendActivityAsync(getNothing, cancellationToken);
                        break;

                    case FilesMain.Intent.Bot_Definition:
                        string strName = "I'm DMaaS - Data Movement as a Service. I'm your virtual assistant." + "\n";
                        strName += "\n" + " I'll provide information and status regarding the data, files & transfers.";
                        var getBotDefinitionText = strName;
                        var getBotDefinition = MessageFactory.Text(getBotDefinitionText, getBotDefinitionText, InputHints.IgnoringInput);
                        await stepContext.Context.SendActivityAsync(getBotDefinition, cancellationToken);
                        break;

                    case FilesMain.Intent.Bot_name:
                        var getBotNameText = "I'm a DMaaS - Data Movement as a Service.";
                        var getBotName = MessageFactory.Text(getBotNameText, getBotNameText, InputHints.IgnoringInput);
                        await stepContext.Context.SendActivityAsync(getBotName, cancellationToken);
                        break;

                    case FilesMain.Intent.BotHelp:
                        string strHelp = "I'll provide information and status regarding the data, files & transfers.";
                        var getBotHelpText = strHelp;
                        var getBotHelp = MessageFactory.Text(getBotHelpText, getBotHelpText, InputHints.IgnoringInput);
                        await stepContext.Context.SendActivityAsync(getBotHelp, cancellationToken);
                        break;

                    case FilesMain.Intent.None:
                        string strNoneVal = "I didn't get that..";
                        var getNoneValText = strNoneVal;
                        var getNone = MessageFactory.Text(getNoneValText, getNoneValText, InputHints.IgnoringInput);
                        await stepContext.Context.SendActivityAsync(getNone, cancellationToken);
                        break;

                    case FilesMain.Intent.Bad_Mood:
                        string strBadMood = "I'm sorry to hear that..";
                        var getBadMoodText = strBadMood;
                        var getBadMood = MessageFactory.Text(getBadMoodText, getBadMoodText, InputHints.IgnoringInput);
                        await stepContext.Context.SendActivityAsync(getBadMood, cancellationToken);
                        break;

                    case FilesMain.Intent.Good_Mood:
                        string strGoodMood = "That's really nice.." + "\n";
                        strGoodMood += "\n" + "What else can I do?";
                        var getGoodMoodText = strGoodMood;
                        var getGoodMood = MessageFactory.Text(getGoodMoodText, getGoodMoodText, InputHints.IgnoringInput);
                        await stepContext.Context.SendActivityAsync(getGoodMood, cancellationToken);
                        break;

                    case FilesMain.Intent.Market_Health_Status:
                        httpMethods = new HttpMethods();
                        string strMarketHealth = await httpMethods.getMarketHealthReport("Shreyas");

                        string strMarketStatus = "Here you go.." + "\n";
                        strMarketStatus += "\n";
                        strMarketStatus += strMarketHealth;
                        var getMarketStatusText = strMarketStatus;
                        var getMarketStatusVal = MessageFactory.Text(getMarketStatusText, getMarketStatusText, InputHints.IgnoringInput);
                        await stepContext.Context.SendActivityAsync(getMarketStatusVal, cancellationToken);
                        break;

                    case FilesMain.Intent.File_Status:
                        httpMethods = new HttpMethods();
                        string strFileStatusResponse = await httpMethods.getFileStatus("Shreyas");
                        string[] strFileResponse = strFileStatusResponse.Replace("\"", "").Replace("[", "").Replace("]", "").Split(",");

                        string strFileStatus = "Here is the file status.." + "\n";
                        strFileStatus += "\n";
                        strFileStatus += "Files received : " + strFileResponse[0] + "\n";
                        strFileStatus += "\n";
                        strFileStatus += "Date received : " + strFileResponse[1] + "\n";
                        strFileStatus += "\n";
                        strFileStatus += "File count : " + strFileResponse[2] + "\n";
                        strFileStatus += "\n";
                        strFileStatus += "City : " + strFileResponse[3];

                        var getFileStatusText = strFileStatus;
                        var getFileStatusVal = MessageFactory.Text(getFileStatusText, getFileStatusText, InputHints.IgnoringInput);
                        await stepContext.Context.SendActivityAsync(getFileStatusVal, cancellationToken);
                        break;

                    case FilesMain.Intent.McD_Abbreviations:
                        string strMCD = stepContext.Context.Activity.Text.ToUpper();
                        string[] strValues = strMCD.Split(" ");
                        int valuesCount = strValues.Length;
                        string strAbbreviation = string.Empty;
                        string strAbbVal = string.Empty;
                        string text = File.ReadAllText(@"C:\Users\shrey\source\repos\CoreBot2\CoreBot2\CognitiveModels\McD_Abbreviations.json");

                        if (valuesCount == 1)
                        {
                            strAbbVal = strValues[0];
                        }
                        else if (valuesCount == 2)
                        {
                            strAbbVal = strValues[1];
                        }
                        else if (valuesCount == 3)
                        {
                            strAbbVal = strValues[2];
                        }
                        else if (valuesCount == 4)
                        {
                            strAbbVal = strValues[3];
                        }

                        var root = JToken.Parse(text);
                        var properties = root.SelectTokens("").SelectMany(t => t.Children().OfType<JProperty>().Select(p => p.Name)).ToArray();

                        if (properties.Contains(strAbbVal))
                        {
                            var data = (JObject)JsonConvert.DeserializeObject(text);
                            string valueVal = data[strAbbVal].Value<string>();
                            strAbbreviation = "Abbreviation of " + strAbbVal + " is " + valueVal;
                        }
                        else
                        {
                            strAbbreviation = "We did not get you..";
                        }

                        var getAbbreviationText = strAbbreviation;
                        var getAbbreviation = MessageFactory.Text(getAbbreviationText, getAbbreviationText, InputHints.IgnoringInput);
                        await stepContext.Context.SendActivityAsync(getAbbreviation, cancellationToken);
                        break;

                    default:
                        // Catch all for unhandled intents
                        var didntUnderstandMessageText = $"Sorry, I didn't get that. Please try asking in a different way.";
                        var didntUnderstandMessage = MessageFactory.Text(didntUnderstandMessageText, didntUnderstandMessageText, InputHints.IgnoringInput);
                        await stepContext.Context.SendActivityAsync(didntUnderstandMessage, cancellationToken);
                        break;
                }
            }
            return await stepContext.NextAsync(null, cancellationToken);
        }

        
        // Shows a warning if the requested From or To cities are recognized as entities but they are not in the Airport entity list.
        // In some cases LUIS will recognize the From and To composite entities as a valid cities but the From and To Airport values
        // will be empty if those entity values can't be mapped to a canonical item in the Airport.
        private static async Task ShowWarningForUnsupportedCities(ITurnContext context, FlightBooking luisResult, CancellationToken cancellationToken)
        {
            var unsupportedCities = new List<string>();

            var fromEntities = luisResult.FromEntities;
            if (!string.IsNullOrEmpty(fromEntities.From) && string.IsNullOrEmpty(fromEntities.Airport))
            {
                unsupportedCities.Add(fromEntities.From);
            }

            var toEntities = luisResult.ToEntities;
            if (!string.IsNullOrEmpty(toEntities.To) && string.IsNullOrEmpty(toEntities.Airport))
            {
                unsupportedCities.Add(toEntities.To);
            }

            if (unsupportedCities.Any())
            {
                var messageText = $"Sorry but the following airports are not supported: {string.Join(',', unsupportedCities)}";
                var message = MessageFactory.Text(messageText, messageText, InputHints.IgnoringInput);
                await context.SendActivityAsync(message, cancellationToken);
            }
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // If the child dialog ("BookingDialog") was cancelled, the user failed to confirm or if the intent wasn't BookFlight
            // the Result here will be null.
            if (stepContext.Result is FileDetails result)
            {
                // Now we have all the booking details call the booking service.

                // If the call to the booking service was successful tell the user.

                var timeProperty = new TimexProperty(result.FileType);
                var travelDateMsg = timeProperty.ToNaturalLanguage(DateTime.Now);
                var messageText = $"I have you booked to {result.Users} from {result.FileName} on {travelDateMsg}";
                var message = MessageFactory.Text(messageText, messageText, InputHints.IgnoringInput);
                await stepContext.Context.SendActivityAsync(message, cancellationToken);
            }

            // Restart the main dialog with a different message the second time around
            var promptMessage = ""; // "What else can I do for you?";
            return await stepContext.ReplaceDialogAsync(InitialDialogId, promptMessage, cancellationToken);
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

        private static string ProcessInput(string text)
        {
            const string queueText = "is you selection.";
            switch (text)
            {
                case "gdw queue 1":
                    {
                        return $"GDW Queue 1 {queueText}";
                    }

                case "gdw queue 2":
                    {
                        return $"GDW Queue 2 {queueText}";
                    }

                case "gdw queue 3":
                    {
                        return $"GDW Queue 3 {queueText}";
                    }

                default:
                    {
                        return "Please select a queue from the suggested action choices";
                    }
            }
        }
    }
}
