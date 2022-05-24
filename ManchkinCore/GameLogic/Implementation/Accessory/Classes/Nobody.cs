using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class Nobody : IClass
{
    public List<string> Descriptions { get; } = new List<string>();
    public string TextRepresentation => "никто";
}