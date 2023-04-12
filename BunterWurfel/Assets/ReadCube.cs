using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCube : MonoBehaviour
{

    public Transform tUp;
    public Transform tDown;
    public Transform tLeft;
    public Transform tRight;
    public Transform tFront;
    public Transform tBack;


    private List<GameObject> frontRays = new List<GameObject>();
    private List<GameObject> backRays = new List<GameObject>();
    private List<GameObject> leftRays = new List<GameObject>();
    private List<GameObject> rightRays = new List<GameObject>();
    private List<GameObject> upRays = new List<GameObject>();
    private List<GameObject> downRays = new List<GameObject>();


    private int layerMask = 1 << 6; 
    CubeState cubeState;
    CubeMap cubeMap;
    public GameObject emptyGO;

    // Start is called before the first frame update
    void Start()
    {

        SetRayTransforms();
        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();
        ReadState();
        CubeState.started = true;

    }

    // Update is called once per frame
    void Update()
    {


    }
    
    public void ReadState()
    {
        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();

        //set state of each postion on the list
        var (l1, l2) = ReadFace(upRays, tUp);
        cubeState.up = l1;
        cubeState.cup = l2;

        (l1, l2) = ReadFace(downRays, tDown);
        cubeState.down = l1;
        cubeState.cdown = l2;

        (l1, l2) = ReadFace(leftRays, tLeft);
        cubeState.left = l1;
        cubeState.cleft = l2;

        (l1, l2) = ReadFace(frontRays, tFront);
        cubeState.front = l1;
        cubeState.cfront = l2;

        (l1, l2) = ReadFace(rightRays, tRight);
        cubeState.right = l1;
        cubeState.cright = l2;

        (l1, l2) = ReadFace(backRays, tBack);
        cubeState.back = l1;
        cubeState.cback = l2;

        cubeState.middle = new List<GameObject>() {cubeState.back[1], cubeState.up[4], cubeState.front[1],
                                                   cubeState.back[4], GameObject.Find("M"), cubeState.front[4],
                                                   cubeState.back[7], cubeState.down[4], cubeState.front[7]};

        cubeState.equatorial = new List<GameObject>() {cubeState.left[3], cubeState.back[4], cubeState.right[5],
                                                   cubeState.left[4], GameObject.Find("M"), cubeState.right[4],
                                                   cubeState.left[5], cubeState.front[4], cubeState.right[3]};

        cubeState.standing = new List<GameObject>() {cubeState.left[1], cubeState.up[4], cubeState.right[1],
                                                   cubeState.left[4], GameObject.Find("M"), cubeState.right[4],
                                                   cubeState.left[7], cubeState.down[4], cubeState.right[7]};

        //Update Map
        cubeMap.Set();

    }

    void SetRayTransforms()
    {
        //populate the ray cast with raycasts eminating from the transfrom angled towards the cube
        upRays = BuildRays(tUp, new Vector3(90, 0, 0));
        downRays = BuildRays(tDown, new Vector3(270, 0, 0));
        leftRays = BuildRays(tLeft, new Vector3(0, 90, 0));
        rightRays = BuildRays(tRight, new Vector3(0, 270, 0));
        frontRays = BuildRays(tFront, new Vector3(0, 0, 0));
        backRays = BuildRays(tBack, new Vector3(0, 180, 0));
    }


    List<GameObject> BuildRays(Transform rayTransform, Vector3 direction )
    {
        int rayCount = 0;
        List<GameObject> rays = new List<GameObject>();

        for (int y = 20; y> - 40; y -= 20)
        {
            for (int x = -20; x < 40; x += 20)
            {
                Vector3 startPos = new Vector3(rayTransform.localPosition.x + x,
                    rayTransform.localPosition.y + y,
                    rayTransform.localPosition.z);
                GameObject rayStart = Instantiate(emptyGO, startPos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;

            }
        }
        rayTransform.localRotation = Quaternion.Euler(direction);
        return (rays);
        }

    public (List<GameObject>facesHit, List<UnityEngine.Material> colorsHit) ReadFace(List<GameObject> rayStarts, Transform rayTransform)
    {
        List<GameObject> facesHit = new List<GameObject>();
        List<UnityEngine.Material> colorsHit = new List<UnityEngine.Material>();
        foreach (GameObject rayStart in rayStarts)
        {
            
            Vector3 ray = rayStart.transform.position;
            RaycastHit hit;

            //does the ray intersect any object

            if (Physics.Raycast(ray, rayTransform.forward, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(ray, rayTransform.forward * hit.distance, Color.yellow);
                facesHit.Add(hit.collider.gameObject);

                var renderer = hit.collider.gameObject.GetComponent<Renderer>();

                MeshCollider collider = hit.collider as MeshCollider;
                // Remember to handle case where collider is null because you hit a non-mesh primitive...

                Mesh mesh = collider.sharedMesh;

                // There are 3 indices stored per triangle
                int limit = hit.triangleIndex * 3;
                int submesh;
                for (submesh = 0; submesh < mesh.subMeshCount; submesh++)
                {
                    int numIndices = mesh.GetTriangles(submesh).Length;
                    if (numIndices > limit)
                        break;

                    limit -= numIndices;
                }
                colorsHit.Add(hit.collider.gameObject.GetComponent<Renderer>().sharedMaterials[submesh]);
                //print(hit.collider.gameObject.GetComponent<Renderer>().sharedMaterials[submesh]);
                //print(hit.collider.gameObject.name);
            }
            else
            {
                Debug.DrawRay(ray, rayTransform.forward * 1000, Color.green);
            }

        }

        return(facesHit, colorsHit);
        //cubeMap.Set(colorsHit);
    }

}
