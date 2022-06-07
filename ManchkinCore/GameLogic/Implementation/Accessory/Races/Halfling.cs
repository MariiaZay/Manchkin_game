using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class Halfling : IRace
{
    public int FlushingBonus => 0;
    public int CardCount => 5;
    public bool CellingByDoublePrice => true;
    public string TextRepresentation => "хаффлинг";

    public List<string> Descriptions { get; } = new List<string>
    {
        FirstFeature, SecondFeature
    };

    private static string FirstFeature => "Раз в ход можешь продать одну шмотку за двойную цену(и другие по обычной)";

    private static string SecondFeature => "Провалив первый бросок смывки, можешь сбросить карту для второй попытки";
}