using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCCIStudentResourcesProject
{
    class Program
    {
        
        static bool exit = true;
        static bool checkName = false;
        static int retrieveStudentInfo;
        static string[] menu = {
            "View Students",
            "View Available Resources",
            "View Student Accounts",
            "Checkout Item",
            "Return Item",
            "Exit"
        };

        static string[,] studentAccount = new string[14, 4] { //Student first name last name and 3 resources
            { "Quinn Bennett", "Available", "Available", "Available" }, 
            { "Sirahn Butler", "Available", "Available", "Available" },
            { "Imari Childess", "Available", "Available", "Available" },
            { "Jennifer Evans","Available","Available", "Available" },
            { "Lawerence Hudson", "Available", "Available", "Available" },
            { "Margaret Landefeld", "Available", "Available", "Available" },
            { "Jacob Lockyer", "Available", "Available", "Available" },
            { "Richard Raponi", "Available", "Available", "Available" },
            { "Cameron Robinson", "Available", "Available", "Available" },
            { "Krista Scholdberg", "Available", "Available", "Available" },
            { "Ashley Stewart", "Available", "Available", "Available" },
            { "Cadale Thomas", "Available", "Available", "Available" },
            { "Kim Vargas", "Available", "Available", "Available" },
            { "Mary Winkelman", "Available", "Available", "Available" }
        };

        static string[,] resources =
        {
            { "ASP.NET MVC 5", "0" },
            { "C# Player's guide", "1" },
            { "Database Manipulation for Mere Mortals", "0" },
            { "Eloquent Javascript", "0" },
            { "HTML & CSS", "1" },
            { "Javascript & Jquery", "0" },
            { "Javascript Can Do Backend Too?", "0" },
            { "Javascript for Dummies", "1"},
            { "Javascript: The Good Parts", "1" },
            { "SQL for Mere Mortals", "0" },
        };

        static void Menu()
        {
            Console.WriteLine("Bootcamp Resources Checkout System\n");
            Console.WriteLine("Choose a menu item number to continue");
            for (int i = 0; i < menu.Length; i++)
                Console.Write("\t{0, 3}. {1}\n", (i + 1), menu[i]);
            string menuMatch = Console.ReadLine().ToLower();
            switch (menuMatch)
            {
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
                    Exit();
                    break;
                default:
                    Console.Clear();
                    Menu();
                    break;
            }
        }

        static void ViewStudents()
        {
            Console.Clear();
            Console.WriteLine("Students:\n\n");
            for (int i = 0; i < studentAccount.GetLength(0); i++)
                Console.Write("\t{0, 3}. {1}\n", (i + 1), studentAccount[i, 0]);
            Console.WriteLine("\n");
            Menu();
        }

        static void ViewResources()
        {
            bool checkResources = false;
            bool isAvailable = false;
            Console.Clear();
            Console.WriteLine("Resources:\n\n");
            for (int i = 0; i < resources.GetLength(0); i++)
            {
                int availability = int.Parse(resources[i, 1]);
                for (int x = 0; x < resources.GetLength(0); x++)
                {
                    int available = int.Parse(resources[x, 1]);
                    if (available != 0)
                    {
                        checkResources = true;
                        isAvailable = true;
                    }
                }
                if (checkResources)
                {
                    if (availability != 0)
                    {
                        Console.Write("\t{0, 3}. {1}\n\t\tAvailability: {2}\n\n", (i + 1), resources[i, 0], resources[i, 1]);
                    }
                }
            }
            if (isAvailable == false)
                Console.WriteLine("\tEverything is checked out");

            Console.WriteLine("\n");
            Menu();
        }

        static void ViewIndividualAccount(string studentAccountNameCheck)
        {
            checkName = false;
            retrieveStudentInfo = -1;
            for (int i = 0; i < studentAccount.GetLength(0); i++)
            {
                if (studentAccountNameCheck == studentAccount[i, 0].ToLower())
                {
                    retrieveStudentInfo = i;
                    checkName = true;
                    break;
                }
            }
            if (checkName == false)
            {
                Console.WriteLine("Error: Request Unavailable");
            } else
            {
                bool check = false;
                Console.Clear();
                Console.WriteLine("Account: " + studentAccount[retrieveStudentInfo, 0]);
                Console.WriteLine("\nItems checked out: ");
                for (int i = 1; i < 4; i++)
                {
                    if (studentAccount[retrieveStudentInfo, i] != "Available")
                    {
                        check = true;
                        Console.WriteLine("\t {0}",studentAccount[retrieveStudentInfo,i]);
                    }
                }
                if (!check)
                {
                    Console.WriteLine("\tNothing is checked out\n\n\n");
                }
            }
            Console.WriteLine();
        }

        static void ViewAccounts()
        {
            Console.Clear();
            Console.WriteLine("View Student Accounts:\n\n");
            for (int i = 0; i < studentAccount.GetLength(0); i++)
                Console.Write("\t{0, 3}. {1}\n", (i + 1), studentAccount[i, 0]);
            Console.Write("\nPlease enter the account name: ");
            ViewIndividualAccount(Console.ReadLine().ToLower());
            Menu();
        }

        static void Checkout()
        {
            bool checkResources = false;
            //bool isAvailable = false;
            Console.Clear();
            Console.WriteLine("Resources:\n\n");
            for (int i = 0; i < resources.GetLength(0); i++)
            {
                int availability = int.Parse(resources[i, 1]);
                for (int x = 0; x < resources.GetLength(0); x++)
                {
                    int available = int.Parse(resources[x, 1]);
                    if (available != 0)
                    {
                        checkResources = true;
                    }
                }
                if (checkResources)
                {
                    if (availability != 0)
                        Console.Write("\t{0, 3}. {1}\n\t\tAvailability: {2}\n\n", (i + 1), resources[i, 0], resources[i, 1]);
                }
            }
            if (checkResources == false)
            {
                Console.WriteLine("\tEverything is checked out");
            } else
            {
                Console.WriteLine("\n");
                Console.WriteLine("\n");
                bool catchAccount = false;
                bool catchResource = false;
                Console.WriteLine("Enter account name:");
                string accountSelect = Console.ReadLine().ToLower();
                for (int i = 0; i < studentAccount.GetLength(0); i++)
                {
                    if (accountSelect == studentAccount[i, 0].ToLower())
                    {
                        catchAccount = true;
                        Console.WriteLine("Enter the name of the item you would like to check out");
                        string itemCheckout = Console.ReadLine().ToLower();
                        for (int j = 0; j < 5; j++)
                        {

                        }
                        if (studentAccount[i, 1] == "Available")
                        {
                            for (int x = 0; x < resources.GetLength(0); x++)
                            {
                                if (itemCheckout == resources[x, 0].ToLower())
                                {
                                    if (resources[x, 1] != "0")
                                    {
                                        Console.Clear();
                                        int resourceUpdate = int.Parse(resources[x, 1]);
                                        --resourceUpdate;
                                        resources[x, 1] = resourceUpdate.ToString();
                                        catchResource = true;
                                        studentAccount[i, 1] = resources[x, 0];
                                        Console.WriteLine("{0} has checked out {1}\n", studentAccount[i, 0], resources[x, 0]);
                                    }
                                }
                            }
                            if (catchResource == false)
                                Console.WriteLine("Error: Request Unavailable");
                        }
                        else if (studentAccount[i, 2] == "Available")
                        {
                            for (int x = 0; x < resources.GetLength(0); x++)
                            {
                                if (itemCheckout == resources[x, 0].ToLower())
                                {
                                    if (resources[x, 1] != "0")
                                    {
                                        Console.Clear();
                                        int resourceUpdate = int.Parse(resources[x, 1]);
                                        --resourceUpdate;
                                        resources[x, 1] = resourceUpdate.ToString();
                                        catchResource = true;
                                        studentAccount[i, 2] = resources[x, 0];
                                        Console.WriteLine("{0} has checked out {1}\n", studentAccount[i, 0], resources[x, 0]);
                                    }
                                }
                            }
                            if (catchResource == false)
                                Console.WriteLine("Error: Request Unavailable");
                        }
                        else if (studentAccount[i, 3] == "Available")
                        {
                            for (int x = 0; x < resources.GetLength(0); x++)
                            {
                                if (itemCheckout == resources[x, 0].ToLower())
                                {
                                    if (resources[x, 1] != "0")
                                    {
                                        Console.Clear();

                                        int resourceUpdate = int.Parse(resources[x, 1]);
                                        --resourceUpdate;
                                        resources[x, 1] = resourceUpdate.ToString();
                                        catchResource = true;
                                        studentAccount[i, 3] = resources[x, 0];
                                        Console.WriteLine("{0} has checked out {1}\n", studentAccount[i, 0], resources[x, 0]);
                                    }
                                }
                            }
                            if (catchResource == false)
                                Console.WriteLine("Error: Request Unavailable");
                        }
                        else
                        {
                            Console.WriteLine("{0} has checked out the maximum number of resources.\n", studentAccount[i, 0]);
                        }
                    }
                }
                if (catchAccount == false)
                {
                    Console.WriteLine("Error: Request Unavailable");
                }
                else
                {
                    catchAccount = false;
                    for (int i = 0; i < resources.GetLength(0); i++)
                    {
                        if (accountSelect == resources[i, 0])
                        {
                            catchAccount = true;
                        }
                    }
                }
            }

            Console.WriteLine();
            Menu();
        }

        static void ReturnItem()
        {
            Console.Clear();
            retrieveStudentInfo = -1;
            Console.Write("Please enter a student account name: ");
            ViewIndividualAccount(Console.ReadLine().ToLower());
            if (checkName == true)
            {
                bool checkResource = false;
                int resourceNum;
                Console.WriteLine("Please enter the name of the resource you would like to return: ");
                string resourceNameCheck = Console.ReadLine().ToLower();
                for (int i = 0; i < resources.GetLength(0); i++)
                {
                    if (resourceNameCheck == resources[i,0].ToLower())
                    {
                        resourceNum = int.Parse(resources[i, 1]);
                        if (studentAccount[retrieveStudentInfo, 3].ToLower() == resourceNameCheck)
                        {
                            Console.WriteLine("{0} has returned {1}",studentAccount[retrieveStudentInfo,0], resources[i,0]);
                            studentAccount[retrieveStudentInfo, 3] = "Available";
                            resources[i, 1] = (resourceNum + 1).ToString();
                        } else if (studentAccount[retrieveStudentInfo, 2].ToLower() == resourceNameCheck)
                        {
                            Console.WriteLine("{0} has returned {1}", studentAccount[retrieveStudentInfo, 0], resources[i, 0]);
                            studentAccount[retrieveStudentInfo, 2] = "Available";
                            resources[i, 1] = (resourceNum + 1).ToString();
                        } else if (studentAccount[retrieveStudentInfo, 1].ToLower() == resourceNameCheck)
                        {
                            Console.WriteLine("{0} has returned {1}", studentAccount[retrieveStudentInfo, 0], resources[i, 0]);
                            studentAccount[retrieveStudentInfo, 1] = "Available";
                            resources[i, 1] = (resourceNum + 1).ToString();
                        } else
                        {
                            Console.WriteLine("Error: Request Unavailable");
                        }
                        checkResource = true;
                    }
                }
                if (checkResource ==  false)
                {
                    Console.WriteLine("Error: Request Unavailable");
                }
            }
            Console.WriteLine();
            Menu();
        }

        static void Exit()
        {
            Console.WriteLine("Press any key to leave. Goodbye!");
            exit = false;
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 45);
            while (exit)
            {
                Menu();
            }            
        }
    }
}