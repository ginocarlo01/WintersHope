/*
 * Written by: ginocarlo01
 */

using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject optionsUIAnim;
    private bool optionsWasPressed = false;

    [Header("Transition Settings")]
    [SerializeField]
    private string newLevelName;
    [SerializeField]
    private TransitionSettings transition;
    [SerializeField]
    private float startDelay;
    [SerializeField]
    string nextScene;

    private void Start()
    {
        InitUI();
    }

    public void HandleStartButtonOnClickEvent()
    {
        //SceneManager.LoadScene(_scene);
        TransitionManager.Instance().Transition(nextScene, transition, startDelay);
    }

    public void HandleOptionsButtonOnClickEvent()
    {
        if (optionsWasPressed)
        {
            optionsWasPressed = false;
            optionsUIAnim.SetActive(optionsWasPressed);
        }
        else
        {
            optionsWasPressed = true;
            optionsUIAnim.SetActive(optionsWasPressed);
        }
    }

    public void HandleQuitButtonOnClickEvent()
    {
        Application.Quit();
    }

    private void InitUI()
    {
        optionsUIAnim = GameObject.FindGameObjectWithTag("OptionsUI");
        optionsUIAnim.SetActive(false);
        Cursor.visible = true;
        // Libera o cursor para se movimentar livremente
        Cursor.lockState = CursorLockMode.None;
    }
}
