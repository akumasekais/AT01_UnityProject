using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Define delegate types and events here
    private MeshRenderer meshRenderer;
    public Node CurrentNode { get; set; }
    public Node TargetNode { get; private set; }

    [SerializeField] Material greenMat;

    [SerializeField] private float speed = 4;

    public List<Player> neighbours = new List<Player>();

    [SerializeField] List<Vector3> neighbourDirections = new List<Vector3>();
   
    //similar movement to ai without the pathfinding algorithm, wasd
    private Vector3 currentDir;
    public Player pathNode { get; private set; }

    private bool moving;

    private void Awake()
    {
        moving = false;
        if(!TryGetComponent<MeshRenderer>(out meshRenderer))
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
            if(node.Parents.Length > 2 && node.Children.Length == 0)
            {
                CurrentNode = node;
                TargetNode = node;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {/*
       if (Input.GetButtonDown("Fire1"))
        {
            MouseInput();
            Debug.Log("Click");
        }
        if (moving == false)
        {

            //detect if movement
            //check if any events receieved from the buttons if so, which node is the current node, set moving to true. if moving is true well go to else, say that our distance.
            //if distance is greater than 0.25f. When the game starts the player will be sitting, moving will set to false until a button is pressed.
            //Implement inputs and event-callbacks here
            
        }
        else
        {
            if (Vector3.Distance(transform.position, TargetNode.transform.position) > 0.25f)
            {
                transform.Translate(currentDir * speed * Time.deltaTime);
            }
            else
            {
                moving = false;
                CurrentNode = TargetNode;
            }
        }*/
    }
    private void FindNeighbours()
    {
        RaycastHit hit;
        Player gridNode;

        for(int i = 0; i < neighbourDirections.Count; i++)
        {
            if(Physics.Raycast(transform.position, neighbourDirections[i], out hit, 2f))
            {
                if (hit.collider.TryGetComponent<Player>(out gridNode))
                {
                    neighbours.Add(gridNode);
                }
                     
            }
        }
    }


    public void MouseInput()
    {
        RaycastHit ray;
        if (Physics.Raycast(Input.mousePosition, Vector3.forward, out ray, 2f))
        {
            if (ray.collider.gameObject.tag == "Button")
            {
                Debug.Log("Button Detected");
            }
        }
  
    }
    //Implement mouse interaction method here
    //ifobject in UI which mouse is over is tagged 'button
    //call the input(direction) method
    //invoke' change colour' event
    //kinda relates to raycasting, how can you have a raycast from our cursor position where it is on screen and work out if mouse is covering something. mouse distance from 3d object.


    /// <summary>
    /// Sets the players target node and current directon to the specified node.
    /// </summary>
    /// <param name="node"></param>
   /* public void MoveToNode(Node node)
    {
        CurrentNode = TargetNode; //update the current node index
        //move to the next node

        
        if (moving == true)
        {
            TargetNode = node;
            currentDir = TargetNode.transform.position - transform.position;
            currentDir = currentDir.normalized;
            moving = true;
        }
    }*/
}
