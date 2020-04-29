using System;
using System.Collections.Generic;
using System.Threading;

namespace Part2
{
    class Program
    {
        public static List<Client> CheckClientList = new List<Client>();
        static void Main(string[] args)
        {
            List<Client> ClientList = new List<Client>();
            TimerCallback timer = new TimerCallback(CheckBalance);
            Timer tm = new Timer(timer, ClientList, 0, 10000);
            bool work = true;
            while (work)
            {
                System.Console.WriteLine("1.Insert");
                System.Console.WriteLine("2.Update");
                System.Console.WriteLine("3.Delete");
                System.Console.WriteLine("4.Select");
                System.Console.WriteLine("5.Exit");
                Console.Write("choice:");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        {
                            System.Console.Write("Ваше Имя:");
                            string name = Console.ReadLine();
                            System.Console.Write("Ваш Баланс:");
                            decimal balance = decimal.Parse(Console.ReadLine());
                            Thread InsertThread = new Thread(() => Insert(ref ClientList, name, balance));
                            InsertThread.Start();
                            Console.Clear();
                        }
                        break;
                    case "4":
                        {
                            Console.Clear();
                            Thread InsertThread = new Thread(() => Select(ClientList));
                            InsertThread.Start();
                            Thread.Sleep(100);
                        }
                        break;
                    case "3":
                        {
                            Console.Clear();
                            foreach (var item in ClientList)
                            {
                                System.Console.WriteLine($"ID:{item.ID} Name:{item.Name} Balance:{item.Balance}");
                            }
                            System.Console.Write("Client ID:");
                            int id = int.Parse(Console.ReadLine());
                            Thread InsertThread = new Thread(() => Delete(ref ClientList, id));
                            InsertThread.Start();
                            Console.Clear();
                        }
                        break;
                    case "2":
                        {
                            Console.Clear();
                            foreach (var item in ClientList)
                            {
                                System.Console.WriteLine($"ID:{item.ID} Name:{item.Name} Balance:{item.Balance}");
                            }
                            System.Console.Write("Client ID:");
                            int id = int.Parse(Console.ReadLine());
                            System.Console.Write("New Name:");
                            string name = Console.ReadLine();
                            System.Console.Write("New Balance:");
                            decimal balance = decimal.Parse(Console.ReadLine());
                            Thread InsertThread = new Thread(() => Update(ref ClientList, id, name, balance));
                            InsertThread.Start();
                            Console.Clear();
                        }
                        break;
                    case "5": { work = false; } break;
                }
            }
        }
        public static void CheckBalance(object obj)
        {
            List<Client> list = obj as List<Client>;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Balance > CheckClientList[i].Balance)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine($"ID:{list[i].ID} Balance increased before:{CheckClientList[i].Balance} after:{list[i].Balance}");
                    Console.ForegroundColor = ConsoleColor.White;
                    CheckClientList[i].Balance = list[i].Balance;
                }
                if (list[i].Balance < CheckClientList[i].Balance)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine($"ID:{list[i].ID} Balance decreased before:{CheckClientList[i].Balance} after:{list[i].Balance}");
                    Console.ForegroundColor = ConsoleColor.White;
                    CheckClientList[i].Balance = list[i].Balance;
                }
            }
        }
        public static void Insert(ref List<Client> ClientList, string name, decimal balance)
        {
            ClientList.Add(new Client { ID = ClientList.Count + 1, Balance = balance, Name = name });
            CheckClientList.Add(new Client { ID = ClientList.Count + 1, Balance = balance, Name = name });
        }
        public static void Select(List<Client> ClientList)
        {
            foreach (var item in ClientList)
            {
                System.Console.WriteLine($"ID:{item.ID} Name:{item.Name} Balance:{item.Balance}");
            }
        }
        public static void Delete(ref List<Client> ClientList, int id)
        {
            try
            {

                var Client = ClientList[id - 1];
                if (Client != null)
                {
                    ClientList.Remove(ClientList[id - 1]);
                    CheckClientList.Remove(ClientList[id - 1]);
                }
            }
            catch (Exception)
            {

            }
        }
        public static void Update(ref List<Client> ClientList, int id, string name, decimal balance)
        {
            try
            {

                var Client = ClientList[id - 1];
                if (Client != null)
                {

                    ClientList[id - 1].Name = name;
                    ClientList[id - 1].Balance = balance;
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Client with this id doesnt exists!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
    public class Client
    {
        public int ID { get; set; }
        public decimal Balance { get; set; }
        public string Name { get; set; }
    }
}
