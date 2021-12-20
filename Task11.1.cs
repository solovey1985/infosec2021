using System;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Task11_1{
    class User{
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public byte[] Salt { get; set; }
        public string[] Roles { get; set; }
        public User(string log, string pass, byte[] salt, string[] roles){
            Login = log;
            PasswordHash = pass;
            Salt = salt;
            Roles = roles;
        }
    }
    
    class Protector{
        
        private static Dictionary<string, User> _users = new Dictionary<string, User>();    

        public static byte[] GenerateSalt(){
            const int saltLength = 32;
            using(var randGen = new RNGCryptoServiceProvider()){
                var randNumb = new byte[saltLength];
                randGen.GetBytes(randNumb);
                return randNumb;
            }
        }
         private static byte[] Comb(byte[] first, byte[] second){
            var ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);

            return ret;
        }
        public static byte[] GenHash(byte[] toBeHashed, byte[] salt){
            using (var sha256 = SHA256.Create()){
                return sha256.ComputeHash(Comb(toBeHashed, salt));
            }
        }
        static bool CheckPassword(string username, string entPass){
                string getPass = _users[username].PasswordHash;
                byte[] getSalt = _users[username].Salt;
                string ePass = Convert.ToBase64String(GenHash(Encoding.Unicode.GetBytes(entPass), getSalt));
                
                if(String.Equals(getPass, ePass)){
                    return true;    
                }
                else{
                    return false;
                }
                
        }
        private static string [] _roles = {"Viewer", "Editor", "Creator", "Admin"};
        public static User Register(string userName, string password, byte[] salt, string[] roles){
            User luser = new User(userName, password, salt, roles);
            return luser;
        }

        public static void LogIn(string userName, string pass){
              if (Protector.CheckPassword(userName, pass)){
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userName, "OIBAuth"), _users[userName].Roles);
                    IPrincipal threadPrincipal = Thread.CurrentPrincipal;
                    Console.WriteLine("Name: {0}\nIsAuthenticated: {1}" +
                    "\nAuthenticationType: {2}", 
                    threadPrincipal.Identity.Name, 
                    threadPrincipal.Identity.IsAuthenticated,
                    threadPrincipal.Identity.AuthenticationType);
                    foreach(string s in _users[userName].Roles){
                        if(s == "Viewer")
                            Protector.mv();
                        if(s == "Editor")
                            Protector.me();
                        if(s == "Creator")
                            Protector.mc();
                        if(s == "Admin")
                            Protector.ma();
                    }

                }
                else{
                    Console.WriteLine("---Authorization wrong---");
                }  
        }

        public static void mv(){
            Console.WriteLine("This method works for viewers.");
        }
        public static void me(){
            Console.WriteLine("This method works for editors.");
        }
        public static void mc(){
            Console.WriteLine("This method works for creators.");
        }
        public static void ma(){
            Console.WriteLine("This method works for admin.");
        }       

        static void Main(string[] args){
            while(true){
            Console.WriteLine("\n<<<<<< MENU >>>>>>\n1. Registration\n2. Authorization");
            string ans = Console.ReadLine();
            switch(ans){
                case "1":
                {
                    
                    Console.WriteLine("\n>>> Here is the REGISTRATION! <<<");
                    Console.WriteLine("Enter name of user: ");
                    string? name = Console.ReadLine();
                    Console.WriteLine("Enter the password: ");
                    string? pass = Console.ReadLine();
                    byte[] passSalt = Protector.GenerateSalt();
                    string getPass = Convert.ToBase64String(GenHash(Encoding.Unicode.GetBytes(pass), passSalt));
                    Console.WriteLine("Choose his/her roles:\n1. Viewer\n2. Editor\n3. Creator\n4. Admin");
                    string? answ = Console.ReadLine();
                    User newUser;
                    switch(answ){
                        case "1":{
                            Console.WriteLine("You chose Viewer.");
                            string [] arr = new string[]{_roles[0]};
                            newUser = Protector.Register(name, getPass, passSalt, arr);
                            _users.Add(name, newUser);
                            break;
                        }
                        case "2":{
                            Console.WriteLine("You chose Editor.");
                            string [] arr = new string[]{_roles[0], _roles[1]};
                            newUser = Protector.Register(name, getPass, passSalt, arr);
                            _users.Add(name, newUser);
                            break;
                        }
                        case "3":{
                            Console.WriteLine("You chose Creator.");
                            string [] arr = new string[]{_roles[0], _roles[1], _roles[2]};
                            newUser = Protector.Register(name, getPass, passSalt, arr);
                            _users.Add(name, newUser);
                            break;
                        }
                        case "4":{
                            Console.WriteLine("You chose Admin.");
                            string [] arr = new string[]{_roles[0], _roles[1], _roles[2], _roles[3]};
                            newUser = Protector.Register(name, getPass, passSalt, arr);
                            _users.Add(name, newUser);
                            break;
                        }
                        default:{
                            Console.WriteLine("Incorrect!");
                            break;
                        }
                        
                    }
                    Console.WriteLine("\nThe list of registered users: ");
                    Dictionary<string, User>.KeyCollection keyColl = _users.Keys;
                    int i = 1;
                    foreach(string s in keyColl){
                        Console.WriteLine(i + ". " + s);
                        i++;
                    }
                    break;
                }
                case "2":
                {
                    Console.WriteLine("\n<<<<<< AUTHORIZATION >>>>>>");
                    Console.WriteLine("Enter your login: ");
                    string? authName = Console.ReadLine();
                    Console.WriteLine("Enter your password: ");                    
                    string? authPass = Console.ReadLine();
                    Protector.LogIn(authName, authPass);

                    break;
                }
                default:{
                    Console.WriteLine("You entered wrong number!");
                    break;
                }
            }
            
            }
        }
    }
}
