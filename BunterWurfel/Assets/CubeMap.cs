using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMap : MonoBehaviour
{

    CubeState cubeState;
    List<UnityEngine.Material> colors;
    // Start is called before the first frame update

    public Transform up;
    public Transform down;
    public Transform left;
    public Transform right;
    public Transform front;
    public Transform back;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()

    {
        
    }

    public void Set()
    {
        cubeState = FindObjectOfType<CubeState>();
        UpdateMap(cubeState.front, front, cubeState.cfront);
        UpdateMap(cubeState.back, back, cubeState.cback);
        UpdateMap(cubeState.left, left, cubeState.cleft);
        UpdateMap(cubeState.right, right, cubeState.cright);
        UpdateMap(cubeState.up, up, cubeState.cup);
        UpdateMap(cubeState.down, down, cubeState.cdown);

    }


    void UpdateMap(List<GameObject> face, Transform side, List<UnityEngine.Material> colors)
    {
        int i = 0;
        foreach (Transform map in side)
        {
            //if (face[0].name[0] == 'F')
            if (colors[i].name == "Blue")
            {
                map.GetComponent<Image>().color = new Color(0, 0.12f, 1);
            }
            if (colors[i].name == "Green")
            {
                map.GetComponent<Image>().color = new Color(0.012f , 0.227f, 0.055f);
            }
            if (colors[i].name == "Orange")
            {
                map.GetComponent<Image>().color = new Color(1, 0.275857f, 0.000691f);
            }
            if (colors[i].name == "Red")
            {
                map.GetComponent<Image>().color = new Color(0.447978f, 0, 0.005f);
            }
            if (colors[i].name == "White")
            {
                map.GetComponent<Image>().color = new Color(0.906752f, 0.906752f, 1);
            }
            if (colors[i].name == "Yellow")
            {
                map.GetComponent<Image>().color = new Color(1, 1, 0);
            }
            i++;

        }
    }
}
