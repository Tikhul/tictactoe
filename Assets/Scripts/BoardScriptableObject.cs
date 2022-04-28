﻿using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BoardScriptableObject", menuName = "ScriptableObjects/BoardSO", order = 1)]
public class BoardScriptableObject : ScriptableObject
{
    public int rowNumber = 3;
    public GameObject buttonExample;
    public GameObject parentPanel;
    public GameObject rows;
    public GameObject columns;
}
