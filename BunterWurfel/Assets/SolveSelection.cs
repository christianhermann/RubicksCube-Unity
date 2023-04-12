using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SolveSelection : MonoBehaviour
{
    public Dropdown Algorithm;
    private SolveTwoPhase solveTwoPhase;
    private RandomSolver randomSolver;

    // Start is called before the first frame update
    void Start()
    {
        solveTwoPhase = FindObjectOfType<SolveTwoPhase>();
        randomSolver = FindObjectOfType<RandomSolver>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {
        if (Algorithm.value == 0) solveTwoPhase.Solver();
        if (Algorithm.value == 1) randomSolver.RunSolver();

    }

}
