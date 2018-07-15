using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase;

public class Quit : MonoBehaviour
{

    public string UserID, UserName;
    public Text qText;
    public string currDay, currYear;

    void Start()
    {
        currDay  = System.DateTime.Now.ToString("ddMMyyyy");
        currYear = System.DateTime.Now.ToString("yyyy");

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://yearsapp.firebaseio.com/");
        UserID = PlayerPrefs.GetString("userID");
        qText.text = "Loading question ...";

        //
        //GetCurrDayYrQuestionID();

        GetQuestion("1");
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(2);
    }

    public void GoBack()
    {
        SceneManager.LoadScene("menu");
    }

    public void GetUserInfo(string UserID)
    {
                FirebaseDatabase.DefaultInstance
                 .GetReference("Users")
                 .OrderByKey()
                 .EqualTo(UserID)
                 .GetValueAsync().ContinueWith(task =>
                   {
                       if (task.IsFaulted)
                       {
                           qText.text = "Failed";
                       }
                       else if (task.IsCompleted)
                       {
                           DataSnapshot snapshot = task.Result;

                           foreach (var childSnapshot in snapshot.Children)
                           {
                               UserName = childSnapshot.Child("username").Value.ToString();
                               qText.text = "Welcome back, " + UserName;
                               
                           }
                       }
                   });

    }

    public void GetQuestion(string questionID)
    {
        FirebaseDatabase.DefaultInstance
       .GetReference("Questions")
       .OrderByKey()
       .EqualTo(questionID)
       .GetValueAsync().ContinueWith(task =>
                 {
                     if (task.IsFaulted)
                     {
                         qText.text = "Failed";
                     }
                     else if (task.IsCompleted)
                     {
                         DataSnapshot snapshot = task.Result;

                         foreach (var childSnapshot in snapshot.Children)
                         {
                             qText.text = childSnapshot.Value.ToString();
                         }
                     }
                 });

    }
}
