using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PivotRotation : MonoBehaviour
{

    private List<GameObject> activeSide;
    private Vector3 localForward;
    private Vector3 mouseRef;
    private bool dragging = false;
    private bool autoRotating = false;
    public float speed = 300f;
    public bool rotating = false;

    private float sensitivity = 0.4f;
    private Vector3 rotation;

    public Quaternion targetQuaternion;
    private ReadCube readCube;
    private CubeState cubeState;
    AudioSource clickSound;
    public Vector3 oldRotation;
    // Start is called before the first frame update
    void Start()
    {

        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
        clickSound = GetComponent<AudioSource>();
    }

    // Late Update is called once per frame at the end
    void LateUpdate()
    {
        if (dragging && !autoRotating)
        {

            SpinSide(activeSide);

            if (Input.GetMouseButtonUp(0))
            {
                dragging = false;
                RotateToRightAngle();
            }
                
        }
        if (autoRotating)
        {
            AutoRotate();
        }
    }

    private void SpinSide(List<GameObject> side)
    {
        rotation = Vector3.zero;

        Vector3 mouseOffset = (Input.mousePosition - mouseRef);

        if(side == cubeState.left || side == cubeState.middle)
        {
            rotation.x = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;
        }

        if (side == cubeState.right)
        {
            rotation.x = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;
        }

        if (side == cubeState.up || side == cubeState.equatorial)
        {
            rotation.z = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;
        }

        if (side == cubeState.down)
        {
            rotation.z = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;
        }
        if (side == cubeState.front || side == cubeState.standing)
        {
            rotation.y = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;
        }

        if (side == cubeState.back)
        {
            rotation.y = (mouseOffset.x + mouseOffset.y) * sensitivity * - 1;
        }


        transform.Rotate(rotation, Space.Self);

        mouseRef = Input.mousePosition;
    }

    public void Rotate(List<GameObject> side)
    {
        activeSide = side;
        mouseRef = Input.mousePosition;
        oldRotation = transform.eulerAngles;

        dragging = true;

        localForward = Vector3.zero - side[4].transform.localPosition;

    }

    public void StartAutoRotate(List<GameObject> side, float angle)
    {
        cubeState.PickUp(side);



        rotation = Vector3.zero;


        if (side == cubeState.left || side == cubeState.middle)
        {
            rotation.x = angle;
        }

        if (side == cubeState.right)
        {
            rotation.x = angle;
        }

        if (side == cubeState.up || side == cubeState.equatorial)
        {
            rotation.z = angle;
        }

        if (side == cubeState.down)
        {
            rotation.z = angle;
        }
        if (side == cubeState.front || side == cubeState.standing)
        {
            rotation.y = angle;
        }

        if (side == cubeState.back)
        {
            rotation.y = angle;
        }

        var originalRotation = transform.rotation;
        transform.Rotate(rotation, Space.Self);
        targetQuaternion.eulerAngles = transform.localEulerAngles;

        transform.rotation = originalRotation;

        activeSide = side;
        autoRotating = true;
    }

    public void RotateToRightAngle()
    {
        Vector3 vec = transform.localEulerAngles;
        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;

        targetQuaternion.eulerAngles = vec;

        autoRotating = true;

    }

    public void AutoRotate()
    {
        dragging = false;
        var step = speed * Time.deltaTime;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);

        if (Quaternion.Angle(transform.localRotation, targetQuaternion) <= 1)
        {

            transform.localRotation = targetQuaternion;

            cubeState.PutDown(activeSide, transform.parent);
            readCube.ReadState();
            CubeState.autoRotating = false;
            autoRotating = false;
            dragging = false;
            clickSound.PlayOneShot(clickSound.clip, 0.3f);

        }

    }




}
