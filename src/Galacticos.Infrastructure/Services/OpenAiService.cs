using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galacticos.Application.Services.OpenAI;
using Galacticos.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using OpenAI_API.Models;
using OpenAI_API.Completions; 
using OpenAI_API.Models;

namespace Galacticos.Infrastructure.Services
{
    public class OpenAiService : IOpenAIService
    {
        private readonly OpenAIConfig _openAIConfig;

        public OpenAiService(IOptions<OpenAIConfig> config)
        {
            _openAIConfig = config.Value;
        }
        public async Task<List<string>> GenerateTags(string text, int numTags = 5)
        {
            var api = new OpenAI_API.OpenAIAPI(_openAIConfig.Key);

            var chat = api.Chat.CreateConversation();
            chat.AppendSystemMessage("you are a language expert and you are helping me to generate tags for this post based on the text I have provided you. Please provide me with 5 tags that you think are relevant to this post. You can use the text I have provided you to help you generate the tags. When you return the tags, please separate them with a comma. Thank you!");

            chat.AppendUserInput(text);

            var response = await chat.GetResponseFromChatbotAsync();

            var tags = response.Split(",").ToList();

            return tags;
        }

        public async Task<bool> ContentModeration(string text)
        {
            var api = new OpenAI_API.OpenAIAPI(_openAIConfig.Key);

            var chat = api.Chat.CreateConversation();
            chat.AppendSystemMessage("You are a language expert, and you are helping me to validate this post based on the following common community guidelines and policies:\n\n"
    + "No hate speech, discrimination, or harassment based on race, ethnicity, religion, gender, etc.\n"
    + "No promotion of violence or threats of harm.\n"
    + "No explicit sexual content, nudity, or pornography.\n"
    + "No bullying, cyberbullying, or malicious targeting.\n"
    + "No impersonation, misinformation, or spreading false information.\n"
    + "No sharing of copyrighted content without proper authorization.\n"
    + "No spam, scams, or fraudulent activities.\n"
    + "No promotion or glorification of illegal activities.\n"
    + "No sharing of graphic or disturbing content, especially involving violence.\n"
    + "No exploitation of minors or child abuse content.\n"
    + "No sharing of private or sensitive information without consent.\n"
    + "Please provide me with a 'true' or 'false' answer to the following question: Is this post appropriate for the website according to the guidelines and policies? You can use the text I have provided you to help you answer the question. return True if the post is appropriate and False if the post is not appropriate. you have to return only one word true or false and nothing else. Thank you!");

            chat.AppendUserInput(text);

            var response = await chat.GetResponseFromChatbotAsync();
            Console.WriteLine(response);
            return response.ToLower() == "true";
        }
    }
}