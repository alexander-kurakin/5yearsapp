using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;

public class FormManager : MonoBehaviour {

    public InputField LoginInput;
    public InputField PassInput;

    public Button LoginButton;

    public Text statusText;

    public AuthManager authManager;

    private void Awake()
    {
        ToggleButtonState(false);
        //Auth delegate subscriptions
        authManager.newUserCallback += HandleNewUserCallback;
    }

    public void ValidateInput() {
        if (LoginInput.text != "" && PassInput.text != "")
            ToggleButtonState(true);
    }

    IEnumerator HandleNewUserCallback(Task<FirebaseUser> task, string operation) {
        yield return null;
    }

    public void OnLogin() {
        Debug.Log("Login");
        authManager.LoginAttempt(LoginInput.text, PassInput.text);
    }

    private void ToggleButtonState(bool toState) {
        LoginButton.interactable = toState;
    }

    public void UpdateStatus(string message) {
        statusText.text = message;
    }

    private void OnDestroy()
    {
        authManager.newUserCallback -= HandleNewUserCallback;
    }
}
