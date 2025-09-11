using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using ItemGlow;
using Mirror;
using UnityEngine;

namespace SCP500s.SuperItems;

public class SCP500_Aphera : CustomItem
{
    public override uint Id { get; set; } = 9;
    public override string Name { get; set; } =  "SCP500-Aphera";
    public override string Description { get; set; } = "SCP500-Aphera : if you eat this you rotrate your head in ground";
    public override float Weight { get; set; } = 1.5f;
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
    public override ItemType Type { get; set; } =  ItemType.SCP500;
    protected override void SubscribeEvents()
    {
        Exiled.Events.Handlers.Player.UsedItem += UsedItem;
        Exiled.Events.Handlers.Map.PickupAdded += AddGlow;
        Exiled.Events.Handlers.Map.PickupDestroyed += RemoveGlow;
        Log.Debug("Subscribed");
        base.SubscribeEvents();
    }

    
    protected override void UnsubscribeEvents()
    {
        Exiled.Events.Handlers.Map.PickupAdded -= AddGlow;
        Exiled.Events.Handlers.Map.PickupDestroyed -= RemoveGlow;
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
            eventArgs.Player.CameraTransform.rotation = player.Rotation;
            eventArgs.Player.ShowHint(Main.Instance.Config.SCP500aphera);
        }
    }
    public Color glowColor = new Color32(0x31, 0x33, 0x00, 0x01);
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
