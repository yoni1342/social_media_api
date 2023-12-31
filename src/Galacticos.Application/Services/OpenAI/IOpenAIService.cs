using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galacticos.Application.Services.OpenAI
{
    public interface IOpenAIService
    {   
        Task<List<string>> GenerateTags(string text, int numTags);
        Task<bool> ContentModeration(string text);
    }
}