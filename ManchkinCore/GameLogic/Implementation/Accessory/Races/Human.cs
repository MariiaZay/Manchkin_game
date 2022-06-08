using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class Human : IRace
{
    public int FlushingBonus => 0;
    public int CardCount => 5;
    public bool CellingByDoublePrice => false;
    public string TextRepresentation => "человек";
    public List<string> Descriptions { get; } = new List<string>();
}