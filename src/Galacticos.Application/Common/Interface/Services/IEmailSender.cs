using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galacticos.Domain.Entities;

namespace Galacticos.Application.Common.Interface.Services
{
    public interface IEmailSender
    {
        public Task SendEmail(Email email);

    }
}