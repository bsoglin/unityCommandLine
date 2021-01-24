using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityCommandLine : MonoBehaviour
{
    public KeyCode showHideKey;

    public GameObject inputContainer;
    public TMPro.TMP_InputField inputField;
    public TMPro.TextMeshProUGUI response;
    public GameObject receiver;

    public bool startHidden = false;
    bool hidden;
    

    private void Awake() {
        response.text = "";
        ShowOrHide(!startHidden);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            OnSubmit(inputField.text);
        }

        if (Input.GetKeyDown(showHideKey)) {
            ShowOrHide(!hidden);
        }
    }

    public void OnSubmit(string input) {
        response.text = "Could not parse command.";

        string[] inputSplit = input.Split(' ');

        if (inputSplit.Length == 1) {
            receiver.SendMessage(inputSplit[0]);
            response.text = $"{input} >> done";
        } else if (inputSplit.Length == 2) {
            receiver.SendMessage(inputSplit[0], inputSplit[1]);
            response.text = $"{input} >> done";
        } else {
            response.text = "could not parse command.";
        }

        inputField.SetTextWithoutNotify("");
        inputField.ActivateInputField();
    }

    void ShowOrHide(bool val) {
        inputContainer.SetActive(val);
        hidden = val;

        if (val) {
            inputField.ActivateInputField();
        }
    }
}
