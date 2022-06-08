using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class Warrior : IClass
{
    public List<string> Descriptions { get; } = new List<string>
    {
        FirstFeature, SecondFeature
    };

    private static string FirstFeature => "Можешь сбросить до трех карт в бою. Каждая даст тебе бонус +1";

    private static string SecondFeature => "ты побеждаешь при равенстве сил в бою";
    public string TextRepresentation => "воин";
}