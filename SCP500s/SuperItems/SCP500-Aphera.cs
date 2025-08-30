using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using UnityEngine;

namespace SCP500s.SuperItems;

public class SCP500_Aphera : CustomItem
{
    public override uint Id { get; set; } = 9;
    public override string Name { get; set; } =  "SCP500-Aphera";
    public override string Description { get; set; } = "SCP500-Aphera : if you eat this you rotrate your head in ground";
    public override float Weight { get; set; } = 1.5f;
    public override SpawnProperties SpawnProperties { get; set; } =  new SpawnProperties();
    public override ItemType Type { get; set; } =  ItemType.SCP500;
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
            eventArgs.Player.ShowHint(Main.Instance.Config.SCP500ops);
            var player = eventArgs.Player;
            player.Rotation = Quaternion.Euler(0, 180, 0) * player.Rotation;
            eventArgs.Player.ShowHint(Main.Instance.Config.SCP500aphera);
        }
    }
    
}
