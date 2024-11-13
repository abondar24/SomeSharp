namespace Basics.Command;

public class DrawShapeCommand : BasicsCommand
{

    public override void Execute()
    {
        _langBasics.DrawShape();
    }
}