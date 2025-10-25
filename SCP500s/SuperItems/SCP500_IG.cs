using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using PlayerRoles.PlayableScps.Scp049.Zombies;
using TMPro;
using Vector3 = UnityEngine.Vector3;

namespace SCP500s.SuperItems;

public class SCP500_IG : CustomItem
{
    public override uint Id { get; set; } = 842455;
    public override string Name { get; set; } = "SCP500-IG";
    public override string Description { get; set; } = "use this and see :)";
    public override float Weight { get; set; } = 1.5f;
    public override ItemType Type { get; set; } =  ItemType.SCP500;
    public override SpawnProperties? SpawnProperties { get; set; } = new()
    {
        Limit = 1,
        DynamicSpawnPoints = new List<DynamicSpawnPoint>
        {
            new()
            {
                Chance = 100,
                Location = SpawnLocationType.Inside127Lab,
            },
            new()
            {
                Chance = 100,
                Location = SpawnLocationType.Inside106Secondary,
            },
        },
    };
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
            eventArgs.Player.Position = Vector3.up * 50;
            if (eventArgs.Player.Position == Vector3.up * 50)
            {
                Timing.CallDelayed(5f, () => eventArgs.Player.Explode(ProjectileType.FragGrenade)
                );
            }
        }
    }

}
