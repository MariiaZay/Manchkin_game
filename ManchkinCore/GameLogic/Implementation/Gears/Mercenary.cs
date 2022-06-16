using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class Mercenary : IMercenary
{
    public IStuff? Item { get; private set; }

    public Mercenary() => Item = null;
    public Mercenary(IStuff? stuff) => Item = stuff;
    
    public void ChangeEquipment(IStuff? stuff) =>Item = stuff;
    public List<string> Descriptions { get;} = new List<string> {FirstFeature, SecondFeature};

    private const string FirstFeature = "Можешь нести и применять еще одну шмотку, даже если не имеешь на это право. " +
                                        "Если теряешь наемничка, то шмотку теряешь с ним";

    private const string SecondFeature = "Можешь сбросить наемничка вместо броска на смывку, " +
                                         "чтобы автоматически смыться от всех монстров в бою";

    public string TextRepresentation => "наемничек";
}