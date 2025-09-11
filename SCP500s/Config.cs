using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Interfaces;
using SCP500s.SuperItems;

namespace SCP500s
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } =  true;

        public bool Debug { get; set; } = false;

        [Description("This we activat all pills or SCP500s in Game with Customitems")]
        public Scp500Ops Scp500Ops { get; set; } = new();
        public Scp500Santa Santa { get; set; } = new ();
        public Scp500Shadow Scp500Shadow { get; set; } = new ();
        public Scp500Sonic Scp500Sonic { get; set; } = new ();
        public Scp500Rakun Scp500Rakun { get; set; } = new();
        public Scp500Yamato scp500Yamato { get; set; } = new();
        public Scp500Super Scp500Super { get; set; } = new ();
        public SCP500_Lucky scp500Lucky { get; set; } = new();
        public SCP500_47 scp500_47 { get; set; } = new();
        public List<SCP500_Lucky> SCP500_Lucky { get; set; } = new();
        public List<SCP500_47> Scp50047 { get; set; } = new();
        public List<Scp500Santa> scp500Santas { get; set; } = new();
        public List<Scp500Shadow> scp500Shadows { get; set; } = new();
        public List<Scp500Sonic>  scp500Sonics { get; set; } = new();
        public List<Scp500Super> scp500Supers { get; set; } = new();
        public List<Scp500Ops>  scp500ops { get; set; } = new();
        [Description("All Hints for SCP500s")]
        public string SCP500Rakun { get; set; } = "<color=#87457A> [Wow good kid] </color>";

        public string SCP500ops { get; set; } = "<color=#8E7CC3> [Now you can see all players and enemys in this game] </color>";

        public string SCP500Santa { get; set; } = "<color=#87457A> [Eat this for take random item] </color>";

        public string SCP500super { get; set; } = "<color= #ca9a19> [If you eat this its well give you a good effects Or Super effects we cant any one kill you] </color>";

        public string SCP500shadow { get; set; } = "<color=#ec3a8b> [Now any one we cant see you - you have just 7s to back] </color>";

        public string SCP500sonic { get; set; } = "<color=#198C19> [For eat this you have a very lot speed so this effect we finish after 10 s !!Ruuuuuun!!] </color>";

        public string SCP500aphera { get; set; } = "<color= #87457A> [WOW your head in the ground] </color>";

        public string SCP500xerneas { get; set; } = "<color=#ec3a8b> [You teleported now] </color>";

        public string SCP500yamato { get; set; } = "<color=#87457A> [More Health more kills XD] </color>";

        public string SCP500_47 { get; set; } = "<color=#198C19> [Now you Spy with another role XD] </color>";
         

        [Description("Items List we can take for use SCP500-santa")]
        public List<ItemType> Items { get; set; } = new List<ItemType>
        {
            ItemType.ArmorHeavy,
            ItemType.ArmorLight,
            ItemType.GunAK,
            ItemType.GunE11SR,
            ItemType.Adrenaline,
            ItemType.KeycardO5,
            ItemType.Radio
            
        };

        public List<RoomType> Room { get; set; } = new List<RoomType>()
        {
            RoomType.EzCafeteria,
            RoomType.EzCheckpointHallwayA,
            RoomType.EzCheckpointHallwayB,
            RoomType.EzChef,
            RoomType.EzGateB,
            RoomType.EzGateA,
            RoomType.Pocket,
            RoomType.LczArmory,
            RoomType.Hcz079
        };
    }
}
