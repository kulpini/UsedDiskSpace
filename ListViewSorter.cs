using System;
using System.Collections;
using System.Windows.Forms;

public class ListViewSorter : IComparer
{
    public int Column { get; set; }
    public SortOrder Order { get; set; }
    private CaseInsensitiveComparer ObjectCompare;
    public ListViewSorter()
    {
        Column = 0;
        Order = SortOrder.None;
        ObjectCompare = new CaseInsensitiveComparer();
    }
    public int Compare(object x, object y)
    {
        int compareResult;
        ListViewItem listviewX, listviewY;

        listviewX = (ListViewItem)x;
        listviewY = (ListViewItem)y;
        string xText = listviewX.SubItems[Column].Text;
        Int32.TryParse(xText.Substring(0, xText.Length - 3),out int sizeX);
        string yText = listviewY.SubItems[Column].Text;
        Int32.TryParse(yText.Substring(0, yText.Length - 3), out int sizeY);
        compareResult = ObjectCompare.Compare(sizeX, sizeY);

        if (Order == SortOrder.Ascending)
        {
            return compareResult;
        }
        else if (Order == SortOrder.Descending)
        {
            return (-compareResult);
        }
        else
        {
            return 0;
        }
    }
}
