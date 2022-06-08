using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class Cleric : IClass
{
    public List<string> Descriptions { get; } = new List<string>
    {
        FirstFeature, SecondFeature
    };

    private static string FirstFeature => "Можешь сбросить до трех карт поле броска смывки: каждая даст +1 на смывку";

    private static string SecondFeature
        => "Можешь сбросить всю руку (не меньше трех карт!), чтобы усмирить одного монстра " +
           "в бою. Сбрось его. Он отдаст тебе свои сокровища, но не даст уровень. " +
           "С другими монстрами этого боя придется биться";

    public string TextRepresentation => "клирик";
}