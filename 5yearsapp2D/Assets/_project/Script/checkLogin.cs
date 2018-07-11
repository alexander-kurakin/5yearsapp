using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.SceneManagement;

public class checkLogin : MonoBehaviour {

    public Text LoginF;
    public Text PassF;
    private DatabaseReference mDatabase;
    private string Pass;
    public Button bttn;
    public Text outText;


	// Use this for initialization
	void Start () {

        PlayerPrefs.SetInt("isAuthenticated", 0);

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://yearsapp.firebaseio.com/");

        outText.text = "Initially set";
        mDatabase = FirebaseDatabase.DefaultInstance.GetReference("Users");
    }

    public void WriteNewUser(string name, string password, int age, string sex)
    {
      
            User user = new User(name, password, age, sex);
            string json = JsonUtility.ToJson(user);

        DatabaseReference newUserID = mDatabase.Push();

        newUserID.SetRawJsonValueAsync(json);

    }

    public void TestLogin()
    {
        if (LoginF.text.Length != 0 && PassF.text.Length != 0)
        {
            FirebaseDatabase.DefaultInstance
            .GetReference("Users")
            .OrderByChild("username")
            .EqualTo(LoginF.text)
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    outText.text = "Failed";
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    outText.text = outText.text + " Got a snapshot";
                    //to-doo too-doo
                    Pass = snapshot.Child("password").Value.ToString();

                    outText.text = outText.text + " Pass:" + Pass;
                    outText.text = outText.text + " Pass_entered:" + PassF.text;

                    if (snapshot.Child("username").Equals(LoginF.text))
                    {
                        if (Pass == PassF.text)
                        {
                            outText.text = outText.text + " Loading main scene!";
                              PlayerPrefs.SetInt("isAuthenticated", 1);
                              //SceneManager.LoadScene("main");
                        }
                        else
                        {
                            outText.text = outText.text + " Password is invalid!";
                        };
                    }
                    else
                    {
                        //WriteNewUser(LoginF.text, PassF.text, 28, "M");
                        outText.text = outText.text + " Wanted to create a New User!";
                        PlayerPrefs.SetInt("isAuthenticated", 1);
                        //SceneManager.LoadScene("main");
                    }
                };
            });

        }
        else
        {
            if (LoginF.text.Length == 0 && PassF.text.Length !=0)
                outText.text = "Enter login!";
            else
            {
                if (LoginF.text.Length != 0 && PassF.text.Length == 0)
                    outText.text = "Enter password!";
                else
                {
                    outText.text = "Enter login and password!";
                }
            }
        }
    }


}
