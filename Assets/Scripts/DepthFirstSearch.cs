using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class DepthFirstSearch : MonoBehaviour
{
    // list stack variable
    private Stack<Node> nodeStack = new Stack<Node>();
    // list svaraible for path
    
    public List<Node> pathlist = new List<Node>();
 //boolean 'occupied'
    public bool occupied =false;
     //variable for the current node
    private Node currentNode;
 //variable starting node
    private Node startNode;
//variable for destination

    private Node targetNode;

// Start is called before the first frame update
void Start()
{
// find all available nodes and add them to the node list
}

// Update is called once per frame
void Update()
{
    // detect user input for triggering the pathfinding

}

public  List <Node> FindPath(Node start, Node end)
{
//set currentNode as startNode
currentNode = start;
//add currentNode to stack
nodeStack.Push(currentNode);
//set local variable 'found' to false
bool found = false;

//initiate while loop to continue so long as 'found' is false
 while (!found)
    {
        //checked if current node is targetnode
    if (currentNode == end)
        {
        //otherwise set 'found' to true and break the loop
        found = true;
           break;
        }

// boolean that called found 
//if it isnt continue the loop

//for each neighbour of currentNode
foreach (Node neighbour in currentNode.neighbours)
{
    //check if its on teh stack
    if (nodeStack.Contains(neighbour))
        {
        continue;
        }
//check if its already searched
 if (neighbour.searched)
      {
        continue;
      }
      //check if its occupied
 if (neighbour.occupied)
    {
      continue;
     }
     //if neither is true, add neighbour to stack and set currentNode as parent
     nodeStack.Push(neighbour);
     neighbour.parent = currentNode;
}
//set currentnode to 'searched'
    currentNode.searched = true;
    //remove CurrentNode from stack.
    nodeStack.Pop();
//list.last currentNode = stack.last()

//check if stack is empty
if (nodeStack.Count == 0)
            {
                //if it is, break loop and return null with error message as path doesn't exist.
                Debug.Log("Path doesn't exist");
                return null;
            }
            else
            {
                //set last node in stack as current node
                currentNode = nodeStack.Peek();
            }
        }
// return to start of loop
// if 'found' is true
while (currentNode != null)
        {
            //add 'crrentNode' to path == path.add(currentNode)
            pathlist.Add(currentNode);
            
            //check if current node has parent
            if (currentNode.parent != null)
            {
                //if it does set parent as current node
                currentNode = currentNode.parent;
            }
            else
            {
                // otherwise return the path value
                break;
            }
        }
        
        //reverse the pathList to get the correct order
        pathlist.Reverse();
        
        return pathlist;
    }
}

// otherwise return the path value
pathList.Insert(0, currentNode);

//mypath script attach a findpath = currentnode, destination /// state variables curr and dest
return null;
}*/

