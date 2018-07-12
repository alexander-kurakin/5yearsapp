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
    private DatabaseReference mDatabase, mDatabase2;
    private string Pass;
    private string Login;
    public Button bttn;
    public Text outText;
    private string existingPerson, newPerson;
    private string UserID;


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
                        FillDates();
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

    public IEnumerable<System.DateTime> EachDay(System.DateTime from, System.DateTime thru)
    {
        for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
            yield return day;
    }

    public void FillDates()
    {
        
            mDatabase2 = FirebaseDatabase.DefaultInstance.GetReference("UserDates");
            UserID = PlayerPrefs.GetString("userID");
            mDatabase2.SetValueAsync(UserID);

            int i = 0;
            foreach (System.DateTime day in EachDay(System.DateTime.Now, System.DateTime.Now.AddYears(5)))
            {
                i++;
                mDatabase2.Child(UserID).Child(i.ToString()).SetValueAsync(day.ToString("ddMMyyyy"));
            }

    }

    public void AssignQuestionsToDates()
    {
        int minNumber = 1;
        int maxNumber = 333   ;

        List<int> possibleNumbers = new List<int>();
        for (int i = minNumber; i <= maxNumber; i++)
            possibleNumbers.Add(i);

        List<int> resultList = new List<int>();

        for (int i = 0; i < maxNumber; i++)
        {
            int randomNumber = Random.Range(1, possibleNumbers.Count)-1;
            
            resultList.Add(possibleNumbers[randomNumber]);

            possibleNumbers.RemoveAt(randomNumber);
        }

        for (int i = 0; i < resultList.Count; i++)
        {
            Debug.Log("Index: " + (i+1) + ", value: "+ resultList[i]);
        }

    }


}
