using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePuzzleManager : MonoBehaviour
{
    //!!IMPORTANT!! Always put the corresponding puzzle piece and slot in the same index.
    public PiecePuzzlePiece[] PuzzlePieces;
    public PiecePuzzlePiece[] PuzzlePiecesPrefabs;
    public PiecePuzzleSlot[] PuzzleSlots;
    public GameObject toDeactivate;


    private int puzzleSolveCount;

    void LateUpdate()
    {
        if (PuzzlePieces.Length == PuzzleSlots.Length)
            for (int i = 0; i < PuzzleSlots.Length; i++)
            {
                if(PuzzlePieces[i] == null)
                {
                    PuzzlePieces[i] = Instantiate(PuzzlePiecesPrefabs[i], Vector3.zero, Quaternion.identity);
                    PuzzlePieces[i].transform.position = PuzzleSlots[i].transform.position;
                }
                    
                if (PuzzleSlots[i].GetComponent<Collider2D>().IsTouching(PuzzlePieces[i].GetComponent<Collider2D>()))
                    puzzleSolveCount++;
            }

        if (puzzleSolveCount == PuzzleSlots.Length && PuzzleSlots.Length != 0)
            toDeactivate.SetActive(false);

        puzzleSolveCount = 0;
    }

    //private float spawnRate = 2f;
    //private IEnumerator coroutine;

    //void spawnDestructible()
    //{
    //    //Debug.Log("Destructible spawn method called.");
    //    //Debug.Log("No player object");

    //    coroutine = DelaySeconds(spawnRate);
    //    StartCoroutine(coroutine);
    //}

    //private int spawnCount;
    //IEnumerator DelaySeconds(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    if (destructible == null)
    //    {
    //        destructible = Instantiate(destructiblePrefab, Vector3.zero, Quaternion.identity);
    //        destructible.transform.position = transform.position;
    //        spawnCount++;
    //    }
    //}
}
