using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using GameCore;
using MEC;
using Mirror;
using UnityEngine;
using Log = Exiled.API.Features.Log;

namespace SCP500s.SuperItems;

public class Scp500Santa : CustomItem
{
    public override uint Id { get; set; } = 5;
    public override string Name { get; set; } =  "SCP500-Santa";
    public override string Description { get; set; } = "SCP500-Santa : If you eat this we give you a rendom weapon or item we use this";
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
                Location = SpawnLocationType.InsideIntercom,
            },
            new()
            {
                Chance = 40,
                Location = SpawnLocationType.InsideLczCafe,
            },
        },
    };
    protected override void SubscribeEvents()
    {
        Exiled.Events.Handlers.Player.UsedItem += UsedItem;
        Exiled.Events.Handlers.Map.PickupAdded += AddGlow;
        Exiled.Events.Handlers.Map.PickupDestroyed += RemoveGlow;

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
            var item = eventArgs.Item;
            var items = eventArgs.Player.Items.ToList();
            Timing.CallDelayed(0.1f, () => eventArgs.Player.CurrentItem = items.RandomItem());
            eventArgs.Player.ShowHint(Main.Instance.Config.SCP500Santa);
            eventArgs.Player.AddItem(Main.Instance.Config.Items);
            Main.Instance.Config.Items = new List<ItemType>()
            {
                ItemType.ArmorHeavy,
                ItemType.GrenadeHE,
                ItemType.GunAK,
                ItemType.GunE11SR,
                ItemType.Adrenaline,
                ItemType.KeycardO5,
                ItemType.Radio

            };
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



