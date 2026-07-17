using task17;
namespace task17tests;

public class ScheluderTests
{
    [Fact]
    public void ServerThread_ShouldExecuteInRoundRobinOrder()
    {
        var handler = new ExceptionHandler();
        var scheduler = new RoundRobin();
        var serverThread = new ServerThread(handler, scheduler);
        var commandA = new LongRunningCommand(serverThread, 3);
        var commandB = new LongRunningCommand(serverThread, 2);

        serverThread.QueueCommand(commandA);
        serverThread.QueueCommand(commandB);

        var softStop = new SoftStopCommand(serverThread);
        serverThread.QueueCommand(softStop);
        serverThread.Start();
        Thread.Sleep(100);
        Assert.Equal(3, commandA.ExecutionCount);
        Assert.True(commandA.IsCompleted);
        Assert.Equal(2, commandB.ExecutionCount);
        Assert.True(commandB.IsCompleted);
        Assert.False(serverThread.IsAlive);
    }
    [Fact]
    public void ServerThread_SoftStop_ShouldFinishLongCommand()
    {
        var handler = new ExceptionHandler();
        var scheduler = new RoundRobin();
        var serverThread = new ServerThread(handler, scheduler);
        var longCommand = new LongRunningCommand(serverThread, 5);
        serverThread.QueueCommand(longCommand);
        var softStop = new SoftStopCommand(serverThread);
        serverThread.QueueCommand(softStop);
        serverThread.Start();
        Thread.Sleep(100);
        Assert.Equal(5, longCommand.ExecutionCount);
        Assert.True(longCommand.IsCompleted);
        Assert.False(serverThread.IsAlive);
    }
    [Fact]
    public void ServerThread_ShouldExecuteCommandOneTime()
    {
        var handler = new ExceptionHandler();
        var scheduler = new RoundRobin();
        var serverThread = new ServerThread(handler, scheduler);
        var command = new LongRunningCommand(serverThread, 1);
        serverThread.QueueCommand(command);
        var softStop = new SoftStopCommand(serverThread);
        serverThread.QueueCommand(softStop);
        serverThread.Start();
        Thread.Sleep(50);
        Assert.Equal(1, command.ExecutionCount);
        Assert.True(command.IsCompleted);
        Assert.False(serverThread.IsAlive);
    }
}
