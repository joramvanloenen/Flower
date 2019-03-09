using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    public Vector3 spawnPos;
    static int maxLines = 10;
    public int linePositions = 10;
    public Line[] allLines = new Line[maxLines];
    public Transform debugSphere;
    public float lineWidth = 0.1f;
    

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < allLines.Length; i++) {
            allLines[i] = new Line(spawnPos, linePositions, debugSphere, lineWidth);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    

    public class Line : MonoBehaviour
    {

        public Vector3 startPos;
        public int maxLinePositions;
        public Vector3[] linePositions;
        public Color LineColor = Color.red;
        public Transform debugSphere;
        public GameObject LineRoot;
        public LineRenderer linerenderer;



        

        public Line(Vector3 Pos, int maxPos, Transform dbSphere,float lineWidth)
        {
            debugSphere = dbSphere;
            startPos = Pos;
            maxLinePositions = maxPos;
            linePositions = new Vector3[maxLinePositions];


            LineRoot = new GameObject("LineRoot");
            linerenderer = LineRoot.AddComponent<LineRenderer>();
            linerenderer.material = new Material(Shader.Find("Sprites/Default"));
            linerenderer.widthMultiplier = lineWidth;
            linerenderer.positionCount = maxLinePositions;

            PopulateLine();
        }

        void Update()
        {
            //MoveLines();
        }

        void MoveLines() {

            for (int i = 0; i < linePositions.Length; i++)
            {

                linerenderer.SetPosition(i, linePositions[i]);
            }
        }
        void PopulateLine() {
            LineColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            //Debug.Log(linePositions.Length);
            for (int i = 0; i < linePositions.Length; i++)
            {
                //Set Line point positions
                linePositions[i] = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                Debug.Log(linePositions[i]);

                //Transform positionObject = Instantiate(debugSphere, linePositions[i], Quaternion.identity);
                //positionObject.GetComponent<Renderer>().material.color = LineColor;

                linerenderer.SetPosition(i, linePositions[i]);
            }


        }
    }  
}
