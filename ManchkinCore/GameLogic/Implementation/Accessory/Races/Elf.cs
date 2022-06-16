using ManchkinCore.GameLogic.Interfaces.Accessory;

namespace ManchkinCore.GameLogic.Implementation.Accessory.Races;

public class Elf : IRace
{
    public int FlushingBonus => 1;
    public int CardCount => 5;
    public bool CellingByDoublePrice => false;

    public List<string> Descriptions { get; } = new List<string>
    {
        FirstFeature, SecondFeature
    };

    public string TextRepresentation => "эльф";

    private static string FirstFeature => "Получай уровень за каждого монстра, которого помог убить";

    private static string SecondFeature => "У тебя +1 на смывку";
}