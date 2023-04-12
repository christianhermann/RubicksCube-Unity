using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kociemba;
using UnityEngine.UI;

public class SolveTwoPhase : MonoBehaviour
{
    private ReadCube readCube;
    private CubeState cubeState;

    private bool doOnce = true;
    public InputField nextMovesAsText;
    public InputField doneMovesAsText;
    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CubeState.started && doOnce)
        {
            doOnce = false;
            Solver();
        }

    }

    public void Solver()
    {


        readCube.ReadState();

        string moveString = cubeState.GetStateString();

        string info = "";

        //only run once to build tables
      //  string solution = SearchRunTime.solution(moveString, out info, buildTables: true);

        string solution = Search.solution(moveString, out info);

        List<string> solutionList = StringToList(solution);

        Automate.moveList = solutionList;
        if(solutionList.Count > 1)  nextMovesAsText.text = string.Join("-", solutionList);
        else if (solutionList.Count == 1) nextMovesAsText.text = solutionList[0];

    }

    List<string> StringToList(string solution)
    {
        List<string> solutionList = new List<string>(solution.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries));
        return solutionList;
    }
}
