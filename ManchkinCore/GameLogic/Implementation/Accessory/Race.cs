using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;


public class Elf : IRace
{
    public int FlushingBonus => 1;
    public int CardCount => 5;
    public bool CellingByDoublePrice => false;
    public List<string> Descriptions { get; } = new List<string>
    {
        FirstFeature, SecondFeature
    };

    private static string FirstFeature => "Получай уровень за каждого монстра, которого помог убить";

    private static string SecondFeature => "У тебя +1 на смывку";
}

public class Human : IRace
{
    public int FlushingBonus => 0;
    public int CardCount => 5;
    public bool CellingByDoublePrice => false;
    public List<string> Descriptions { get; } = new List<string>();
}

public class Dwarf : IRace
{
    public int FlushingBonus => 0;
    public int CardCount => 6;
    public bool CellingByDoublePrice => false;
    public List<string> Descriptions { get; } = new List<string>
    {
        FirstFeature, SecondFeature
    };
    private static  string FirstFeature => "У тебя может быть любое количество больших шмоток";
    private static string SecondFeature => "В конце хода можешь оставлять на руке 6 карт";
}
public class Halfling : IRace
{
    public int FlushingBonus => 0;
    public int CardCount => 5;
    public bool CellingByDoublePrice => true;
    public List<string> Descriptions { get; } = new List<string>
    {
        FirstFeature, SecondFeature
    };
    
    private static string FirstFeature => "Раз в ход можешь продать одну шмотку за двойную цену(и другие по обычной)";

    private static string SecondFeature => "Провалив первый бросок смывки, можешь сбросить карту для второйпопытки";
}

