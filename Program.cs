using System;
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
            string aidData = Program.ParseStudentAidData(lines);

            try {
                System.IO.File.WriteAllText(@"studentAidData.json", aidData);
                Console.WriteLine("Task complete.");
            }
            catch {
                Console.WriteLine("Problem writing file.");
            }
              
       } 
       private static string ParseStudentAidData(string[] lines)
       {
            StudentInfo student = StudentInfo.StudentFactory(lines);
            
            student.StudentLoans = Loan.CovertLoans(Loan.CreateList(lines));
            string jsonString = JsonSerializer.Serialize(student, new JsonSerializerOptions { WriteIndented = true });
            
            return jsonString;
       }
    }
}