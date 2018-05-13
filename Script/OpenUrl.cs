using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUrl : MonoBehaviour
{
    public string url;

    void Start()
    {
        url = @"https://www.google.co.th/maps/place/%E0%B8%AD%E0%B8%B2%E0%B8%84%E0%B8%B2%E0%B8%A3%E0%B8%A1%E0%B8%AB%E0%B8%B2%E0%B8%A7%E0%B8%8A%E0%B8%B4%E0%B8%A3%E0%B8%B8%E0%B8%93%E0%B8%AB%E0%B8%B4%E0%B8%A8/@13.7367397,100.5310551,19.75z/data=!4m5!3m4!1s0x0:0x6d3b7bdcf2d8eef2!8m2!3d13.7366729!4d100.531401";
    }
    public void Open()
    {
        Application.OpenURL(url);
    }
}
