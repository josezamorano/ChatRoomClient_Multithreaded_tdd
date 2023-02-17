using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Utils.Enumerations;
using ChatRoomClient.Utils.Interfaces;

namespace ChatRoomClient.DomainLayer
{
    public class MessageFactory : IMessageFactory
    {
        ISerializationProvider _serializationProvider;
        public MessageFactory(ISerializationProvider serializationProvider)
        {
            _serializationProvider = serializationProvider;
        }

        public string CreateMessageByActionType(Payload payload )
        {
 
            switch (payload.MessageActionType)
            {
                case MessageActionType.ClientConnectToServer:
                    string serializedPayload =_serializationProvider.SerializeObject(payload);
                    return serializedPayload;
                    


            }
            return string.Empty;
        }
    }
}
