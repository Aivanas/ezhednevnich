using ezhednevnich;
using Newtonsoft.Json;
using System;



Console.WriteLine("Ежедневник");
Console.WriteLine();



bool Ngr_is_working = true;
int plused_time = 0;



Task_check(plused_time);

static void Task_check(int plused_time)
{
    string text = File.ReadAllText("jsonvalidator.json");
    List<Note> notes = JsonConvert.DeserializeObject<List<Note>>(text);
    
    int choosed_task = 1;
    List<string> tasks_list = new List<string>();
    DateTime date = DateTime.Now.AddDays(plused_time);
    string day = date.ToString("dd");
    string month = date.ToString("MM");
    string year = date.ToString("yyyy");
    for (int i = 0; i < (notes.Count); i++)
    {
        Note note = notes[i];
        if ((note.Day == day) & (note.Month == month) & (note.Year == year))
        {
            tasks_list.Add($"{note.Name} - {note.Hour}:{note.Minute}");
        }
    }
    Tasks_output(tasks_list, date, choosed_task, notes, plused_time);

}


static void Tasks_output(List<string> tasks_list, DateTime date, int choosed_task, List<Note> notes, int plused_time)
{

    
    int task_num = 0;
    Console.WriteLine($"Выбранная дата: {date.ToString("dd.MM.yyyy")}");
    Console.WriteLine("На этот день вы запланировали: ");
    Console.WriteLine(" ");
    for (int i = 0; i < tasks_list.Count; i++)
    {
        if (choosed_task == i + 1)
        {
            Console.WriteLine($"----> {i + 1}. {tasks_list[i]}");
            
        }
        else
        {
            Console.WriteLine($"      {i + 1}. {tasks_list[i]}");
        }
        task_num = i + 1;
        
    }
    if (choosed_task == task_num + 1 )
    {
        Console.WriteLine($"----> {task_num + 1}. Добавить новую запись.");
    }
    else
    {
        Console.WriteLine($"      {task_num + 1}. Добавить новую запись.");
    }
    
    ConsoleKeyInfo key = Console.ReadKey();

    if (key.Key == ConsoleKey.UpArrow)
    {
        if (choosed_task > 1)
        {
            choosed_task = choosed_task - 1;
            Console.Clear();
            Tasks_output(tasks_list, date, choosed_task, notes, plused_time);
        }
        else
        {
            Console.Clear();
            Tasks_output(tasks_list, date, choosed_task, notes, plused_time);
        }
 
    }

    if (key.Key == ConsoleKey.DownArrow)
    {
        if (choosed_task < tasks_list.Count+1)
        {
            choosed_task = choosed_task + 1;
            Console.Clear();
            Tasks_output(tasks_list, date, choosed_task, notes, plused_time);
        }
        else
        {
            Console.Clear();
            Tasks_output(tasks_list, date, choosed_task, notes, plused_time);
        }
    }

    if (key.Key == ConsoleKey.RightArrow)
    {
        plused_time = plused_time + 1;
        Console.Clear();
        Task_check(plused_time);
    }

    if (key.Key == ConsoleKey.LeftArrow)
    {

        plused_time = plused_time - 1;
        Console.Clear();
        Task_check(plused_time);
    }

    if (key.Key == ConsoleKey.Enter)
    {
        Description_output(notes, plused_time, choosed_task);
    }

}

static void Description_output(List<Note> notes, int plused_time, int choosed_task)
{
    List<string> descriptions_list = new List<string>();
    DateTime date = DateTime.Now.AddDays(plused_time);
    string day = date.ToString("dd");
    string month = date.ToString("MM");
    string year = date.ToString("yyyy");
    for (int i = 0; i < (notes.Count); i++)
    {
        Note note = notes[i];
        if ((note.Day == day) & (note.Month == month) & (note.Year == year))
        {
            descriptions_list.Add(note.Description);
        }
    }

    if ((choosed_task - 1) >= (descriptions_list.Count))
    {
        Add_new(date, plused_time, notes);
    }
    else
    {
        Console.WriteLine(" ");
        Console.WriteLine(descriptions_list[choosed_task - 1]);
        Console.ReadLine();
        Console.Clear();
    }
    Task_check(plused_time);
    
}


static void Add_new(DateTime date, int plused_time, List<Note> notes)
{
    int Json_count = notes.Count;

    
    Note note = new Note();
    Console.Write("Введите год: ");
    note.Year = Console.ReadLine();
    
    Console.Write("Введите номер месяца: ");
    note.Month = Console.ReadLine();
    
    Console.Write("Введите число: ");
    note.Day = Console.ReadLine();
   
    Console.Write("Введите часы: ");
    note.Hour = Console.ReadLine();
   
    Console.Write("Введите минуты: ");
    note.Minute = Console.ReadLine();
   
    Console.Write("Введите наименование: ");
    note.Name = Console.ReadLine();
    
    Console.Write("Введите описание: ");
    note.Description = Console.ReadLine();
    Console.Clear();

    notes.Add(note);
    string serilized = JsonConvert.SerializeObject(notes);
    File.WriteAllText("jsonvalidator.json", serilized);

    Task_check(plused_time);
}






