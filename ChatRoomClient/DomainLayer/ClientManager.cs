using ChatRoomClient.Utils.Interfaces;
using System.Net.Sockets;
using System.Net;
using ChatRoomClient.DomainLayer.Models;
using ChatRoomClient.Services;
using ChatRoomClient.Utils.Enumerations;

namespace ChatRoomClient.DomainLayer
{

    public delegate void ServerActionReportDelegate(ServerActionResolvedReport serverActionResolvedReport);
    internal class ClientManager : IClientManager
    {
        //Variables
        private IPAddress _serverIpAddress;        
        private TcpClient _tcpClient;
        private string _clientLogger;
        private bool _ClientIsActive = false;

        IServerAction _serverAction;
        public ClientManager( IServerAction serverAction )
        {
            _serverAction = serverAction;
        }

        public void ConnectToServer(ServerCommunicationInfo serverCommunicationInfo)
        {
            try
            {                
                _serverIpAddress = IPAddress.Parse(serverCommunicationInfo.IPAddress);                
                _clientLogger = Notification.CRLF + "Attempting to Connect to Server...";
                serverCommunicationInfo.LogReportCallback(_clientLogger);
                _tcpClient = new TcpClient(_serverIpAddress.ToString(), serverCommunicationInfo.Port);

                Thread threadClientConnection = new Thread(() => 
                {
                    _ClientIsActive = true;
                    serverCommunicationInfo.ConnectionReportCallback(_ClientIsActive);
                    _clientLogger = Notification.CRLF + "Client connected to server Successfully.";
                    serverCommunicationInfo.LogReportCallback(_clientLogger);
                    ExecuteCommunicationWithServer(MessageActionType.ClientConnectToServer, serverCommunicationInfo);
                });
                threadClientConnection.IsBackground = true;
                threadClientConnection.Name = "BackgroundThreadConnection";
                threadClientConnection.Start();
            }
            catch (Exception ex)
            {
                _ClientIsActive= false;
                serverCommunicationInfo.ConnectionReportCallback(_ClientIsActive);
                _clientLogger = Notification.CRLF + Notification.Exception + "Problem connecting to server... " + Notification.CRLF + ex.ToString();
                serverCommunicationInfo.LogReportCallback(_clientLogger);
            }
        }


        public void SendMessageToServer( ServerCommunicationInfo serverCommunicationInfo )
        {
            ExecuteCommunicationWithServer(MessageActionType.CreateUser, serverCommunicationInfo);
        }
        public void DisconnectFromServer(ClientLogReportDelegate logReportCallback , ClientConnectionReportDelegate connectionReportCallback)
        {
            try
            {
                _ClientIsActive = false;
                connectionReportCallback(_ClientIsActive);
                _tcpClient.Close();
                _clientLogger = Notification.CRLF + "Disconnected from the server!";
                logReportCallback(_clientLogger);
            }
            catch (Exception ex)
            {
                connectionReportCallback(_ClientIsActive);
                _clientLogger = Notification.CRLF + Notification.Exception + "Problem disconnecting from the server..." + Notification.CRLF + ex.ToString();
                logReportCallback(_clientLogger);
            }
        }

        #region Private Methods

        private void ExecuteCommunicationWithServer( MessageActionType messageActionType, ServerCommunicationInfo serverCommunicationInfo)
        {
            string messageSent = _serverAction.ResolveCommunicationToServer(_tcpClient, messageActionType, serverCommunicationInfo.Username);
            if (messageSent.Contains(Notification.Exception))
            {
                serverCommunicationInfo.LogReportCallback(messageSent);
                DisconnectFromServer(serverCommunicationInfo.LogReportCallback, serverCommunicationInfo.ConnectionReportCallback);
                return;
            }

            serverCommunicationInfo.LogReportCallback(messageSent);

            void GetServerActionResolvedReport(ServerActionResolvedReport serverActionResolvedReport)
            {
                if (string.IsNullOrEmpty(serverActionResolvedReport.MessageFromServer))
                {
                    DisconnectFromServer(serverCommunicationInfo.LogReportCallback, serverCommunicationInfo.ConnectionReportCallback);
                }
                else if (serverActionResolvedReport.MessageFromServer.Contains(Notification.Exception))
                {
                    serverCommunicationInfo.LogReportCallback(serverActionResolvedReport.MessageFromServer);
                    DisconnectFromServer(serverCommunicationInfo.LogReportCallback, serverCommunicationInfo.ConnectionReportCallback);
                }
                else if (serverActionResolvedReport.MessageFromServer.Contains(Notification.ServerPayload))
                {
                    serverCommunicationInfo.UsernameStatusReportCallback(serverActionResolvedReport.MessageActionType);
                }
            }


            ServerActionReportDelegate serverActionReportCallback = new ServerActionReportDelegate(GetServerActionResolvedReport);

            Thread ThreadServerCommunication = new Thread(() => 
            {
                _serverAction.ResolveCommunicationFromServer(_tcpClient, serverActionReportCallback);
            });

            ThreadServerCommunication.IsBackground= true;
            ThreadServerCommunication.Name = "ThreadServerCommunication";
            ThreadServerCommunication.Start();

        }

        #endregion Private Methods
    }
}
