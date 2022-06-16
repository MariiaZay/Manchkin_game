using ManchkinCore.GameLogic.Interfaces.Accessory;

namespace ManchkinCore.GameLogic.Implementation.Accessory.Classes;

public class Thief : IClass
{
    public List<string> Descriptions { get; } = new List<string>
    {
        FirstFeature, SecondFeature
    };

    private static string FirstFeature
        => "Можешь сбросить карту, чтобы подрезать другого манчкина в бою (-2 к боевой силе). " +
           "Одну жертву ты не в праве подрезать больше раза за бой, но можно подрезать помощника";

    private static string SecondFeature
        => "Можешь сбросить карту и бросить кубик, чтобы попытаться украсть у соперника мелкую шмотку. На 4 и больше " +
           "кража удалась. На три и меньше теб мутузят, потеряй уровень";

    public string TextRepresentation => "вор";
}