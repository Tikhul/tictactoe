﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellButton : MonoBehaviour
{
    public int cellInt;
    public char cellChar;
    public bool taken;
    public TMP_Text buttonText;

    public delegate void ClickAction(string marker, int cellInt, char cellChar);
    public static event ClickAction OnPlayerClick;

    private void OnEnable()
    {
        Player.OnTurnGenerated += CellTaken;
    }

    private void OnDisable()
    {
        Player.OnTurnGenerated -= CellTaken;
    }

    public void CellClicked()
    {
        taken = true;
        buttonText.text = Process.human.marker;
        buttonText.gameObject.SetActive(true);
        OnPlayerClick(Process.human.marker, cellInt, cellChar);
    }

    public delegate void TakeAction(string marker, int cellInt, char cellChar);
    public static event TakeAction OnPCTaken;

    public void CellTaken(CellButton chosenButton)
    {
        if(this == chosenButton)
        {
            taken = true;
            buttonText.text = Process.pc.marker;
            buttonText.gameObject.SetActive(true);
            OnPCTaken(Process.pc.marker, cellInt, cellChar);
        }
    }
}