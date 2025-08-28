using System.Collections.Generic;
using System.ComponentModel;
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
        public Scp500Super Scp500Super { get; set; } = new ();
        public List<Scp500Santa> scp500Santas { get; set; } = new();
        public List<Scp500Shadow> scp500Shadows { get; set; } = new();
        public List<Scp500Sonic>  scp500Sonics { get; set; } = new();
        public List<Scp500Super> scp500Supers { get; set; } = new();
        public List<Scp500Ops>  scp500ops { get; set; } = new();

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
   }
}
