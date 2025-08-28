using Exiled.API.Enums;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;

namespace SCP500s.SuperItems;

public class Scp500Ops : CustomItem
{
    public override uint Id { get; set; } = 3;
    public override string Name { get; set; } = "SCP500 Traker";

    public override string Description { get; set; } =
        "SCP500_Ops : for eat this you can see All players like use SCP-1344";

    public override ItemType Type { get; set; } = ItemType.SCP500;

    public override float Weight { get; set; } = 1.5f;
    public override SpawnProperties SpawnProperties { get; set; }

    protected override void SubscribeEvents()
    {
        Exiled.Events.Handlers.Player.UsedItem += UsedItem;

        base.SubscribeEvents();
    }

    protected override void UnsubscribeEvents()
    {
        Exiled.Events.Handlers.Player.UsedItem -= UsedItem;

        base.UnsubscribeEvents();
    }

    private void UsedItem(UsedItemEventArgs eventArgs)
    {
        if (Check(eventArgs.Item))
        {
            eventArgs.Player.EnableEffect(EffectType.Scp1344, 15f);
            eventArgs.Player.ShowHint("<color=#8E7CC3> [Now you can see all players and enemys in this game] </color>",
                5f);

        }
    }
    
}



