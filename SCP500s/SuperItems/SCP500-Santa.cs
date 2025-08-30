using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using GameCore;
using MEC;
using UnityEngine;

namespace SCP500s.SuperItems;

public class Scp500Santa : CustomItem
{
    public override uint Id { get; set; } = 5;
    public override string Name { get; set; } =  "SCP500-Santa";
    public override string Description { get; set; } = "SCP500-Santa : If you eat this we give you a rendom weapon or item we use this";
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
    
}


