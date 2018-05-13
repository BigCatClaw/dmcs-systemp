using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NavigationRoom : MonoBehaviour {

    public Camera currentCamera;
    public GameObject GameControl;
    public GameObject target;
    public GameObject agentObj;
    public GameObject NavBar;
    public string targetName;
    public NavMeshAgent Agent;
    public GameObject symbols;
    public Dropdown ddlist;
    public Button BackButton;
    private Vector3 currentDestination;
    private Vector3 currentStart;
    private Vector3 offset = new Vector3(-51.109722f, 129.37406f, 51.92314f);
    private bool IsNavEnable = false;
    private int stage = 0;
    private bool clicked = false;
    private GameObject room;
    private GameObject floor;
    private GameObject building;
    //private GameObject broom;
    //private GameObject bfloor;
    //private GameObject bbuilding;
    private CreateSearchResult func;
    private const int left = 0;
    private const int right = 1;
    private const int dest = 2;
    private const int elev_stair = 3;
    private const int dest_left = 4;
    private const int dest_right = 5;
    private const int door = 6;
    private const int bd = 7;
    private const int staironly = 8;
    private const int up = 9;
    private bool IsTakeStair = false;
    private bool IsEnterMATH = false;
    private Vector3 vectorIncrease;
    //private bool mustDestroy = false;
    //private bool IsBackward = false;
    private bool C2 = false;
    private bool C3 = false;
    private List<string> C2RoomList = new List<string> {
        "7084", "8097", "8096", "8095", "9097", "9096", "90952", "90951",
        "100810", "10089", "11086", "11085", "12085", "130914", "130913", "130912",
        "130911", "130910", "13098", "13097", "140814", "140813", "140812", "140811",
        "140810", "14088", "14087" };
    private List<string> C3RoomList = new List<string> {
        "6086", "6084", "6083", "6082", "7083", "7082", "808", "9091", "1008D", "10086",
        "10088", "10085", "10084", "10083", "10082", "11084", "11083", "11082", "1108A",
        "12084", "12083", "12082", "12088", "12087", "12086", "13099", "13096", "13095",
        "13094", "13093", "13092", "1309241", "14089", "14086", "14085", "14084", "14083",
        "14082", "1408241" };
    private List<string> C3Right = new List<string> {
        "1008D", "13099", "14089" };
    private List<string> StartPointMATH = new List<string> {
        "MATH West Entrance", "MATH East Entrance" };
    private List<string> StartPointCHEM2 = new List<string> {
        "CHEM2 Southeast Entrance", "CHEM2 East Entrance" };
    private List<string> StartPointMHMK = new List<string> {
        "MHMK East Entrance", "MHMK North Entrance", "MHMK West Entrance" };
    private List<string> StartPointTAB = new List<string> {
        "TAB South Entrance", "TAB East Entrance", "TAB North Entrance" };
    /*private List<string> AllRoomList = new List<string> {
        "MATH", "CHEM2", "MHMK", "TAB", "MATH 508/1", "MATH 508/2", "MATH 508/3", "MATH 509/1", "MATH 509/2",
        "MATH 608/1", "MATH 608/2", "MATH 608/3", "MATH 608/4", "MATH 608/5", "MATH 608/6", "MATH 608/8", "MATH 608/9",
        "MATH 708/1", "MATH 708/2", "MATH 708/3", "MATH 708/4", "MATH 708/5", "MATH 708/7",
        "MATH 808", "MATH 809/2", "MATH 809/3", "MATH 809/4", "MATH 809/5", "MATH 809/6", "MATH 809/7", "MATH 809/9",
        "MATH 909/1", "MATH 909/2", "MATH 909/3", "MATH 909/4", "MATH 909/5.1", "MATH 909/5.2", "MATH 909/6", "MATH 909/7", "MATH 909/8", "MATH 909/9",
        "MATH 1008A", "MATH 1008B", "MATH 1008C", "MATH 1008D", "MATH 1008/2", "MATH 1008/3", "MATH 1008/4", "MATH 1008/5", "MATH 1008/6", "MATH 1008/8", "MATH 1008/9", "MATH 1008/10", "MATH 1008/12.1", "MATH 1008/12.2", "MATH 1008/13", "MATH 1008/14", "MATH 1008/16", "MATH 1008/19",
        "MATH 1108A", "MATH 1108/2", "MATH 1108/3", "MATH 1108/4", "MATH 1108/5", "MATH 1108/6", "MATH 1108/7", "MATH 1108/8", "MATH 1108/10", "MATH 1108/11", "MATH 1108/12", "MATH 1108/13", "MATH 1108/14", "MATH 1108/15",
        "MATH 1208A", "MATH 1208/2", "MATH 1208/3", "MATH 1208/4", "MATH 1208/5", "MATH 1208/6", "MATH 1208/7", "MATH 1208/8", "MATH 1208/9", "MATH 1208/11", "MATH 1208/12", "MATH 1208/13", "MATH 1208/14", "MATH 1208/15", "MATH 1208/16", "MATH 1208/17", "MATH 1208/19",
        "MATH 1309A", "MATH 1309/2", "MATH 1309/3", "MATH 1309/4", "MATH 1309/5", "MATH 1309/6", "MATH 1309/7", "MATH 1309/8", "MATH 1309/9", "MATH 1309/10", "MATH 1309/11", "MATH 1309/12", "MATH 1309/13", "MATH 1309/14", "MATH 1309/16", "MATH 1309/17", "MATH 1309/18", "MATH 1309/19", "MATH 1309/20", "MATH 1309/21", "MATH 1309/22", "MATH 1309/24.1", "MATH 1309/24.2",
        "MATH 1408A", "MATH 1408/2", "MATH 1408/3", "MATH 1408/4", "MATH 1408/5", "MATH 1408/6", "MATH 1408/7", "MATH 1408/8", "MATH 1408/9", "MATH 1408/10", "MATH 1408/11", "MATH 1408/12", "MATH 1408/13", "MATH 1408/14", "MATH 1408/16", "MATH 1408/17", "MATH 1408/18", "MATH 1408/19", "MATH 1408/20", "MATH 1408/21", "MATH 1408/22", "MATH 1408/24.1", "MATH 1408/24.2",
        "CHEM2 104", "CHEM2 204", "CHEM2 205",
        "MHMK M01", "MHMK M02",
        "MHMK 201", "MHMK 202", "MHMK 203", "MHMK 204", "MHMK 205", "MHMK 206", "MHMK 207", "MHMK 208",
        "MHMK 301", "MHMK 302", "MHMK 304", "MHMK 305", "MHMK 306", "MHMK 307", "MHMK 308", "MHMK 309",
        "TAB 220", "TAB 221", "TAB 222", "TAB 226", "TAB 227", "TAB 228", "TAB 229", "TAB 230", "TAB 231"};*/

    /*void Start()
    {
        ddlist.AddOptions(AllRoomList);
    }*/

    public void InitNav()
    {
        room = target;
        floor = target.transform.parent.parent.gameObject;
        building = target.transform.parent.parent.parent.gameObject;
        func = GameControl.GetComponent<CreateSearchResult>();
        switch (building.name)
        {
            case "MATH":
                ddlist.AddOptions(StartPointMATH);
                break;
            case "CHEM2":
                ddlist.AddOptions(StartPointCHEM2);
                break;
            case "MHMK":
                ddlist.AddOptions(StartPointMHMK);
                break;
            case "TAB":
                ddlist.AddOptions(StartPointTAB);
                break;
            default:
                return;
        }
        ddlist.value = 0;
    }

    public void SetPath()
    {
        if (stage == 0)
        {
            Destroy(func.currentChar);
        }
        switch (building.name)
        {
            case "MATH":
                if (stage == 0)
                {
                    if (C2RoomList.Contains(room.name))
                        C2 = true;
                    else if (C3RoomList.Contains(room.name))
                        C3 = true;
                    if (ddlist.value == 0)
                    {
                        agentObj = Instantiate(building.transform.GetChild(2).GetChild(1).Find("S1").GetChild(0).gameObject, building.transform.GetChild(2).GetChild(1).Find("S1").GetChild(0).parent);
                        SetNavBar("Enter Maha Vajirunhis building (MATH), go straight then turn right", false, right);
                    } else
                    {
                        agentObj = Instantiate(building.transform.GetChild(2).GetChild(1).Find("S2").GetChild(0).gameObject, building.transform.GetChild(2).GetChild(1).Find("S2").GetChild(0).parent);
                        SetNavBar("Enter Maha Vajirunhis building (MATH), go straight then turn left", false, left);
                    }
                    currentDestination = building.transform.GetChild(2).GetChild(1).Find("S3").position;
                    agentObj.SetActive(true);
                    func.ShowOnlyTargetObject(building.name, "1_2");
                    func.ChangeCameraView(agentObj);
                    Agent = agentObj.GetComponent<NavMeshAgent>();
                    IsNavEnable = true;
                    Agent.SetDestination(currentDestination);
                    IsEnterMATH = true;
                }
                if (stage == 1)
                {
                    IsEnterMATH = false;
                    agentObj = Instantiate(building.transform.GetChild(2).GetChild(1).Find("C11").GetChild(0).gameObject, building.transform.GetChild(2).GetChild(1).Find("C11").GetChild(0).parent);
                    SetNavBar("Enter the elevator hall on 1st floor, take the elevator or stairs from 1st to " + floor.name + "th floor", false, elev_stair);
                    currentDestination = building.transform.GetChild(2).GetChild(1).GetChild(2).position;
                    agentObj.SetActive(true);
                    func.ChangeCameraView(agentObj);
                    Agent = agentObj.GetComponent<NavMeshAgent>();
                    IsNavEnable = true;
                    Agent.SetDestination(currentDestination);
                }
                else if (stage == 2)
                {
                    currentStart = building.transform.Find(floor.name).GetChild(2).Find("E" + floor.name).position;
                    //currentDestination = room.transform.position;
                    currentDestination = building.transform.Find(floor.name).GetChild(2).Find("C" + floor.name + "1").position;
                    agentObj = Instantiate(building.transform.Find(floor.name).GetChild(3).gameObject, building.transform.Find(floor.name).GetChild(3).parent);
                    agentObj.SetActive(true);
                    agentObj.transform.position = currentStart;
                    Agent = agentObj.GetComponent<NavMeshAgent>();
                    SetNavBar("Take the door near the stair", false, door);
                    func.ShowOnlyTargetObject(building.name, floor.name);
                    func.ChangeCameraView(agentObj);
                    Agent.SetDestination(currentDestination);
                    IsNavEnable = true;
                }
                else if (stage == 3)
                {
                    if (!(C2 || C3))
                    {
                        ++stage;
                        SetPath();
                        return;
                    }
                    agentObj = Instantiate(building.transform.Find(floor.name).GetChild(2).Find("C" + floor.name + "1").GetChild(0).gameObject, building.transform.Find(floor.name).GetChild(2).Find("C" + floor.name + "1").GetChild(0).parent);
                    agentObj.SetActive(true);
                    Agent = agentObj.GetComponent<NavMeshAgent>();
                    currentDestination = building.transform.Find(floor.name).GetChild(2).Find("C" + floor.name + "2").position;
                    SetNavBar("Go straight then turn left", false, left);
                    Agent.SetDestination(currentDestination);
                    IsNavEnable = true;
                }
                else if (stage == 4)
                {
                    if (!C3)
                    {
                        ++stage;
                        SetPath();
                        return;
                    }
                    agentObj = Instantiate(building.transform.Find(floor.name).GetChild(2).Find("C" + floor.name + "2").GetChild(0).gameObject, building.transform.Find(floor.name).GetChild(2).Find("C" + floor.name + "2").GetChild(0).parent);
                    agentObj.SetActive(true);
                    Agent = agentObj.GetComponent<NavMeshAgent>();
                    currentDestination = building.transform.Find(floor.name).GetChild(2).Find("C" + floor.name + "3").position;
                    string C3direction = "left";
                    int sb_temp = left;
                    if (C3Right.Contains(room.name))
                    {
                        C3direction = "right";
                        sb_temp = right;
                    }
                    SetNavBar("Go straight then turn " + C3direction, false, sb_temp);
                    Agent.SetDestination(currentDestination);
                    IsNavEnable = true;
                }
                else if (stage == 5)
                {
                    string cst = "1";
                    if (C2)
                        cst = "2";
                    else if (C3)
                        cst = "3";
                    agentObj = Instantiate(building.transform.Find(floor.name).GetChild(2).Find("C" + floor.name + cst).GetChild(0).gameObject, building.transform.Find(floor.name).GetChild(2).Find("C" + floor.name + cst).GetChild(0).parent);
                    Vector3 startPoint = agentObj.transform.position;
                    string direction = "right";
                    int sb_temp = right;
                    if (C3)
                    {
                        if (room.transform.position.x < startPoint.x)
                        {
                            direction = "right";
                            sb_temp = dest_right;
                        }
                        else
                        {
                            direction = "left";
                            sb_temp = dest_left;
                        }
                    }
                    else if (C2)
                    {
                        if (room.transform.position.z > startPoint.z)
                        {
                            direction = "right";
                            sb_temp = dest_right;
                        }
                        else
                        {
                            direction = "left";
                            sb_temp = dest_left;
                        }
                    }
                    else
                    {
                        if (room.transform.position.x > startPoint.x)
                        {
                            direction = "right";
                            sb_temp = dest_right;
                        }
                        else
                        {
                            direction = "left";
                            sb_temp = dest_left;
                        }
                    }
                    agentObj.SetActive(true);
                    Agent = agentObj.GetComponent<NavMeshAgent>();
                    currentDestination = room.transform.position;
                    SetNavBar("Go straight. Room " + targetName +" is on the " + direction, true, sb_temp);
                    Agent.SetDestination(currentDestination);
                    IsNavEnable = true;
                }
                break;
            case "CHEM2":
                if (stage == 0)
                {
                    if (ddlist.value == 0)
                    {
                        agentObj = Instantiate(building.transform.GetChild(2).GetChild(1).Find("S1").GetChild(0).gameObject, building.transform.GetChild(2).GetChild(1).Find("S1").GetChild(0).parent);
                        currentDestination = building.transform.GetChild(2).GetChild(1).Find("C11").position;
                    }
                    else
                    {
                        agentObj = Instantiate(building.transform.GetChild(2).GetChild(1).Find("S2").GetChild(0).gameObject, building.transform.GetChild(2).GetChild(1).Find("S2").GetChild(0).parent);
                        currentDestination = building.transform.GetChild(2).GetChild(1).Find("C13").position;
                    }
                    agentObj.SetActive(true);
                    SetNavBar("Enter the Chemistry2 building (CHEM2)", false, bd);
                    func.ShowOnlyTargetObject(building.name, "1");
                    func.ChangeCameraView(agentObj);
                    Agent = agentObj.GetComponent<NavMeshAgent>();
                    IsNavEnable = true;
                    Agent.SetDestination(currentDestination);
                }
                else if (stage == 1)
                {
                    if (ddlist.value == 0)
                    {
                        ++stage;
                        SetPath();
                        return;
                    }
                    if (floor.name == "1")
                    {
                        agentObj = Instantiate(building.transform.GetChild(2).GetChild(1).Find("C13").GetChild(0).gameObject, building.transform.GetChild(2).GetChild(1).Find("C13").GetChild(0).parent);
                        currentDestination = room.transform.position;
                        agentObj.SetActive(true);
                        SetNavBar("Go straight. Room " + targetName + " is on the right", true, dest_right);
                        Agent = agentObj.GetComponent<NavMeshAgent>();
                        IsNavEnable = true;
                        Agent.SetDestination(currentDestination);
                    }
                    else
                    {
                        agentObj = Instantiate(building.transform.GetChild(2).GetChild(1).Find("C13").GetChild(0).gameObject, building.transform.GetChild(2).GetChild(1).Find("C13").GetChild(0).parent);
                        currentDestination = building.transform.GetChild(2).GetChild(1).Find("C11").position;
                        agentObj.SetActive(true);
                        SetNavBar("Go straight to the stairs", false, up);
                        Agent = agentObj.GetComponent<NavMeshAgent>();
                        IsNavEnable = true;
                        Agent.SetDestination(currentDestination);
                    }
                }
                else if (stage == 2)
                {
                    if (floor.name == "1")
                    {
                        ++stage;
                        SetPath();
                        return;
                    }
                    agentObj = Instantiate(building.transform.GetChild(2).GetChild(1).Find("C11").GetChild(0).gameObject, building.transform.GetChild(2).GetChild(1).Find("C11").GetChild(0).parent);
                    currentStart = agentObj.transform.position;
                    currentDestination = building.transform.GetChild(3).GetChild(1).Find("C21").position;
                    agentObj.SetActive(true);
                    SetNavBar("Take the stairs from 1st floor to 2nd floor", false, staironly);
                    func.ShowOnlyTargetObject(building.name, "1");
                    func.ChangeCameraView(agentObj);
                    Agent = agentObj.GetComponent<NavMeshAgent>();
                    IsNavEnable = true;
                    IsTakeStair = true;
                    Agent.SetDestination(currentDestination);
                }
                else if (stage == 3)
                {
                    IsTakeStair = false;
                    if (floor.name == "1")
                    {
                        agentObj = Instantiate(building.transform.GetChild(2).GetChild(1).Find("C11").GetChild(0).gameObject, building.transform.GetChild(2).GetChild(1).Find("C11").GetChild(0).parent);
                        currentDestination = building.transform.GetChild(2).GetChild(1).Find("C12").position;
                        SetNavBar("Turn right", false, right);
                    }
                    else
                    {
                        agentObj = Instantiate(building.transform.GetChild(3).GetChild(1).Find("C21").GetChild(0).gameObject, building.transform.GetChild(3).GetChild(1).Find("C21").GetChild(0).parent);
                        currentDestination = building.transform.GetChild(3).GetChild(1).Find("C22").position;
                        SetNavBar("Turn left", false, left);
                    }
                    agentObj.SetActive(true);
                    func.ChangeCameraView(agentObj);
                    func.ShowOnlyTargetObject(building.name, floor.name);
                    Agent = agentObj.GetComponent<NavMeshAgent>();
                    IsNavEnable = true;
                    Agent.SetDestination(currentDestination);
                }
                else if (stage == 4)
                {
                    if (room.name != "205")
                    {
                        ++stage;
                        SetPath();
                        return;
                    }
                    agentObj = Instantiate(building.transform.GetChild(3).GetChild(1).Find("C22").GetChild(0).gameObject, building.transform.GetChild(3).GetChild(1).Find("C22").GetChild(0).parent);
                    currentDestination = building.transform.GetChild(3).GetChild(1).Find("C23").position;
                    agentObj.SetActive(true);
                    SetNavBar("Go straight then turn right", false, right);
                    func.ChangeCameraView(agentObj);
                    Agent = agentObj.GetComponent<NavMeshAgent>();
                    IsNavEnable = true;
                    Agent.SetDestination(currentDestination);
                }
                else if (stage == 5)
                {
                    string cst = "2";
                    if (room.name == "205")
                        cst = "3";
                    agentObj = Instantiate(building.transform.Find(floor.name).GetChild(1).Find("C" + floor.name + cst).GetChild(0).gameObject, building.transform.Find(floor.name).GetChild(1).Find("C" + floor.name + cst).GetChild(0).parent);
                    currentDestination = room.transform.position;
                    agentObj.SetActive(true);
                    SetNavBar("Go straight. Room " + targetName + " is on the left", true, dest_left);
                    Agent = agentObj.GetComponent<NavMeshAgent>();
                    IsNavEnable = true;
                    Agent.SetDestination(currentDestination);
                }
                break;
            case "MHMK":
                if (stage == 0)
                {
                    if (ddlist.value == 0)
                    {
                        agentObj = Instantiate(building.transform.GetChild(2).GetChild(2).Find("S1").GetChild(0).gameObject, building.transform.GetChild(2).GetChild(2).Find("S1").GetChild(0).parent);
                        currentDestination = building.transform.GetChild(2).GetChild(2).Find("C1").position;
                        SetNavBar("Enter the Mahamakut building (MHMK)", false, bd);
                    }
                    else if (ddlist.value == 1)
                    {
                        agentObj = Instantiate(building.transform.GetChild(2).GetChild(2).Find("S2").GetChild(0).gameObject, building.transform.GetChild(2).GetChild(2).Find("S2").GetChild(0).parent);
                        currentDestination = building.transform.GetChild(2).GetChild(2).Find("C1").position;
                        SetNavBar("Enter the Mahamakut building (MHMK), go straigh then turn right", false, right);
                    }
                    else
                    {
                        agentObj = Instantiate(building.transform.GetChild(2).GetChild(2).Find("S3").GetChild(0).gameObject, building.transform.GetChild(2).GetChild(2).Find("S3").GetChild(0).parent);
                        currentDestination = building.transform.GetChild(2).GetChild(2).Find("C2").position;
                        SetNavBar("Enter the Mahamakut building (MHMK)", false, bd);
                    }
                    agentObj.SetActive(true);
                    func.ShowOnlyTargetObject(building.name, "1");
                    func.ChangeCameraView(agentObj);
                    Agent = agentObj.GetComponent<NavMeshAgent>();
                    IsNavEnable = true;
                    Agent.SetDestination(currentDestination);
                }
                else if (stage == 1)
                {
                    if (ddlist.value != 2)
                    {
                        agentObj = Instantiate(building.transform.GetChild(2).GetChild(2).Find("C1").GetChild(0).gameObject, building.transform.Find("1").GetChild(2).Find("C1").GetChild(0).parent);
                    }
                    else
                    {
                        agentObj = Instantiate(building.transform.GetChild(2).GetChild(2).Find("C2").GetChild(0).gameObject, building.transform.Find("1").GetChild(2).Find("C2").GetChild(0).parent);
                    }
                    currentDestination = building.transform.GetChild(2).GetChild(2).Find("E1").position;
                    agentObj.SetActive(true);
                    SetNavBar("Go straight to the elevator or stairs at the 1st floor", false, elev_stair);
                    //func.ShowOnlyTargetObject(building.name, "1");
                    func.ChangeCameraView(agentObj);
                    Agent = agentObj.GetComponent<NavMeshAgent>();
                    IsNavEnable = true;
                    Agent.SetDestination(currentDestination);
                }
                else if (stage == 2)
                {
                    agentObj = Instantiate(building.transform.Find("1").GetChild(2).Find("E1").GetChild(0).gameObject, building.transform.Find("1").GetChild(2).Find("E1").GetChild(0).parent);
                    currentDestination = building.transform.Find(floor.name).GetChild(2).Find("E" + floor.name).position;
                    agentObj.SetActive(true);
                    vectorIncrease.y = 1f;
                    currentStart = building.transform.GetChild(2).GetChild(2).Find("E1").position + vectorIncrease;
                    string floorname = floor.name;
                    if (floor.name == "M")
                    {
                        floorname = "M";
                    }
                    else if (floor.name == "2")
                    {
                        floorname = "2nd";
                    }
                    else if (floor.name == "3")
                    {
                        floorname = "3rd";
                    }
                    SetNavBar("Take the elevator or stairs from 1st floor to "+floorname+" floor", false, elev_stair);
                    func.ShowOnlyTargetObject(building.name, "1");
                    func.ChangeCameraView(agentObj);
                    Agent = agentObj.GetComponent<NavMeshAgent>();
                    IsNavEnable = true;
                    IsTakeStair = true;
                    Agent.SetDestination(currentDestination);
                }
                else if (stage == 3)
                {
                    IsTakeStair = false;
                    agentObj = Instantiate(building.transform.Find(floor.name).GetChild(2).Find("E" + floor.name).GetChild(0).gameObject, building.transform.Find(floor.name).GetChild(2).Find("E" + floor.name).GetChild(0).parent);
                    if (room.name == "203" || room.name == "304")
                    {
                        //In front of finish
                        agentObj.SetActive(true);
                        Agent = agentObj.GetComponent<NavMeshAgent>();
                        func.ShowOnlyTargetObject(building.name, floor.name);
                        func.ChangeCameraView(agentObj);
                        currentDestination = room.transform.position;
                        SetNavBar("Go straight. Room " + targetName + " is in front of you", true, dest);
                        Agent.SetDestination(currentDestination);
                        IsNavEnable = true;
                        return;
                    }
                    else if (room.transform.position.x > building.transform.Find(floor.name).GetChild(2).Find("E" + floor.name).position.x)
                    {
                        //turn right
                        currentDestination = building.transform.Find(floor.name).GetChild(2).Find("C" + floor.name + "1").position;
                        SetNavBar("Turn right", false, right);
                    }
                    else
                    {
                        //turn left
                        currentDestination = building.transform.Find(floor.name).GetChild(2).Find("C" + floor.name + "2").position;
                        SetNavBar("Turn left", false, left);
                    }
                    agentObj.SetActive(true);
                    Agent = agentObj.GetComponent<NavMeshAgent>();
                    func.ShowOnlyTargetObject(building.name, floor.name);
                    func.ChangeCameraView(agentObj);
                    Agent.SetDestination(currentDestination);
                    IsNavEnable = true;
                }
                else if (stage == 4)
                {
                    if (room.name == "204" || room.name == "305")
                    {
                        //turn left C4
                        agentObj = Instantiate(building.transform.Find(floor.name).GetChild(2).Find("C" + floor.name + "1").GetChild(0).gameObject, building.transform.Find(floor.name).GetChild(2).Find("C" + floor.name + "1").GetChild(0).parent);
                        currentDestination = building.transform.Find(floor.name).GetChild(2).Find("C" + floor.name + "4").position;
                        SetNavBar("Go straight then turn left", false, left);
                    }
                    else if (room.name == "206" || room.name == "307")
                    {
                        //turn right C3
                        agentObj = Instantiate(building.transform.Find(floor.name).GetChild(2).Find("C" + floor.name + "2").GetChild(0).gameObject, building.transform.Find(floor.name).GetChild(2).Find("C" + floor.name + "2").GetChild(0).parent);
                        currentDestination = building.transform.Find(floor.name).GetChild(2).Find("C" + floor.name + "3").position;
                        SetNavBar("Go straight then turn right", false, right);
                    }
                    else
                    {
                        ++stage;
                        SetPath();
                        return;
                    }
                    agentObj.SetActive(true);
                    Agent = agentObj.GetComponent<NavMeshAgent>();
                    func.ChangeCameraView(agentObj);
                    Agent.SetDestination(currentDestination);
                    IsNavEnable = true;
                }
                else if (stage == 5)
                {
                    string direction = "left";
                    int sb_temp;
                    if (room.transform.position.x > building.transform.Find(floor.name).GetChild(2).Find("E" + floor.name).position.x)
                    {
                        //turn right
                        agentObj = Instantiate(building.transform.Find(floor.name).GetChild(2).Find("C" + floor.name + "1").GetChild(0).gameObject, building.transform.Find(floor.name).GetChild(2).Find("C" + floor.name + "1").GetChild(0).parent);
                        Vector3 startPoint = agentObj.transform.position;
                        Debug.Log(room.transform.position.x + " : " + startPoint.x);
                        if (room.transform.position.z < startPoint.z)
                        {
                            direction = "right";
                            sb_temp = dest_right;
                        }
                        else
                        {
                            direction = "left";
                            sb_temp = dest_left;
                        }
                    }
                    else
                    {
                        //turn left
                        agentObj = Instantiate(building.transform.Find(floor.name).GetChild(2).Find("C" + floor.name + "2").GetChild(0).gameObject, building.transform.Find(floor.name).GetChild(2).Find("C" + floor.name + "2").GetChild(0).parent);
                        Vector3 startPoint = agentObj.transform.position;
                        if (room.transform.position.z < startPoint.z)
                        {
                            direction = "left";
                            sb_temp = dest_left;
                        }
                        else
                        {
                            direction = "right";
                            sb_temp = dest_right;
                        }
                    }
                    agentObj.SetActive(true);
                    Agent = agentObj.GetComponent<NavMeshAgent>();
                    currentDestination = room.transform.position;
                    SetNavBar("Go straight. Room " + targetName + " is on the " + direction, true, sb_temp);
                    Agent.SetDestination(currentDestination);
                    IsNavEnable = true;
                }
                break;
            case "TAB":
                if (stage == 0)
                {
                    if (ddlist.value == 0)
                    {
                        agentObj = Instantiate(building.transform.GetChild(3).Find("S1").GetChild(0).gameObject, building.transform.GetChild(3).Find("S1").GetChild(0).parent);
                        currentDestination = building.transform.GetChild(3).Find("C1").position;
                        SetNavBar("Enter the TAB building then go to the stairs at 1st floor", false, bd);
                    }
                    else if (ddlist.value == 1)
                    {
                        agentObj = Instantiate(building.transform.GetChild(3).Find("S2").GetChild(0).gameObject, building.transform.GetChild(3).Find("S2").GetChild(0).parent);
                        currentDestination = building.transform.GetChild(3).Find("C5").position;
                        SetNavBar("Take the stairs to the 2nd floor", false, staironly);
                    }
                    else
                    {
                        agentObj = Instantiate(building.transform.GetChild(3).Find("S3").GetChild(0).gameObject, building.transform.GetChild(3).Find("S3").GetChild(0).parent);
                        currentDestination = building.transform.GetChild(3).Find("C7").position;
                        SetNavBar("Enter the TAB building and take the elevator or stairs to the 2nd floor", false, elev_stair);
                        building.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
                        vectorIncrease.y = 3.5f;
                        currentStart = agentObj.transform.position + vectorIncrease;
                    }
                    agentObj.SetActive(true);
                    func.ShowOnlyTargetObject(building.name, floor.name);
                    func.ChangeCameraView(agentObj);
                    Agent = agentObj.GetComponent<NavMeshAgent>();
                    IsNavEnable = true;
                    Agent.SetDestination(currentDestination);
                    IsTakeStair = true;
                }
                else if (stage == 1)
                {
                    IsTakeStair = false;
                    building.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);
                    if (ddlist.value == 0)
                    {
                        agentObj = Instantiate(building.transform.GetChild(3).Find("C1").GetChild(0).gameObject, building.transform.GetChild(3).Find("C1").GetChild(0).parent);
                        currentDestination = building.transform.GetChild(3).Find("C2").position;
                        SetNavBar("Take the stairs from 1st floor to 2nd floor", false, staironly);
                    }
                    else if (ddlist.value == 1)
                    {
                        agentObj = Instantiate(building.transform.GetChild(3).Find("C5").GetChild(0).gameObject, building.transform.GetChild(3).Find("C5").GetChild(0).parent);
                        currentDestination = building.transform.GetChild(3).Find("C6").position;
                        SetNavBar("Enter the TAB building", false, bd);
                    }
                    else
                    {
                        stage = stage + 2;
                        SetPath();
                        return;
                    }
                    agentObj.SetActive(true);
                    func.ChangeCameraView(agentObj);
                    Agent = agentObj.GetComponent<NavMeshAgent>();
                    IsNavEnable = true;
                    Agent.SetDestination(currentDestination);
                }
                else if (stage == 2)
                {
                    if (ddlist.value != 0)
                    {
                        stage = stage + 2;
                        SetPath();
                        return;
                    }
                    agentObj = Instantiate(building.transform.GetChild(3).Find("C2").GetChild(0).gameObject, building.transform.GetChild(3).Find("C2").GetChild(0).parent);
                    currentDestination = building.transform.GetChild(3).Find("C3").position;
                    agentObj.SetActive(true);
                    SetNavBar("Turn left", false, left);
                    func.ChangeCameraView(agentObj);
                    Agent = agentObj.GetComponent<NavMeshAgent>();
                    IsNavEnable = true;
                    Agent.SetDestination(currentDestination);
                }
                else if (stage == 3)
                {
                    if (ddlist.value == 0)
                    {
                        agentObj = Instantiate(building.transform.GetChild(3).Find("C3").GetChild(0).gameObject, building.transform.GetChild(3).Find("C3").GetChild(0).parent);
                        currentDestination = building.transform.GetChild(3).Find("C4").position;
                        SetNavBar("Go straight then turn right", false, right);
                    }
                    else
                    {
                        agentObj = Instantiate(building.transform.GetChild(3).Find("C7").GetChild(0).gameObject, building.transform.GetChild(3).Find("C7").GetChild(0).parent);
                        currentDestination = building.transform.GetChild(3).Find("C4").position;
                        SetNavBar("Go straight then turn left", false, left);
                    }
                    agentObj.SetActive(true);
                    func.ChangeCameraView(agentObj);
                    Agent = agentObj.GetComponent<NavMeshAgent>();
                    IsNavEnable = true;
                    Agent.SetDestination(currentDestination);
                }
                else if (stage == 4)
                {
                    string direction = "left";
                    int sb_temp;
                    if (ddlist.value != 1)
                    {
                        agentObj = Instantiate(building.transform.GetChild(3).Find("C4").GetChild(0).gameObject, building.transform.GetChild(3).Find("C4").GetChild(0).parent);
                        Vector3 startPoint = agentObj.transform.position;
                        if (room.transform.position.x > startPoint.x)
                        {
                            direction = "left";
                            sb_temp = dest_left;
                        }
                        else
                        {
                            direction = "right";
                            sb_temp = dest_right;
                        }
                    }
                    else
                    {
                        agentObj = Instantiate(building.transform.GetChild(3).Find("C6").GetChild(0).gameObject, building.transform.GetChild(3).Find("C6").GetChild(0).parent);
                        Vector3 startPoint = agentObj.transform.position;
                        if (room.transform.position.x < startPoint.x)
                        {
                            direction = "left";
                            sb_temp = dest_left;
                        }
                        else
                        {
                            direction = "right";
                            sb_temp = dest_right;
                        }
                    }
                    
                    currentDestination = room.transform.position;
                    agentObj.SetActive(true);
                    SetNavBar("Go straight. Room " + targetName + " is on the " + direction, true, sb_temp);
                    func.ChangeCameraView(agentObj);
                    Agent = agentObj.GetComponent<NavMeshAgent>();
                    IsNavEnable = true;
                    Agent.SetDestination(currentDestination);
                }
                break;
            default:
                break;
        }
    }

    void SetNavBar(string mes, bool IsFinish, int current_sb)
    {
        func.googleButton.SetActive(false);
        func.routeButton.GetComponent<Image>().enabled = false;
        func.routeButton.GetComponent<Button>().enabled = false;
        NavBar.SetActive(true);
        NavBar.transform.GetChild(1).GetComponent<Text>().text = mes;
        symbols.transform.GetChild(0).gameObject.SetActive(false);
        symbols.transform.GetChild(1).gameObject.SetActive(false);
        symbols.transform.GetChild(2).gameObject.SetActive(false);
        symbols.transform.GetChild(3).gameObject.SetActive(false);
        symbols.transform.GetChild(4).gameObject.SetActive(false);
        symbols.transform.GetChild(5).gameObject.SetActive(false);
        symbols.transform.GetChild(6).gameObject.SetActive(false);
        symbols.transform.GetChild(7).gameObject.SetActive(false);
        symbols.transform.GetChild(8).gameObject.SetActive(false);
        symbols.transform.GetChild(9).gameObject.SetActive(false);
        symbols.transform.GetChild(current_sb).gameObject.SetActive(true);
        if (IsFinish)
        {
            //NavBar.transform.GetChild(2).GetComponent<Image>().color = new Vector4(0.416f, 0.792f, 0.42f, 1f);
            //NavBar.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "Finish";
            NavBar.transform.GetChild(2).gameObject.SetActive(true);
            NavBar.transform.GetChild(3).gameObject.SetActive(false);
        }
        else
        {
            //NavBar.transform.GetChild(2).GetComponent<Image>().color = new Vector4(0.259f, 0.522f, 0.957f, 1f);
            //NavBar.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "Next";
            NavBar.transform.GetChild(2).gameObject.SetActive(false);
            NavBar.transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    public void ClickNext(bool IsReplay)
    {
        if (IsReplay)
        {
            --stage;
        }
        clicked = true;
    }

    public void NavReset()
    {
        stage = 0;
        Agent.isStopped = true;
        IsNavEnable = false;
        Destroy(agentObj);
        agentObj = null;
        Agent = null;
        C2 = false;
        C3 = false;
        ClearStartPointDropdown();
        //building.GetComponent<Lean.Touch.LeanRotate>().enabled = true;
        func.routeButton.GetComponent<Image>().enabled = true;
        func.routeButton.GetComponent<Button>().enabled = true;
    }

    public void ClearStartPointDropdown()
    {
        ddlist.options.Clear();
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/

    void FixedUpdate () {
        if (clicked)
        {
            clicked = false;
            ++stage;
            IsNavEnable = false;
            Agent.isStopped = true;
            Destroy(agentObj);
            Agent = null;
            SetPath();
        }
    }

    private void Update()
    {
        if (IsTakeStair)
        {
            if (Agent.transform.position.y > currentStart.y + 19.8)
            {
                if (building.name == "MHMK")
                {
                    func.ShowOnlyTargetObject(building.name, "3");
                    IsTakeStair = false;
                }
            }
            else if (Agent.transform.position.y > currentStart.y + 12.4)
            {
                if (building.name == "MHMK")
                {
                    func.ShowOnlyTargetObject(building.name, "2");
                }
            }
            else if (Agent.transform.position.y > currentStart.y + 5)
            {
                if (building.name == "MHMK")
                {
                    func.ShowOnlyTargetObject(building.name, "M");
                }
                else if (building.name == "CHEM2")
                {
                    func.ShowOnlyTargetObject(building.name, "2");
                    IsTakeStair = false;
                }
                else if (building.name == "TAB")
                {
                    building.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);
                    IsTakeStair = false;
                }
            }
        }
        if (IsEnterMATH)
        {
            if (Agent != null)
                if ((Agent.remainingDistance == 0) && (!Agent.pathPending))
                {
                    Agent.SetDestination(building.transform.GetChild(2).GetChild(1).Find("C11").position);
                    IsEnterMATH = false;
                }
        }
    }

    void LateUpdate()
    {
        if (IsNavEnable)
            currentCamera.transform.position = agentObj.transform.position + offset;
    }
}
