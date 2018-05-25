using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour {

    public void GoBack()
    {
        SceneManager.LoadScene("menu");
    }
}
