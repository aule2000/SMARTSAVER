using SmartSaver.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSaver.Presentation.Helpers
{
    public class UsersHelper
    {
        public static User[] GenerateMockData()
        {
            Guid guid = new Guid("6e33fa08-bc0f-438c-a21b-bcf4fc227661");
            User[] goals = new User[1];
            goals[0] = new User
            {
                Gmail = "test@gmail.com",
                Cash = 100,
                Card = 150,
                FullName = "testi", 
                Id = guid
            };
           
            return goals;
        }

        public static List<User> staticGoals = new List<User>();

        public static User[] _savingGoals;

        public static bool isAlreadyInit = false;
    }
}

