using ManchkinCore.GameLogic.Interfaces.Accessory;

namespace ManchkinCore.GameLogic.Implementation.Accessory.Classes;

public class Nobody : IClass
{
    public List<string> Descriptions { get; } = new List<string>();
    public string TextRepresentation => "никто";
}