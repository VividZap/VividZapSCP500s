using System.Collections.Generic;
using System.Numerics;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;

namespace SCP500s.SuperItems;

public class SCP500_Xerneas : CustomItem
{
    public override uint Id { get; set; } = 7;
    public override string Name { get; set; } = "SCP500-Xerneas";
    public override string Description { get; set; } = "SCP500-Xerneas : if you eat this pill you teleport to a rendom place in Facility";
    public override float Weight { get; set; } = 1.5f;
    public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties();
    public override ItemType Type { get; set; } = ItemType.SCP500;
    protected override void SubscribeEvents()
    {
        Exiled.Events.Handlers.Player.UsedItem += UsedItem;
        Log.Debug("Subscribed");
        base.SubscribeEvents();
    }

    protected override void UnsubscribeEvents()
    {
        Exiled.Events.Handlers.Player.UsedItem += UsedItem;
        Log.Debug("Subscribed");
        base.SubscribeEvents();
    }
    private void UsedItem(UsedItemEventArgs eventArgs)
    {
        if (Check(eventArgs.Item))
        {
            eventArgs.Player.ShowHint(Main.Instance.Config.SCP500xerneas);
            eventArgs.Player.RandomTeleport(typeof(Room));
        }
    }
}

