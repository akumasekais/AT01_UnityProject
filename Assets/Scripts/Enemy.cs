using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Movement speed modifier.")]
    [SerializeField] private float speed = 3;
    private Node currentNode;
    private Vector3 currentDir;
    private bool playerCaught = false;
 
    public delegate void GameEndDelegate();
    public event GameEndDelegate GameOverEvent = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        InitializeAgent();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCaught == false)
        {
            if (currentNode != null)
            {
                //If within 0.25 units of the current node.
                if (Vector3.Distance(transform.position, currentNode.transform.position) > 0.25f)
                {
                    transform.Translate(currentDir * speed * Time.deltaTime);
                }
                else;
                {
                    DepthFirstSearch();
                    //Implement path finding algorithm here + invoke the method: call it here
                }

            }
            else
            {
                Debug.LogWarning($"{name} - No current node");
            }

            Debug.DrawRay(transform.position, currentDir, Color.cyan);
        }
    }

    //Called when a collider enters this object's trigger collider.
    //Player or enemy must have rigidbody for this to function correctly.
    private void OnTriggerEnter(Collider other)
    {
        if (playerCaught == false)
        {
            if (other.tag == "Player")
            {
                playerCaught = true;
                GameOverEvent.Invoke(); //invoke the game over event
            }
        }
    }

    /// <summary>
    /// Sets the current node to the first in the Game Managers node list.
    /// Sets the current movement direction to the direction of the current node.
    /// </summary>
    void InitializeAgent()
    {
        //refer to the game manager without having reference just saying game.managerinstance index 0
        currentNode = GameManager.Instance.Nodes[0];
        //Vector3.direction, working out direction of one vector to another
        currentDir = currentNode.transform.position - transform.position;
        //giving vectro with a magnitude of 1, takes all values and it becomes a float between 0 and 1
        //its important to normalize because it gives consistent 
        currentDir = currentDir.normalized;
        //finding two vectors for direction is dir = b - a
        //finding two vector for distance is distance = a - b
    }
}


    //Implement DFS algorithm method here
    
    private void DepthFirstSearch()
    {
        Player player = GameManager.Instance.Player; //LOCAL VARIABLE 'PLAYER' = GAMEMANAGER.INSTANCE.PLAYER
        Node Nodes = GameManager.Instance.nodes();//DO SAME FOR LIST OF NODES
        Node nodeCurrentlyUnsearched;  //LOCAL VARIABLE 'NODE BEING SEARCHED'
        Node UnsearchedNodes;
    
        //boolean for target found
        private bool targetFound;

        while not (targetFound == false)
        {
            if (UnsearchedNodes.Count == 0)
            {
                Node nodeCurrentlyUnsearched = UnsearchedNodes[UnsearchedNodes.Count - 1]; 
                //1. take the last item in unsearched nodes list and assign it to node current unsearched'
                
                if (nodeCurrentlyUnsearched == GameManager.Instance.Player.TargetNode) //Check if nodeCurrentlyUnsearched is the same as either the target node of the player
                {
                    nodeCurrentlyUnsearched.searched = true; //nodeCurrentlyUnsearched.searched = true;

                // Assign nodeCurrentlyUnsearched to current node
                    GameManager.Instance.Player.currentNode = nodeCurrentlyUnsearched;
                // Break the loop and finish the method
                    targetFound = true;
                }
                else
                {
                    // Set the searched property of the node to true

                    nodeCurrentlyUnsearched.searched = true;
                
                    // 3. Use a for loop to add each child of nodeCurrentlyUnsearched to unsearched nodes list
                    foreach (Node childNode in currentNode.Children)
                    {
                        if (childNode.searched = false)
                        {
                            UnsearchedNodes.Add(childNode);
                        }
                    }

                    // 4. Remove nodeCurrentlyUnsearched from unsearched nodes list
                    UnsearchedNodes.Remove(currentNode);
                }
            }
        //}   
    }



        //Assign gamemanager.instance.nodes[0] to your unsearched nodest list
       // loop starts here
       //while target found is false, continue the loop
       //.5 if unsearched nodes == null
      //then do 1.
       //2. check if node currently being searched is the same as *either*
        //the target node of the player (node they are heading towards)
        //the current node of the player (the last node they visited)
      //If THIS IS TRUE ('NODE  CURRENTLY BEING SEARCHED IS THE ONE WE WANT):
       
            //ASSIGN NODE CURRENTLY BEING SEARCHED TO CURRENT NODE
            //BREAK THE LOOP AND FINISH THE METHOD
      //IF ITISNT TRUE CONTINUE
      //3. USE A FOR LOOP TO ADD EACH CHILD OF NODE CURRENTLY BEING SEARCHED TO UNSEARCHED NODES LIST
      //4. REMOVE 'NODE CURRENTLY BEING SEARCHED' FROM UNSEARCHED
      //6. RETURN TO START OF LOOP



