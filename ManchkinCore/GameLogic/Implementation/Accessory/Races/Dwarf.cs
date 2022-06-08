using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class Dwarf : IRace
{
    public int FlushingBonus => 0;
    public int CardCount => 6;
    public bool CellingByDoublePrice => false;
    public string TextRepresentation => "дворф";

    public List<string> Descriptions { get; } = new List<string>
    {
        FirstFeature, SecondFeature
    };

    private static string FirstFeature => "У тебя может быть любое количество больших шмоток";
    private static string SecondFeature => "В конце хода можешь оставлять на руке 6 карт";
}