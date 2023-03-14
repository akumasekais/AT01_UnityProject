using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // variable for tracking the parent of this node
    [Tooltip("Parent directly above node should always be first in array.")]
    [SerializeField] private Node[] parents;

    // a list of node variables representing all neighbour for this node
    [Tooltip("Child directly below node should always be first in array.")]
    //[SerializeField] private Node[] neighbours;
    //boolean variabl for checking if node has been searched
    public bool searched = false;

    [SerializeField] private Node[] children;
    private void Start()
    {
        //fimnd all neighbouring nodes and add them to neighbour

    }
    /// <summary>
    /// Returns the children of the node.
    /// </summary>
    public Node[] Children { get { return children; } }
    /// <summary>
    /// Returns the parents of the node.
    /// </summary>
    public Node[] Parents { get { return parents; } }
    // public Node[] Neighbours { get { return neighbours; } }

    private Vector3 offset = new Vector3(0, 1, 0);

    private void OnDrawGizmos()
    {
        //Draws red lines between a parent and its children.
        if (parents.Length > 0)
        {
            foreach (Node node in parents)
            {
                Debug.DrawLine(transform.position, node.transform.position, Color.red);
            }
        }
        //Draws green lines between a child and its children.
        if (children.Length > 0)
        {
            foreach (Node node in children)
            {
                Debug.DrawLine(transform.position + offset, node.transform.position + offset, Color.green);
            }
        }
    }
}
    

    /* method for toggling 'search' between true and false
    public void ToggleSearch()
    {
        if (!(searched = false)) //clear before you take algorithm
        {

        }
        else
        {
            searched = !searched;
        }
    }
}


    */

        //method for setting
      //  public void SetNeighbors(Node[] nodes)
   
