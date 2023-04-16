using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeNdate : MonoBehaviour
{
    // Start is called before the first frame update
    public Text TimeText;
    public Text DateText;
    void Start()
    {
        UpdateTimeNdate();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeNdate();   
    }

    public void UpdateTimeNdate()
    {
        DateTime currettime = DateTime.Now;
        string time = currettime.ToString("hh:mm:ss");
        string date = currettime.ToString("dd/MM/yyyy");
        TimeText.text = time;
        DateText.text = date;
    }
}
