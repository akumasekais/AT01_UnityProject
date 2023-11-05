using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("EventManager required!");

            gameObject.SetActive(false);
        }
    }


    public delegate void UpdateColor(Color color, int direction);
    public static UpdateColor updateColorEvent;

    public delegate void Movement(int direction);
    public static Movement MovementEvent;
}
