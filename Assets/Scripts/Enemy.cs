using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Movement speed modifier.")]
    [SerializeField] private float speed = 3;
    [SerializeField] private Node currentNode;
    private Vector3 currentDir;
    private bool playerCaught = false;

    [SerializeField] private Node startNode;
    [SerializeField] private Player player;

    public bool started = false;

    [SerializeField] private Node tempNode;

    public Node playerDestination;

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
            if (player.TargetNode == null)
            {
                playerDestination = player.CurrentNode;
            }
            else
            {
                playerDestination = player.TargetNode;
            }

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
        // create lists of nodes visited and nodes in the stack
        List<Node> visitedNodes = new List<Node>();
        List<Node> stackNodes = new List<Node>();

        List<Node> childrenNodes = new List<Node>();

        Node currNode = startNode;

        int childrenVisited = 0;

        int debugLoopLimit = 0;

        bool targetFound = false;

        while (targetFound == false)
        {
            tempNode = currNode;
            debugLoopLimit++;

            if (debugLoopLimit > 100)
            {
                targetFound = true;

            }
            if (tempNode != playerDestination | currNode != playerDestination)
            {
                visitedNodes.Add(currNode);
                childrenNodes = currNode.listChildren;
                stackNodes.Remove(currNode);

                if (childrenNodes.Count != 0)
                {

                    foreach (Node node in childrenNodes)
                    {
                        if (!visitedNodes.Contains(node))
                        {
                            if (!stackNodes.Contains(node))
                            {
                                stackNodes.Add(node);
                            }
                        }
                        else
                        {
                            childrenVisited++;
                        }
                    }
                    if (childrenNodes.Count != childrenVisited)
                    {
                        if (stackNodes.Count > 0)
                        {
                            currNode = stackNodes[stackNodes.Count - 1];
                        }
                    }
                    else
                    {
                        stackNodes.RemoveAt(stackNodes.Count - 1);
                        currNode = stackNodes[stackNodes.Count - 1];

                    }

                }
                else
                {
                    visitedNodes.Add(currNode);

                    stackNodes.Remove(currNode);

                    currNode = stackNodes[stackNodes.Count - 1];
                }

            }
            else
            {
                targetFound = true;
                currentNode = currNode;

                if (currentNode != null)
                {
                    currentDir = (currentNode.transform.position - transform.position).normalized;

                }
                else
                {
                    Debug.Log("currentNode is null");

                }
            }
              
        }
        //Debug.Log("DFS completed.");


    }
}