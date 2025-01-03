using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UnitySocketTest : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;

    void Start()
    {
        ConnectServer();
    }

    public void ConnectServer()
    {
        ConnectToServer();
        StartListening();
    }

    void ConnectToServer()
    {
        try
        {
            client = new TcpClient("127.0.0.1", 5000);  // 连接到 Python 服务端
            stream = client.GetStream();
            Debug.Log("Connected to server");
        }
        catch (Exception e)
        {
            Debug.LogError("Connection failed: " + e.Message);
        }
    }

    public void StartExperiment()
    {

        var data = new Message.Data
        {
            msg = "test_experiment.py",
        };

        // 请求实验
        SendExperimentMessage("request_experiment", data);
    }

    public void SendExperimentData()
    {

        var data = new Message.Data
        {
            msg = "SendExperimentMessage('send_experiment_data', message);",
        };

        // 请求实验
        SendExperimentMessage("send_experiment_data", data);
    }

    public void ControlCommand()
    {

        var data = new Message.Data
        {
            msg = "SendExperimentMessage('control_command', message);",
        };
        // 请求实验
        SendExperimentMessage("control_command", data);
    }

    public void StopServer()
    {
        
        var data = new Message.Data
        {
            msg = "SendExperimentMessage('stop_server', data);",
        };
        // 请求实验
        SendExperimentMessage("stop_server", data);
    }

    void SendExperimentMessage(string type, Message.Data data)
    {
        var message = new Message
        {
            type = type,
            data = data
        };
        Debug.Log($"Sent message: {message}");
        string json = JsonUtility.ToJson(message);
        byte[] bytes = Encoding.UTF8.GetBytes(json);
        stream.Write(bytes, 0, bytes.Length);
        Debug.Log($"Sent message: {json}");
    }

    void StartListening()
    {
        byte[] buffer = new byte[1024];
        stream.BeginRead(buffer, 0, buffer.Length, OnMessageReceived, buffer);
    }

    void OnMessageReceived(IAsyncResult result)
    {
        try
        {
            int bytesRead = stream.EndRead(result);
            Debug.Log($"New message comming: result: {result}; bytesRead: {bytesRead}");
            byte[] buffer = (byte[])result.AsyncState;
            string json = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Debug.Log($"json: {json}");

            // 解析消息
            var message = JsonUtility.FromJson<Message>(json);
            switch (message.type)
            {
                case "send_xml":
                    Debug.Log("Received XML data: " + message.data.xml_data);
                    break;

                case "control_command":
                    Debug.Log("Received control command: " + message.data);
                    break;

                case "stop_server":
                    Debug.Log("Received stop server command " + message.data);
                    StopConnection();   // 停止连接
                    return;
                    // break;

                default:
                    Debug.LogWarning("Unknown message type: " + message.type);
                    break;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error receiving message: " + e.Message);
        } finally
        {
            StartListening();
        }
    }

    [Serializable]
    public class Message
    {
        public string type;
        public Data data;

        [Serializable]
        public class Data
        {
            public string msg;
            public string xml_data;
            // public int step;
            // public Control[] controls;
        }

        [Serializable]
        public class Control
        {
            public int id;
            public float position;
            public float velocity;
        }
    }
    void OnDestroy()
    {
        StopConnection();
    }

    void StopConnection()
    {
        stream.Close();
        client.Close();
        Debug.Log("Disconnected from server");
    }
}
