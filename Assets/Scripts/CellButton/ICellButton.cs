using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICellButton
{
    void CellClicked();
    void CellTaken(ICellButton chosenButton);
}
