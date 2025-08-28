using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using UnityEngine;

namespace SCP500s.SuperItems;

public class Scp500Rakun :  CustomItem
{
    public override uint Id { get; set; } = 6;
    public override string Name { get; set; } =  "SCP500 Rakun";
    public override string Description { get; set; } = "SCP500 Rakun : Its a very special - if you eat this you have a small";
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
        Log.Debug("Unsubscribed");
        base.UnsubscribeEvents();
    }
    private void UsedItem(UsedItemEventArgs eventArgs)
    {
        if (Check(eventArgs.Item))
        {
            eventArgs.Player.ShowHint("<color=#87457A> [Wow good kid] </color>");
            eventArgs.Player.Scale *= 0.5f;
        }
    }
}



