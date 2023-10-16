using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class Piece : MonoBehaviour
{
    public Board board { get; private set;}
    public Vector3Int position { get; private set;}
    public Vector3Int[] cells{ get; private set;}
    public TetrominoData data { get; private set;}

    public void Initialize(Board newBoard, Vector3Int newPosition, TetrominoData newData)
    {
        board = newBoard;
        position = newPosition;
        data = newData;
        
        if (cells == null)
        {
            cells = new Vector3Int[data.cells.Length]; 
        }

        for (int i = 0; i < data.cells.Length; i++)
        {
            cells[i] = (Vector3Int)data.cells[i];
        }
    }

    private void Update()
    {
        board.Clear(this);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector2Int.left);
        } else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector2Int.right);
        } else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector2Int.down);
        }
        board.Set(this);
    }

    private bool Move(Vector2Int translation)
    {
        Vector3Int newPosition = position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;
        
        bool valid = board.IsValidPositon(this, newPosition);

        if (valid)
        {
            position = newPosition;
        }

        return valid;
    }
}
