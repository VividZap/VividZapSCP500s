
using System;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomItems;
using Exiled.CustomItems.API;
using Exiled.CustomItems.API.Features;
using Exiled.Events.Features;
using SCP500s.SuperItems;


namespace SCP500s
{
    public class Main : Plugin<Config>
    {
        public override string Name { get; } = "SCP500s";
        public override string Author { get;} = "VividZap";
        public override string Prefix { get; } = "SCP500s";
        public override Version Version { get; } = new Version(9, 8, 1);
        public override Version RequiredExiledVersion { get; } = new Version(9, 8, 1);
        public static Main Instance { get; set; } 
        
        public override void OnEnabled()
        {
            CustomItem.RegisterItems();
            new Scp500Super().Register();
            new Scp500Ops().Register();
            new Scp500Rakun().Register();
            new Scp500Santa().Register();
            new Scp500Shadow().Register();
            new Scp500Sonic().Register();
            Instance = this;
            Log.Info("Scp500s plugin loaded");
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            CustomItem.UnregisterItems();
            Log.Info("Scp500s plugin unloaded");
            base.OnDisabled();
        }
    }
}