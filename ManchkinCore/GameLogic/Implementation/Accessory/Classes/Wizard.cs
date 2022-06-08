using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class Wizard : IClass
{
    public List<string> Descriptions { get; } = new List<string>
    {
        FirstFeature, SecondFeature
    };

    private static string FirstFeature
        => "Когда положено тянуть карты в открытую, можешь вместо всех или некоторых брать верхние карты " +
           "из соответствующего сброса. Затем за каждую воскрешенную карту сбрось карту с руки";

    private static string SecondFeature
        => "Можешь сбросить до трех карт в бою против андеда (андедов). Получи бонус +3 за каждую сброшенную карту";

    public string TextRepresentation => "волшебник";
}