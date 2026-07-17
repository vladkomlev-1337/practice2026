using task17;
namespace task17tests;

public class ServerThreadTests
{
    private class TestExceptionHandler : ExceptionHandler
    {
        public Exception _exception {get;set;}
        public ICommand _command {get;set;}
        public override void Handle(Exception exception, ICommand command)
        {
            _exception = exception;
            _command = command;
        }
    }
    public class TestCommand : ICommand
    {
        public bool Executed {get;set;}
        public void Execute() => Executed = true;
    }
    public class ErrorCommand : ICommand
    {
        public void Execute() => throw new Exception("test error");
    }
    [Fact]
    public void HardStopCommand_ShouldStop_ThenIgnoreComands()
    {
        var handler = new TestExceptionHandler();
        var serverThread = new ServerThread(handler);
        var command1 = new TestCommand();
        var hardStop = new HardStopCommand(serverThread);
        var command2 = new TestCommand();
        serverThread.QueueCommand(command1);
        serverThread.QueueCommand(hardStop);
        serverThread.QueueCommand(command2);
        serverThread.Start();
        Thread.Sleep(100);
        Assert.True(command1.Executed);
        Assert.False(command2.Executed);
        Assert.False(serverThread.IsAlive);
    }
    [Fact]
    public void SoftStopCommand_ShouldExecuteAllCommandsAndStop()
    {
        var handler = new TestExceptionHandler();
        var serverThread = new ServerThread(handler);
        var command1 = new TestCommand();
        var softStop = new SoftStopCommand(serverThread);
        var command2 = new TestCommand();
        serverThread.QueueCommand(command1);
        serverThread.QueueCommand(softStop);
        serverThread.QueueCommand(command2);
        serverThread.Start();
        Thread.Sleep(100);
        Assert.True(command1.Executed);
        Assert.True(command2.Executed);
        Assert.False(serverThread.IsAlive);
    }
    [Fact]
    public void Stop_ShouldThrowExcWhenWrongThread()
    {
        var handler = new TestExceptionHandler();
        var serverThread = new ServerThread(handler);
        var hardStop = new HardStopCommand(serverThread);
        Assert.Throws<InvalidOperationException>(() => hardStop.Execute());
    }
    [Fact]
    public void ExceptionHandler_ShouldCatchException()
    {
        var handler = new TestExceptionHandler();
        var serverThread = new ServerThread(handler);
        var errorcommand = new ErrorCommand();
        var softStop = new SoftStopCommand(serverThread);
        serverThread.QueueCommand(errorcommand);
        serverThread.QueueCommand(softStop);
        serverThread.Start();
        Thread.Sleep(100);
        Assert.NotNull(handler._exception);
        Assert.Equal("test error", handler._exception.Message);
        Assert.Same(errorcommand, handler._command);
    }
}
