// A C# program for Client
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Client
{
	public class MyMessage
	{
		public string StringPropert { get; set; }
	}

	class Client
	{
		// Main Method
		static void Main(string[] args)
		{
			ExecuteClient();
			//Menu();
		}


		// ExecuteClient() Method
		static void ExecuteClient()
		{

			try
			{

				// Establish the remote endpoint
				// for the socket. This example
				// uses port 11111 on the local
				// computer.
				IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
				IPAddress ipAddr = ipHost.AddressList[0];
				IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

				// Creation TCP/IP Socket using
				// Socket Class Constructor
				Socket sender = new Socket(ipAddr.AddressFamily,
						SocketType.Stream, ProtocolType.Tcp);

				try
				{

					// Connect Socket to the remote
					// endpoint using method Connect()
					sender.Connect(localEndPoint);

					// We print EndPoint information
					// that we are connected
					Console.WriteLine("Socket connected to -> {0} ",
						sender.RemoteEndPoint.ToString());
					Console.WriteLine("Write a command if you dont know any command write a 'help'");

					while (true)
					{
						ClientMethod method = new ClientMethod();

						// Creation of message that
						// we will send to Server
						string msg = null;
						Console.Write(">");
						msg = Console.ReadLine().ToLower();

						//byte[] messageSent = Encoding.ASCII.GetBytes(msg);
						//int byteSent = sender.Send(messageSent);

						if (msg == "displayuser")
						{
							method.DisplayUser(sender, msg);
						}

						else if (msg == "help" || msg == "info" || msg == "uptime")
						{
							method.ReceiveMsg(sender, msg);
						}
						else if (msg == "adduser")
						{
							method.AddUser(sender, msg);
						}
						else if (msg == "login")
						{
							method.LogIn(sender, msg);
						}
						else if (msg == "stop")
						{
							method.Exit(sender, msg);
						}
						else
						{
							method.ReceiveMsg(sender, msg);
						}

					}
				}

				// Manage of Socket's Exceptions
				catch (ArgumentNullException ane)
				{

					Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
				}

				catch (SocketException se)
				{

					Console.WriteLine("SocketException : {0}", se.ToString());
				}

				catch (Exception e)
				{
					Console.WriteLine("Unexpected exception : {0}", e.ToString());
				}
			}

			catch (Exception e)
			{

				Console.WriteLine(e.ToString());
			}
		}
	}
}
