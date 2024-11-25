namespace ClubMember.Views;

public interface IView
{
    void RunView();

    IFieldValidator FieldValidator { get };
}