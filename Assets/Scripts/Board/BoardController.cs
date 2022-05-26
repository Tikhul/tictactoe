using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoardController : TicTacToeElement
{
    private void OnEnable()
    {
        CreatePlayersButton.OnPlayerChosen += CreateBoard;
        Game.PCController.OnPCTurn += CellsAfterTurn;
        CellButton.OnPlayerClick += CellsAfterTurn;
    }
    private void OnDisable()
    {
        Game.PCController.OnPCTurn -= CellsAfterTurn;
        CellButton.OnPlayerClick -= CellsAfterTurn;
    }
    public event System.Action<string> OnBoardCreated = delegate { };

    public void CreateBoard(string actualMarker)
     // Создание борда для игры
    {
        BoardPartsPrototype prototype = new StandardPartPrototype();

        GameObject boardPanel = prototype.Clone(prototype.ParentPanel, prototype.BoardParent, 
            prototype.ParentPanelSide, prototype.ParentPanelSide);

        GameObject row = prototype.Clone(prototype.Rows, boardPanel, prototype.ParentPanelSide, prototype.ParentPanelSide);

        for (int r = 0; r < prototype.RowNumber; r++)
        {
            GameObject column = prototype.Clone(prototype.Columns, row, prototype.ButtonWidth, prototype.ParentPanelSide);

            for (int b = 0; b < prototype.RowNumber; b++)
            {
                GameObject button = prototype.Clone(prototype.ButtonExample, column, prototype.ButtonWidth, prototype.ButtonWidth);
                FillCellist(button, b, r);
            }
        }
        CreateWinCombinations();
        CreatePlayersButton.OnPlayerChosen -= CreateBoard;
        OnBoardCreated?.Invoke(actualMarker);
    }

    private void CreateWinCombinations()
    // Создание общих выигрышных комбинаций в игре
    {
        Builder builder = new WinCombinationsBuilder();
        Director director = new Director(builder);
        director.Construct();
        Game.BoardModel.WinCombinations.AddRange(builder.GetResult());
    }

    void FillCellist(GameObject button, int b, int r)
    // Заполняю список ячеек в борде
    {
        CellButton buttonSettings = button.GetComponent<CellButton>();
        buttonSettings.CellChar = BoardModel.Alphabet[b];
        buttonSettings.CellInt = r;
        Game.BoardModel.CellList.Add(buttonSettings);
    }

    public void CellsAfterTurn(CellButton receivedCell)
    // Проверка оставшихся ячеек после каждого хода
    {
        Debug.Log("CellsAfterTurn");
        if (Game.BoardModel.CellList.Any())
        {
            List<CellButton> tempList = new List<CellButton>();

            foreach (var cell in Game.BoardModel.CellList)
            {
                if (cell.CellChar.Equals(receivedCell.CellChar) && cell.CellInt.Equals(receivedCell.CellInt)) 
                {
                    tempList.Add(cell);
                } 
            }
            Game.BoardModel.CellList.RemoveAll(item => tempList.Contains(item));
        }
        else
        {
            Game.GameController.CheckGameState(true);
            Game.GameController.GetResults("Ничья");
        }    
    }
    public void ManageButtons(bool state)
    {
        foreach (var cell in Game.BoardModel.CellList)
        {
            cell.ButtonElement.enabled = state;
        }
    }
}
