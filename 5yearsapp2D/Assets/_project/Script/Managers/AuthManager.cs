using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class AuthManager : MonoBehaviour {

    private string Pass;
    private string Login;

    private string existingPerson, newPerson;
    private string UserID, qText;

    public FormManager formManager;

    //Delegates
    public delegate IEnumerator NewUserCallback(Task<FirebaseUser> task, string operation);
    public event NewUserCallback newUserCallback;

    public void WriteNewUser(string name, string password, int age, string sex)
    {

        User user = new User(name, password, age, sex);
        string json = JsonUtility.ToJson(user);

        DatabaseReference newUserID = Router.Users().Push();
        newPerson = newUserID.Key;

        PlayerPrefs.SetString("userID", newPerson);

        newUserID.SetRawJsonValueAsync(json);

    }

    public void LoginAttempt(string LoginF, string PassF)
    {
        
            Router.Users()
            .OrderByChild("username")
            .EqualTo(LoginF)
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    formManager.UpdateStatus("Failed read_user task");
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

                    }

                    if (Login == LoginF)
                    {
                        if (Pass == PassF)
                        {
                            formManager.UpdateStatus("Passwords match! Well Done");
                            PlayerPrefs.SetInt("isAuthenticated", 1);
                            PlayerPrefs.SetString("userID", existingPerson);
                           // SceneManager.LoadScene("main");
                        }
                        else
                        {
                            formManager.UpdateStatus("Password is invalid, please retype");

                        };
                    }
                    else
                    {
                        WriteNewUser(LoginF, PassF, 28, "M");
                        formManager.UpdateStatus("Created new user with login:" + LoginF);
                        //FillDates();
                        //FillYears();
                        //AssignQuestionsToDates();
                        PlayerPrefs.SetInt("isAuthenticated", 1);
                       // SceneManager.LoadScene("main");
                    }
                };
            });

        
    }
}
