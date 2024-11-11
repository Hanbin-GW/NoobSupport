using System;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.MicroHID;

namespace NoobSupport
{
    public class EventHandlers
    {
        private readonly Plugin _plugin;

        public EventHandlers(Plugin plugin)
        {
            this._plugin = plugin;
        }

        public void OnPickingUpMicroHid(PickingUpItemEventArgs ev)
        {
            if (ev.Pickup.Base is MicroHIDPickup microHidPickup)
            {
                float energyPercentage = microHidPickup.Energy * 100;
                float roundedEnergyPercentage = (float)Math.Round(energyPercentage, 1);
                if (roundedEnergyPercentage < 5)
                {
                    ev.Player.ShowHint($"<color=red>{new string('\n', 10)}{string.Format(Plugin.Instance.Config.MicroHidLowEnergyMessage)}</color>", 4);
                }
                else
                {
                    ev.Player.ShowHint($"<color=#4169E1>{new string('\n', 10)}{string.Format(Plugin.Instance.Config.MicroHidEnergyMessage, roundedEnergyPercentage)}</color>", 4);
                }
            }
        }

        public void OnEquipMicroHid(ChangingItemEventArgs ev)
        {
            if (Plugin.Instance.Config.ShowHintOnEquipItem)
            {
                if (ev.Item == null)
                {
                    return;
                }
                if (ev.Item.Base is MicroHIDItem microHidItem)
                {

                    float energyPercentage = microHidItem.RemainingEnergy * 100;
                    float roundedEnergyPercentage = (float)Math.Round(energyPercentage, 1);

                    if (roundedEnergyPercentage < 5)
                    {
                        ev.Player.ShowHint($"<color=red>{new string('\n', 10)}{string.Format(Plugin.Instance.Config.MicroHidLowEnergyMessage)}</color>", 2);
                    }
                    else
                    {
                        ev.Player.ShowHint($"<color=#4169E1>{new string('\n', 10)}{string.Format(Plugin.Instance.Config.MicroHidEnergyMessage, roundedEnergyPercentage)}</color>", 2);
                    }
                }
            }
        }
        
        public void OnPickingUpSCP207(PickingUpItemEventArgs ev)
        {
            if (ev.Pickup.Type == ItemType.SCP207)
            {
                CustomPlayerEffects.StatusEffectBase scp207Effect = ev.Player.ActiveEffects.FirstOrDefault(effect => effect.GetEffectType() == EffectType.Scp207);

                if (scp207Effect != null)
                {
                    ev.Player.ShowHint($"<color=#A60C0E>{new string('\n', 10)}{string.Format(Plugin.Instance.Config.Scp207HintMessage, scp207Effect.Intensity)}</color>", 4);
                }
            }
            if (ev.Pickup.Type == ItemType.AntiSCP207)
            {
                CustomPlayerEffects.StatusEffectBase antiscp207Effect = ev.Player.ActiveEffects.FirstOrDefault(effect => effect.GetEffectType() == EffectType.AntiScp207);

                if (antiscp207Effect != null)
                {
                    ev.Player.ShowHint($"<color=#C53892>{new string('\n', 10)}{string.Format(Plugin.Instance.Config.AntiScp207HintMessage, antiscp207Effect.Intensity)}</color>", 4);
                }
            }
        }
    }
}