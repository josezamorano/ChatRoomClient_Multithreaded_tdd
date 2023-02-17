using Autofac;
using ChatRoomClient.Utils.DependencyInjection;
using ChatRoomClient.Utils.Interfaces;

namespace ChatRoomClient
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Autofac.IContainer  container = ContainerConfig.Configure();
            
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            IClientManager _clientManager = container.Resolve<IClientManager>();
            IInputValidator _inputValidator = container.Resolve<IInputValidator>();
            Application.Run(new PresentationLayer(_clientManager, _inputValidator));
        }
    }
}