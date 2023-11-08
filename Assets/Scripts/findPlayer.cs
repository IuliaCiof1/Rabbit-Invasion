using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindGameObjectsInLayer(6);
        //return ret;
    }
    void FindGameObjectsInLayer(int layer)
    {
        var goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var goList = new System.Collections.Generic.List<GameObject>();
        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == layer)
            {
                Debug.Log(goArray[i].name);
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0)
        {
            //return null;
        }
        //return goList.ToArray();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
