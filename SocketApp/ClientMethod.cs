using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace Client
{
	class ClientMethod
	{

		public void DisplayUser(Socket sender, string msg)
		{
			byte[] messageSent = Encoding.ASCII.GetBytes(msg);
			int byteSent = sender.Send(messageSent);
			while (true)
			{
				byte[] messageReceivedUser = new byte[1024];
				int byteRecv2 = sender.Receive(messageReceivedUser);

				var encondingString2 = Encoding.ASCII.GetString(messageReceivedUser, 0, byteRecv2);
				var onMessage2 = new MyMessage
				{
					StringPropert = encondingString2
				};

				string jsonString2 = JsonConvert.SerializeObject(onMessage2);

				Console.WriteLine($"{jsonString2}");
				if (jsonString2.Contains("End"))
				{
					break;
				}
			}
		}

		public void ReceiveMsg(Socket sender, string msg)
		{
			byte[] messageSent = Encoding.ASCII.GetBytes(msg);
			int byteSent = sender.Send(messageSent);

			byte[] messageReceived = new byte[1024];
			int byteRecv = sender.Receive(messageReceived);
			var encondingString = Encoding.ASCII.GetString(messageReceived, 0, byteRecv);
			var onMessage = new MyMessage
			{
				StringPropert = encondingString
			};

			string jsonString = JsonConvert.SerializeObject(onMessage);

			Console.WriteLine($"{jsonString}");

		}

		public void AddUser(Socket sender, string msg)
		{

			ReceiveMsg(sender, msg);

			msg = null;
			Console.Write(">");
			msg = Console.ReadLine().ToLower();

			ReceiveMsg(sender, msg);
			Console.Write(">");
			string Password = null;
			ConsoleKeyInfo key = default;
			do
			{
				key = Console.ReadKey(true);
				if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
				{
					Password += key.KeyChar;
					Console.Write("*");
				}
				else
				{
					if (key.Key == ConsoleKey.Backspace && Password.Length > 0)
					{
						Password = Password.Substring(0, (Password.Length - 1));
						Console.Write("\b \b");
					}
				}
			} while (key.Key != ConsoleKey.Enter);

			Console.WriteLine();

			ReceiveMsg(sender, Password);

		}
		public void LogIn(Socket sender, string msg)
		{
			ReceiveMsg(sender, msg);

			msg = null;
			Console.Write(">");
			msg = Console.ReadLine().ToLower();

			ReceiveMsg(sender, msg);
			Console.Write(">");
			string Password = null;
			ConsoleKeyInfo key = default;
			do
			{
				key = Console.ReadKey(true);
				if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
				{
					Password += key.KeyChar;
					Console.Write("*");
				}
				else
				{
					if (key.Key == ConsoleKey.Backspace && Password.Length > 0)
					{
						Password = Password.Substring(0, (Password.Length - 1));
						Console.Write("\b \b");
					}
				}
			} while (key.Key != ConsoleKey.Enter);

			Console.WriteLine();

			ReceiveMsg(sender, Password);

		}

		public void Exit(Socket sender, string msg)
		{
			ReceiveMsg(sender, msg);
			sender.Shutdown(SocketShutdown.Both);
			sender.Close();
			Environment.Exit(0);
		}

	}
}
