using Basics.Lang;

namespace Basics.Command;

public abstract class BasicsCommand : ICommand
{
    protected readonly LangBasics _langBasics;

    public BasicsCommand()
    {
        _langBasics = LangBasics.Instance;
    }

    public abstract void Execute();

}