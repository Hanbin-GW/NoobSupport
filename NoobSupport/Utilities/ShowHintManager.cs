using System.Collections.Generic;
using Exiled.API.Features;
using MEC;

namespace NoobSupport.Utilities
{
    public class ShowHintManager
    {
        private readonly Dictionary<Player, List<string>> playerHints = new Dictionary<Player, List<string>>();

        public void AddHint(Player player, string message, float duration = 5)
        {
            if (!playerHints.ContainsKey(player))
                playerHints[player] = new List<string>();
            playerHints[player].Add(message);
            
            string combinedMessage = string.Join("\n", playerHints[player]);
            player.ShowHint(combinedMessage,duration);

            Timing.CallDelayed(duration, () => RemoveHint(player, message));
        }

        private void RemoveHint(Player player, string message)
        {
            if(!playerHints.ContainsKey(player)) return;

            playerHints[player].Remove(message);
            
            if (playerHints[player].Count == 0)
            {
                playerHints.Remove(player);
            }
            else
            {
                string combinedMessage = string.Join("\n", playerHints[player]);
                player.ShowHint(combinedMessage, 5);
            }
        }
    }
}