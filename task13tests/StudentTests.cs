namespace task13tests;
using task13;

public class StudentTests
{
    [Fact]
    public void Serialize_ShouldGiveCorrectAnswer()
    {
        StudentService service = new StudentService();
        Student student = new Student
        {
            FirstName = "Petr",
            LastName = "Ivanov",
            BirthDate = new DateTime(2007, 7, 9),
            Grades = new List<Subject>
            {
                new Subject {Name = "Math", Grade = 5}
            }
        };
        string json = service.Serialize(student);
        Assert.Contains("Petr", json);
        Assert.Contains("Ivanov", json);
        Assert.Contains("09.07.2007", json);
    }
    [Fact]
    public void Deserialize_ShouldGiveCorrectAnswer()
    {
        StudentService service = new StudentService();
        string json = @"
        {
        ""FirstName"": ""Petr"",
        ""LastName"": ""Ivanov"",
        ""BirthDate"": ""09.07.2007"",
        ""Grades"": [{ ""Name"": ""Math"", ""Grade"": 5 }]
        
        
        
        }";
        var student = service.Deserialize(json);
        Assert.Equal("Petr", student.FirstName);
        Assert.Equal("Ivanov", student.LastName);
        Assert.Equal(new DateTime(2007, 7, 9), student.BirthDate);
        Assert.Equal("Math", student.Grades[0].Name);
        Assert.Equal(5, student.Grades[0].Grade);
    }
    [Fact]
    public void Serialize_IgnoreNull()
    {
        StudentService service = new StudentService();
        var student = new Student
        {
            FirstName = "Ivan",
            LastName = null,
            BirthDate = new DateTime(1991, 12, 26),
            Grades = null
        };
        string json = service.Serialize(student);
        Assert.DoesNotContain("LastName", json);
        Assert.DoesNotContain("Grades", json);
        Assert.Contains("FirstName", json);
    }
    [Fact]
    public void Deserialize_EmpyFirstName_ShouldThrowException()
    {
        StudentService service = new StudentService();
        string json = @"
        {
        ""FirstName"": """",
        ""LastName"": ""Ivanov"",
        ""BirthDate"": ""09.07.2007"",
        ""Grades"": []
        
        
        
        }";
        var exc = Assert.Throws<Exception>(() => service.Deserialize(json));
        Assert.Contains("Name or surname cant be empty", exc.Message);
    }
    [Fact]
    public void Deserialize_EmpyLastName_ShouldThrowException()
    {
        StudentService service = new StudentService();
        string json = @"
        {
        ""FirstName"": ""Petr"",
        ""LastName"": """",
        ""BirthDate"": ""09.07.2007"",
        ""Grades"": []
        
        
        
        }";
        var exc = Assert.Throws<Exception>(() => service.Deserialize(json));
        Assert.Contains("Name or surname cant be empty", exc.Message);
    }
    [Fact]
    public void Deserialize_FutureDate_ShouldThrowException()
    {
        StudentService service = new StudentService();
        string json = @"
        {
        ""FirstName"": ""Petr"",
        ""LastName"": ""Ivanov"",
        ""BirthDate"": ""09.07.2077"",
        ""Grades"": []
        
        
        
        }";
        var exc = Assert.Throws<Exception>(() => service.Deserialize(json));
        Assert.Contains("Birth date cant be in future", exc.Message);
    }
    [Fact]
    public void Deserialize_GradeHigherThanFive_ShoudThrowException()
    {
        StudentService service = new StudentService();
        string json = @"
        {
        ""FirstName"": ""Petr"",
        ""LastName"": ""Ivanov"",
        ""BirthDate"": ""09.07.2007"",
        ""Grades"": [{ ""Name"": ""Math"", ""Grade"": 777 }]
        
        
        
        }";
        
        var exc = Assert.Throws<Exception>(() => service.Deserialize(json));
        Assert.Contains("Grades cant be lower than 1 or higher than 5", exc.Message);
    }
    [Fact]
    public void Deserialize_GradeLowerThanOne_ShoudThrowException()
    {
        StudentService service = new StudentService();
        string json = @"
        {
        ""FirstName"": ""Petr"",
        ""LastName"": ""Ivanov"",
        ""BirthDate"": ""09.07.2007"",
        ""Grades"": [{ ""Name"": ""Math"", ""Grade"": -1 }]
        
        
        
        }";
        
        var exc = Assert.Throws<Exception>(() => service.Deserialize(json));
        Assert.Contains("Grades cant be lower than 1 or higher than 5", exc.Message);
    }
    [Fact]
    public void Save_ShouldSaveFile()
    {
        StudentService service = new StudentService();
        Student student = new Student
        {
            FirstName = "Petr",
            LastName = "Ivanov",
            BirthDate = new DateTime(2007, 7, 9),
            Grades = new List<Subject>
            {
                new Subject {Name = "Math", Grade = 5}
            }
        };
        string path = Path.Combine(Directory.GetCurrentDirectory(), "test.json");
        service.Save(student, path);
        Assert.True(File.Exists(path));
        string info = File.ReadAllText(path);
        Assert.Contains("Petr", info);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
    [Fact]
    public void Load_ShouldLoadFile()
    {
        StudentService service = new StudentService();
        Student student = new Student
        {
            FirstName = "Petr",
            LastName = "Ivanov",
            BirthDate = new DateTime(2007, 7, 9),
            Grades = new List<Subject>
            {
                new Subject {Name = "Math", Grade = 5}
            }
        };
        string path = Path.Combine(Directory.GetCurrentDirectory(), "test.json");
        service.Save(student, path);
        var loaded = service.Load(path);
        Assert.Equal(student.FirstName, loaded.FirstName);
        Assert.Equal(student.LastName, loaded.LastName);
        Assert.Equal(student.BirthDate, loaded.BirthDate);
        Assert.Equal(student.FirstName, loaded.FirstName);
    }
}
