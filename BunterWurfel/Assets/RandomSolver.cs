using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RandomSolver : MonoBehaviour
{
    private ReadCube readCube;
    private CubeState cubeState;
    private Automate automate;
    private PivotRotation pivotRotation;
    private bool doOnce = true;
    public InputField nextMovesAsText;
    public InputField doneMovesAsText;
    public static List<string> DoneMoves = new List<string>();
    private Boolean run = false;
    string moveString;
    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
        automate = FindObjectOfType<Automate>();
        pivotRotation = FindObjectOfType<PivotRotation>();
    }


    // Update is called once per frame
    void Update()
    {
        if (run == true && !cubeState.GetStateString().Equals("UUUUUUUUURRRRRRRRRFFFFFFFFFDDDDDDDDDLLLLLLLLLBBBBBBBBB") && !CubeState.autoRotating)
        {
            List<string> moves = new List<string>();
            int randomMove = Random.Range(0, automate.allMoves.Count);
            moves.Add(automate.allMoves[randomMove]);
            Automate.moveList = moves;
            automate.MoveListToNextMoveasText();
            automate.GoThroughMoveList();

        }
    }

    public void RunSolver()
    {
        moveString = cubeState.GetStateString();

        run = true;
        if (moveString.Equals("UUUUUUUUURRRRRRRRRFFFFFFFFFDDDDDDDDDLLLLLLLLLBBBBBBBBB")) run = false;

    }
}
