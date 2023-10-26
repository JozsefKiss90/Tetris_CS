using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Ghost : MonoBehaviour
{
    public Tile tile;
    public Board board;
    public Piece trackingPiece;
    
    public Tilemap tilemap { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; }

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        cells = new Vector3Int[4];
    }

    private void LateUpdate()
    {
        Clear(); // First, clear the ghost piece
    
        if(board.gameOver) return; // Then, check if the game is over and halt further logic
    
        Copy();
        Drop();
        Set();
    }


    public void Clear()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            Vector3Int tilePosition = cells[i] + position;
            tilemap.SetTile(tilePosition, null);
        }
    }

    private void Copy()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = trackingPiece.cells[i];
        }
    }

    private void Drop()
    {
        if(board.gameOver) return;
        Vector3Int position = trackingPiece.position;
        int current = position.y;
        int bottom = -board.boardSize.y / 2 -1;
        
        board.Clear(trackingPiece);
        
        for (int row = current; row >= bottom; row--)
        {
            position.y = row;
            if (board.IsValidPosition(trackingPiece, position))
            {
                this.position = position;
            }
            else
            {
                break;
            }
        }
        
        board.Set(trackingPiece);
    }

    private void Set()
    {
        if(board.gameOver) return;
        for (int i = 0; i < cells.Length; i++)
        {
            Vector3Int tilePosition = cells[i] + position;
            tilemap.SetTile(tilePosition, tile);
        }
    }
}
