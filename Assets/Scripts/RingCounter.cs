using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RingCounter : MonoBehaviour
{
    public static RingCounter instance;

    public TMP_Text ringText;
    public int currentRings = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ringText.text = currentRings.ToString();
    }

    public void IncreaseRing(int value)
    {
        currentRings += value;
        ringText.text = currentRings.ToString();
    }
}
