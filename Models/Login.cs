using System;
namespace CICD.Models
{
	public class Login
	{
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool KeepLoginIn { get; set; }
    }
}

