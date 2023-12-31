using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galacticos.Infrastructure.Configuration
{
    public class OpenAIConfig
    {
        public const string SectionName = "OpenAI settings";
        public string Key { get; set; }
    }
}