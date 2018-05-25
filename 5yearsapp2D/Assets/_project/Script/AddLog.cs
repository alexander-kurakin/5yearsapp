using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddLog : MonoBehaviour {

    [SerializeField]
    private string myText;
    [SerializeField]
    private TextLogControl logControl;

    public Text myInputText;
    public Text myYearText;

    public void LogText()
    {
        logControl.LogText(myYearText.text + ": \n"+ myInputText.text);
    }

}
