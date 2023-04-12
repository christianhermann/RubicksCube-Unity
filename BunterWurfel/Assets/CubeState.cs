using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeState : MonoBehaviour
{

    public List<GameObject> front = new List<GameObject>();
    public List<GameObject> back = new List<GameObject>();
    public List<GameObject> up = new List<GameObject>();
    public List<GameObject> down = new List<GameObject>();
    public List<GameObject> left = new List<GameObject>();
    public List<GameObject> right = new List<GameObject>();
    public List<GameObject> middle = new List<GameObject>();
    public List<GameObject> equatorial = new List<GameObject>();
    public List<GameObject> standing = new List<GameObject>();

    public List<UnityEngine.Material> cfront = new List<UnityEngine.Material>();
    public List<UnityEngine.Material> cback = new List<UnityEngine.Material>();
    public List<UnityEngine.Material> cup = new List<UnityEngine.Material>();
    public List<UnityEngine.Material> cdown = new List<UnityEngine.Material>();
    public List<UnityEngine.Material> cleft = new List<UnityEngine.Material>();
    public List<UnityEngine.Material> cright = new List<UnityEngine.Material>();
    //public List<UnityEngine.Material> cmiddle = new List<UnityEngine.Material>();
    //public List<UnityEngine.Material> cequatorial = new List<UnityEngine.Material>();
    //public List<UnityEngine.Material> cstanding = new List<UnityEngine.Material>();

    public static bool autoRotating = false;
    public static bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


   // public void PickUp((List<GameObject> cubeSideFace , List<UnityEngine.Material> cubeMaterial) cubeSide)
       public void PickUp(List<GameObject> cubeSideFace)

    {
        foreach(GameObject face in cubeSideFace)
        {
            if (face != cubeSideFace[4])
            {
                face.transform.parent= cubeSideFace[4].transform;
            }
        }

    }

    public void PutDown(List<GameObject> littleCubes, Transform pivot)
    {
        foreach (GameObject littleCube in littleCubes)
        {
            if (littleCube != littleCubes[4])
            {
                littleCube.transform.parent = pivot;
            }
        }
    }

    //string GetSideString(List<GameObject> side)
    //{
    //    string sideString = "";

    //    foreach (GameObject face in side)
    //    {
    //        sideString += face.name[0].ToString();
    //    }
    //    return sideString;
    //}

    //public string GetStateString()
    //{
    //    string stateString = "";
    //    stateString += GetSideString(up);
    //    stateString += GetSideString(right);
    //    stateString += GetSideString(front);
    //    stateString += GetSideString(down);
    //    stateString += GetSideString(left);
    //    stateString += GetSideString(back);
    //    return stateString;


    //}

    string GetSideString(List<Material> side)
    {
        string sideString = "";

        foreach (Material face in side)
        {
            sideString += ColorToFace(face.name);
        }
        return sideString;
    }

    public string GetStateString()
    {
        string stateString = "";
        stateString += GetSideString(cup);
        stateString += GetSideString(cright);
        stateString += GetSideString(cfront);
        stateString += GetSideString(cdown);
        stateString += GetSideString(cleft);
        stateString += GetSideString(cback);
        //stateString += GetSideString(cmiddle);
        //stateString += GetSideString(cequatorial);
        //stateString += GetSideString(cstanding);

        return stateString;


    }

    public string ColorToFace(string color)
    {
        string FaceString = "";

        if (color == "Red") FaceString = "U";
        if (color == "Blue") FaceString = "F";
        if (color == "White") FaceString = "L";
        if (color == "Orange") FaceString = "D";
        if (color == "Yellow") FaceString = "R";
        if (color == "Green") FaceString = "B";

        return FaceString;
    }
}

