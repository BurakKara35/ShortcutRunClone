using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalIsland : MonoBehaviour
{
    public FinalIslandData finalIslandData;

    public Text count;

    public void Display()
    {
        count.text = "x" + finalIslandData.count;
    }

    public int Multiplier
    {
        get { return finalIslandData.count; }
    }
}
