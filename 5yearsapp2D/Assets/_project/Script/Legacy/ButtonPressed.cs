using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonPressed : MonoBehaviour {

    public void LoadScene()
    {
        if (PlayerPrefs.GetInt("isAuthenticated") == 1)
        {
            SceneManager.LoadScene("main");
        }
        else
        {
            SceneManager.LoadScene("login");
        }
        
    }
	
}
