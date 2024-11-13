using Basics.Command;
using Basics.Lang;

namespace Basics.Command;

public class GuessCommand : BasicsCommand
{
    public override void Execute()
    {
        _langBasics.Guess();
    }
}