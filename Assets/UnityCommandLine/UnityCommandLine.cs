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

    List<string> history = new List<string>();
    int historyIndex = -1;

    private void Awake() {
        response.text = "";
        ShowOrHide(!startHidden);
    }

    private void Update() {

        // submit text
        if (Input.GetKeyDown(KeyCode.Return)) {
            OnSubmit(inputField.text);
        }

        // gow up or down thru history
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            HandleHistoryChange(1);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            HandleHistoryChange(-1);
        }

        // hide console
        if (Input.GetKeyDown(showHideKey)) {
            ShowOrHide(!hidden);
        }
    }

    // page up or down through the commands that were given previously
    void HandleHistoryChange(int amount) {
        historyIndex = Mathf.Clamp(historyIndex + amount, -1, history.Count - 1);

        if (historyIndex < 0) {
            inputField.SetTextWithoutNotify("");
        } else {
            inputField.SetTextWithoutNotify(history[historyIndex]);
        }
    }

    public void OnSubmit(string input) {
        response.text = "Could not parse command.";


        // handle the respond coming in and send it to the receiver
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

        // update history
        historyIndex = -1;
        history.Insert(0, input);

        // empty the input and select it
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
