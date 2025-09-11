using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using Mirror;
using PlayerRoles;
using UnityEngine;

namespace SCP500s.SuperItems;

public class SCP500_47 : CustomItem
{
    public override uint Id { get; set; } = 12;
    public override string Name { get; set; } = "SCP500-47";
    public override string Description { get; set; } = "So this plugin you can spy any one";
    public override float Weight { get; set; } = 1.5f;
    public override ItemType Type { get; set; } = ItemType.SCP500;

  
    public override SpawnProperties? SpawnProperties { get; set; } = new()
    {
        Limit = 1,
        DynamicSpawnPoints = new List<DynamicSpawnPoint>
        {
            new()
            {
                Chance = 100,
                Location = SpawnLocationType.InsideHidUpper,
                
            },
            new()
            {
                Chance = 100,
                Location = SpawnLocationType.Inside330Chamber,
            },
        },
    };
    protected override void SubscribeEvents()
    {
        Exiled.Events.Handlers.Player.UsedItem += OnUsed; 
        Exiled.Events.Handlers.Map.PickupAdded += AddGlow;
        Log.Debug("SCP500_47 Subscribed");
        base.SubscribeEvents();
    }

    protected override void UnsubscribeEvents()
    {
        Exiled.Events.Handlers.Player.UsedItem -= OnUsed;
        Log.Debug("SCP500_47 Unsubscribed");
        base.UnsubscribeEvents();
    }

    private void OnUsed(UsedItemEventArgs eventArgs)
    {
        if (Check(eventArgs.Item))
        {
            eventArgs.Player.ShowHint(Main.Instance.Config.SCP500_47,3);
            List<RoleTypeId> roleType = new()
            {
                PlayerRoles.RoleTypeId.Scp049,
                PlayerRoles.RoleTypeId.Scp079,
                PlayerRoles.RoleTypeId.Scp096,
                PlayerRoles.RoleTypeId.Scp173,
                PlayerRoles.RoleTypeId.Scp3114,
                PlayerRoles.RoleTypeId.Scp106
            };
            int GiveRole = Random.Range(0, roleType.Count);
            RoleTypeId RoleTypeId = roleType[GiveRole];
            eventArgs.Player.ChangeAppearance(RoleTypeId);
        }
    }
    public Color glowColor = new Color32(0xFF, 0x69, 0xB4, 0xFF);

    private Dictionary<Exiled.API.Features.Pickups.Pickup, Exiled.API.Features.Toys.Light> ActiveLights = [];
    
    public void RemoveGlow(PickupDestroyedEventArgs ev)
    {
        if (Check(ev.Pickup))
        {
            if (ev.Pickup != null)
            {
                if (ev.Pickup?.Base?.gameObject == null) return;
                if (TryGet(ev.Pickup.Serial, out CustomItem ci) && ci != null)
                {
                    if (ev.Pickup == null || !ActiveLights.ContainsKey(ev.Pickup)) return;
                    Exiled.API.Features.Toys.Light light = ActiveLights[ev.Pickup];
                    if (light != null && light.Base != null)
                    {
                        NetworkServer.Destroy(light.Base.gameObject);
                    }
                    ActiveLights.Remove(ev.Pickup);
                }
            }
        }

    }
    public void AddGlow(PickupAddedEventArgs ev)
    {
        if (Check(ev.Pickup) && ev.Pickup.PreviousOwner != null)
        {
            if (ev.Pickup?.Base?.gameObject == null) return;
            TryGet(ev.Pickup, out CustomItem ci);
            Log.Debug($"Pickup is CI: {ev.Pickup.Serial} | {ci.Id} | {ci.Name}");

            var light = Exiled.API.Features.Toys.Light.Create(ev.Pickup.Position);
            light.Color = glowColor;

            light.Intensity = 0.7f;
            light.Range = 0.5f;
            light.ShadowType = LightShadows.None;

            light.Base.gameObject.transform.SetParent(ev.Pickup.Base.gameObject.transform);
            ActiveLights[ev.Pickup] = light;
        }
    }
}
