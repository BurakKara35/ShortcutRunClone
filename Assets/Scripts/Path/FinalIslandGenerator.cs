using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalIslandGenerator : MonoBehaviour
{
    private int islandCount = 14;
    public GameObject island;

    private Vector3 islandPosition;

    private float newIslandPositionX_Min = 1f;
    private float newIslandPositionX_Max = 3f;
    private float newIslandPositionZ_Min = -2f;
    private float newIslandPositionZ_Max = 2f;

    private float lastIslandScaleXZ = 2;

    public FinalIslandData[] finalIslands;
    public Material[] finalMaterials;
    public List<GameObject> finalIslandsTempList;

    private float displayingInSeconds = 0.1f;
    private IEnumerator displayCoroutine;
    private bool displayingFinish = false;
    private int displayingCount = 0;


    private void Awake()
    {
        islandPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        finalIslandsTempList = new List<GameObject>();

        for (int i = 0; i < islandCount; i++)
        {
            var gameObject = Instantiate(island, islandPosition, Quaternion.identity);
            gameObject.transform.SetParent(transform);
            gameObject.GetComponent<MeshRenderer>().material = finalMaterials[Random.Range(0,finalMaterials.Length)];

            var finalIslandsScript = gameObject.transform.GetComponent<FinalIsland>();
            finalIslandsScript.finalIslandData = finalIslands[i];
            finalIslandsScript.Display();

            islandPosition = new Vector3(islandPosition.x + Random.Range(newIslandPositionX_Min, newIslandPositionX_Max),
                                         islandPosition.y,
                                         islandPosition.z + Random.Range(newIslandPositionZ_Min, newIslandPositionZ_Max));

            if (i == islandCount - 1)
                gameObject.transform.localScale = new Vector3(2f, gameObject.transform.localScale.y, lastIslandScaleXZ);

            gameObject.SetActive(false);

            finalIslandsTempList.Add(gameObject);
        }

        DisplayFinalIslandsInOrder();
    }

    private void DisplayFinalIslandsInOrder()
    {
        displayCoroutine = Displaying();
        StartCoroutine(displayCoroutine);
    }

    public IEnumerator Displaying()
    {
        yield return new WaitForSeconds(displayingInSeconds);

        if (displayingCount < finalIslandsTempList.Count - 1)
        {
            finalIslandsTempList[displayingCount].SetActive(true);
            displayingCount++;

            displayCoroutine = Displaying();
            StartCoroutine(displayCoroutine);
        }
    }
}
