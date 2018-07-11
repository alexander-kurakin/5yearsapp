using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDefault : MonoBehaviour {

	// Use this for initialization
	public void SetDef () {
        PlayerPrefs.SetInt("isAuthenticated", 0);
	}
	
}
