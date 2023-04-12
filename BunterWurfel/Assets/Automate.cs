using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Automate : MonoBehaviour
{   
    public static List<string> moveList = new List<string>() {/*"U", "U", "D", "D", "L", "L", "R", "R", "F","F"*/};
    public readonly List<string> allMoves = new List<string>()
    {"U", "D", "L", "R", "F", "B","M","E","S",
        "U2","D2", "L2", "R2", "F2", "B2","M2","E2","S2",
        "U'", "D'", "L'", "R'", "F'", "B'","M","E'","S'"
    };
    public static List<string> DoneMoves = new List<string>();
    public InputField nextMovesAsText;
    public InputField doneMovesAsText;
    public Slider shuffleSlider;
    public Text shuffleSliderNumber;
    public Slider speedSlider;
    public Text speedSliderNumber;
    private float speed = 300f;
    private int shuffleLength = 20;
    private CubeState cubeState;
    private ReadCube readCube;
    private bool goThroughList = false;
    float interval = 1f;
    float lastTime;

    // Start is called before the first frame update
    void Start()
    {
        cubeState = FindObjectOfType<CubeState>();
        readCube = FindObjectOfType<ReadCube>();

        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (moveList.Count > 0 && !CubeState.autoRotating && CubeState.started == true && goThroughList)
        {//this somehow fixes the problem with overlapping blocks.
            if (Time.time > lastTime + 0.001)
            {
                //           print(moveList[0]);
                DoMove(moveList[0]);
                DoneMoves.Insert(0, moveList[0]);
                moveList.Remove(moveList[0]);
                MoveListToNextMoveasText();
            }
        }
        if (moveList.Count == 0) goThroughList = false;
    }

    public void GoThroughMoveList()
    {
        goThroughList = true;
    }

    public void ForwardThroughMoveList()
    {
        if (moveList.Count > 0 && !CubeState.autoRotating && CubeState.started == true)
        {
            DoMove(moveList[0]);
            DoneMoves.Insert(0, moveList[0]);
            moveList.Remove(moveList[0]);
            MoveListToNextMoveasText();
        }
    }

    public void ReverseThroughMoveList()
    {
        if (DoneMoves.Count > 0 && !CubeState.autoRotating && CubeState.started == true)
        {
            DoMove(ReverseMove(DoneMoves[0]));
            moveList.Insert(0,DoneMoves[0]);
            DoneMoves.Remove(DoneMoves[0]);
            MoveListToNextMoveasText();
        }
    }


    public void Shuffle()
    {
        List<string> moves = new List<string>();
        for (int i = 0; i < shuffleLength; i++)
        {
            int randomMove = Random.Range(0, allMoves.Count);
            moves.Add(allMoves[randomMove]);
         //   moves.Add("-");
        }
        moves.RemoveAt(moves.Count - 1);
        moveList = moves;
        MoveListToNextMoveasText();

    }

    private string ReverseMove (string move)
    {
        if (move == "U")
        {
            return("U'");
        }
        if (move == "U'")
        {
            return("U");
        }
        if (move == "D")
        {
            return ("D'");
        }
        if (move == "D'")
        {
            return("D");
        }
        if (move == "F")
        {
            return ("F'");
        }
        if (move == "F'")
        {
            return("F");
        }
        if (move == "B")
        {
            return("B'");
        }
        if (move == "B'")
        {
            return ("B");
        }
        if (move == "R")
        {
            return("R'");
        }
        if (move == "R'")
        {
            return ("R");
        }
        if (move == "L")
        {
            return("L'");
        }
        if (move == "L'")
        {
            return("L");
        }
        if (move == "M")
        {
            return ("M'");
        }
        if (move == "M'")
        {
            return ("M");
        }
        if (move == "E")
        {
            return ("E'");
        }
        if (move == "E'")
        {
            return ("E");
        }
        if (move == "S")
        {
            return ("S'");
        }
        if (move == "S'")
        {
            return ("S");
        }
        return (move);
    }



    public void DoMove (string move)

    {
        readCube.ReadState();
        CubeState.autoRotating = true;

        if (move == "U'")
        {
            RotateSide(cubeState.up, -90);
        }
        if (move == "U")
        {
            RotateSide(cubeState.up, 90);
        }
        if (move == "U2")
        {
            RotateSide(cubeState.up, -180);
        }
        if (move == "D")
        {
            RotateSide(cubeState.down, -90);
        }
        if (move == "D'")
        {
            RotateSide(cubeState.down, 90);
        }
        if (move == "D2")
        {
            RotateSide(cubeState.down, -180);
        }
        if (move == "L")
        {
            RotateSide(cubeState.left, -90);
        }
        if (move == "L'")
        {
            RotateSide(cubeState.left, 90);
        }
        if (move == "L2")
        {
            RotateSide(cubeState.left, -180);
        }
        if (move == "R'")
        {
            RotateSide(cubeState.right, -90);
        }
        if (move == "R")
        {
            RotateSide(cubeState.right, 90);
        }
        if (move == "R2")
        {
            RotateSide(cubeState.right, -180);
        }
        if (move == "B")
        {
            RotateSide(cubeState.back, -90);
        }
        if (move == "B'")
        {
            RotateSide(cubeState.back, 90);
        }
        if (move == "B2")
        {
            RotateSide(cubeState.back, -180);
        }
        if (move == "F'")
        {
            RotateSide(cubeState.front, -90);
        }
        if (move == "F")
        {
            RotateSide(cubeState.front, 90);
        }
        if (move == "F2")
        {
            RotateSide(cubeState.front, -180);
        }
        if (move == "M")
        {
            DoMove("L'");
            DoMove("R");
        }
        if (move == "M'")
        {
            DoMove("L");
            DoMove("R'");
        }
        if (move == "M2")
        {
            DoMove("L2");
            DoMove("R2");
        }
        if (move == "E")
        {
            DoMove("U'");
            DoMove("D");
        }
        if (move == "E'")
        {
            DoMove("U");
            DoMove("D'");
        }
        if (move == "E2")
        {
            DoMove("U2");
            DoMove("D2");
        }
        if (move == "S")
        {
            DoMove("F'");
            DoMove("B");
        }
        if (move == "S'")
        {
            DoMove("F");
            DoMove("B'");
        }
        if (move == "S2")
        {
            DoMove("F2");
            DoMove("B2");
        }


    }

    void RotateSide(List<GameObject> side, float angle)
    {

        PivotRotation pr = side[4].GetComponent<PivotRotation>();
        pr.speed = speed;
        pr.StartAutoRotate(side, angle);
    }

   public void changedNextMoveasText()
    {
       
        moveList = nextMovesAsText.text.Split('-').ToList();
    }

    public void MoveListToNextMoveasText()
    {
        if (moveList.Count > 0)     nextMovesAsText.text = string.Join("-", moveList);
        if (moveList.Count == 0) nextMovesAsText.text = "";

        if (DoneMoves.Count > 1)    doneMovesAsText.text = string.Join("-", DoneMoves);
        if (DoneMoves.Count == 1)  doneMovesAsText.text =  DoneMoves[0];
        if (DoneMoves.Count == 0) doneMovesAsText.text = "";

    }

    public void ShuffleSliderChanged()
    {
        shuffleLength = (int)shuffleSlider.value;
        shuffleSliderNumber.text = shuffleSlider.value.ToString();
    }

    public void SpeedSliderChanged()
    {
        speed = speedSlider.value;
        speedSliderNumber.text = speedSlider.value.ToString();
    }

}
