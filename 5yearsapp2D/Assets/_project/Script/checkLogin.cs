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
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://yearsapp.firebaseio.com/");

        outText.text = "Initialized";
        mDatabase = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void WriteNewUser(string name, string password)
    {
        mDatabase.Child("Users").Child(name).SetValueAsync(password);        
    }

    public void TestLogin()
    {
        if (LoginF.text.Length != 0 && PassF.text.Length != 0)
        {
            FirebaseDatabase.DefaultInstance
            .GetReference("Users")
            .OrderByKey()
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

                    foreach (var childSnap in snapshot.Children)
                    {
                        var q = childSnap.Value.ToString();
                        Pass = q.ToString();
                    }

                    if (snapshot.HasChild(LoginF.text))
                    {
                        if (Pass == PassF.text)
                        {
                            outText.text = outText.text + " Loading main scene!";
                              PlayerPrefs.SetInt("isAuthenticated", 1);
                              SceneManager.LoadScene("main");
                        }
                        else
                        {
                            outText.text = outText.text + " Password is invalid!";
                        };
                    }
                    else
                    {
                        WriteNewUser(LoginF.text, PassF.text);
                        outText.text = "Created New User!";
                        PlayerPrefs.SetInt("isAuthenticated", 1);
                        SceneManager.LoadScene("main");
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
