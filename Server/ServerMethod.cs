using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Server
{
	class ServerMethod
	{
		public void DisplayUser(Socket clientSocket, string jsonFile)
		{
			IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
			IPAddress ipAddr = ipHost.AddressList[0];
			IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

			Socket sender = new Socket(ipAddr.AddressFamily,
				SocketType.Stream, ProtocolType.Tcp);
			sender.Connect(localEndPoint);

			byte[] bytes = new Byte[1024];

			string data = null;

			List<User> users = new List<User>();
			if (File.Exists(jsonFile))
			{
				string content = File.ReadAllText(jsonFile);
				users = JsonConvert.DeserializeObject<List<User>>(content);

				//for (int i = 0; i < users.Count; i++)
				//{
				//	byte[] message3 = Encoding.ASCII.GetBytes($"User => {users[i].UserName} Role => {users[i].Role}");
				//	clientSocket.Send(message3);
				//}
				foreach (var user in users)
				{
					byte[] message3 = Encoding.ASCII.GetBytes($"User => {user.UserName} Role => {user.Role}");
					// Send a message to Client
					// using Send() method
					clientSocket.Send(message3);
				}

				byte[] message4 = Encoding.ASCII.GetBytes("End");

				clientSocket.Send(message4);

			}
			else
			{
				File.Create(jsonFile).Close();
				File.WriteAllText(jsonFile, "[]");
				DisplayUser(clientSocket, jsonFile);
			}

		}

		public void NewUser(Socket clientSocket, string jsonFile)
		{
			try
			{
				var userData = File.ReadAllText(jsonFile);
				List<User> users = new List<User>();

				users = JsonConvert.DeserializeObject<List<User>>(userData);

				if (users == null)
				{
					users = new List<User>();
				}

				IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
				IPAddress ipAddr = ipHost.AddressList[0];
				IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

				Socket sender = new Socket(ipAddr.AddressFamily,
					SocketType.Stream, ProtocolType.Tcp);
				sender.Connect(localEndPoint);

				byte[] bytes = new Byte[1024];
				byte[] bytes2 = new byte[1024];
				string data = null;
				string data2 = null;
				User user = new User();
				byte[] message = Encoding.ASCII.GetBytes("Enter Username:");
				// Send a message to Client
				// using Send() method
				clientSocket.Send(message);

				int numByte = clientSocket.Receive(bytes);
				data += Encoding.ASCII.GetString(bytes,
					0, numByte);

				Console.WriteLine($"Text received -> {data} ");

				user.UserName = data;

				byte[] message2 = Encoding.ASCII.GetBytes("Enter Password:");
				// Send a message to Client
				// using Send() method
				clientSocket.Send(message2);

				int numByte2 = clientSocket.Receive(bytes2);
				data2 += Encoding.ASCII.GetString(bytes2,
					0, numByte2);

				Console.WriteLine($"Text received -> {data2} ");

				user.Password = data2;
				user.Role = Role.user;

				byte[] message3 = Encoding.ASCII.GetBytes("Done...");
				// Send a message to Client
				// using Send() method
				clientSocket.Send(message3);

				users.Add(user);

				File.WriteAllText(jsonFile, JsonConvert.SerializeObject(users, Formatting.Indented));

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		public void LogIn(Socket clientSocket, string jsonFile)
		{
			try
			{
				var userData = File.ReadAllText(jsonFile);
				List<User> users = new List<User>();
				users = JsonConvert.DeserializeObject<List<User>>(userData);
				dynamic jsonFileRead = JsonConvert.DeserializeObject(userData);


				IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
				IPAddress ipAddr = ipHost.AddressList[0];
				IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

				Socket sender = new Socket(ipAddr.AddressFamily,
					SocketType.Stream, ProtocolType.Tcp);
				sender.Connect(localEndPoint);

				byte[] bytes = new Byte[1024];
				byte[] bytes2 = new byte[1024];
				string data = null;
				string data2 = null;

				var name = string.Empty;
				var pass = string.Empty;

				User user = new User();
				byte[] message = Encoding.ASCII.GetBytes("Enter Username:");
				// Send a message to Client
				// using Send() method
				clientSocket.Send(message);

				int numByte = clientSocket.Receive(bytes);
				data += Encoding.ASCII.GetString(bytes,
					0, numByte);

				Console.WriteLine($"Text received -> {data} ");

				var hUserName = data;

				byte[] message2 = Encoding.ASCII.GetBytes("Enter Password:");
				// Send a message to Client
				// using Send() method
				clientSocket.Send(message2);

				int numByte2 = clientSocket.Receive(bytes2);
				data2 += Encoding.ASCII.GetString(bytes2,
					0, numByte2);

				Console.WriteLine($"Text received -> {data2} ");

				var hPassword = data2;


				//using (StreamReader file = File.OpenText(jsonFile))
				//using (JsonTextReader reader = new JsonTextReader(file))
				//{
				//	name = reader.Read();
				//	pass = reader.ReadAsString();
				//}

				if (  && )
				{
					byte[] message3 = Encoding.ASCII.GetBytes("Login succesfull");
					// Send a message to Client
					// using Send() method
					clientSocket.Send(message3);

				}
				else
				{
					byte[] message3 = Encoding.ASCII.GetBytes("Login failed");
					// Send a message to Client
					// using Send() method
					clientSocket.Send(message3);
				}



				//byte[] byData = Encoding.ASCII.GetBytes("un:" + username + ";pw:" + password);
				//int byteSent = sender.Send(byData);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

		}

		public void DeleteUser(Socket clientSocket, string jsonFile)
		{
			var userData = File.ReadAllText(jsonFile);
			List<User> users = new List<User>();

			users = JsonConvert.DeserializeObject<List<User>>(userData);

			if (users == null)
			{
				users = new List<User>();
			}

			IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
			IPAddress ipAddr = ipHost.AddressList[0];
			IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

			Socket sender = new Socket(ipAddr.AddressFamily,
				SocketType.Stream, ProtocolType.Tcp);
			sender.Connect(localEndPoint);

			byte[] bytes = new Byte[1024];
			byte[] bytes2 = new byte[1024];
			string data = null;
			string data2 = null;

			User user = new User();
			byte[] message = Encoding.ASCII.GetBytes("Enter Username who you want delete:");
			// Send a message to Client
			// using Send() method
			clientSocket.Send(message);

			int numByte = clientSocket.Receive(bytes);
			data += Encoding.ASCII.GetString(bytes,
				0, numByte);

			Console.WriteLine($"Text received -> {data} ");

			//user.UserName == data
		}
	}
}
