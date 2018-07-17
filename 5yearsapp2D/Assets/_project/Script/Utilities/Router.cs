using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class Router : MonoBehaviour {

    private static DatabaseReference baseRef = FirebaseDatabase.DefaultInstance.RootReference;

    public static DatabaseReference Users()
    {
        return baseRef.Child("Users");
    }

    public static DatabaseReference UserWithID(string userID)
    {
        return baseRef.Child("Users").Child(userID);
    }

    public static DatabaseReference UserYears()
    {
        return baseRef.Child("UserYears");
    }

    public static DatabaseReference UserDates()
    {
        return baseRef.Child("UserDates");
    }

    public static DatabaseReference Questions()
    {
        return baseRef.Child("Questions");
    }

    public static DatabaseReference QuestionAnswers()
    {
        return baseRef.Child("QuestionAnswers");
    }
}
