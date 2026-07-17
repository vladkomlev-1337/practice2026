using task17;
using System.Threading;
namespace task17tests;

public class ServerThreadTests
{
    public class TestCommand : ICommand
    {
        public bool Executed {get; set;}
        public void Execute()
        {
            Executed = true;
        }
    }
    public class ErrorCommand : ICommand
    {
        public void Execute()
        {
            throw new Exception("error test");
        }
    }
    [Fact]
    public void HardStop_ShouldStop()
    {
        var server = new ServerThread();
        server.Start();
        TestCommand command1 = new TestCommand();
        TestCommand command2 = new TestCommand();
        HardStopCommand hardstop = new HardStopCommand();
        server.AddCommand(command1);
        server.AddCommand(hardstop);
        server.AddCommand(command2);

        server.Join();
        Assert.True(command1.Executed);
        Assert.False(command2.Executed);
    }
    [Fact]
    public void SoftStop_AllComandShouldExecute()
    {
        var server = new ServerThread();
        server.Start();
        TestCommand command1 = new TestCommand();
        TestCommand command2 = new TestCommand();
        SoftStopCommand softstop = new SoftStopCommand();
        server.AddCommand(command1);
        server.AddCommand(softstop);
        server.AddCommand(command2);
        server.Join();
        Assert.True(command1.Executed && command2.Executed);

    }
    [Fact]
 
    public void ExceptionHandler_ShouldCatchException()
    {
        ExceptionHandler.Reset();
        var server = new ServerThread();
        server.Start();
        ErrorCommand errorCommand1 = new ErrorCommand();
        ErrorCommand errorCommand2 = new ErrorCommand();
        HardStopCommand hardStop = new HardStopCommand();
        server.AddCommand(errorCommand1);
        server.AddCommand(errorCommand2);
        server.AddCommand(hardStop);
        server.Join();
        Assert.True(ExceptionHandler.GetException() != null);
        Assert.True(ExceptionHandler.GetCommand() != null);
        Assert.Equal(ExceptionHandler.GetErrorCount(), 2);
    }
}
