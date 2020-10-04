using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public Image theseusNametag;
    public Image ariadneNametag;

    public GameController gc;

    public TMPro.TextMeshProUGUI content;
    public TMPro.TextMeshProUGUI continueMessage;

    private DialogueUI ui;
    private DialogueRunner runner;

    private void Start()
    {
        ui = GetComponent<DialogueUI>();
        runner = GetComponent<DialogueRunner>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            ui.MarkLineComplete();
        }
    }

    [YarnCommand("name")]
    public void SwapNametag(string name)
    {
        if (name == "Theseus")
        {
            ariadneNametag.gameObject.SetActive(false);
            theseusNametag.gameObject.SetActive(true);
        }
        else if (name == "Ariadne")
        {
            ariadneNametag.gameObject.SetActive(true);
            theseusNametag.gameObject.SetActive(false);
        }
    }

    [YarnCommand("earnthread")]
    public void EarnThread()
    {
        Debug.Log("Running EarnThread");
        gc.EarnedThread = true;
    }

    [YarnCommand("earnsword")]
    public void EarnSword()
    {
        gc.EarnedSword = true;
    }

    [YarnCommand("earncrown")]
    public void EarnCrown()
    {
        gc.EarnedCrown = true;
    }

    public void DisplayContinueMessage(bool toggle)
    {
        continueMessage.gameObject.SetActive(toggle);
    }

    public void ClearDialog()
    {
        content.text = "";
    }

    public void StartDialog()
    {
        runner.ResetDialogue();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void EndDialog()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gc.TrySpawnThread();
    }
}
