using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;

namespace SCP500s.SuperItems;
// This special to YAMATO DEVLOPER EXILED ineed tell you like my brother and realy good man
// thx for Exiled devlopers to Creat this amazing EXILED 
public class Scp500Yamato : CustomItem
{
    public override uint Id { get; set; } = 8;
    public override string Name { get; set; } = "SCP500 Yamato";
    public override string Description { get; set; } = "If you eat this we have a more health";
    public override float Weight { get; set; } = 1.5f;
    public override ItemType Type { get; set; } = ItemType.SCP500;
    public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties();
    protected override void SubscribeEvents()
    {
        Exiled.Events.Handlers.Player.UsedItem += UsedItem;
        Log.Debug("Yamato Subscribed");
        base.SubscribeEvents();
    }

    protected override void UnsubscribeEvents()
    {
        Log.Debug("yamato Unsubscribed");
        base.UnsubscribeEvents();
    }
    private void UsedItem(UsedItemEventArgs eventArgs)
    {
        if (Check(eventArgs.Item))
        {
            eventArgs.Player.ShowHint(Main.Instance.Config.SCP500yamato);
            eventArgs.Player.Health = 150;
        }
    }
}

