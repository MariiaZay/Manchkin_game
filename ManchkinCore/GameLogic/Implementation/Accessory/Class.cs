using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class Cleric : IClass
{
    public List<string> Descriptions{ get; } = new List<string>
    {
        FirstFeature, SecondFeature
    };

    private static string FirstFeature => "Можешь сбросить до трех карт поле броска смывки: каждая даст +1 на смывку";

    private static string SecondFeature
        => "Можешь сбросить всю руку (не меньше трех карт!), чтобы усмирить одного монстра " +
           "в бою. Сбрось его. Он отдаст тебе свои сокровища, но не даст уровень. " +
           "С другими монстрами этого боя придется биться";
}

public class Warrior : IClass
{
    public List<string> Descriptions{ get; } = new List<string>
    {
        FirstFeature, SecondFeature
    };

    private static string FirstFeature => "Можешь сбросить до трех карт в бою. Каждая даст тебе бонус +1";

    private static string SecondFeature => "ты побеждаешь при равенстве сил в бою";
}

public class Thief : IClass
{
    public List<string> Descriptions{ get; } = new List<string>
    {
        FirstFeature, SecondFeature
    };

    private static string FirstFeature
        => "Можешь сбросить карту, чтобы подрезать другого манчкина в бою (-2 к боевой силе). " +
           "Одну жертву ты не в праве подрезать больше раза за бой, но можно подрезать помощника";

    private static string SecondFeature
        => "Можешь сбросить карту и бросить кубик, чтобы попытаться украсть у соперника мелкую шмотку. На 4 и больше " +
           "кража удалась. На три и меньше теб мутузят, потеряй уровень";
}

public class Wizard : IClass
{
    public List<string> Descriptions{ get; } = new List<string>
    {
        FirstFeature, SecondFeature
    };

    private static string FirstFeature
        => "Когда положено тянуть карты в открытую, можешь вместо всех или некоторых брать верхние карты " +
           "из соответствующего сброса. Затем за каждую воскрешенную карту сбрось карту с руки";

    private static string SecondFeature
        => "Можешь сбросить до трех карт в бою против андеда (андедов). Получи бонус +3 за каждую сброшенную карту";
}

public class Nobody : IClass
{
    public List<string> Descriptions{ get; } = new List<string>();
}

