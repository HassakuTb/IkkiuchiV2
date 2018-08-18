namespace Ikkiuchi.TitleScenes.ViewModels {
    public class CreateRoomModel{

        public string RoomName { get; set; } = "";
        public int MomentCount { get; set; } = 3;
        public int MaxLife { get; set; } = 8;
        public bool IsEnabledTrump { get; set; } = true;

    }
}
