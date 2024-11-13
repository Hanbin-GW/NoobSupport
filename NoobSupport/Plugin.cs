using System;
using Exiled.API.Features;

namespace NoobSupport
{
    public class Plugin : Plugin<Config>
    {
        public override string Name { get; } = "Noob Support";
        public override string Author { get; } = "Hanbin-GW";
        public override Version Version { get; } = new Version(0, 6, 2);
        public static Plugin Instance { get; private set; }
        private EventHandlers EventHandlers { get; set; }
        public override void OnEnabled()
        {
            Instance = this;
            EventHandlers = new EventHandlers(this);
            Exiled.Events.Handlers.Player.ReceivingEffect += EventHandlers.OnEffectAdded;
            Exiled.Events.Handlers.Player.Hurting += EventHandlers.OnHurting;
            Exiled.Events.Handlers.Player.PickingUpItem += EventHandlers.OnPickingUpMicroHid;
            Exiled.Events.Handlers.Player.PickingUpItem += EventHandlers.OnPickingUpSCP207;
            Exiled.Events.Handlers.Player.ChangingItem += EventHandlers.OnEquipMicroHid;
            Exiled.Events.Handlers.Scp096.AddingTarget += EventHandlers.OnLookingAtScp096;
            Exiled.Events.Handlers.Item.ChargingJailbird += EventHandlers.OnChargingJailbird;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {            
            Exiled.Events.Handlers.Player.ReceivingEffect -= EventHandlers.OnEffectAdded;
            Exiled.Events.Handlers.Player.Hurting -= EventHandlers.OnHurting;
            Exiled.Events.Handlers.Player.PickingUpItem -= EventHandlers.OnPickingUpMicroHid;
            Exiled.Events.Handlers.Player.PickingUpItem -= EventHandlers.OnPickingUpSCP207;
            Exiled.Events.Handlers.Player.ChangingItem -= EventHandlers.OnEquipMicroHid;
            Exiled.Events.Handlers.Scp096.AddingTarget -= EventHandlers.OnLookingAtScp096;
            Exiled.Events.Handlers.Item.ChargingJailbird -= EventHandlers.OnChargingJailbird;
            base.OnDisabled();
        }
    }
}