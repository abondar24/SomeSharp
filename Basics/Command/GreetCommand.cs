namespace Basics.Command;

public class GreetCommand : BasicsCommand
{

    public override void Execute()
    {
        _langBasics.Greet();
    }
}