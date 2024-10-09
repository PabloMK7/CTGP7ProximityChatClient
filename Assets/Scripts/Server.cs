using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine.Events;
using System;
using TMPro;

public class Server : MonoBehaviour
{
    int port = 2727;
    public TMP_Text statusText;

    private Socket udp;

    [HideInInspector] public string hostString = "";
    [HideInInspector] public UnityAction<byte[]> onReceive;

    private byte[] receiveBuffer = new byte[1024];

    void Start()
    {
        foreach (var arg in Environment.GetCommandLineArgs())
        {
            if (arg.StartsWith("--listenport"))
            {
                port = int.Parse(arg.Substring(12));
            }
        }

        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress useIP = null;
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                useIP = ip; break;
            }
        }
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);

        hostString = new IPEndPoint(useIP, port).ToString();
        statusText.SetText("Listening on " + hostString);
        udp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        udp.Bind(endPoint);
        udp.Blocking = false;
    }

    void Update()
    {
        if(udp.Available != 0)
        {
            EndPoint sender = new IPEndPoint(IPAddress.Any, port);

            int rec = udp.ReceiveFrom(receiveBuffer, ref sender);
            
            if (rec > 0)
            {
                byte[] data = new byte[rec];
                Buffer.BlockCopy(receiveBuffer, 0, data, 0, rec);
                onReceive(data);
            }
        }
    }
}
