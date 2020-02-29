using Domain.Entities;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class ConversationInfoRepository:Repository<ConversationInfo>, IConversationInfoRepository
    {
        public ConversationInfoRepository(MessengerContext context):base(context)
        {

        }
    }
}
