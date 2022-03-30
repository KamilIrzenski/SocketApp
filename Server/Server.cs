// A C# Program for Server
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Server
{
	class Server
	{

		public static string jsonFile = @"C:\Users\Kamili\source\repos\SocketApp\SocketDb.json";
		// Main Method
		static void Main(string[] args)
		{
			ExecuteServer();

		}

		public static void ExecuteServer()
		{
			ServerMethod serverMethod = new ServerMethod();
			// Establish the local endpoint
			// for the socket. Dns.GetHostName
			// returns the name of the host
			// running the application.
			IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
			IPAddress ipAddr = ipHost.AddressList[0];
			IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

			// Creation TCP/IP Socket using
			// Socket Class Constructor
			Socket listener = new Socket(ipAddr.AddressFamily,
						SocketType.Stream, ProtocolType.Tcp);
			string serverVersion = "v1.0.0";
			DateTime dateServerCreated = DateTime.Now;
			DateTime startDateTime = DateTime.Now;

			try
			{

				//// Using Bind() method we associate a
				//// network address to the Server Socket
				//// All client that will connect to this
				//// Server Socket must know this network
				//// Address
				listener.Bind(localEndPoint);

				// Using Listen() method we create
				// the Client list that will want
				// to connect to Server
				listener.Listen(10);

				while (true)
				{

					Console.WriteLine("Waiting connection ... ");

					// Suspend while waiting for
					// incoming connection Using
					// Accept() method the server
					// will accept connection of client
					Socket clientSocket = listener.Accept();
					Console.WriteLine("Connected");

					while (true)
					{
						// Data buffer
						byte[] bytes = new Byte[1024];
						string data = null;


						int numByte = clientSocket.Receive(bytes);
						data += Encoding.ASCII.GetString(bytes,
							0, numByte);

						Console.WriteLine($"Text received -> {data} ");

						if (data == "adduser")
						{
							serverMethod.NewUser(clientSocket, jsonFile);
						}
						else if (data == "deleteuser")
						{
							serverMethod.DeleteUser(clientSocket, jsonFile);
						}
						else if (data == "login")
						{
							serverMethod.LogIn(clientSocket,jsonFile);
						}
						else if (data == "displayuser")
						{
							serverMethod.DisplayUser(clientSocket, jsonFile);
						}
						else if (data == "get time")
						{
							byte[] message = Encoding.ASCII.GetBytes(DateTime.Now.ToLongDateString());
							// Send a message to Client
							// using Send() method
							clientSocket.Send(message);
						}

						else if (data == "uptime")
						{
							DateTime endDateTime = DateTime.Now;
							TimeSpan responseTime = endDateTime - startDateTime;
							byte[] message = Encoding.ASCII.GetBytes($"Czas dzialania serwera: {responseTime}");
							// Send a message to Client
							// using Send() method
							clientSocket.Send(message);
						}

						else if (data == "info")
						{
							byte[] message = Encoding.ASCII.GetBytes($"Wersja servera: {serverVersion} Data utworzenia serwera: {dateServerCreated}");

							clientSocket.Send(message);

						}

						else if (data == "help")
						{
							byte[] message = Encoding.ASCII.GetBytes("uptime - czas dzialania serwera " +
																	 "info - wersja serwera oraz data jego utworzenia " +
																	 "help - lista dostepnych polecen " +
																	 "stop - zatrzymuje dzialanie serwera" +
																	 "adduser - dodaje nowego uzytkownika");

							clientSocket.Send(message);
						}

						else if (data == "stop")
						{
							byte[] message = Encoding.ASCII.GetBytes("Exit");
							// Send a message to Client
							// using Send() method
							clientSocket.Send(message);
							// Close client Socket using the
							// Close() method. After closing,
							// we can use the closed Socket
							// for a new Client Connection
							clientSocket.Shutdown(SocketShutdown.Both);
							clientSocket.Close();
							Environment.Exit(0);

						}
						else
						{
							byte[] message = Encoding.ASCII.GetBytes("Nie poprawna komenda... wpisz help zeby zobaczyc dostepne komendy");
							// Send a message to Client
							// using Send() method
							clientSocket.Send(message);
						}
					}
				}
			}

			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		//public static void DisplayUser(Socket clientSocket)
		//{
		//	IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
		//	IPAddress ipAddr = ipHost.AddressList[0];
		//	IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

		//	Socket sender = new Socket(ipAddr.AddressFamily,
		//		SocketType.Stream, ProtocolType.Tcp);
		//	sender.Connect(localEndPoint);

		//	byte[] bytes = new Byte[1024];

		//	string data = null;

		//	List<User> users = new List<User>();
		//	if (File.Exists(jsonFile))
		//	{
		//		string content = File.ReadAllText(jsonFile);
		//		users = JsonConvert.DeserializeObject<List<User>>(content);

		//		foreach (var user in users)
		//		{
		//			byte[] message3 = Encoding.ASCII.GetBytes($"User => {user.UserName} Role => {user.Role}");
		//			// Send a message to Client
		//			// using Send() method
		//			clientSocket.Send(message3);

		//		}

		//		byte[] message4 = Encoding.ASCII.GetBytes("End");
			
		//		clientSocket.Send(message4);
		//	}
		//	else
		//	{
		//		File.Create(jsonFile).Close();
		//		File.WriteAllText(jsonFile, "[]");
		//		DisplayUser(clientSocket);
		//	}

		//}
		//public static void NewUser(Socket clientSocket)
		//{
		//	try
		//	{

		//		var userData = File.ReadAllText(jsonFile);
		//		List<User> users = new List<User>();

		//		users = JsonConvert.DeserializeObject<List<User>>(userData);

		//		if (users == null)
		//		{
		//			users = new List<User>();
		//		}

		//		IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
		//		IPAddress ipAddr = ipHost.AddressList[0];
		//		IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

		//		Socket sender = new Socket(ipAddr.AddressFamily,
		//			SocketType.Stream, ProtocolType.Tcp);
		//		sender.Connect(localEndPoint);

		//		byte[] bytes = new Byte[1024];
		//		byte[] bytes2 = new byte[1024];
		//		string data = null;
		//		string data2 = null;
		//		User user = new User();
		//		byte[] message = Encoding.ASCII.GetBytes("Enter Username:");
		//		// Send a message to Client
		//		// using Send() method
		//		clientSocket.Send(message);

		//		int numByte = clientSocket.Receive(bytes);
		//		data += Encoding.ASCII.GetString(bytes,
		//			0, numByte);

		//		Console.WriteLine($"Text received -> {data} ");

		//		user.UserName = data;

		//		byte[] message2 = Encoding.ASCII.GetBytes("Enter Password:");
		//		// Send a message to Client
		//		// using Send() method
		//		clientSocket.Send(message2);

		//		int numByte2 = clientSocket.Receive(bytes2);
		//		data2 += Encoding.ASCII.GetString(bytes2,
		//			0, numByte2);

		//		Console.WriteLine($"Text received -> {data2} ");

		//		user.Password = data2;
		//		user.Role = Role.user;

		//		byte[] message3 = Encoding.ASCII.GetBytes("Done...");
		//		// Send a message to Client
		//		// using Send() method
		//		clientSocket.Send(message3);

		//		users.Add(user);

		//		File.WriteAllText(jsonFile, JsonConvert.SerializeObject(users, Formatting.Indented));

		//	}
		//	catch (Exception e)
		//	{
		//		Console.WriteLine(e);
		//		throw;
		//	}

		//}

		//public static void LogIn(Socket clientSocket)
		//{
		//	try
		//	{
		//		var userData = File.ReadAllText(jsonFile);
		//		List<User> users = new List<User>();

		//		users = JsonConvert.DeserializeObject<List<User>>(userData);

		//		if (users == null)
		//		{
		//			users = new List<User>();
		//		}

		//		IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
		//		IPAddress ipAddr = ipHost.AddressList[0];
		//		IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

		//		Socket sender = new Socket(ipAddr.AddressFamily,
		//			SocketType.Stream, ProtocolType.Tcp);
		//		sender.Connect(localEndPoint);

		//		byte[] bytes = new Byte[1024];
		//		byte[] bytes2 = new byte[1024];
		//		string data = null;
		//		string data2 = null;

		//		User user = new User();
		//		byte[] message = Encoding.ASCII.GetBytes("Enter Username:");
		//		// Send a message to Client
		//		// using Send() method
		//		clientSocket.Send(message);

		//		int numByte = clientSocket.Receive(bytes);
		//		data += Encoding.ASCII.GetString(bytes,
		//			0, numByte);

		//		Console.WriteLine($"Text received -> {data} ");

		//		user.UserName = data;

		//		byte[] message2 = Encoding.ASCII.GetBytes("Enter Password:");
		//		// Send a message to Client
		//		// using Send() method
		//		clientSocket.Send(message2);

		//		int numByte2 = clientSocket.Receive(bytes2);
		//		data2 += Encoding.ASCII.GetString(bytes2,
		//			0, numByte2);

		//		Console.WriteLine($"Text received -> {data2} ");

		//		user.Password = data2;

		//		if (user.UserName == data && user.Password == data2)
		//		{
		//			byte[] message3 = Encoding.ASCII.GetBytes("Login succesfull");
		//			// Send a message to Client
		//			// using Send() method
		//			clientSocket.Send(message3);

		//		}
		//		else
		//		{
		//			byte[] message3 = Encoding.ASCII.GetBytes("Login failed");
		//			// Send a message to Client
		//			// using Send() method
		//			clientSocket.Send(message3);
		//		}

		//		//byte[] byData = Encoding.ASCII.GetBytes("un:" + username + ";pw:" + password);
		//		//int byteSent = sender.Send(byData);
		//	}
		//	catch (Exception e)
		//	{
		//		Console.WriteLine(e);
		//		throw;
		//	}

		//}
	}
}
