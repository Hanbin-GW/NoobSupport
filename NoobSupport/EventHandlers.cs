using System;
using System.Linq;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Item;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp096;
using InventorySystem.Items.Jailbird;
using InventorySystem.Items.MicroHID;
using MEC;
using NoobSupport.Utilities;
using PlayerRoles;

namespace NoobSupport
{
    public class EventHandlers
    {
        private readonly Plugin _plugin;
        private ShowHintManager _showHintManager;
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

        public void OnChargingJailbird(ChargingJailbirdEventArgs ev)
        {
            if (ev.Item == null)
            {
                return;
            }

            if (ev.Item.Base is JailbirdItem jailbirdItem)
            {
                int maxCharges = 5;
                int remainingCharges = maxCharges - jailbirdItem.TotalChargesPerformed;

                if (remainingCharges > 1)
                {
                    ev.Player.ShowHint($"<color=#00B7EB>{new string('\n', 10)}{string.Format(_plugin.Config.JailbirdUseMessage, remainingCharges)}</color>", 2);
                }
                else
                {
                    ev.Player.ShowHint($"<color=#C73804>{new string('\n', 10)}{string.Format(_plugin.Config.JailbirdUseMessage, remainingCharges)}</color>", 2);
                }
            }
        }

        public void OnEffectAdded(ReceivingEffectEventArgs ev)
        {
            if (ev.Player.IsHost || !ev.Player.IsConnected) return;

            Timing.CallDelayed(0.5f, () =>
            {
                if (!ev.Player.IsConnected) return;

                switch (ev.Effect.GetEffectType())
                {
                    case EffectType.CardiacArrest:
                        //ev.Player.ShowHint($"<color=red>{_plugin.Config.CardiacArrestMessage}</color>", 5);
                        _showHintManager.AddHint(ev.Player,$"<color=red>{new string('\n',10)}{string.Format(_plugin.Config.CardiacArrestMessage)}</color>",5f);
                        break;

                    case EffectType.Poisoned:
                        //ev.Player.ShowHint($"<color=green>{_plugin.Config.PoisonMessage}</color>", 5);
                        _showHintManager.AddHint(ev.Player,$"<color=DarkGreen>{new string('\n',10)}{string.Format(_plugin.Config.PoisonMessage)}</color>",5f);
                        break;

                    case EffectType.Burned:
                        //ev.Player.ShowHint($"<color=orange>{_plugin.Config.BurnedMessage}</color>", 5);
                        _showHintManager.AddHint(ev.Player,$"<color=orange>{new string('\n',10)}{string.Format(_plugin.Config.BurnedMessage)}</color>",5f);
                        break;

                    case EffectType.Scanned:
                        //ev.Player.ShowHint($"<color=blue>{_plugin.Config.ScannedMessage}</color>", 5);
                        _showHintManager.AddHint(ev.Player,$"<color=darkblue>{new string('\n',10)}{string.Format(_plugin.Config.ScannedMessage)}</color>",5f);
                        break;
                }
            });
        }

        /*public void OnEffectAdded(ReceivingEffectEventArgs ev)
        {
            if (ev.Player.IsHost|| !ev.Player.IsConnected) return;
            // Define the effect name
            //string effectName = ev.Effect.name;
            switch (ev.Effect.GetEffectType())
            {
                case EffectType.CardiacArrest:
                    ev.Player.ShowHint($"<color=blue>{_plugin.Config.CardiacArrestMessage}</color>", 5); // 개행 없이 테스트
                    //ev.Player.Broadcast(5,$"<color=red>{_plugin.Config.CardiacArrestMessage}</color>");
                    break;

                case EffectType.Poisoned:
                    //ev.Player.Broadcast(5, $"<color=green>{_plugin.Config.PoisonMessage}</color>");
                    ev.Player.ShowHint($"<color=blue>{_plugin.Config.PoisonMessage}</color>", 5); // 개행 없이 테스트
                    break;

                case EffectType.Burned:
                    //ev.Player.Broadcast(5, $"<color=orange>{_plugin.Config.BurnedMessage}</color>");
                    ev.Player.ShowHint($"<color=blue>{_plugin.Config.BurnedMessage}</color>", 5); // 개행 없이 테스트
                    break;

                case EffectType.Scanned:
                    ev.Player.ShowHint($"<color=blue>{_plugin.Config.ScannedMessage}</color>", 5); // 개행 없이 테스트
                    //ev.Player.Broadcast(5, $"<color=blue>{new string('\n', 10)}{string.Format(_plugin.Config.ScannedMessage)}</color>");
                    break;
            }
        }*/
        public void OnHurting(HurtingEventArgs ev)
        {
            if (ev.DamageHandler.Type == DamageType.Scp049)
            {
                //ev.Player.ShowHint($"<color=red>{new string('\n', 10)}{string.Format(Plugin.Instance.Config.CardiacArrestMessage)}</color>", 2);
            }

            if (ev.DamageHandler.Type == DamageType.A7)
            {
                //ev.Player.ShowHint($"<color=orange>{new string('\n',10)}{_plugin.Config.A7Info}");
            }

            if (ev.Player.Health <= 20 && ev.Player.IsHuman)
            {
                ev.Player.ShowHint($"<color=red>{new string('\n',10)}{string.Format(_plugin.Config.LowHpMessage)}</color>");
            }
        }
        
        public void OnLookingAtScp096(AddingTargetEventArgs ev)
        {
            ev.Target.Broadcast(5,$"<color=red>{new string('\n',10)}{string.Format(_plugin.Config.Looking096)}</color>");
        }

        public void OnPickingUpSCP207(PickingUpItemEventArgs ev)
        {
            if (ev.Pickup.Type == ItemType.SCP207)
            {
                StatusEffectBase scp207Effect = ev.Player.ActiveEffects.FirstOrDefault(effect => effect.GetEffectType() == EffectType.Scp207);

                if (scp207Effect != null)
                {
                    ev.Player.ShowHint($"<color=#A60C0E>{new string('\n', 10)}{string.Format(Plugin.Instance.Config.Scp207HintMessage, scp207Effect.Intensity)}</color>", 4);
                }
            }
            if (ev.Pickup.Type == ItemType.AntiSCP207)
            {
                StatusEffectBase antiscp207Effect = ev.Player.ActiveEffects.FirstOrDefault(effect => effect.GetEffectType() == EffectType.AntiScp207);

                if (antiscp207Effect != null)
                {
                    ev.Player.ShowHint($"<color=#C53892>{new string('\n', 10)}{string.Format(Plugin.Instance.Config.AntiScp207HintMessage, antiscp207Effect.Intensity)}</color>", 4);
                }
            }
        }
    }
}