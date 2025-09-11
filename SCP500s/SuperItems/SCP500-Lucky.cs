using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using Mirror;
using UnityEngine;
using Random = System.Random;

namespace SCP500s.SuperItems;

public class SCP500_Lucky : CustomItem
{
    public override uint Id { get; set; } = 11;
    public override string Name { get; set; } =  "SCP500-Lucky";
    public override string Description { get; set; } = "Here you need use all you lucky if you eat this we can give you good effect or bed so good luck";
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
                Location = SpawnLocationType.Inside049Armory,
            },
            new()
            {
                Chance = 100,
                Location = SpawnLocationType.InsideSurfaceNuke,
            },
        },
    };

    protected override void SubscribeEvents()
    {
        Exiled.Events.Handlers.Player.UsedItem += OnUsed;
        Exiled.Events.Handlers.Map.PickupAdded += AddGlow;
        Log.Debug("Lucky subscribed");
        base.SubscribeEvents();
    }

    protected override void UnsubscribeEvents()
    {
        Exiled.Events.Handlers.Player.UsedItem -= OnUsed;
        Log.Debug("lucky Unsubscribed");
        base.UnsubscribeEvents();
    }

    private void OnUsed(UsedItemEventArgs eventArgs)
    {
        if (Check(eventArgs.Item))
        {
            eventArgs.Player.Health = 105;
        }
        Random random = new Random();
    
        bool isBeneficial = random.Next(0, 2) == 0;
    
        if (isBeneficial)
        {
            eventArgs.Player.ApplyRandomEffect(EffectCategory.Positive, 50, 4);
        }
        else
        {
            eventArgs.Player.ApplyRandomEffect(EffectCategory.Harmful, 50, 4);
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
