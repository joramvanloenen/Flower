using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenSingles : MonoBehaviour
{
    public int searcherAmount = 1;
    searcher[] searchersArray;

    public float searcherSpeed = 0.5f;
    public float lookatSpeed = 0.5f;

    public float randomPosMod = 5.0f;

    Transform spawnPos;
    public Mesh sphereMesh;
    public Material sphereMaterial;

    public Transform Target;

    // Start is called before the first frame update
    void Start()
    {
        searchersArray = new searcher[searcherAmount];
        spawnPos = this.transform;

        for (int i=0;i<searcherAmount;i++) {
            searchersArray[i] = new searcher(spawnPos.position, searcherSpeed,lookatSpeed, sphereMesh, sphereMaterial, Target.position, randomPosMod);
            Debug.Log("Added searcher");
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < searcherAmount; i++)
        {
            searchersArray[i].Move();
        }
    }

    public class searcher : MonoBehaviour {
        
        public GameObject searcherObject;
        Rigidbody SearcherRigibody;
        Vector3 moveDirection;
        Vector3 targetPosition;
        float searcherSpeed;
        float lookatSpeed;


        public searcher(Vector3 StartPos,float startSpeed,float lookSpeed,Mesh sphereMesh, Material sphereMaterial, Vector3 targ, float randomPosMod) {

            targetPosition = targ;
            searcherSpeed = startSpeed;
            lookatSpeed = lookSpeed;
            searcherObject = new GameObject("searcherRoot");
            searcherObject.transform.position = StartPos + new Vector3(Random.Range(-randomPosMod, randomPosMod), Random.Range(-randomPosMod, randomPosMod), Random.Range(-randomPosMod, randomPosMod));
            searcherObject.AddComponent<MeshFilter>().mesh = sphereMesh;
            searcherObject.AddComponent<MeshRenderer>().material = sphereMaterial;
            searcherObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            SearcherRigibody = searcherObject.AddComponent<Rigidbody>();
            SearcherRigibody.drag = 1.4f;
            SearcherRigibody.angularDrag = 1.5f;
            SearcherRigibody.useGravity = false;



        }
        void Start()
        {
            Debug.Log("started");
        }
        public void Move()
        {

            //searcher update
            Collider[] hitColliders = Physics.OverlapSphere(searcherObject.transform.position, 100.0f);

            int i = 0;
            if (i< hitColliders.Length) {

                Collider tMin = null;
                float minDist = Mathf.Infinity;
                Vector3 currentPos = searcherObject.transform.position;
                foreach (Collider t in hitColliders)
                {
                    float dist = Vector3.Distance(t.gameObject.transform.position, searcherObject.transform.position);
                    if (dist < minDist)
                    {
                        tMin = t;
                        minDist = dist;
                    }
                }

                Vector3 colliderDirection = tMin.transform.position - searcherObject.transform.position;

                RaycastHit hit;
                if (Physics.Raycast(searcherObject.transform.position, colliderDirection, out hit, 100.0f)) {
                    Debug.DrawRay(searcherObject.transform.position, colliderDirection * hit.distance, Color.yellow);

                    



                    Vector3 targetDirection = hit.transform.position - searcherObject.transform.position;

                    float step = lookatSpeed * Time.deltaTime;
                    Vector3 newDir = Vector3.RotateTowards(searcherObject.transform.forward, targetDirection, step, 0.0f);
                    searcherObject.transform.rotation = Quaternion.LookRotation(newDir);
                    SearcherRigibody.AddRelativeForce(Vector3.forward * searcherSpeed * Time.deltaTime);


                }

                
            }
            
        }


    }
}
