using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;
using Newtonsoft.Json;

namespace Server
{
	class User
	{ 
		//public int Id { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public Role Role { get; set; }

	}
}
