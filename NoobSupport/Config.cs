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
    }
}