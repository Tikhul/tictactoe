using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Service : TicTacToeElement
{
    void OnEnable()
    {
        CellButton.OnPlayerClick += BlockButtons;
        CellButton.OnPCTaken += ActivateButtons;
    }

    void OnDisable()
    {
        CellButton.OnPlayerClick -= BlockButtons;
        CellButton.OnPCTaken += ActivateButtons;
    }

    public static void BlockButtons(string marker, int cellInt, char cellChar)
    {
        foreach (var cell in FindObjectsOfType<CellButton>())
        {
            if (cell.GetComponent<Button>()) cell.GetComponent<Button>().enabled = false;
        }
    }

    public static void ActivateButtons(string marker, int cellInt, char cellChar)
    {
        foreach (var cell in FindObjectsOfType<CellButton>())
        {
            if (cell.GetComponent<Button>() && !cell.taken) cell.GetComponent<Button>().enabled = true;
        }
    }
}
