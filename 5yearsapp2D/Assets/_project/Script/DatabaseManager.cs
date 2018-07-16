using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour {

    public static DatabaseManager sharedInstance = null;
	
	void Awake () {
        if (sharedInstance == null)
            sharedInstance = this;
        else if (sharedInstance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
