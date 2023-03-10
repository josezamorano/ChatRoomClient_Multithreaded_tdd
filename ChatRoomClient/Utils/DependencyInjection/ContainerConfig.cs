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
            builder.RegisterType<ObjectCreator>().As<IObjectCreator>();
            builder.RegisterType<SerializationProvider>().As<ISerializationProvider>();
            builder.RegisterType<ServerAction>().As<IServerAction>().SingleInstance();
            builder.RegisterType<StreamProvider>().As<IStreamProvider>();
            builder.RegisterType<TcpClientProvider>().As<ITcpClientProvider>();
            builder.RegisterType<Transmitter>().As<ITransmitter>();
            builder.RegisterType<User>().As<IUser>().SingleInstance();
            builder.RegisterType<UserChatRoomAssistant>().As<IUserChatRoomAssistant>().SingleInstance();

            return builder.Build();
        }


       
    }
}
