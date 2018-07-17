using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class DatabaseManager : MonoBehaviour {

    public static DatabaseManager sharedInstance = null;
	
	void Awake () {
        if (sharedInstance == null)
            sharedInstance = this;
        else if (sharedInstance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://yearsapp.firebaseio.com/");
       // Debug.Log(Router.Users());
    }
	
}
