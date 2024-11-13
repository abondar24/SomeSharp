namespace Basics.Command;

public class GuessCommand : BasicsCommand
{
    public override void Execute()
    {
        _langBasics.Guess();
    }
}