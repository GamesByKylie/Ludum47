using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public Image theseusNametag;
    public Image otherNametag;
    public TMPro.TextMeshProUGUI otherName;
    public Image otherSprite;
    public Sprite ariadneSprite;
    public Sprite athenaSprite;
    public Sprite guardSprite;

    public GameController gc;

    public TMPro.TextMeshProUGUI content;
    public TMPro.TextMeshProUGUI continueMessage;

    private DialogueUI ui;
    private DialogueRunner runner;
    private bool ariadne = false;

    [HideInInspector] public bool dialogRunning = false;

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
            otherNametag.gameObject.SetActive(false);
            theseusNametag.gameObject.SetActive(true);
        }
        else
        {
            otherNametag.gameObject.SetActive(true);
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

    [YarnCommand("earncandle")]
    public void EarnCandle()
    {
        gc.ShowTimer = true;
    }

    [YarnCommand("setname")]
    public void SetName(string n)
    {
        if (n == "Ariadne")
        {
            ariadne = true;
            otherSprite.sprite = ariadneSprite;
            otherName.text = "Ariadne";
        }
        else if (n == "Athena")
        {
            ariadne = false;
            otherSprite.sprite = athenaSprite;
            otherName.text = "Athena";
        }
        else if (n == "Guard")
        {
            ariadne = false;
            otherSprite.sprite = guardSprite;
            otherName.text = "Guard";
        }
    }

    public void DisplayContinueMessage(bool toggle)
    {
        continueMessage.text = "Spacebar or Left Click to advance";
        continueMessage.gameObject.SetActive(toggle);
    }

    public void ClearDialog()
    {
        content.text = "";
        continueMessage.text = "Select an option to continue";
    }

    public void StartDialog(string startNode)
    {
        runner.startNode = startNode;
        runner.ResetDialogue();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        dialogRunning = true;
    }

    public void EndDialog()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (ariadne)
        {
            gc.TrySpawnThread();
        }
        dialogRunning = false;
    }
}
