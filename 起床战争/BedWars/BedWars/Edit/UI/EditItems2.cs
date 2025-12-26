using BedWars.BedWarsData;
using System.Collections.Generic;
using tContentPatch.Content.UI;

namespace BedWars.Edit.UI
{
    internal class EditItems2 : UIScrollViewer2, IPanel
    {
        private List<SpawItemData> SpawItem = null;

        public EditItems2()
        {
            Width.Precent = 1;
            Height.Precent = 1;

        }

        public void OnOpen()
        {
            ClearChild();

            SpawItem = EditData.instance.DataSpawItems;
            if (SpawItem == null) return;


        }
    }
}
