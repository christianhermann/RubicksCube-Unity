using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideCubeMap : MonoBehaviour
{

   public GameObject cubemap;
   public Toggle cubemapToggle;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeCubeMap()
    {
        if(cubemapToggle.isOn == true) cubemap.transform.localScale += new Vector3(1, 1, 1);
        if (cubemapToggle.isOn == false) cubemap.transform.localScale += new Vector3(-1, -1, -1);

    }
}
