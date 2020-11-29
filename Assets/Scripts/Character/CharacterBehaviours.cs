using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviours : MonoBehaviour
{
    [HideInInspector] public int woodCount;
    private float woodHeight = 0.015f;

    CharacterBottomTrigger trigger;

    public Transform woodStackTransform;
    public Transform underFootTransform;
    public Transform interactableObjectsTransform;
    public Transform finisTransform;

    private float characterStartingY;

    private bool passedFinish = false;

    private void Awake()
    {
        woodCount = 0;

        trigger = GameObject.FindGameObjectWithTag("CharacterBottomTrigger").GetComponent<CharacterBottomTrigger>();

        characterStartingY = transform.position.y;
    }

    public void GatherWood(GameObject gameObject)
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.transform.position = new Vector3(woodStackTransform.position.x,
                                                      woodStackTransform.position.y + (woodHeight * woodCount),
                                                      woodStackTransform.position.z);
        gameObject.transform.rotation = transform.rotation;
        gameObject.transform.SetParent(woodStackTransform);
    }

    public void MakeShortcutWithWoods()
    {
        woodCount--;
        TurnWoodtoPath();
        PreventCharacterfromDescending();
    }

    private void TurnWoodtoPath()
    {
        GameObject wood = woodStackTransform.GetChild(woodStackTransform.childCount - 1).gameObject;
        BoxCollider woodCollider = wood.GetComponent<BoxCollider>();
        woodCollider.enabled = true;
        woodCollider.isTrigger = false;
        wood.transform.position = underFootTransform.position;
        wood.transform.SetParent(interactableObjectsTransform);
        wood.tag = "Path";
    }

    private void PreventCharacterfromDescending()
    {
        transform.position = new Vector3(transform.position.x, characterStartingY, transform.position.z);
    }

    public bool OnPath
    {
        get { return trigger.onPath; }
        set { trigger.onPath = value; }
    }

    public bool PassedFinish
    {
        get { return passedFinish; }
        set { passedFinish = value; }
    }

    public bool HasWood()
    {
        if (woodCount == 0)
            return false;
        
        return true;
    }

    public void Finished(Transform lastIsland, int multiplier, GameManager gameManager)
    {
        if(lastIsland == null)
        {
            transform.position = finisTransform.position;
            gameManager.MultiplyPoint(1);
        }
        else
        {
            transform.position = lastIsland.position;
            gameManager.MultiplyPoint(multiplier);
        }
    }
}