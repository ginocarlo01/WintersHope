using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState
{
    walk,
    interact
}

public class BallMovement : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    Vector3 changeMovementInput;

    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    private Animator anim;

    private bool pause;

    PlayerState currentState;

    [Header("Death Settings")]
    [SerializeField]
    private TransitionSettings transition;
    [SerializeField]
    private float startDelay;
    private CheckPointManager checkPointManager;
    private LifeBorder lifeBorder;
    [SerializeField]
    private float delayDeath = .2f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        checkPointManager = GetComponent<CheckPointManager>();
        lifeBorder = GetComponentInChildren<LifeBorder>();

        currentState = PlayerState.walk;
    }

    private void FixedUpdate()
    {
        if (currentState == PlayerState.interact)
        {
            return;
        }

        GetInput();

        MoveCharacter();

        AnimateCharacter();
    }

    void MoveCharacter()
    {
        rb.MovePosition(transform.position + changeMovementInput.normalized * Time.deltaTime * moveSpeed);
    }

    void GetInput()
    {
        changeMovementInput = Vector3.zero;
        changeMovementInput.x = Input.GetAxisRaw("Horizontal");
        changeMovementInput.y = Input.GetAxisRaw("Vertical");
    }

    void AnimateCharacter()
    {
        if(changeMovementInput != Vector3.zero)
        {
            anim.SetFloat("moveX", changeMovementInput.x);
            anim.SetFloat("moveY", changeMovementInput.y);
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }
    }

    public void HandlePauseInput()
    {
        pause = !pause;
        TogglePause(pause);

    }

    private void TogglePause(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void EnableInteraction()
    {
        currentState = PlayerState.interact;
        anim.SetBool("moving", false);
        Time.timeScale = 0f;
    }

    public void EnableDeath()
    {
        currentState = PlayerState.interact;
    }

    public void DisableInteraction()
    {
        currentState = PlayerState.walk;
        Time.timeScale = 1f;
    }

    public void Death()
    {
        anim.SetTrigger("Death");

    }

    public void ReloadScene()
    {
        //TransitionManager.Instance().Transition(SceneManager.GetActiveScene().name, transition, startDelay);
        
        TransitionManager.Instance().Transition(transition, startDelay);
        StartCoroutine(IAfterDeath());
    }

    #region SubjectSubscription
    private void OnEnable()
    {
        SignInteraction.playerInteractAction += EnableInteraction;
        CollectableInteraction.playerInteractAction += EnableInteraction;
        AddToProjectileArsenal.playerInteractAction += EnableInteraction;
        DialogueManager.endPlayerInteractAction += DisableInteraction;
        LifeBorder.deathAction += EnableDeath;
        LifeBorder.deathAction += Death;
    }
    private void OnDisable()
    {
        SignInteraction.playerInteractAction -= EnableInteraction;
        CollectableInteraction.playerInteractAction -= EnableInteraction;
        AddToProjectileArsenal.playerInteractAction -= EnableInteraction;
        DialogueManager.endPlayerInteractAction -= DisableInteraction;
        LifeBorder.deathAction += EnableDeath;
        LifeBorder.deathAction -= Death;
    }

    #endregion

    IEnumerator IAfterDeath()
    {
        yield return new WaitForSeconds(delayDeath);
        transform.position = checkPointManager.GetLastPosition();
        lifeBorder.RestartLife();
        DisableInteraction();
        anim.SetTrigger("Restart");
        lifeBorder.dead = false;
    }
}
