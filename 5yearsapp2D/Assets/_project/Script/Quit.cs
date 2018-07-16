using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase;
using System;
using System.Collections.Generic;

public class Quit : MonoBehaviour
{

    public string UserID, UserName;
    public Text qText, yrText, dayTxt;
    public string currDay, currYear, currDayFm, qTextT, qTextID;
    public int yrID, dayID;
    public bool finQ1, finQ2;

   void Start()
    {
        currDay  = System.DateTime.Now.ToString("ddMMyyyy");
        currYear = System.DateTime.Now.ToString("yyyy");

        currDayFm = System.DateTime.Now.ToString("d MMMM");

        dayTxt.text = currDayFm;
        yrText.text = currYear;

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://yearsapp.firebaseio.com/");
        UserID = PlayerPrefs.GetString("userID");
        qText.text = "Loading question ...";
                
        StartCoroutine(Waiter(2));

    }

    IEnumerator Waiter(int secs)
    {
        GetDayID();
        GetYearID();
        yield return new WaitForSeconds(secs);
        GetQuestionID(yrID, dayID);
        yield return new WaitForSeconds(secs);
        if (Convert.ToInt32(qTextID) < 101)
            GetQuestion(qTextID);
        else
            qText.text = "Question is on development";


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

    public void GetQuestionID(int yrID, int dayID)
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("QuestionAnswers")
            .OrderByChild("username")
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                    qText.text = "Failed";
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    foreach (var childSnapshot in snapshot.Children)
                    {
                        if (childSnapshot.Child("userID").Value.ToString() == UserID
                        && childSnapshot.Child("yearID").Value.ToString() == yrID.ToString()
                        && childSnapshot.Child("dateID").Value.ToString() == dayID.ToString())
                            qTextID = childSnapshot.Child("questionID").Value.ToString();
                    }
                }

            }
            );
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

    public void GetYearID()
    {
      FirebaseDatabase.DefaultInstance
      .GetReference("UserYears")
      .Child(UserID)
      .OrderByValue()
      .EqualTo(currYear)
      .GetValueAsync().ContinueWith(task =>
      {
          if (task.IsFaulted)
          {
              qText.text = "Failed";
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;

              foreach (DataSnapshot dataSnap in snapshot.Children)
              {
                  yrID = Convert.ToInt32(dataSnap.Key.ToString());
              }

          }
      });

    }

    public void GetDayID()
    {
       FirebaseDatabase.DefaultInstance
       .GetReference("UserDates")
       .Child(UserID)
       .OrderByValue()
       .EqualTo(currDay)
       .GetValueAsync()
       .ContinueWith
       (task =>
       {
           if (task.IsFaulted)
           {
               qText.text = "Failed";
           }
           else if (task.IsCompleted)
           {
               DataSnapshot snapshot = task.Result;

               foreach (DataSnapshot dataSnap in snapshot.Children)
               {
                   dayID = Convert.ToInt32(dataSnap.Key.ToString());
               }
           }
       });
    }

}
