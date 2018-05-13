using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CreateSearchResult : MonoBehaviour {

    public Text searchInput;
    public GameObject searchResult;
    public GameObject searchview;
    public GameObject resultview;
    public GameObject routeButton;
    public GameObject googleButton;
    public GameObject backButton;
    public GameObject crossButton;
    public GameObject Char;
    public GameObject currentChar;
    public GameObject moveView;
    public Dropdown categoryIden;
    public Transform contentPanel;
    public Transform contentPanel2;
    public List<SampleButtonScript> items;
    private string queryURL = "http://bigcat.thddns.net:3442/dmcs/getdata.php";
    private string imgURL = "http://bigcat.thddns.net:3442/dmcs/people/";
    //public GameObject[] viewpoints;
    private Vector3 offset;
    public Camera currentCamera;
    public Button currentButton;
    public List<GameObject> MainBuilding; // 0 = MATH; 1 = CHEM2; 2 = MHMK; 3 = TAB
    //public GameObject MATH;
    //public GameObject CHEM2;
    //public GameObject MHMK;
    //public GameObject TAB;
    private LeanCameraZoomSmooth ZoomScript;

    private void Start()
    {
        offset = new Vector3(-51.109722f, 129.37406f, 51.92314f);
    }

    public void PopulateList()
    {
        if (string.IsNullOrWhiteSpace(searchInput.text))
        {
            return;
        }
        /*if (viewpoints == null)
            viewpoints = GameObject.FindGameObjectsWithTag("viewpoint");
        Debug.Log(viewpoints.Length);*/
        foreach (Transform child in contentPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        WWW www = new WWW(queryURL + "?category=" + categoryIden.value + "&keyword=" + searchInput.text); //GET data is sent via the URL

        while (!www.isDone && string.IsNullOrEmpty(www.error))
        {
            yield return null;
        }

        if (string.IsNullOrEmpty(www.error))
        {
            string[] Allresult = www.text.Split("#".ToCharArray());
            string[] result = Allresult[0].Split(";".ToCharArray());
            string[] result2 = Allresult[1].Split(";".ToCharArray());
            for (int i = 0; i < (int)(result.Length / 5); i++)
            {
                /*int check = 0;
                if (result[i * 5 + 1].Contains(keyword)) check++;
                if (result[i * 5 + 2].Contains(keyword)) check++;
                if (result[i * 5 + 3].Contains(keyword)) check++;
                if (result[i * 5 + 4].Contains(keyword)) check++;
                //Debug.Log(check);
                if (check == 0) continue;*/
                GameObject newResult = Instantiate(searchResult) as GameObject;
                SampleButtonScript buttonScript = newResult.GetComponent<SampleButtonScript>();
                int bd = i * 5 + 2;
                int fl = i * 5 + 3;
                //buttonScript.section.gameObject.SetActive(false);
                //buttonScript.time.gameObject.SetActive(false);
                buttonScript.nameLabel.text = result[i * 5 + 1];
                //Debug.Log(result[i*5 + 1]);
                buttonScript.placeLabel.text = result[bd] + ", Floor " + result[fl] + ", Room " + result[i * 5 + 4];
                //Debug.Log(result[i*5 + 2]);
                /*WWW wwwimg = new WWW(imgURL + result[i * 5] + ".png");
                while (!wwwimg.isDone && string.IsNullOrEmpty(wwwimg.error))
                {
                    yield return null;
                }
                buttonScript.exImage.texture = wwwimg.texture;*/
                string ori_roomname = result[i * 5 + 4];
                string roomname = ori_roomname.Replace("/", string.Empty);
                roomname = roomname.Replace(".", string.Empty);
                //Debug.Log("roomname : "+roomname);
                GameObject target = FindObject(result[bd], result[fl], roomname);
                //Debug.Log("target1 : "+target.name);
                buttonScript.button.onClick.AddListener(() => ChangeUI(newResult, target, ori_roomname));
                buttonScript.button.onClick.AddListener(() => ShowOnlyTargetObject(result[bd], result[fl]));
                buttonScript.button.onClick.AddListener(() => ChangeCameraView(target));

                newResult.transform.SetParent(contentPanel);
                newResult.transform.localScale = new Vector3(1, 1, 1);
                items.Add(buttonScript);
                //Debug.Log("finish");
            }
            for (int i = 0; i < (int)(result2.Length / 7); i++)
            {
                GameObject newResult = Instantiate(searchResult) as GameObject;
                SampleButtonScript buttonScript = newResult.GetComponent<SampleButtonScript>();
                int bd = i * 7 + 2;
                int fl = i * 7 + 3;

                buttonScript.nameLabel.text = result2[i * 7] + " " + result2[i * 7 + 1];

                buttonScript.placeLabel.text = result2[bd] + ", Floor " + result2[fl] + ", Room " + result2[i * 7 + 4];

                buttonScript.section.text = "SEC " + result2[i * 7 + 5];
                string sptemp = result2[i * 7 + 6];
                string spday = "", sptime = "";
                for (int x = 0; x < sptemp.Length; ++x)
                {
                    if (sptemp[x] >= '0' && sptemp[x] <= '9')
                    {
                        spday  = sptemp.Substring(0, (x - 1));
                        sptime = sptemp.Substring(x);
                        break;
                    } 
                }
                buttonScript.day.text = spday;
                buttonScript.time.text = sptime;
                buttonScript.section.gameObject.SetActive(true);
                buttonScript.day.gameObject.SetActive(true);
                buttonScript.time.gameObject.SetActive(true);

                string ori_roomname = result2[i * 7 + 4];
                string roomname = ori_roomname.Replace("/", string.Empty);
                roomname = roomname.Replace(".", string.Empty);
                //Debug.Log("roomname : " + roomname);
                GameObject target = FindObject(result2[bd], result2[fl], roomname);
                //Debug.Log("FL : " + fl);
                buttonScript.button.onClick.AddListener(() => ChangeUI(newResult, target, ori_roomname));
                buttonScript.button.onClick.AddListener(() => ShowOnlyTargetObject(result2[bd], result2[fl]));
                buttonScript.button.onClick.AddListener(() => ChangeCameraView(target));

                newResult.transform.SetParent(contentPanel);
                newResult.transform.localScale = new Vector3(1, 1, 1);
                items.Add(buttonScript);
            }
            for (int i = 0; i < (int)(result.Length / 5); i++)
            {
                WWW wwwimg = new WWW(imgURL + result[i * 5] + ".jpg");
                while (!wwwimg.isDone && string.IsNullOrEmpty(wwwimg.error))
                {
                    yield return null;
                }
                if (string.IsNullOrEmpty(wwwimg.error))
                {
                    items[i].exImage.texture = wwwimg.texture;
                    items[i].exImage.color = Color.white;
                    items[i].mask.gameObject.SetActive(true);
                }
            }
            items.Clear();
        }
        else Debug.LogWarning(www.error);
    }

    public GameObject FindObject(string building, string floor, string room)
    {
        //Transform[] trs;
        //Debug.Log(building);
        //Debug.Log(floor);
        //Debug.Log(room);
        switch (building)
        {
            case "MATH":
                return MainBuilding[0].transform.Find(floor).GetChild(2).Find(room).gameObject;
            case "CHEM2":
                return MainBuilding[1].transform.Find(floor).GetChild(1).Find(room).gameObject;
            case "MHMK":
                return MainBuilding[2].transform.Find(floor).GetChild(2).Find(room).gameObject;
            case "TAB":
                return MainBuilding[3].transform.Find(floor).GetChild(1).Find(room).gameObject;
            default:
                return null;
        }
    }

    public void ChangeCameraView(GameObject target)
    {
        SetDefaultRotation();
        //Debug.Log("target2 : "+target.name);
        ZoomScript = currentCamera.GetComponent<LeanCameraZoomSmooth>();
        ZoomScript.Zoom = 40;
        currentCamera.transform.localEulerAngles = new Vector3(60f, 135f, 0f);
        currentCamera.transform.position = target.transform.position + offset;
        //Debug.Log(target.transform.position + offset);
    }

    void ChangeUI(GameObject result, GameObject target, string targetName)
    {
        currentButton = result.GetComponent<SampleButtonScript>().GetComponent<Button>();
        foreach (Transform child in contentPanel2.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        searchview.SetActive(false);
        currentCamera.GetComponent<Lean.Touch.LeanCameraZoomSmooth>().enabled = true;
        MainBuilding[0].GetComponent<Lean.Touch.LeanRotate>().enabled = false;
        MainBuilding[1].GetComponent<Lean.Touch.LeanRotate>().enabled = false;
        MainBuilding[2].GetComponent<Lean.Touch.LeanRotate>().enabled = false;
        MainBuilding[3].GetComponent<Lean.Touch.LeanRotate>().enabled = false;
        SetDefaultRotation();
        GameObject clone = Instantiate(result, contentPanel2, false) as GameObject;
        Destroy(clone.GetComponent<Button>());
        clone.transform.GetChild(3).gameObject.SetActive(false);
        resultview.SetActive(true);
        NavigationRoom Nav = routeButton.GetComponent<NavigationRoom>();
        Nav.target = target;
        Nav.targetName = targetName;
        moveView.SetActive(false);
        routeButton.SetActive(true);
        backButton.SetActive(false);
        crossButton.SetActive(true);
        googleButton.SetActive(true);
        backButton.GetComponent<BackStage>().IsResult = true;
        if (currentChar != null) {
            Destroy(currentChar);
            currentChar = null;
        }
        currentChar = Instantiate(Char.transform.GetChild(0).gameObject, target.transform.position, Quaternion.Euler(new Vector3(0f, 315f, 0f)), target.transform);
        currentChar.SetActive(true);
    }

    public void ShowOnlyTargetObject(string building, string floor)
    {
        bool ShowNext = true;
        switch (building)
        {
            case "MATH":
                googleButton.GetComponent<OpenUrl>().url = @"https://www.google.co.th/maps/place/%E0%B8%AD%E0%B8%B2%E0%B8%84%E0%B8%B2%E0%B8%A3%E0%B8%A1%E0%B8%AB%E0%B8%B2%E0%B8%A7%E0%B8%8A%E0%B8%B4%E0%B8%A3%E0%B8%B8%E0%B8%93%E0%B8%AB%E0%B8%B4%E0%B8%A8/@13.7367397,100.5310551,19.75z/data=!4m5!3m4!1s0x0:0x6d3b7bdcf2d8eef2!8m2!3d13.7366729!4d100.531401";
                MainBuilding[0].SetActive(true);
                MainBuilding[1].SetActive(false);
                MainBuilding[2].SetActive(false);
                MainBuilding[3].SetActive(false);
                if (floor.Equals("1_2")) ShowNext = false;
                MainBuilding[0].transform.GetChild(3).gameObject.SetActive(ShowNext);
                MainBuilding[0].transform.GetChild(4).gameObject.SetActive(ShowNext);
                MainBuilding[0].transform.GetChild(5).gameObject.SetActive(ShowNext);
                MainBuilding[0].transform.GetChild(6).gameObject.SetActive(ShowNext);
                if (floor.Equals("5")) ShowNext = false;
                MainBuilding[0].transform.GetChild(7).gameObject.SetActive(ShowNext);
                if (floor.Equals("6")) ShowNext = false;
                MainBuilding[0].transform.GetChild(8).gameObject.SetActive(ShowNext);
                if (floor.Equals("7")) ShowNext = false;
                MainBuilding[0].transform.GetChild(9).gameObject.SetActive(ShowNext);
                if (floor.Equals("8")) ShowNext = false;
                MainBuilding[0].transform.GetChild(10).gameObject.SetActive(ShowNext);
                if (floor.Equals("9")) ShowNext = false;
                MainBuilding[0].transform.GetChild(11).gameObject.SetActive(ShowNext);
                if (floor.Equals("10")) ShowNext = false;
                MainBuilding[0].transform.GetChild(12).gameObject.SetActive(ShowNext);
                if (floor.Equals("11")) ShowNext = false;
                MainBuilding[0].transform.GetChild(13).gameObject.SetActive(ShowNext);
                if (floor.Equals("12")) ShowNext = false;
                MainBuilding[0].transform.GetChild(14).gameObject.SetActive(ShowNext);
                if (floor.Equals("13")) ShowNext = false;
                MainBuilding[0].transform.GetChild(15).gameObject.SetActive(ShowNext);
                if (floor.Equals("14")) ShowNext = false;
                MainBuilding[0].transform.GetChild(16).gameObject.SetActive(ShowNext);
                MainBuilding[0].transform.GetChild(17).gameObject.SetActive(ShowNext);
                MainBuilding[0].transform.GetChild(18).gameObject.SetActive(ShowNext);
                break;
            case "CHEM2":
                googleButton.GetComponent<OpenUrl>().url = @"https://www.google.com/maps/place/%E0%B8%AD%E0%B8%B2%E0%B8%84%E0%B8%B2%E0%B8%A3%E0%B9%80%E0%B8%84%E0%B8%A1%E0%B8%B5+2+%E0%B8%84%E0%B8%93%E0%B8%B0%E0%B8%A7%E0%B8%B4%E0%B8%97%E0%B8%A2%E0%B8%B2%E0%B8%A8%E0%B8%B2%E0%B8%AA%E0%B8%95%E0%B8%A3%E0%B9%8C+%E0%B8%88%E0%B8%B8%E0%B8%AC%E0%B8%B2%E0%B8%A5%E0%B8%87%E0%B8%81%E0%B8%A3%E0%B8%93%E0%B9%8C%E0%B8%A1%E0%B8%AB%E0%B8%B2%E0%B8%A7%E0%B8%B4%E0%B8%97%E0%B8%A2%E0%B8%B2%E0%B8%A5%E0%B8%B1%E0%B8%A2/@13.7375653,100.5298524,17.25z/data=!4m5!3m4!1s0x0:0x7e60b7bde78a8972!8m2!3d13.7373206!4d100.5308917?hl=th-TH";
                MainBuilding[0].SetActive(false);
                MainBuilding[1].SetActive(true);
                MainBuilding[2].SetActive(false);
                MainBuilding[3].SetActive(false);
                if (floor.Equals("1")) ShowNext = false;
                MainBuilding[1].transform.GetChild(3).gameObject.SetActive(ShowNext);
                if (floor.Equals("2")) ShowNext = false;
                MainBuilding[1].transform.GetChild(4).gameObject.SetActive(ShowNext);
                break;
            case "MHMK":
                googleButton.GetComponent<OpenUrl>().url = @"https://www.google.co.th/maps/place/%E0%B8%AD%E0%B8%B2%E0%B8%84%E0%B8%B2%E0%B8%A3%E0%B8%A1%E0%B8%AB%E0%B8%B2%E0%B8%A1%E0%B8%81%E0%B8%B8%E0%B8%8E/@13.7359854,100.5285835,17.25z/data=!4m5!3m4!1s0x30e29ed54662d125:0x78f74973283617c3!8m2!3d13.7359755!4d100.5304481?hl=th";
                MainBuilding[0].SetActive(false);
                MainBuilding[1].SetActive(false);
                MainBuilding[2].SetActive(true);
                MainBuilding[3].SetActive(false);
                if (floor.Equals("1")) ShowNext = false;
                MainBuilding[2].transform.GetChild(3).gameObject.SetActive(ShowNext);
                if (floor.Equals("M")) ShowNext = false;
                MainBuilding[2].transform.GetChild(4).gameObject.SetActive(ShowNext);
                if (floor.Equals("2")) ShowNext = false;
                MainBuilding[2].transform.GetChild(5).gameObject.SetActive(ShowNext);
                if (floor.Equals("3")) ShowNext = false;
                MainBuilding[2].transform.GetChild(6).gameObject.SetActive(ShowNext);
                break;
            case "TAB":
                googleButton.GetComponent<OpenUrl>().url = @"https://www.google.co.th/maps/place/%E0%B8%AD%E0%B8%B2%E0%B8%84%E0%B8%B2%E0%B8%A3%E0%B9%81%E0%B8%96%E0%B8%9A+%E0%B8%99%E0%B8%B5%E0%B8%A5%E0%B8%B0%E0%B8%99%E0%B8%B4%E0%B8%98%E0%B8%B4/@13.7366519,100.5288239,17.25z/data=!4m5!3m4!1s0x30e29ed53bb94c45:0x8d68b360d39d4376!8m2!3d13.7365458!4d100.5305554?hl=th";
                MainBuilding[0].SetActive(false);
                MainBuilding[1].SetActive(false);
                MainBuilding[2].SetActive(false);
                MainBuilding[3].SetActive(true);
                if (floor.Equals("2")) ShowNext = false;
                MainBuilding[3].transform.GetChild(2).gameObject.SetActive(ShowNext);
                break;
            default:
                break;
        }
    }

    public void SetDefaultRotation()
    {
        //MainBuilding[0].transform.position = new Vector3(0f, 118.28f, 0f);
        MainBuilding[0].transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        //MainBuilding[0].transform.localScale = new Vector3(1f, 1f, 1f);
        MainBuilding[1].transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        MainBuilding[2].transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        MainBuilding[3].transform.localEulerAngles = new Vector3(0f, 0f, 0f);
    }

    public void SetDefaultView()
    {
        SetDefaultRotation();
        backButton.GetComponent<BackStage>().IsResult = false;
        ZoomScript = currentCamera.GetComponent<LeanCameraZoomSmooth>();
        ZoomScript.Zoom = 120;
        LeanCameraMoveSmooth LCMS = currentCamera.GetComponent<LeanCameraMoveSmooth>();
        LCMS.ClampPositionX_max = -20f;
        LCMS.ClampPositionX_min = -150f;
        currentCamera.transform.localEulerAngles = new Vector3(30f, 135f, 0f);
        currentCamera.transform.position = new Vector3(-95.11f, 186.56f, 118.63f);
        if (currentChar != null) Destroy(currentChar);
        currentChar = null;
        currentCamera.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        if (MainBuilding[0].activeSelf)
        {
            ShowOnlyTargetObject("MATH", "0");
        }
        if (MainBuilding[1].activeSelf)
        {
            LCMS.ClampPositionX_max += 100f;
            currentCamera.GetComponent<LeanCameraMoveSmooth>().ClampPositionX_min += 100f;
            currentCamera.transform.position += new Vector3(100f, 0f, 0f);
            ShowOnlyTargetObject("CHEM2", "0");
        }
        if (MainBuilding[2].activeSelf)
        {
            LCMS.ClampPositionX_max += 300f;
            LCMS.ClampPositionX_min += 300f;
            currentCamera.transform.position += new Vector3(300f, 0f, 0f);
            ShowOnlyTargetObject("MHMK", "0");
        }
        if (MainBuilding[3].activeSelf)
        {
            LCMS.ClampPositionX_max += 400f;
            LCMS.ClampPositionX_min += 400f;
            currentCamera.transform.position += new Vector3(400f, 0f, 0f);
            ShowOnlyTargetObject("TAB", "0");
        }
        currentCamera.transform.localEulerAngles = new Vector3(30f, 135f, 0f);
        moveView.SetActive(true);
        googleButton.SetActive(true);
        MainBuilding[0].GetComponent<Lean.Touch.LeanRotate>().enabled = true;
        MainBuilding[1].GetComponent<Lean.Touch.LeanRotate>().enabled = true;
        MainBuilding[2].GetComponent<Lean.Touch.LeanRotate>().enabled = true;
        MainBuilding[3].GetComponent<Lean.Touch.LeanRotate>().enabled = true;
        currentCamera.GetComponent<Lean.Touch.LeanCameraZoomSmooth>().enabled = true;
        currentCamera.GetComponent<Lean.Touch.LeanCameraMoveSmooth>().enabled = true;
    }
}
