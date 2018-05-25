using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
//using System;

public class GetQuestion : MonoBehaviour {

    public Text text;
    private int randomNum;

    // Use this for initialization
    void Start () {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://yearsapp.firebaseio.com/");
        Debug.Log("Instance set to yearsapp, yeehah");

        DatabaseReference myDatabase = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void GenerateQuestion()
    {

        randomNum = Random.Range(1, 100);

        FirebaseDatabase.DefaultInstance
    .GetReference("Questions")
    .OrderByKey()
    .EqualTo(randomNum.ToString())
    .ValueChanged += (object sender2, ValueChangedEventArgs e2) =>
    {
        if (e2.DatabaseError != null)
        {
            Debug.Log(e2.DatabaseError.Message);
        }

        if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0)
        {
            foreach (var childSnapshot in e2.Snapshot.Children)
            {
                var q = childSnapshot.Value.ToString();
                text.text = q.ToString();
            }
        }

    };

    }
	
}
