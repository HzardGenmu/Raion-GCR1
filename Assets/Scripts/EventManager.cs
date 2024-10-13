using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private void Start()
    {
        // Subscribe to the event
        GameController.OnObjectiveComplete += DisplayWinText;
        GameController.OnObjectiveComplete += DisplayWinImage;
    }

    private void DisplayWinText()
    {
        GameController.instance.ShowWinText();
        Debug.Log("Displaying Win Text");
    }

    private void DisplayWinImage()
    {
        GameController.instance.ShowWinImage();
        Debug.Log("Displaying Win Image");
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event when destroyed
        GameController.OnObjectiveComplete -= DisplayWinText;
        GameController.OnObjectiveComplete -= DisplayWinImage;
    }
}
