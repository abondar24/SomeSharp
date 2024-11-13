using Basics.Lang;

namespace Basics.Command;

public class CalculatorCommand : BasicsCommand
{

    public override void Execute()
    {
        _langBasics.Calculator();
    }
}