using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currentDay : MonoBehaviour
{
    public GameObject Text;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Text.GetComponent<Text>().text = System.DateTime.Now.ToString("d MMMM", System.Globalization.CultureInfo.GetCultureInfo("ru-ru")); ;
    }
}