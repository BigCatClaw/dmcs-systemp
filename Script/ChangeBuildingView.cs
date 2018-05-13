using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBuildingView : MonoBehaviour {

    public List<GameObject> Building;
    public GameObject FunctionObject;
    private CreateSearchResult func;
    // 0 = MATH; 1 = CHEM2; 2 = MHMK; 3 = TAB

    void Start()
    {
        func = FunctionObject.GetComponent<CreateSearchResult>();
    }

    public void GoLeft()
    {
        if (Building[0].activeSelf == true)
        {
            Building[0].SetActive(false);
            Building[3].SetActive(true);
        }
        else if (Building[1].activeSelf == true)
        {
            Building[1].SetActive(false);
            Building[0].SetActive(true);
        }
        else if (Building[2].activeSelf == true)
        {
            Building[2].SetActive(false);
            Building[1].SetActive(true);
        }
        else if (Building[3].activeSelf == true)
        {
            Building[3].SetActive(false);
            Building[2].SetActive(true);
        }
        func.SetDefaultView();
    }

    public void GoRight()
    {
        if (Building[0].activeSelf == true)
        {
            Building[0].SetActive(false);
            Building[1].SetActive(true);
        }
        else if (Building[1].activeSelf == true)
        {
            Building[1].SetActive(false);
            Building[2].SetActive(true);
        }
        else if (Building[2].activeSelf == true)
        {
            Building[2].SetActive(false);
            Building[3].SetActive(true);
        }
        else if (Building[3].activeSelf == true)
        {
            Building[3].SetActive(false);
            Building[0].SetActive(true);
        }
        func.SetDefaultView();
    }

}
