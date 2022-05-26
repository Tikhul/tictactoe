using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class WinsComparer : IComparer<List<CellButton>>
{
    public int Compare(List<CellButton> list1, List<CellButton> list2)
    {
        int count1 = list1.FindAll(c => !c.Taken).Count;
        int count2 = list2.FindAll(c => !c.Taken).Count;

        if (count1 > count2)
        {
            return 1;
        }
        else if (count1 < count2)
        {
            return -1;
        }
        return 0;
    }
}
