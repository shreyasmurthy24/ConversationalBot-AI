# ConversationalBot-AI
This is a conversational bot that interacts with the users and gives responses.

This Bot implements Natural Language Processing through LUIS. All the Entities, Intents and Utterances are created in Microsoft LUIS.AI portal.
Technologies used - Microsoft .Net Core 3.1 framework with all the latest class libraries, C#, SQL Server.

The conversations can be handled using the Bot Emulator.

All the Entities, Intents and Utterances are exported as a JSON file and using a library named LUISGen, the JSON can be converted into C# classes. Follow the below to 
generate the C# classes for the LUIS Json file.

1. Clone the LuisGen (Already in my local - LUIS installed). Make sure you have .Net 2.1 SDK installed. Open "tools.sln" inside packages folder 
   with Visual studio and build it. Now go to "Tools" > "Nuget Package Manager" > "Package Manager Console". It will open at the bottom, type and enter 
   "dotnet tool install -g LUISGen" It will install LUISGen which you will need to use in step 3
   
2. To get the JSON file it's somewhat hidden. Go to the Luis.ai. There are two ways, either
	○ Go To "My Apps" at the top, select tickbox for the app you need then "Export" then "Export as JSON".
	○ other way is "Manage" Tab next to Build. Then "Version", then again you need to select the checkbox (for a version) then export > export as JSON.
  
3. Go to the location of the JSON file. Hold Shift and right click, select "Open PowerShell Here". 
   Now type "LUISGen {Name-of-json-file}.json -cs {Desired-Name-for-CS-file}" without quotes and replace braces with the json file name 
   and the filename you want for the C# Model file.
