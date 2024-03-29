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

    [SerializeField] Material greenMat;

    [SerializeField] private float speed = 4;
    [SerializeField] EventSystem _eventSystem;
    [SerializeField] GraphicRaycaster gRaycaster;
    private PointerEventData pData;

    private NavButton currentButton;
    //public Image oldImage;
    //public Sprite newImage;
    public Player player;


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
        //mouseinputbackwards
        if (moving == false)
        {


            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit hitinfo, 20f))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back), Color.red, 0.3f);
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
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 20f, Color.green);
                }
            }

        }
        else
        {
            if (Vector3.Distance(-transform.position, TargetNode.transform.position) > 0.1f)
            {
                transform.Translate(currentDir * speed * Time.deltaTime);
            }
            else
            {
                Debug.Log("Destination reached");
                moving = false;

            }
        }
        if (moving == false)
        {


            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (Physics.Raycast(transform.position, -transform.right, out RaycastHit hitinfo, 20f))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left), Color.red, 0.3f);
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
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 20f, Color.green);
                }
            }

        }
        else
        {
            if (Vector3.Distance(-transform.position, TargetNode.transform.position) > 0.1f)
            {
                transform.Translate(currentDir * speed * Time.deltaTime);
            }
            else
            {
                Debug.Log("Destination reached");
                moving = false;

            }
        }
        if (moving == false)
        {


            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (Physics.Raycast(transform.position, transform.right, out RaycastHit hitinfo, 20f))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right), Color.red, 0.3f);
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
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 20f, Color.green);
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

        MouseInteraction(pData);
    }

    //Implement mouse interaction method here


    private void MouseInteraction(PointerEventData eventData)
    {

        if (moving == false)

        {
            if (eventData.button == PointerEventData.InputButton.Left)


            {
                Debug.Log("Mouse was clicked");
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
                Debug.Log("It didnt work your mouse interaction.");
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
}
    
    


