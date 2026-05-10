namespace wpf.ui.model.diagrams.electric
{
    public class SettingGroupItem
    {
        public required string RowName { get; set; }
        public required string ColName { get; set; }
        public bool IsActive { get; set; }
        public int RowIndex { get; set; }
        public int ColIndex { get; set; }
    }
}
