using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using LabApi.Loader.Features.Plugins;

namespace SCP500s.SuperItems;

public class Scp500Super : CustomItem
{
    public override uint Id { get; set; } = 4;
    public override string Name { get; set; } = "Super SCP500";
    public override string Description { get; set; } = "Super SCP500 : its pill we have a crazy multi effects to help you in the game";
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
            eventArgs.Player.ShowHint(Main.Instance.Config.SCP500super);
            eventArgs.Player.EnableEffect(EffectType.Invisible, 7f);
            eventArgs.Player.EnableEffect(EffectType.MovementBoost, 50);
            eventArgs.Player.EnableEffect(EffectType.SilentWalk, 7f);
            eventArgs.Player.EnableEffect(EffectType.SeveredHands,1f);
        }

        SpawnProperties.StaticSpawnPoints = new List<StaticSpawnPoint>();
        {
            
        }
    }
}


