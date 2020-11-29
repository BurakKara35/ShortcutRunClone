using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointHandler : MonoBehaviour
{
    private UIController uIController;

    private void Awake()
    {
        uIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
    }

    public void Reset()
    {
        gameObject.SetActive(false);
        transform.position = new Vector3(transform.position.x, uIController.pointTextStartingY, transform.position.z);
        gameObject.GetComponent<Text>().color += new Color(0, 0, 0, uIController.pointTextStartingAlpha);
        uIController.oldtempPoint += uIController.tempPoint;
    }
}
