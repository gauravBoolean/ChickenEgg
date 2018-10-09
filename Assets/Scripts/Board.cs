using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// New
public enum CellState
{
    None,
    Friendly,
    Enemy,
    Free,
    OutOfBounds
}

public class Board : MonoBehaviour
{
    public GameObject mCellPrefab;

    [HideInInspector]
    public Cell[,] mAllCells = new Cell[8, 8];

    public void Create()
    {
        for (int i = 0; i < 8; i++){
            for (int j = 0; j < 8; j++){
                GameObject newCell = Instantiate(mCellPrefab, transform);

                RectTransform rectTransform = newCell.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2((j * 100) + 50, (i * 100) + 50);

                mAllCells[j, i] = newCell.GetComponent<Cell>();
                mAllCells[j, i].Setup(new Vector2Int(j, i), this);
            }
        }


        for (int i = 0; i < 8; i+= 2)
        {
            for (int j = 0; j < 8; j++)
            {

                int offSet = (j % 2 != 0) ? 0 : 1;
                int finalX = i + offSet;

                mAllCells[finalX, j].GetComponent<Image>().color = new Color(230, 220, 187, 255);
            }
        }

    }

    public CellState ValidateCell(int targetX, int targetY, BasePiece checkingPiece)
    {   
        if(targetX < 0 || targetX > 7){
            return CellState.OutOfBounds;
        }

        if (targetY < 0 || targetY > 7)
        {
            return CellState.OutOfBounds;
        }

        Cell targetCell = mAllCells[targetX, targetY];

        if(targetCell.mCurrentPiece != null){
            if (targetCell.mCurrentPiece.mColor == checkingPiece.mColor){
                return CellState.Friendly;
            }
            else{
                return CellState.Enemy;
            }
        }

        return CellState.Free;
    }
}
