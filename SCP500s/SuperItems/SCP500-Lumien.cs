using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using Mirror;
using PlayerRoles;
using UnityEngine;

namespace SCP500s.SuperItems;

public class SCP500_Lumien : CustomItem
{
    // Lumien idea
    public override uint Id { get; set; } = 8000;
    public override string Name { get; set; } = "SCP500 Lumien";
    public override string Description { get; set; } = "Take this pill to instantly betray your teammates.";
    public override float Weight { get; set; } = 1.5f;
    public override ItemType Type { get; set; } = ItemType.SCP500;

    public override SpawnProperties SpawnProperties { get; set; } = new()
    {
        Limit = 2,
        DynamicSpawnPoints = new List<DynamicSpawnPoint>
        {
            new()
            {
                Chance = 100,
                Location = SpawnLocationType.Inside173Gate,
            },
            new()
            {
                Chance = 100,
                Location = SpawnLocationType.InsideGr18Glass,
            },
        },
    };

    protected override void SubscribeEvents()
    {
        Exiled.Events.Handlers.Player.UsedItem += OnUsedItem;
        Exiled.Events.Handlers.Map.PickupAdded += AddGlow;
        Exiled.Events.Handlers.Map.PickupDestroyed += RemoveGlow;
        base.SubscribeEvents();
    }

    protected override void UnsubscribeEvents()
    {
        Exiled.Events.Handlers.Player.UsedItem -= OnUsedItem;
        Exiled.Events.Handlers.Map.PickupAdded -= AddGlow;
        Exiled.Events.Handlers.Map.PickupDestroyed -= RemoveGlow;
        Log.Debug("Unsubscribed");
        base.UnsubscribeEvents();
    }
    private void OnUsedItem(UsedItemEventArgs ev)
    {
        if (Check(ev.Item))
        {
            RoleTypeId newRole = RoleTypeId.None;

            switch (ev.Player.Role.Type)
            {
                case RoleTypeId.ClassD:
                    newRole = RoleTypeId.Scientist;
                    break;
                case RoleTypeId.Scientist:
                    newRole = RoleTypeId.ClassD;
                    break;
                case RoleTypeId.FacilityGuard:
                    newRole = RoleTypeId.ClassD;
                    break;
                case RoleTypeId.ChaosConscript:
                    newRole = RoleTypeId.NtfSpecialist;
                    break;
                case RoleTypeId.NtfSpecialist:
                    newRole = RoleTypeId.ChaosConscript;
                    break;
                case RoleTypeId.ChaosRifleman:
                    newRole = RoleTypeId.NtfPrivate;
                    break;
                case RoleTypeId.NtfPrivate:
                    newRole = RoleTypeId.ChaosRifleman;
                    break;
                case RoleTypeId.ChaosMarauder:
                    newRole = RoleTypeId.NtfSergeant;
                    break;
                case RoleTypeId.NtfSergeant:
                    newRole = RoleTypeId.ChaosMarauder;
                    break;
                case RoleTypeId.ChaosRepressor:
                    newRole = RoleTypeId.NtfCaptain;
                    break;
                case RoleTypeId.NtfCaptain:
                    newRole = RoleTypeId.ChaosRepressor;
                    break;
                default:
                    newRole = RoleTypeId.ClassD;  
                    break;
            }

            
            ev.Player.Role.Set(newRole, RoleSpawnFlags.None);
            ev.Player.ShowHint(Main.Instance.Config.SCP500Lumien);
        }
    }
    
    public Color glowColor = new Color32(0xFF, 0x00, 0xFF, 0xFF);
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
