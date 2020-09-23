using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBot2.Bots
{
    public class LuisDialogs
    {
        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            var response = context.MakeMessage();
            response.Text = "Welcome to Mixtape! How can I help?";

            response.InputHint = InputHints.ExpectingInput;
            await context.PostAsync(response);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Mixtape")]
        public async Task MixtapeIntent(IDialogContext context, LuisResult result)
        {
            var response = context.MakeMessage();
            response.Text = "Welcome to Mixtape! Let's find a song to play.";

            response.InputHint = InputHints.ExpectingInput;
            await context.PostAsync(response);
            context.Wait(MessageReceived);
        }

        [LuisIntent("PlaySong")]
        public async Task PlaySongIntent(IDialogContext context, LuisResult result)
        {
            var response = context.MakeMessage();
            response.Text = "Here's a song for you.";

            response.InputHint = InputHints.ExpectingInput;
            await context.PostAsync(response);
            context.Wait(MessageReceived);
        }

        [LuisIntent("SaveSong")]
        public async Task SaveSongIntent(IDialogContext context, LuisResult result)
        {
            var response = context.MakeMessage();
            response.Text = "Sign in to save the song to a mixtape.";

            response.InputHint = InputHints.AcceptingInput;
            await context.PostAsync(response);
            context.Wait(MessageReceived);
        }
    }
}
