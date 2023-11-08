using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeapon : MonoBehaviour
{
    static public int selectIndex;

    // Start is called before the first frame update
    void Start()
    {
        selectIndex = 0;
        transform.GetChild(selectIndex).gameObject.SetActive(true);

    }

    private void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0 && selectIndex<transform.childCount-1)
        {
            transform.GetChild(selectIndex).gameObject.SetActive(false);
            selectIndex +=1;
            transform.GetChild(selectIndex).gameObject.SetActive(true);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && selectIndex > 0)
        {
            transform.GetChild(selectIndex).gameObject.SetActive(false);
            selectIndex -= 1;
            transform.GetChild(selectIndex).gameObject.SetActive(true);
        }

    }
}
