using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Warsztat_1
{
	class Program
	{
		static void Main(string[] args)
		{
			List<TaskModel> list = new List<TaskModel>();

			do
			{
				ConsoleEx.WriteLine("Menu: ", ConsoleColor.Green);
				ConsoleEx.WriteLine("1. ADD - Dodanie zadania", ConsoleColor.Green);
				ConsoleEx.WriteLine("2. REMOVE - Usunięcie zadania", ConsoleColor.Green);
				ConsoleEx.WriteLine("3. SHOW - Wyświetlenie wszystkich zadań", ConsoleColor.Green);
				ConsoleEx.WriteLine("4. SAVE - Zapis zadania do pliku Data.csv", ConsoleColor.Green);
				ConsoleEx.WriteLine("5. LOAD - Wczytanie zadania z pliku newTaskFile.csv", ConsoleColor.Green);
				Console.WriteLine();

				ConsoleEx.Write("Podaj komendę: ", ConsoleColor.Green);
				string command = Console.ReadLine();

				if (command == "exit")
				{
					break;
				}
				if (command.ToLower().Trim(' ') == "add")
				{
					AddTask(list);
				}
				else if (command.ToLower().Trim(' ') == "remove")
				{
					RemoveTask(list);
				}
				else if (command.ToLower().Trim(' ') == "show")
				{
					ShowAll(list);
				}
				else if (command.ToLower().Trim(' ') == "save")
				{
					SaveTasks(list);
				}
				else if (command.ToLower().Trim(' ') == "load")
				{
					LoadFile(list);
				}
				else
				{
					ConsoleEx.Write("Nie rozpoznano polecenia, nacisnij dowolny przycisk aby spróbować ponownie", ConsoleColor.Red);
					Console.ReadKey();
					Console.Clear();
				}
			} while (true);
		}

		private static void LoadFile(List<TaskModel> list)
		{
			string taskDescription;
			DateTime startDate;
			DateTime? endDate;
			bool isAllDay;
			bool isImportant;


			string file = "newTasksFile.csv";
			bool exists = File.Exists(file);


			if (exists)
			{
				string[] loader = File.ReadAllLines("newTasksFile.csv");

				foreach (string s in loader)
				{
					string[] loaderData = s.Split(',');
					taskDescription = loaderData[0];

					startDate = Convert.ToDateTime(loaderData[1]);

					if (loaderData[2] == "")
					{
						endDate = null;
					}
					else
					{
						endDate = Convert.ToDateTime(loaderData[2]);
					}

					loaderData[3] = Convert.ToString(loaderData[3]);
					isAllDay = Convert.ToBoolean(loaderData[3]);
					loaderData[4] = Convert.ToString(loaderData[4]);
					isImportant = Convert.ToBoolean(loaderData[4]);

					list.Add(new TaskModel(taskDescription, startDate, endDate, isAllDay, isImportant));

					list.OrderBy(item => item.StartDate).ThenBy(item => item.IsImportant);
				}

			}
			else
			{
				ConsoleEx.WriteLine("Plik Nie istnieje! nacisnij dowolny przycisk aby spróbować ponownie", ConsoleColor.Red);
				Console.ReadKey();
				Console.Clear();
			}
		}

		private static void SaveTasks(List<TaskModel> list)
		{
			List<string> saver = new List<string>();

			foreach (TaskModel s in list)
			{
				saver.Add($"{s.TaskDescription},{s.StartDate},{s.EndDate},{s.IsAllDay},{s.IsImportant}");
			}
			File.WriteAllLines("Data.csv", saver);
		}

		private static void ShowAll(List<TaskModel> list)
		{
			Console.Clear();
			ConsoleEx.WriteLine("AKTYWNE ZADANIA", ConsoleColor.Cyan);
			ConsoleEx.WriteLine("".PadLeft(107, '_'), ConsoleColor.Cyan);
			ConsoleEx.WriteLine($"{"|ID".PadRight(5)}{"|Opis".PadRight(25)} {"|Data rozpoczęcia".PadRight(25)} {"|Data zakończenia".PadRight(25)} {"|Całodniowe".PadRight(12)} {"|Priorytet".PadLeft(10)}|", ConsoleColor.Cyan);

			foreach (TaskModel s in list)
			{
				ConsoleEx.WriteLine($"|{(list.IndexOf(s) + 1).ToString().PadRight(4)}|{s.TaskDescription.PadRight(25)}|{s.StartDate.ToString().PadRight(25)}|{s.EndDate.ToString().PadRight(25)}|{s.IsAllDay.ToString().PadRight(12)}|{s.IsImportant.ToString().PadRight(9)}|", ConsoleColor.Red);
			}
			ConsoleEx.WriteLine("Nacisnij dowolny przycisk aby powrócić do menu", ConsoleColor.Green);
			Console.ReadKey();
			Console.Clear();
		}

		private static void RemoveTask(List<TaskModel> list)
		{
			while (true)
			{
				ConsoleEx.WriteLine("Podaj ID zadania które ma zostać usunięte: ", ConsoleColor.Red);
				int remove = int.Parse(Console.ReadLine());
				try
				{
					list.RemoveAt(remove - 1);
					break;
				}
				catch (System.ArgumentOutOfRangeException exception)
				{
					Console.Clear();
					ConsoleEx.WriteLine("Nie znaleziono ID, nacisnij dowolny przycisk aby powrócić do menu", ConsoleColor.Red);
					Console.ReadKey();
					Console.Clear();
					break;
				}


			}

		}

		private static void AddTask(List<TaskModel> list)
		{
			string taskDescription;
			DateTime startDate = DateTime.Now;
			DateTime? endDate = null;
			bool isAllDay = false;
			bool isImportant = false;

			ConsoleEx.WriteLine("Podaj opis zadania: ", ConsoleColor.Blue);
			taskDescription = Console.ReadLine();

			ConsoleEx.WriteLine("Podaj datę rozpoczęcia zadania (lub nacisnij Enter aby ustawic datę dzisiejszą) Dopuszczalny format MM/DD/RRRR): ", ConsoleColor.Blue);

			while (true)
			{
				try
				{
					string startDateText = Console.ReadLine().ToString(CultureInfo.InvariantCulture);
					if (startDateText == "")
						startDateText = DateTime.Now.ToString();
					startDate = Convert.ToDateTime(startDateText);
					break;
				}
				catch (System.FormatException exception)
				{
					Console.Clear();
					ConsoleEx.WriteLine("Podany tekst nie odpowiada formatowi daty, spróbuj ponownie",
						ConsoleColor.Red);
					Console.ReadKey();
				}
			}

			ConsoleEx.WriteLine("Czy jest to zadanie całodniowe? [T/N] ", ConsoleColor.Blue);
			do
			{
				string decision = Console.ReadLine().ToLower();
				if (decision == "t")
				{
					isAllDay = true;
					break;
				}
				if (decision == "n")
				{
					do
					{
						ConsoleEx.WriteLine("Podaj datę planowanego zakończenia zadania (MM/DD/RRRR): ",
							ConsoleColor.Blue);
						string endDateText = Console.ReadLine().ToString(CultureInfo.InvariantCulture);

						if (endDateText == "")
						{
							Console.Clear();
							ConsoleEx.WriteLine("Nieprawidłowa Data, spróbuj ponownie", ConsoleColor.Red);
							endDateText = Console.ReadLine().ToString(CultureInfo.InvariantCulture);
						}
						else
						{
							try
							{
								endDate = Convert.ToDateTime(endDateText);
								break;
							}
							catch (System.FormatException exception)
							{
								Console.Clear();
								ConsoleEx.WriteLine("Podany tekst nie odpowiada formatowi daty, nacisnij dowolny przycisk aby spróbować ponownie", ConsoleColor.Red);
							}
						}


					} while (true);

					break;
				}
				ConsoleEx.WriteLine("Błędna komenda, dopuszczalne odpowiedzi: [T/N]", ConsoleColor.Red);

			} while (true);

			ConsoleEx.WriteLine("Czy zaznaczyć zadanie jako ważne? [T/N]", ConsoleColor.Blue);
			if (Console.ReadLine().ToLower() == "t")
			{
				isImportant = true;
			}

			list.Add(new TaskModel(taskDescription, startDate, endDate, isAllDay, isImportant));

			list.OrderBy(item => item.StartDate).ThenBy(item => item.IsImportant);

			Console.Clear();
		}
	}
}