namespace ManchkinCore.GameLogic.Interfaces.Manchkin;

public interface IDescriptable
{
    public List<string> Descriptions { get; }
    public string TextRepresentation { get; }
}