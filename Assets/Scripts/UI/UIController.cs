using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text pointText;
    private IEnumerator pointDisplayCoroutine;
    private float pointDisplayInSeconds = 1f;
    private Animator pointAnimator;
    public int tempPoint = 0;
    public int oldtempPoint = 0;
    [HideInInspector] public float pointTextStartingY;
    [HideInInspector] public float pointTextStartingAlpha;

    public GameObject gameOverUI;
    public GameObject levelCompletedUI;

    public Text finalPointText;

    public Button playButton;
    public Button restartButton;

    private void Awake()
    {
        pointText.gameObject.SetActive(false);
        pointTextStartingY = pointText.transform.position.y;

        pointTextStartingAlpha = pointText.GetComponent<Text>().color.a;

        pointAnimator = pointText.gameObject.GetComponent<Animator>();
    }

    public void DisplayPoint(int point)
    {
        pointText.gameObject.SetActive(true);
        DiscardOldPointFromNewPoint(point);
        StopAllCoroutines();

        pointText.text = "+" + tempPoint;

        pointAnimator.enabled = false;
        ResetPointUI();

        pointDisplayCoroutine = PointDisplaying();
        StartCoroutine(pointDisplayCoroutine);
    }

    public IEnumerator PointDisplaying()
    {
        yield return new WaitForSeconds(pointDisplayInSeconds);
        pointAnimator.enabled = true;
        pointAnimator.Rebind();
        pointAnimator.SetTrigger("pointfly");
    }

    private void ResetPointUI()
    {
        pointText.transform.position = new Vector3(transform.position.x, pointTextStartingY, transform.position.z);
        pointText.GetComponent<Text>().color += new Color(0, 0, 0, 255);
    }

    private void DiscardOldPointFromNewPoint(int point)
    {
        tempPoint = point;
        tempPoint -= oldtempPoint;
    }

    public void DisplayGameOverUI()
    {
        gameOverUI.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void DisplayLevelCompletedUI(int finalPoint)
    {
        levelCompletedUI.SetActive(true);
        finalPointText.text = "Final Point : " + finalPoint;
        restartButton.gameObject.SetActive(true);
    }
}
