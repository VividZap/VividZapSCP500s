using Exiled.API.Enums;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;


namespace SCP500s.SuperItems;

public class Scp500Sonic : CustomItem
{
    public override uint Id { get; set; } = 2;
    public override string Name { get; set; } = "SCP500 Sonic";
    public override string Description { get; set; } = "SCP500 Sonic : if you eat this You will become very fast like sonic";
    public override float Weight { get; set; } = 1.5f;
    public override ItemType Type { get; set; } = ItemType.SCP500;
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
            eventArgs.Player.EnableEffect(EffectType.MovementBoost, 100f);
            eventArgs.Player.ShowHint("<color=#198C19> [For eat this you have a very lot speed so this effect we finish after 10 s !!Ruuuuuun!!] </color>");
        }
    }
}




