using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;


public class Elf : IRace
{
    public string FirstFeature => "Получай уровень за каждого монстра, которого помог убить";

    public string SecondFeature => "У тебя +1 на смывку";
}

public class Human : IRace
{
    public string FirstFeature => "";

    public string SecondFeature => "";
}

public class Dwarf : IRace
{
    public string FirstFeature => "У тебя может быть любое количество больших шмоток";
    public string SecondFeature => "В конце хода можешь оставлять на руке 6 карт";
}
public class Halfling : IRace
{
    public string FirstFeature => "Раз в ход можешь продать одну шмотку за двойную цену(и другие по обычной)";

    public string SecondFeature => "Провалив первый бросок смывки, можешь сбросить карту для второйпопытки";
}

