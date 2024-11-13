using System.Collections.Generic;
using Exiled.API.Features;
using MEC;

namespace NoobSupport.Utilities
{
    public class ShowHintManager
    {
        private readonly Dictionary<Player, List<string>> playerHints = new Dictionary<Player, List<string>>();
        private readonly Dictionary<Player, CoroutineHandle> activeTimers = new Dictionary<Player, CoroutineHandle>();

        public void AddHint(Player player, string message, float duration = 5)
        { 
            //player.ShowHint(message, duration);

            if (!playerHints.ContainsKey(player))
                playerHints[player] = new List<string>();

            playerHints[player].Add(message);
            UpdateHint(player, duration);
        }
        
        private void UpdateHint(Player player, float duration)
        {
            // 기존 타이머가 있는 경우 취소하고 새로 시작
            if (activeTimers.TryGetValue(player, out CoroutineHandle handle))
            {
                Timing.KillCoroutines(handle);
                activeTimers.Remove(player);
            }

            string combinedMessage = string.Join("\n", playerHints[player]);
            player.ShowHint(combinedMessage, duration);

            // 타이머 설정하여 메시지 전체를 정리
            activeTimers[player] = Timing.CallDelayed(duration, () => ClearAllHints(player));
        }

        private void ClearAllHints(Player player)
        {
            // 플레이어가 여전히 연결된 경우에만 실행
            if (!playerHints.ContainsKey(player) || !player.IsConnected)
                return;

            playerHints[player].Clear();
            playerHints.Remove(player);

            // 빈 메시지를 표시하여 이전 힌트를 제거
            player.ShowHint(string.Empty, 1);

            // 타이머 제거
            if (activeTimers.ContainsKey(player))
                activeTimers.Remove(player);
        }
    }
}