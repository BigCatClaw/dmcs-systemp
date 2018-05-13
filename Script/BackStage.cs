using UnityEngine;

public class BackStage : MonoBehaviour
{

    public bool IsResult;
    public bool IsCross;
    public GameObject searchBG;
    public GameObject resultBG;
    public GameObject back;
    public GameObject cross;
    public GameObject home;
    public GameObject routeGoogleMap;
    public GameObject routeInsideBuilding;
    public Camera mainCamera;
    public GameObject[] MainBuilding;
    public GameObject moreView;

    public void SetActiveUI()
    {
        if (IsResult)
        {
            searchBG.SetActive(false);
            resultBG.SetActive(true);
            back.SetActive(false);
            cross.SetActive(true);
            home.SetActive(false);
            routeGoogleMap.SetActive(true);
            routeInsideBuilding.SetActive(true);
        }
        else
        {
            searchBG.SetActive(false);
            resultBG.SetActive(false);
            back.SetActive(false);
            cross.SetActive(false);
            home.SetActive(true);
            routeGoogleMap.SetActive(true);
            routeInsideBuilding.SetActive(false);
            moreView.SetActive(true);
            MainBuilding[0].GetComponent<Lean.Touch.LeanRotate>().enabled = true;
            MainBuilding[1].GetComponent<Lean.Touch.LeanRotate>().enabled = true;
            MainBuilding[2].GetComponent<Lean.Touch.LeanRotate>().enabled = true;
            MainBuilding[3].GetComponent<Lean.Touch.LeanRotate>().enabled = true;
            mainCamera.GetComponent<Lean.Touch.LeanCameraZoomSmooth>().enabled = true;
            mainCamera.GetComponent<Lean.Touch.LeanCameraMoveSmooth>().enabled = true;
        }
    }
}
