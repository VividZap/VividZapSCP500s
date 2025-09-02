using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using Mirror;
using UnityEngine;
using Server = Exiled.Events.Handlers.Server;

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
    public override SpawnProperties? SpawnProperties { get; set; } = new()
    {
        Limit = 1,
        DynamicSpawnPoints = new List<DynamicSpawnPoint>
        {
            new()
            {
                Chance = 50,
                Location = SpawnLocationType.InsideGateA,
            },
            new()
            {
                Chance = 40,
                Location = SpawnLocationType.InsideHczArmory,
            },
        },
    };              
    protected override void SubscribeEvents()
    {
        Exiled.Events.Handlers.Player.UsedItem += UsedItem;
        Exiled.Events.Handlers.Map.PickupAdded += AddGlow;
        Exiled.Events.Handlers.Map.PickupDestroyed += RemoveGlow;
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
    
    public Color glowColor = new Color32(0x00, 0xFF, 0x00, 0xFF);

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


