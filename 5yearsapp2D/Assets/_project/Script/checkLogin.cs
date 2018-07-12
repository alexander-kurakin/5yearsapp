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
    private string Login;
    public Button bttn;
    public Text outText;
    private string existingPerson, newPerson;


	// Use this for initialization
	void Start () {

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://yearsapp.firebaseio.com/");

        outText.text = "Connection established!";
        mDatabase = FirebaseDatabase.DefaultInstance.GetReference("Users");
    }

    public void WriteNewUser(string name, string password, int age, string sex)
    {
      
            User user = new User(name, password, age, sex);
            string json = JsonUtility.ToJson(user);

        DatabaseReference newUserID = mDatabase.Push();
        newPerson = newUserID.Key;

        Debug.Log("New user created:" + newPerson);

        PlayerPrefs.SetString("userID", newPerson);

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

                    foreach (DataSnapshot user in snapshot.Children)
                    {
                        IDictionary dictUser = (IDictionary)user.Value;
                        Pass = dictUser["password"].ToString();
                        Login = dictUser["username"].ToString();
                        existingPerson = user.Key;
                        Debug.Log("Existing user found:" + existingPerson);

                    }

                    if (Login == LoginF.text)
                    {
                        if (Pass == PassF.text)
                        {
                            outText.text = outText.text + " Passwords match! Well Done";
                              PlayerPrefs.SetInt("isAuthenticated", 1);
                              PlayerPrefs.SetString("userID", existingPerson);
                              SceneManager.LoadScene("main");
                        }
                        else
                        {
                            outText.text = outText.text + " Password is invalid, please retype";
                        };
                    }
                    else
                    {
                        WriteNewUser(LoginF.text, PassF.text, 28, "M");
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
