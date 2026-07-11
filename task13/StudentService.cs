namespace task13;
using System.Text.Json;
using System.Text.Json.Serialization;
public class StudentService
{
    public JsonSerializerOptions options = new JsonSerializerOptions();
    public StudentService()
    {
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.Converters.Add(new DateConverter());
    }
    public string Serialize(Student student)
    {
        return JsonSerializer.Serialize(student, options);
    }
    public Student Deserialize(string json)
    {
        Student student = JsonSerializer.Deserialize<Student>(json, options);
        if (string.IsNullOrWhiteSpace(student.FirstName) || string.IsNullOrWhiteSpace(student.LastName))
        {
            throw new Exception("Name or surname cant be empty");
        }
        if (student.BirthDate > DateTime.Now)
        {
            throw new Exception("Birth date cant be in future");
        } 
        if (student.Grades == null)
        {
            throw new Exception("Grades cant be empty");
        }
        foreach (var subj in student.Grades)
        {
            if (string.IsNullOrWhiteSpace(subj.Name))
            {
                throw new Exception("Name of subject cant be empty");
            }
            if (subj.Grade < 1 || subj.Grade > 5)
            {
                throw new Exception("Grades cant be lower than 1 or higher than 5");
            }
         }
        return student;
    }
    public void Save(Student student, string path)
    {
        File.WriteAllText(path, Serialize(student));
    }
    public Student Load(string path)
    {
        return Deserialize(File.ReadAllText(path));
    }
}