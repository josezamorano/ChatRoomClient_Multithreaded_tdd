﻿using ChatRoomClient.DomainLayer;
using System.Net.Sockets;

namespace ChatRoomClient.Utils.Interfaces
{
    public interface ITransmitter
    {
        string SendMessageToServer(TcpClient tcpClient, string payloadAsMessageLine);

        void ReceiveMessageFromServer(TcpClient tcpClient, MessageFromServerDelegate messageFromServerCallback);
    }
}
