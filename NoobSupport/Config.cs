using Exiled.API.Interfaces;

namespace NoobSupport
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public string MicroHidLowEnergyMessage { get; set; } = "레일건의 베터리가 매우 부족합니다...";
        public string MicroHidEnergyMessage { get; set; } = "남은 에너지: {0}%";

        public bool ShowHintOnEquipItem { get; set; } = false;

        public string Scp207HintMessage { get; set; } = "당신은 {0} 개의 SCP-207 를 마셧습니다!";
        public string AntiScp207HintMessage { get; set; } = "당신은 {0} 개의 Anti SCP-207 를 마셧습니다!";

        public string CardiacArrestMessage { get; set; } = "심정지 가 감지되었습니다.. 대량의 의료용 아이탬 또는 아드레난린 주사기를 사용하여 회복하십시요..!";

        public string PoisonMessage { get; set; } = "당신은 중독되었습니다..!\n<color=green>SCP-500</color> 을 사용하여 독을 완화하십시요...!";
        public string BurnedMessage { get; set; } = "화상이 감지되었습니다..!\n화상일 때 공격당하면 기존대미지의 25% 가 추가됩니다";
        public string ScannedMessage { get; set; } = "SCP079 가 당신을 추적했습니다..!";

        public string LowHpMessage { get; set; } = "HP 가 매우낮습니다...\n가능한 빨리 의료장비를 사용하십시요..!";
        public string A7Info { get; set; } = "A7 에 피격당하셧습니다...\nA7 의 피격시 화상효과가 일어납니다..";
        public string Looking096 { get; set; } = "SCP096 이 당신을 추적하고있습니다..!\n폭주가 끝날때까지 버티십시요..!";
        public string JailbirdUseMessage { get; set; } = "남은 돌진공격 수: {0}";
        public string ScpHealMessage { get; set; } = "적을 처치하여 {0} 의 HP 를 회복하셧습니다.";
        public string BleedingMessage { get; set; } = "출혈이 감지되었습니다..\n의료용 아이탬을 사용하십시요..!";

        public string PocketDimensionMessage { get; set; } = "당신의 신체가 썩어가고 있습니다..!\n빨리 이곳에서 나가십시요...!";
    }
}