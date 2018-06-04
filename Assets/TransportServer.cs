using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class TransportServer : MonoBehaviour {

	void Awake() 
	{
		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
	}

	// Use this for initialization
	void Start () {
		NetworkTransport.Init ();

		ConnectionConfig config = new ConnectionConfig ();
		int channelId = config.AddChannel (QosType.Unreliable);

		HostTopology topology = new HostTopology (config, 10);

		int hostId = NetworkTransport.AddWebsocketHost (topology, 8888, "127.0.0.1");
		Debug.Log ("Websocket configured. Host id is " + hostId);

		/*byte error;
		int connectionId = NetworkTransport.Connect(hostId, "127.0.0.1", 8888, 0, out error);
		Debug.Log("Connected to server. ConnectionId: " + connectionId);*/

		//NetworkTransport.Send (hostId, connectionId, 8888, new byte[1024], 1024, error);
	}
	
	// Update is called once per frame
	void Update () {
		int recHostId;
		int connectionId;
		int channelId;
		byte[] recBuffer = new byte[1024];
		int bufferSize = 1024;
		int dataSize;
		byte error;

		NetworkEventType recData = NetworkTransport.Receive (out recHostId, out connectionId, out channelId, recBuffer, bufferSize, out dataSize, out error);

		switch (recData) {
		case NetworkEventType.Nothing:
			break;
		case NetworkEventType.ConnectEvent:
			Debug.Log("incoming connection event received");
			break;
		case NetworkEventType.DataEvent:
			//Stream stream = new MemoryStream(recBuffer);
			//BinaryFormatter formatter = new BinaryFormatter();
			//string message = formatter.Deserialize(stream) as string;
			string message = System.Text.Encoding.Default.GetString(recBuffer);
			Debug.Log("incoming message event received: " + message);
			break;
		case NetworkEventType.DisconnectEvent:
			Debug.Log("remote client event disconnected");
			break;
		}

	}
}
