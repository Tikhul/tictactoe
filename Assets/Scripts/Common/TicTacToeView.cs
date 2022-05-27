using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeView : MonoBehaviour
{
    [SerializeField] private InitialUI _initialUI;
    [SerializeField] private FinalUI _finalUI;

    public InitialUI InitialUI
    {
        get => _initialUI;
        set => _initialUI = value;
    }

    public FinalUI FinalUI
    {
        get => _finalUI;
        set => _finalUI = value;
    }
}
