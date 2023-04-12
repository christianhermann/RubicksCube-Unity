using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectFace : MonoBehaviour
{

    CubeState cubeState;
    ReadCube readCube;
    int layerMask =  1 << 6;
    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0))
        {
            readCube.ReadState();

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000f, layerMask))
            {
                
                MeshCollider collider = hit.collider as MeshCollider;
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
                UnityEngine.Material materialHit = (hit.collider.gameObject.GetComponent<Renderer>().sharedMaterials[submesh]);



                var cubeSides = new List<(List<GameObject> cubeSideFace, List<UnityEngine.Material> cubeMaterial)>
                    {
                (cubeState.up, cubeState.cup),
                (cubeState.down, cubeState.cdown),
                (cubeState.left, cubeState.cleft),
                (cubeState.right, cubeState.cright),
                (cubeState.front, cubeState.cfront),
                (cubeState.back, cubeState.cback)

                };

                foreach ((List<GameObject> cubeSideFace, List<UnityEngine.Material> cubeMaterial) cubeSide in cubeSides)
                {


                    // if (cubeSide.cubeMaterial.Contains(materialHit) && cubeSide.cubeSideFace.Contains(collider.gameObject))
                    int cubeHit = cubeSide.cubeSideFace.FindIndex(i => i.name == collider.gameObject.name);
                    if (cubeHit < 0) continue;
                    if (cubeSide.cubeMaterial[cubeHit] == materialHit)
 
                    {
                        cubeState.PickUp(cubeSide.cubeSideFace);
                        cubeSide.cubeSideFace[4].transform.GetComponent<PivotRotation>().Rotate(cubeSide.cubeSideFace);

                    }
                }
            }

        }
    }
}
