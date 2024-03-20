using System;
using System.Reflection;
using System.Text.Json;

namespace StudentAidData{
    class Program{
        static void Main(string[] args){

            if (args.Length <1)
            {
                Console.WriteLine("No file provided as argument.");
                return;
            }
            string filePath = args[0];
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"The file '{filePath}' does not exist.");
                return;
            }
            string[] lines = File.ReadAllLines(filePath);
            
            StudentInfo student = new();
            Type studentType = typeof(StudentInfo);
            foreach (var line in lines)
            {
                var parts = line.Split(new[] { ':' }, 2);
                string key = parts[0];
                string value = parts[1] != null ? parts[1] : "";

                PropertyInfo? propertyInfo = studentType.GetProperty(String.Concat(key.Where(c => !Char.IsWhiteSpace(c))), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null && propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(student, value);
                }
            }
            //Combine these two methods
            List<List<string>> loans = Loan.CreateList(lines);
            List<Loan> loanList = Loan.CovertLoans(loans);
            student.StudentLoans = loanList;
            string jsonString = JsonSerializer.Serialize(student, new JsonSerializerOptions { WriteIndented = true });
            try {
                System.IO.File.WriteAllText(@"studentAidData.json", jsonString);
                Console.WriteLine("Task complete.");
            }
            catch {
                Console.WriteLine("Problem writing file.");
            }
              
       }
    }
}