using UnityEngine;
using System.Net;
using System.Net.Sockets;
using UnityEngine.Events;
using System;

public class Server : MonoBehaviour
{

    private TcpClient client = null;
    private NetworkStream stream = null;
    private int receiveTotal = 0;
    private int receiveTotalExpected = 4;
    private byte[] receiveBuffer = new byte[1024];
    private bool Connected = false;
    private bool expectHeader = true;

    public byte serverVersion;
    public UInt16 defaultPort;
    
    public UnityAction<byte[]> onReceive;
    public UnityAction<string> onError;

    public bool Connect(string address)
    {
        if (Connected)
        {
            Disconnect();
        }

        UInt16 port = defaultPort;
        if (address.Contains(":"))
        {
            string[] parts = address.Split(':');

            if (parts.Length == 2)
            {
                address = parts[0];
                if (!UInt16.TryParse(parts[1], out port))
                {
                    onError("Invalid address");
                    return false;
                }
            } else
            {
                onError("Invalid address");
                return false;
            }
        }

        IPAddress ip;
        if (!IPAddress.TryParse(address, out ip))
        {
            onError("Invalid address");
            return false;
        }
        
        try
        {
            client = new TcpClient(ip.ToString(), port);
            stream = client.GetStream();
        }
        catch (SocketException e)
        {
            onError("Communication error: Socket error " +  e.ErrorCode);
            return false;
        }

        Connected = true;
        return true;
    }

    public void Disconnect()
    {
        Connected = false;
        OnDestroy();
        receiveTotal = 0;
        receiveTotalExpected = 4;
        expectHeader = true;
    }

    void Update()
    {
        if (stream == null) {
            return;
        }

        try
        {
            int process_counter = 0;
            while (stream.DataAvailable)
            {
                if (receiveTotal != receiveTotalExpected)
                {
                    receiveTotal += stream.Read(receiveBuffer, receiveTotal, receiveTotalExpected - receiveTotal);
                }

                while (receiveTotal == receiveTotalExpected)
                {
                    // Waiting for header
                    if (expectHeader)
                    {
                        if (receiveBuffer[0] != serverVersion)
                        {
                            onError("Version mismatch, update mod and app.");
                            OnDestroy();
                            return;
                        }
                        receiveTotalExpected = BitConverter.ToUInt16(receiveBuffer, 2);
                        expectHeader = false;
                    }
                    // Entire packet received
                    else
                    {
                        byte[] data = new byte[receiveTotal];
                        Buffer.BlockCopy(receiveBuffer, 0, data, 0, receiveTotal);
                        onReceive(data);
                        receiveTotal = 0;
                        receiveTotalExpected = 4;
                        expectHeader = true;
                    }
                }

                // Process 5 packets at a time
                if (process_counter++ >= 5)
                {
                    break;
                }
            }
        }
        catch (SocketException e) {
            onError("Communication error: Socket error " + e.ErrorCode);
            OnDestroy();
        }
        catch (Exception)
        {
            onError("An exception has occurred!");
            OnDestroy();
        }
    }

    private void OnDestroy()
    {
        if (stream != null)
        {
            stream.Close();
            stream = null;
        }
        if (client != null)
        {
            client.Close();
            client = null;
        }
    }
}
