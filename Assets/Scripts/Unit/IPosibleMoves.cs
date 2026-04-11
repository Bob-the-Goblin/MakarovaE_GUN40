using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project;

public interface IPosibleMoves
{
    Cell[] Cells { get; set; }
    NeighbourType[] neighbours { get; set; }
    private void FindNeighbours(ChessPieces pieces) { }


}
