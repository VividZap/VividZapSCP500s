using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;

namespace SCP500s.SuperItems
{
    public class Scp500Shadow : CustomItem
    {
        public override uint Id { get; set; } = 1;
        public override string Name { get; set; } = "SCP500 Shadow";
        public override string Description { get; set; } =
            "SCP500 Shadow : if you eat this any one we cant see you like real shadow";
        public override ItemType Type { get; set; } = ItemType.SCP500;
        public override float Weight { get; set; } = 0.5f;
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
                eventArgs.Player.ShowHint("<color=#ec3a8b> [Now any one we cant see you - you have just 7s to back] </color>",4f);
                eventArgs.Player.EnableEffect(EffectType.Ghostly,7f);
            }
        }
    }
}

    
