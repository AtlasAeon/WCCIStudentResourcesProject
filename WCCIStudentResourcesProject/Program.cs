using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WCCIStudentResourcesProject {
    class Program {
        static string[] menu = {
            "View Students",
            "View Available Resources",
            "View Student Accounts",
            "Checkout Item",
            "Return Item",
            "Exit"
        };

        static List<string> StudentAccounts = new List<string>(); //text file holds student list

        static Dictionary<string, int> Resources = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        static void Menu() {
            Console.WriteLine("Bootcamp Resources Checkout System\n");
            Console.WriteLine("Choose a menu item number to continue");
            for (int i = 0; i < menu.Length; i++)
                Console.Write("\t{0, 3}. {1}\n", (i + 1), menu[i]);
            string menuMatch = Console.ReadLine().ToLower();
            switch (menuMatch) {
                case "1":
                case "view students":
                    ViewStudents();
                    break;
                case "2":
                case "view available resources":
                    ViewResources();
                    break;
                case "3":
                case "view student accounts":
                    ViewAccounts();
                    break;
                case "4":
                case "checkout item":
                    Checkout();
                    break;
                case "5":
                case "return item":
                    ReturnItem();
                    break;
                case "6":
                case "exit":
                    Console.WriteLine("Press any key to exit. Goodbye!");
                    Console.ReadKey();
                    break;
                default:
                    Console.Clear();
                    Menu();
                    break;
            }
        }

        static void ViewStudents() {
            Console.Clear();
            Console.WriteLine("Students:\n\n");
            for (int i = 0; i < StudentAccounts.Count(); i++) {
                StringBuilder studentList = new StringBuilder();
                studentList.Append("\t");
                studentList.Append((i+1));
                studentList.Append(". ");
                studentList.Append(StudentAccounts[i]);
                studentList.Append("\n");
                Console.WriteLine(studentList.ToString());
            }
                //Console.Write("\t{0, 3}. {1}\n", (i + 1), StudentAccounts[i]);
            Console.WriteLine("\n");
            Menu();
        }

        static void ViewResources() {
            Console.Clear();
            Console.WriteLine("Resources:\n\n");
            int counter = 1;
            foreach (KeyValuePair<string, int> pair in Resources) {
                if (pair.Value > 0) {
                    StringBuilder resourceBuilder = new StringBuilder();
                    resourceBuilder.Append(counter);
                    resourceBuilder.Append(". ");
                    resourceBuilder.Append(pair.Key);
                    resourceBuilder.Append(" Availability: ");
                    resourceBuilder.Append(pair.Value);
                    Console.WriteLine(resourceBuilder.ToString());
                } else {
                    StringBuilder resourceBuilder = new StringBuilder();
                    resourceBuilder.Append(counter);
                    resourceBuilder.Append(". ");
                    resourceBuilder.Append(pair.Key);
                    resourceBuilder.Append(" Unavailable for checkout.");
                    Console.WriteLine(resourceBuilder.ToString());
                }
                counter++;
            }
            Console.WriteLine("Would you like to see all checked out resources? (y/n)");
            string userAnswer = Console.ReadLine().ToLower();
            switch (userAnswer) {
                case "y":
                    using(StreamReader resourcesCheckedOutReader = new StreamReader("resourcesChecked.txt")) {
                        Console.WriteLine(resourcesCheckedOutReader.ReadToEnd());
                    }
                    break;
                case "n":
                    break;
                default:
                    Console.WriteLine("Invalid input.");
                    break;
            }
            Menu(); 
        }

        static void WriteStudentFile(string studentAccount) {
            StringBuilder accountBuilder = new StringBuilder();
            accountBuilder.Append(studentAccount);
            accountBuilder.Append(".txt");
            string studentAccountTxt = accountBuilder.ToString();
            int idCounter;
            using (StreamReader idCounterReader = new StreamReader("ID.txt")) {
                idCounter = int.Parse(idCounterReader.ReadLine());
            }
            if (!File.Exists(studentAccountTxt)) {
                using (StreamWriter studentFileWriter = new StreamWriter(studentAccountTxt)) {
                    ++idCounter;
                    studentFileWriter.WriteLine("Account Name: {0} ID: {1}", studentAccount, idCounter);
                    studentFileWriter.WriteLine("Available");
                    studentFileWriter.WriteLine("Available");
                    studentFileWriter.WriteLine("Available");
                }
                using (StreamWriter writer = new StreamWriter("ID.txt")) {
                    writer.Write(idCounter);
                }
            }
        }

        static void UpdateStudentFile(string studentAccount, string resourceItem, int action) {
            StringBuilder accountBuilder = new StringBuilder();
            accountBuilder.Append(studentAccount);
            accountBuilder.Append(".txt");
            string studentAccountTxt = accountBuilder.ToString();
            List<string> temp = new List<string>();
            bool flag;
            switch (action) {
                case 1://checkout item
                    flag = false;
                    using (StreamReader studentCheckReader = new StreamReader(studentAccountTxt)) {
                        while (!studentCheckReader.EndOfStream) {
                            string line = studentCheckReader.ReadLine();
                            if (flag == false && line == "Available") {
                                temp.Add(resourceItem);
                                flag = true;
                            }
                            else{
                                temp.Add(line);
                            }
                        }
                    }
                    using (StreamWriter studentCheckWriter = new StreamWriter(studentAccountTxt)) {
                        foreach (string line in temp) {
                            studentCheckWriter.WriteLine(line);
                        }
                    }
                    temp.Clear();
                    if (flag == false)
                        Console.WriteLine("You checked out the maximum amount of resources.");
                    else {
                        using (StreamReader resourcesCheckedReader = new StreamReader("resourcesChecked.txt")) {
                            while (!resourcesCheckedReader.EndOfStream) {
                                string line = resourcesCheckedReader.ReadLine();
                                temp.Add(line);
                            }
                            temp.Add(resourceItem);
                        }
                        using (StreamWriter resourcesCheckedWriter = new StreamWriter("resourcesChecked.txt")) {
                            foreach (string line in temp) {
                                resourcesCheckedWriter.WriteLine(line);
                            }
                        }
                    }
                    break;
                case 2://return item
                    flag = false;
                    using (StreamReader studentReturnReader = new StreamReader(studentAccountTxt)) {
                        while (!studentReturnReader.EndOfStream) {
                            string line = studentReturnReader.ReadLine();
                            if (line == resourceItem && flag == false) {
                                temp.Add("Available");
                                flag = true;
                            } else {
                                temp.Add(line);
                            }
                        }
                    }
                    using (StreamWriter studentReturnWriter = new StreamWriter(studentAccountTxt)) {
                        foreach (string line in temp) {
                            studentReturnWriter.WriteLine(line);
                        }
                    }
                    if (flag == false)
                            Console.WriteLine("Resource not found");
                    flag = false;
                    temp.Clear();
                    using (StreamReader resourcesReturnedReader = new StreamReader("resourcesChecked.txt")) {
                        while (!resourcesReturnedReader.EndOfStream) {
                            string line = resourcesReturnedReader.ReadLine();
                            if (line == resourceItem && flag == false) {
                                flag = true;
                            } else {
                                temp.Add(line);
                            }
                        }
                    }
                    using (StreamWriter resourcesReturnedWriter = new StreamWriter("resourcesChecked.txt")) {
                        foreach (string line in temp) {
                            resourcesReturnedWriter.WriteLine(line);
                        }
                    }
                    break;
            }
        }

        static void ViewStudentFile(string studentAccount) {
            try {
                StringBuilder accountBuilder = new StringBuilder();
                accountBuilder.Append(studentAccount);
                accountBuilder.Append(".txt");
                string studentAccountTxt = accountBuilder.ToString();
                using (StreamReader studentViewReader = new StreamReader(studentAccountTxt)) {
                    while (!studentViewReader.EndOfStream) {
                        string line = studentViewReader.ReadLine();
                        if (line == "Available")
                            continue;
                        Console.WriteLine(line);
                    }
                }
            } catch (FileNotFoundException){
                Console.Error.WriteLine("Student Account Not Found");
            }
        }

        static void ViewAccounts() {
            Console.Clear();
            Console.WriteLine("View Student Accounts:\n\n");
            for (int i = 0; i < StudentAccounts.Count(); i++) {
                StringBuilder accountList = new StringBuilder();
                accountList.Append("\t");
                accountList.Append((i+1));
                accountList.Append(". ");
                accountList.Append(StudentAccounts[i]);
                accountList.Append("\n");
                Console.WriteLine(accountList.ToString());
            }
                //Console.Write("\t{0, 3}. {1}\n", (i + 1), StudentAccounts[i]);
            Console.Write("\nPlease enter the account name: ");
            string accountName = Console.ReadLine().ToLower();
            if(StudentName(accountName) == "Student not found") {
                Console.Error.WriteLine("Student Account Not Found");
            } else {
                ViewStudentFile(accountName);
            }
            Menu();
        }

        static string StudentName(string accountName) {
            string value = "";
            for (int i = 0; i < StudentAccounts.Count(); i++) {
                if (accountName == StudentAccounts[i].ToLower())
                    value = StudentAccounts[i];
            }
            if (value == "") 
                return "Student not found";
             else 
                return value;
        }
        static string ResourceName(string resourceName) {
            string value = "";
            foreach (KeyValuePair<string, int> pair in Resources) {
                if (resourceName.ToLower() == pair.Key.ToLower())
                    value = pair.Key;
            }
            if (value == "")
                return "Resource not found";
            else
                return value;
        }

        static void Checkout() {
            Console.Clear();
            Console.WriteLine("Resources:\n\n");
            int counter = 1;
            foreach (KeyValuePair<string, int> pair in Resources) {
                if (pair.Value != 0) {
                    StringBuilder resourceBuilder = new StringBuilder();
                    resourceBuilder.Append(counter);
                    resourceBuilder.Append(". ");
                    resourceBuilder.Append(pair.Key);
                    resourceBuilder.Append(" Availability: ");
                    resourceBuilder.Append(pair.Value);
                    Console.WriteLine(resourceBuilder.ToString());
                } else {
                    StringBuilder resourceBuilder = new StringBuilder();
                    resourceBuilder.Append(counter);
                    resourceBuilder.Append(". ");
                    resourceBuilder.Append(pair.Key);
                    resourceBuilder.Append(" Unavailable for checkout.");
                    Console.WriteLine(resourceBuilder.ToString());
                }
                counter++;
            }
            Console.WriteLine("Please enter your student name.");
            string accountName = StudentName(Console.ReadLine());
            ViewStudentFile(accountName);
            if (accountName != "Student not found") {
                Console.WriteLine("Please enter the resource to be checked out.");
                string resourceName = ResourceName(Console.ReadLine());
                if (resourceName != "Resource not found") {
                    int resourceNum = Resources[resourceName];
                    Resources[resourceName] = Resources[resourceName] - 1;
                    UpdateStudentFile(accountName, resourceName, 1);
                } else {
                    Console.WriteLine("Resource not found. Please try again. . .");
                    Console.ReadKey();
                    Checkout();
                }
            } else {
                Console.WriteLine("Account not found. Please try again. . .");
                Console.ReadKey();
                Checkout();
            }
            Menu();
        }

        static void ReturnItem() {
            Console.Clear();
            Console.WriteLine("Resources:\n\n");
            int counter = 1;
            foreach (KeyValuePair<string, int> pair in Resources) {
                if (pair.Value != 0) {
                    Console.WriteLine(counter + ". " + pair.Key + "Availability: " + pair.Value);
                } else {
                    Console.WriteLine(counter + ". " + pair.Key + "Unavailable for checkout");
                }
            }
            Console.WriteLine("Please enter a student account name: ");
            string accountName = StudentName(Console.ReadLine());
            if (accountName != "Student not found") {
                string resourceName = ResourceName(Console.ReadLine());
                if (resourceName != "Resource not found" && Resources[resourceName] != 0) {
                    int resourceNum = Resources[resourceName];
                    Resources[resourceName] = Resources[resourceName] + 1;
                    UpdateStudentFile(accountName, resourceName, 2);
                } else {
                    if (Resources[resourceName] == 0) {
                        Console.WriteLine("Resource not available");
                    } else {
                        Console.WriteLine("Resource not found. Please try again. . .");
                        Console.ReadKey();
                        Checkout();
                    }
                }
            } else {
                Console.WriteLine("Account not found. Please try again. . .");
                Console.ReadKey();
                Checkout();
            }
            Menu();
        }

        static void Main(string[] args) {
            Console.SetWindowSize(100, 45);
            using (StreamReader studentListReader = new StreamReader("studentlist.txt")) {
                while (!studentListReader.EndOfStream) {
                    StudentAccounts.Add(studentListReader.ReadLine());
                }
            }
            using (StreamReader reader = new StreamReader("resources.txt")) {
                while (!reader.EndOfStream) {
                    string line = reader.ReadLine();
                    string[] lineSplit = line.Split('|');
                    Resources.Add(lineSplit[0], int.Parse(lineSplit[1]));
                }
            }
            StudentAccounts.Sort();
            foreach(string studentFile in StudentAccounts)
                WriteStudentFile(studentFile);
            Menu();
            using (StreamWriter resourceUpdateWriter = new StreamWriter("resources.txt")) {
                foreach (KeyValuePair<string, int> pair in Resources)
                    resourceUpdateWriter.WriteLine(pair.Key + "|" + pair.Value);

            }
        }
    }
}