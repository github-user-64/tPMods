namespace BedWars.Edit.UI.EditWindow
{
    internal class EditPanel3 : EditSpawItem.EditPanel, IPanel
    {
        public void OnOpen()
        {
            UpdateData();
        }

        public override void OnEditEnable()
        {
            UpdateData();
        }
    }
}
