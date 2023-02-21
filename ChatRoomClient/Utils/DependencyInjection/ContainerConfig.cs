using Autofac;
using ChatRoomClient.DataAccessLayer.IONetwork;
using ChatRoomClient.DomainLayer;
using ChatRoomClient.Services;
using ChatRoomClient.Utils.Interfaces;

namespace ChatRoomClient.Utils.DependencyInjection
{
    public static class ContainerConfig
    {
        public static Autofac.IContainer Configure()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<ClientManager>().As<IClientManager>();
            builder.RegisterType<InputValidator>().As<IInputValidator>();
            builder.RegisterType<MessageFactory>().As<IMessageFactory>();
            builder.RegisterType<ObjectCreator>().As<IObjectCreator>();
            builder.RegisterType<SerializationProvider>().As<ISerializationProvider>();
            builder.RegisterType<ServerAction>().As<IServerAction>();
            builder.RegisterType<StreamProvider>().As<IStreamProvider>();
            builder.RegisterType<Transmitter>().As<ITransmitter>();
            builder.RegisterType<User>().As<IUser>();
            builder.RegisterType<UserChatRoomAssistant>().As<IUserChatRoomAssistant>().SingleInstance();

            return builder.Build();
        }


       
    }
}
