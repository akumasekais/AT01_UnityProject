using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEngine.UI.ContentSizeFitter;


public class Player : MonoBehaviour
{
    //Define delegate types and events here
    private MeshRenderer meshRenderer;
    public Node CurrentNode { get; set; }
    public Node TargetNode { get; private set; }

    float x_move = 0f;
    float y_move = 0f;

    [SerializeField] Material greenMat;

    [SerializeField] private float speed = 4;
    [SerializeField] EventSystem _eventSystem;
    [SerializeField] GraphicRaycaster gRaycaster;
    private PointerEventData pData;

    public Player player;


    private Vector3 currentDir;
    public Player pathNode { get; private set; }

    private bool moving = false;

    public bool bleft;
    public bool bright;
    public bool bup;
    public bool bdown;

    Ray hit;

   

    public LayerMask layersToHit;
    private void Awake()
    {
        moving = false;
        if (!TryGetComponent<MeshRenderer>(out meshRenderer))
        {
            Debug.Log("You need to attach a MeshRender to this object");
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        EventManager.MovementEvent += Inputs;
        //access game manager instnace and it goes throguh all nodes that contain nodes list adn checks if the amount of children is equal to 0, essentially grab one nodes 6
        //for instance has three parents
        foreach (Node node in GameManager.Instance.Nodes)
        {
            if (node.Parents.Length > 2 && node.Children.Length == 0)
            {
                CurrentNode = node;
                TargetNode = node;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //MouseInputForward
        if (moving == false)
        {
            //Inputs and event-callbacks here
            x_move = Input.GetAxis("Horizontal");
            y_move = Input.GetAxis("Vertical");
            Inputs(0);
        }

        else
        {
            if (Vector3.Distance(transform.position, TargetNode.transform.position) > 0.1f)
            {
                transform.Translate(currentDir * speed * Time.deltaTime);
            }
            else
            {
                // Debug.Log("Destination reached");
                moving = false;
            }
        }

           
    }
    //Implement mouse interaction method here
    void Inputs(int direction)
    {
        if (moving == false)
        {
            RaycastHit hit;
            if (x_move < 0 | direction == 1)
            {
                if (Physics.Raycast(transform.position, Vector3.left, out hit, 10))
                {
                    MoveToNode(hit.collider.gameObject.GetComponent<Node>());
                    EventManager.updateColorEvent(Color.green, 1);
                    direction = 0;
                }
                else
                {
                    EventManager.updateColorEvent(Color.red, 1);
                }
            }
            if (x_move > 0 | direction == 2)
            {
                if (Physics.Raycast(transform.position, Vector3.right, out hit, 10))
                {
                    MoveToNode(hit.collider.gameObject.GetComponent<Node>());
                    EventManager.updateColorEvent(Color.green, 2);
                    direction = 0;
                }
                else
                {
                    EventManager.updateColorEvent(Color.red, 2);
                }
            }
            if (y_move < 0 | direction == 3)
            {
                if (Physics.Raycast(transform.position, Vector3.back, out hit, 10))
                {
                    MoveToNode(hit.collider.gameObject.GetComponent<Node>());
                    EventManager.updateColorEvent(Color.green, 3);
                    direction = 0;
                }
                else
                {
                    EventManager.updateColorEvent(Color.red, 3);
                }
            }
            if (y_move > 0 | direction == 4)
            {
                if (Physics.Raycast(transform.position, Vector3.forward, out hit, 10))
                {
                    MoveToNode(hit.collider.gameObject.GetComponent<Node>());
                    EventManager.updateColorEvent(Color.green, 4);
                    direction = 0;
                }
                else
                {
                    EventManager.updateColorEvent(Color.red, 4);
                }
            }
        }
    }
    private void MoveToNode(Node node)
    {
        CurrentNode = TargetNode; //update the current node index
                                  //move to the next node


        if (moving != true)
        {
            TargetNode = node;
            currentDir = TargetNode.transform.position - transform.position;
            currentDir = currentDir.normalized;
            moving = true;
        }
    }
    public void buttonpress(int button)
    {
        Inputs(button);
    }

}