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
                    //DepthFirstSearch();
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
    /*
    private void DepthFirstSearch()
    {
        Player player = GameManager.Instance.Player; //LOCAL VARIABLE 'PLAYER' = GAMEMANAGER.INSTANCE.PLAYER
        Node Nodes = GameManager.Instance.Nodes();//DO SAME FOR LIST OF NODES
        Node SearchingNodes;  //LOCAL VARIABLE 'NODE BEING SEARCHED'
        Node UnsearchedNodes;
        //boolean for target found
        private bool TargetFound;

    //List of type node storing 'Unsearched nods' (this is your stack)

    //set target found false
    TargetFound = false;

        //Assign gamemanager.instance.nodes[0] to your unsearched nodest list
       // loop starts here
       //while target found is false, continue the loop
       //.5 if unsearched nodes == null
      //then do 1.
       //1. take the last item in unsearched nodes list and assign it to node current unsearched'
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



        //ADD THE ROOT NODE TO UNSEARCHED NODES LIST
        List<Node> UnsearchedNodes() =

       GameManager.Instance.Nodes(0),

       



        //Access the nodes on Game Manager script
        List<Node> unsearchedNodes = new List<Node>(GameManager.Instance.Nodes(0)); //DON'T NEED TO ADD ALL THE NODES, JUST THE ONE AT POSITION 0 (ROOT NODE)


        //BEGIN LOOP HERE
        //NODE AT LAST POSITION OF UNSEARCHED NODES LIST = NODE BEING SEARCHED
        //IS NODE BEING SEARCHED EQUAL TO THE TARGET NODE ON THE PLAYER?
            //IF YES: ASSIGN NODE BEING SEARCHED AS THE DESTINATION FOR THIS ENEMY AI
            //IF NO: CONTINUE SERACHING
        //ADD ALL THE CHILDREN OF NODE BEING SEARCHED TO UNSEARCHED NODE LIST
        //REMOVE NODE BEING SEARCHED FROM UNSEARCHED NODES LIST
        //RETURN TO START OF LOOP


        parents.Push(unsearchedNodes[0]); // add GameManager.instance.nodes[0] to a list of unsearched nodes (root node)
        unsearchedNodes.RemoveAt(0); // remove root node from unsearched nodes

        //check if root node is the same as game manager.instance.player.targetnode

        //if it is the same: return that as the new destination for this enemy.
        }

    }
}
    */