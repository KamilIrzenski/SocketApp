using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Server
{
	class UserController
	{
		public static string jsonFile = @"C:\Users\Kamili\source\repos\SocketApp";

		public List<User> GetUsersDetails()
		{
			List<User> people = new List<User>();
			if (File.Exists(jsonFile))
			{
				string content = File.ReadAllText(jsonFile);
				people = JsonConvert.DeserializeObject<List<User>>(content);
				return people;
			}
			else
			{
				File.Create(jsonFile).Close();
				File.WriteAllText(jsonFile, "[]");
				GetUsersDetails();
			}

			return people;
		}
		public bool InsertPerson(User iList)
		{
			var PersonFile = UserController.jsonFile;
			var PeopleData = System.IO.File.ReadAllText(PersonFile);
			List<User> PersonList = new List<User>();

			PersonList = JsonConvert.DeserializeObject<List<User>>(PeopleData);

			if (PersonList == null)
			{
				PersonList = new List<User>();
			}

			PersonList.Add(iList);

			File.WriteAllText(PersonFile, JsonConvert.SerializeObject(PersonList));

			return true;
		}



		//private void AddUser()
		//{

		//	User user = new User();
		//	Console.WriteLine("Wprowadz Nazwe uzytkownika");
		//	user.Name = Console.ReadLine();
		//	Console.WriteLine("Wprowadz haslo");
		//	user.Password = Console.ReadLine();

		//	try
		//	{
		//		var jsonToWrite = JsonConvert.SerializeObject(user, Formatting.Indented);

		//		using (var writer = new StreamWriter(jsonFile))
		//		{
		//			writer.Write(jsonToWrite);
		//		}

		//	}
		//	catch (Exception e)
		//	{
		//		Console.WriteLine(e);
		//		throw;
		//	}
		//}
	}
}
