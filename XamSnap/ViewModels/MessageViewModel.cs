﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamSnap
{
    public class MessageViewModel : BaseViewModel
    {
        private ILocationService locationService = ServiceContainer.Resolve<ILocationService>();

        public Conversation[] Conversations { get; private set; }

        public Conversation Conversation { get; set; }

        public Message[] Messages { get; private set; }

        public string Text { get; set; }

        public string Image { get; set; }

        public async Task GetConversations()
        {
            if (settings.User == null)
                throw new Exception("Not logged in.");

            IsBusy = true;
            try
            {
                Conversations = await service.GetConversations(settings.User.Name);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task GetMessages()
        {
            if (Conversation == null)
                throw new Exception("No conversation.");

            IsBusy = true;
            try
            {
                Messages = await service.GetMessages(Conversation.Id);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task SendMessage()
        {
            if (settings.User == null)
                throw new Exception("Not logged in.");

            if (Conversation == null)
                throw new Exception("No conversation.");

            if (string.IsNullOrEmpty(Text) && string.IsNullOrEmpty(Image))
                throw new Exception("Message is blank.");

            IsBusy = true;
            try
            {
                var location = await locationService.GetCurrentLocation();

                var message = await service.SendMessage(new Message
                {
                    UserName = settings.User.Name,
                    Conversation = Conversation.Id,
                    Text = Text,
                    Image = Image,
                    Location = location,
                });

                //Clear our variables
                Text = 
                    Image = null;

                //Update our local list of messages
                var messages = new List<Message>();
                if (Messages != null)
                    messages.AddRange(Messages);
                messages.Add(message);

                Messages = messages.ToArray();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

