using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChange : MonoBehaviour
{
    private Image buttonImage;
    public int buttonDirection;
    public int mDirection;
    private float timeMax = 0.5f;
    private float timer = 0.5f;

    public GameObject player;
    // Start is called before the first frame update
    private void Awake()
    {
        if (!TryGetComponent<Image>(out buttonImage))
        {
            Debug.Log("Image attatched");
        }
    }
    void Start()
    {
        EventManager.updateColorEvent += ButtonColorChange;
    }

    // Update is called once per frame
    void Update()
    {



        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = -1;
            buttonImage.color = Color.white;
        }
    }



    void ButtonColorChange(Color color, int direction)
    {
        if (direction == buttonDirection)
        {
            buttonImage.color = color;
            timer = timeMax;
        }
    }
}

