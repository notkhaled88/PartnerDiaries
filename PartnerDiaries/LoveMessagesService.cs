namespace PartnerDiaries
{
    public class Message: JsonElement
    {
        public string Partner { get; set; }
        public string MessageText { get; set; }
    
        public Message() { }
        public Message(string partner, string messageText)
        {
            const int maxLength = 20;
            this.Partner = partner;
            this.MessageText = messageText;
            if (this.MessageText.Count() > maxLength)
            {
                this.SearchParameter = messageText.Substring(0, maxLength);
            }
            else
            {
                this.SearchParameter = messageText;
            }
        }

    }

    public class MessageList : JsonElementsList<Message>
    {
        public MessageList() { }
    }


    public class MessageService : JsonService<MessageList, Message>
    {
        public MessageService(string _path) : base(_path) { }
        public string getRandomMessage(string partner) 
        {
            Random rand = new Random();
            List<Message> PartnerMessages = this.jsonElements.elements.Where(x => x.Partner != partner).ToList();
            if (PartnerMessages.Count> 0)
            {
                return PartnerMessages[rand.Next(0, PartnerMessages.Count)].MessageText;
            }
            else
            {
                return "بحبك";
            }
        }
    }
}


