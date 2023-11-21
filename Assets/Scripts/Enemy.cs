using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    bool targetfound = false;

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
                else
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
        currentNode = GameManager.Instance.Nodes[0];
        currentDir = currentNode.transform.position - transform.position;
        currentDir = currentDir.normalized;
    }

    //Implement DFS algorithm method here
    void DepthFirstSearch()
    {

       
       Player player = GameManager.Instance.Player;
       Node nodeCurrentlyBeingSearched;
       
      
     
        List<Node> stack = new List<Node>();

        stack.Add(GameManager.Instance.Nodes[0]);

        bool targetfound = false;


        //iterate through all Nodes in GameManager and set 'searched' back to 'false

        foreach (Node node in GameManager.Instance.Nodes)
        {
            node.searched = false;
        }
       Debug.Log("Player's Target node is: " + player.TargetNode.name);
       Debug.Log("Player's currentnode is: " + player.CurrentNode.name);

        while (!targetfound)
        {          

            if (stack.Count() > 0)
            {
                nodeCurrentlyBeingSearched = stack.Last<Node>(); //take the last item in unsearched nodes list and assign it to node current unsearched'
                if (nodeCurrentlyBeingSearched == player.CurrentNode) //Check if nodeCurrentlyUnsearched is the same as either the target node of the player

                {
                    //nodeCurrentlyBeingSearched.searched = true; //nodeCurrentlyUnsearched.searched = true;


                    // Assign nodeCurrentlyUnsearched to current node
                    currentNode = nodeCurrentlyBeingSearched;
                    currentDir = currentNode.transform.position - transform.position;
                    //giving vectro with a magnitude of 1, takes all values and it becomes a float between 0 and 1
                    //its important to normalize because it gives consistent 
                    currentDir = currentDir.normalized;
                    targetfound = true;
                    //asdasd
                    // Break the loop and finish the method

                }
                else
                {
                    
                    foreach (Node childNode in nodeCurrentlyBeingSearched.Children)
                    {

                        if (!childNode.searched)
                        {
                            // Debug.Log("Adding children to stack." + childNode.name);

                            stack.Add(childNode);
                            childNode.searched = true;
                        }
                        else
                        {
                            Debug.Log("This node is already searched.");
                        }
                        
                        //Debug.Log(stack.Count);

                       /* if (stack.Count > 0)
                            currentNode = GameManager.Instance.Nodes[0];*/
                    }
                }
                // Debug.Log("All children of node " + nodeCurrentlyBeingSearched.name + " are already searched.");
                stack.Remove(nodeCurrentlyBeingSearched);

            }
            else
            {
                // Debug.Log("Didn't find the target. Exhausted all options. Exiting.");
                stack.Add(player.CurrentNode);
                break;

            }


            //doesn't need to be an else statement here
            //else
            //searchedNodes.Add(nodeCurrentlyBeingSearched); //add nodecurrentlybeingsearcehd to the searchednodes list
            /*
            currentNode = Stack[0];

            Stack.RemoveAt(0);

            currentNode.searched = true;*/

            // Set the searched property of the node to true

            //nodeCurrentlyUnsearched.searched = true;

            // 3. Use a for loop to add each child of nodeCurrentlyUnsearched to unsearched nodes list


            // 4. Remove nodeCurrentlyUnsearched from unsearched nodes list



        }
    }
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



