using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePuzzleManager : MonoBehaviour
{
    //!!IMPORTANT!! Always put the corresponding puzzle piece and slot in the same index.
    public PiecePuzzlePiece[] PuzzlePieces;
    public PiecePuzzleSlot[] PuzzleSlots;
    public GameObject toDeactivate;


    public int puzzleSolveCount;

    void LateUpdate()
    {
        if (PuzzlePieces.Length == PuzzleSlots.Length)
            for (int i = 0; i < PuzzleSlots.Length; i++)
            {
                if (PuzzleSlots[i].GetComponent<Collider2D>().IsTouching(PuzzlePieces[i].GetComponent<Pol>()))
                    puzzleSolveCount++;
            }

        if (puzzleSolveCount == PuzzleSlots.Length && PuzzleSlots.Length != 0)
            toDeactivate.SetActive(false);

        puzzleSolveCount = 0;
    }
}
