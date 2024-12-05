namespace PretendCompany.Commands;

public interface ICommand
{
    void Execute();

    void Execute(string queryParam);
}
