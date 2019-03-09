using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlowerGenerator : MonoBehaviour
{
    [Header("Prefab Objects")]
    public Transform StemPrefab;
    public Transform leafPrefab;
    public Transform pebblePrefab;

    [Header("Main Flower setings")]
    public int pebbleAmount = 20;
    public int LeafAmount = 20;
    public int stemPieces = 4;
    public float flowerHeadTiltVariation = 4.0f;
    [Header("Stem settings")]
    public float stemAngleVariation = 4.0f;
    [Header("Leaf settings")]
    //public float leafAngleVariation = 360.0f;
    public float leafUpAngleVariation = 1.0f;
    public float leafStartUpAngle = 10.0f;
    public float leafStartScale = 1.0f;
    public float randomLeafOffset = 0.1f;
    public float scaleMultiplyMod = 1.2f;
    [Header("Pebble settings")]
    public float pebbleStartScale = 1.0f;
    public float randomPebbleScaleMod = 1.2f;
    public int concentricPebbles = 4;
    public float concentricAngle = 5.0f;
    public float concentricScaler = 0.8f;


    bool flowerGenerated = false;
    Transform prevStem;

    void Start()
    {
        Generate();
    }
    
    void Update()
    {
        
    }

    public void Generate()
    {
        if (flowerGenerated) {
            ClearOldFlower();
        }
        
        GenerateStem();
        //GenerateLeafs();
        GeneratePebbles();
        ExtraRandom();
        
    }

    public void GenerateStem()
    {
        
         

        //generate stem and leafs
        for (int i = 0; i < stemPieces; i++) {

            if (i > 3 && i != stemPieces-1) {
                // generate leafs somewhere from ground and with random
                int randomNr  = Random.Range(0,10);
                if (randomNr<8) {
                   
                    Transform prevStemChild = prevStem.Find("stemEnd");
                    //generate leafs
                    GenerateLeafs(prevStemChild);
                }
                
            }

            if (i == 0)
            {
                //First stem
                
                Transform FirstStem = Instantiate(StemPrefab, transform.position, Quaternion.identity);
                prevStem = FirstStem;
                Debug.Log("First Stems Placed");
            }
            else
            {
                if (i < stemPieces-1)
                {
                    Transform prevStemChild = prevStem.Find("stemEnd");
                    Vector3 prevStemEndPos = prevStemChild.transform.position;

                    Transform MiddleStem = Instantiate(StemPrefab, prevStemEndPos, Quaternion.Euler(Random.Range(-stemAngleVariation, stemAngleVariation), 0.0f, Random.Range(-stemAngleVariation, stemAngleVariation)));
                    MiddleStem.parent = prevStem.transform;
                    prevStem = MiddleStem;
                    Debug.Log("New Stem Placed");
                    
                }
                else {

                    //final stem

                    Transform prevStemChild = prevStem.Find("stemEnd");
                    Vector3 prevStemEndPos = prevStemChild.transform.position;

                    Transform EndStem = Instantiate(StemPrefab, prevStemEndPos, Quaternion.Euler(Random.Range(-stemAngleVariation, stemAngleVariation), 0.0f, Random.Range(-stemAngleVariation, stemAngleVariation)));
                    EndStem.parent = prevStem.transform;
                    prevStem = EndStem;

                    prevStemChild = prevStem.Find("stemEnd");
                    GameObject FlowerRoot = new GameObject("FlowerRoot");
                    FlowerRoot.transform.parent = prevStemChild;
                    FlowerRoot.transform.position = prevStemChild.position;
                    
                    FlowerRoot.tag = "FlowerRoot";
                    Debug.Log("All Stems Places");
                }
            }
        }

        //generate lines
    }

    void GenerateLeafs(Transform stem)
    {


        Color randomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        float randomRotation = Random.Range(-180.0f, 180.0f);

        for (int i = 0; i < LeafAmount; i++)
        {
            float randomUpAngle = 1 - (leafStartUpAngle + Random.Range(-leafUpAngleVariation, leafUpAngleVariation));

            Vector3 randomStemPosion = stem.position + new Vector3(Random.Range(-randomLeafOffset, randomLeafOffset), Random.Range(-randomLeafOffset, randomLeafOffset), Random.Range(-randomLeafOffset, randomLeafOffset));
            Transform Leaf = Instantiate(leafPrefab, randomStemPosion, Quaternion.Euler(0.0f, randomRotation, 0.0f));
            Leaf.parent = stem;

            Leaf.GetChild(0).GetComponent<Renderer>().material.color = randomColor;

            Leaf.localRotation *= Quaternion.AngleAxis(randomUpAngle, Vector3.forward);

            Leaf.localScale = leafStartScale * new Vector3(Random.Range(1, scaleMultiplyMod), Random.Range(1, scaleMultiplyMod), Random.Range(1, scaleMultiplyMod));
            
        }
        Debug.Log("GeneratedLeaves" + stem.gameObject.name);

    }

    void GeneratePebbles()
    {


        float pebbleStepAngle = 360 / (pebbleAmount);
        Transform flowerRoot = GameObject.Find("FlowerRoot").transform;
        Vector3 rootPosition = flowerRoot.position;
        int pebbleCount = 0;
        for (int i = 0; i<pebbleAmount;i++)
        {
            for (int v = 0; v < concentricPebbles;v++)
            {
                //instatiate pebble and rotate
                Transform pebble = Instantiate(pebblePrefab, rootPosition, Quaternion.identity);
                pebble.parent = flowerRoot;
                pebble.tag = "Pebble";
                pebble.name = "Pebble" + pebbleCount;
                pebble.transform.localRotation = Quaternion.AngleAxis(pebbleStepAngle * i, Vector3.up);
                pebble.transform.localRotation = pebble.transform.localRotation * Quaternion.AngleAxis(-concentricAngle * v, Vector3.forward);
                pebble.localScale = (pebbleStartScale - (v * concentricScaler)) * new Vector3(Random.Range(1, randomPebbleScaleMod), Random.Range(1, randomPebbleScaleMod), Random.Range(1, randomPebbleScaleMod));


             

                pebbleCount++;
            }
        }

    }

    void ExtraRandom()
    {
        Vector3 flowerHeadRandomRot = new Vector3(Random.Range(-flowerHeadTiltVariation, flowerHeadTiltVariation), Random.Range(-flowerHeadTiltVariation, flowerHeadTiltVariation), Random.Range(-flowerHeadTiltVariation, flowerHeadTiltVariation));
        GameObject flowerRoot = GameObject.Find("FlowerRoot");
        flowerRoot.transform.localRotation = Quaternion.Euler(flowerHeadRandomRot);

        flowerGenerated = true;

    }

    void ClearOldFlower() {




        //    //Destroy roots
        GameObject[] Stems;

        Stems = GameObject.FindGameObjectsWithTag("Stem");

        foreach (GameObject stem in Stems)
        {
            Destroy(stem);
        }

        //Destroy flower roots

        GameObject[] Roots;

        Roots = GameObject.FindGameObjectsWithTag("FlowerRoot");

        foreach (GameObject root in Roots)
        {
            Destroy(root);
        }

        GameObject[] Leafs;

        Leafs = GameObject.FindGameObjectsWithTag("Leaf");

        foreach (GameObject leaf in Leafs)
        {
            Destroy(leaf);
        }

        GameObject[] Pebbles;

        Pebbles = GameObject.FindGameObjectsWithTag("Pebble");

        foreach (GameObject Pebble in Pebbles)
        {
            Destroy(Pebble);
        }

    }


}
