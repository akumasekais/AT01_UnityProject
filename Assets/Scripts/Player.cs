using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    //Define delegate types and events here
    private MeshRenderer meshRenderer;
    public Node CurrentNode { get; set; }
    public Node TargetNode { get; private set; }

    [SerializeField] Material greenMat;

    [SerializeField] private float speed = 4;
    [SerializeField] EventSystem _eventSystem;
    [SerializeField] GraphicRaycaster gRaycaster;
    private PointerEventData pData;

    private NavButton currentButton;

    //public List<Player> neighbours = new List<Player>();

    //[SerializeField] List<Vector3> neighbourDirections = new List<Vector3>();

    //similar movement to ai without the pathfinding algorithm, wasd
    private Vector3 currentDir;
    public Player pathNode { get; private set; }

    private bool moving = false;

    Ray hit;

    float maxDistance = 100;

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
        // MoveToNode(); 
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
            MouseInteraction();
        
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitinfo, 20f))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red, 0.3f);
                    Node hitNode;

                    if (hitinfo.collider.TryGetComponent<Node>(out hitNode))
                    {
                        Debug.Log("Hit " + hitNode.name);

                        MoveToNode(hitNode);

                        //CurrentNode = TargetNode;
                    }


                }
                else
                {
                    Debug.Log("Hit Nothing");
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20f, Color.green);
                }
            }
        }
        else 
        {
            if (Vector3.Distance(transform.position, TargetNode.transform.position) > 0.1f)
            {
                transform.Translate(currentDir * speed * Time.deltaTime);
            }
            else
            {
                Debug.Log("Destination reached");
                moving = false;

            }
        }
        /*if (Input.GetKeyDown(KeyCode.DownArrow)) //mouseinputbackwards
        {
            if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit hitinfo, 20f))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.forward), Color.red, 0.3f);
                Node hitNode;

                if (hitinfo.collider.TryGetComponent<Node>(out hitNode))
                {
                    Debug.Log("Hit " + hitNode.name);

                    MoveToNode(hitNode);

                    //CurrentNode = TargetNode;
                }


            }
            else
            {
                Debug.Log("Hit Nothing");
                Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.forward) * 20f, Color.green);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, TargetNode.transform.position) > 0.1f)
            {
                transform.Translate(currentDir * speed * Time.deltaTime);
            }
            else
            {
                Debug.Log("Destination reached");
                moving = false;

            }*/
        
    }

    //Implement mouse interaction method here
    private void MouseInteraction()
    {
        pData = new PointerEventData(_eventSystem);
        pData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        gRaycaster.Raycast(pData, results);
        NavButton nButton;
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.TryGetComponent<NavButton>(out nButton))
            {
                currentButton = nButton;
            }
            else
            {
                Debug.Log("Done");
            }

            if (Input.GetMouseButtonDown(0) && currentButton != null)
            {
                Debug.Log("Input detected!");
                //UpdateTargetNode(currentButton.direction);
            }
        }
    }
    //call the input(direction) method
    //invoke' change colour' event


    /// <summary>
    /// Sets the players target node and current directon to the specified node.
    /// </summary>
    /// <param name="node"></param>
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

        private void OnDrawGizmos()
            {

            }
        
    }


