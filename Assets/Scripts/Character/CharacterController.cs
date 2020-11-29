using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private CharacterMovement movement;
    private CharacterAnimation animation;
    private CharacterBehaviours character;
    private GameManager game;

    private bool swipe = false;
    private bool swipeFinished = true;
    private float swipeFirstPosition;
    private float differenceBetweenSwipePositions;
    private IEnumerator swipeCoroutine;

    private enum CharacterDirection {None, Left, Right}
    private CharacterDirection characterDirection;

    private Transform lastFinalIslandPassed;
    private int finalIslandMultiplier;

    private float swipingInSeconds = 0.2f;

    private void Awake()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        movement = GetComponent<CharacterMovement>();
        character = GetComponent<CharacterBehaviours>();
        animation = GetComponent<CharacterAnimation>();

        characterDirection = CharacterDirection.None;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            swipe = true;
            swipeFirstPosition = Input.mousePosition.x;
            swipeCoroutine = Swiping();
            StartCoroutine(swipeCoroutine);
        }
        if (Input.GetMouseButton(0) && swipe)
        {
            swipeFinished = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            swipe = false;
            swipeFinished = true;
            characterDirection = CharacterDirection.None;
            StopCoroutine(swipeCoroutine);
        }

        if (characterDirection == CharacterDirection.Left)
            movement.TurnLeft();
        else if (characterDirection == CharacterDirection.Right)
            movement.TurnRight();
    }

    private void FixedUpdate()
    {
        if(game.isOn)
        {
            if (character.OnPath)
            {
                movement.Run();

                if (character.HasWood())
                    animation.RunAndCarryWood();
                else
                    animation.Run();
            }
            else
            {
                if(character.HasWood())
                {
                    character.MakeShortcutWithWoods();
                    movement.Run();
                    animation.RunAndMakeShortcut();
                }
                else
                {
                    if (!movement.JumpedOnce)
                    {
                        movement.Jump();
                        animation.Jump();
                        movement.JumpedOnce = true;
                    }
                }
            }
        }
    }

    public IEnumerator Swiping()
    {
        yield return new WaitForSeconds(swipingInSeconds);
        if (swipe)
        {
            differenceBetweenSwipePositions = Input.mousePosition.x - swipeFirstPosition;

            if (differenceBetweenSwipePositions < 0)
                characterDirection = CharacterDirection.Left;
            else if (differenceBetweenSwipePositions > 0)
                characterDirection = CharacterDirection.Right;

            if (!swipeFinished)
            {
                swipeCoroutine = Swiping();
                StartCoroutine(swipeCoroutine);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Path"))
        {
            character.OnPath = true;
            movement.JumpedOnce = false;

            if (collision.gameObject.layer == 8) // 8 for FinalIslands
            {
                lastFinalIslandPassed = collision.gameObject.transform;
                finalIslandMultiplier = (int)collision.gameObject.GetComponent<FinalIsland>().Multiplier;

                if(collision.gameObject.GetComponent<FinalIsland>().count.text == "x15") // Last Final Island
                {
                    game.isOn = false;
                    character.Finished(lastFinalIslandPassed, finalIslandMultiplier, game);
                    animation.Finish();
                    game.LevelCompleted();
                }
            }
        }
        
        if (movement.JumpedOnce && collision.gameObject.CompareTag("Sea"))
        {
            if (!character.PassedFinish)
            {
                movement.Stop();
                animation.Stop();
                game.GameOver();
            }
            else
            {
                character.Finished(lastFinalIslandPassed, finalIslandMultiplier, game);
                animation.Finish();
                game.LevelCompleted();
            }

            game.isOn = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            game.GenerateFinalIslands();
            character.PassedFinish = true;
        }

        if (other.CompareTag("Wood"))
        {
            character.woodCount++;
            character.GatherWood(other.gameObject);
            game.IncreasePoint();
        }
    }
}
