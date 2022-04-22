using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BoardScriptableObject", menuName = "ScriptableObjects/BoardSO", order = 1)]
public class BoardScriptableObject : ScriptableObject
{
    public int rowNumber = 3;
    public Button buttonExample;
    public CanvasRenderer parentPanel;
    public HorizontalLayoutGroup columns;
    public VerticalLayoutGroup rows;
}
