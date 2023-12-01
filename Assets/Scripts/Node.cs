using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class Node : MonoBehaviour
{
    // variable for tracking the parent of this node
    [Tooltip("Parent directly above node should always be first in array.")]
    [SerializeField] private Node[] parents;
    // a list of node variables representing all neighbour for this node
    [SerializeField] private Node[] children;
    [Tooltip("Child directly below node should always be first in array.")]

    [SerializeField] public List<Node> listChildren = new List<Node>();

    public Node[] Children { get { return children; } }    /// Returns the children of the node.
    public Node[] Parents { get { return parents; } }    /// Returns the parents of the node.

    public Vector3 Location;

    private Vector3 offset = new Vector3(0, 1, 0);

    private void Awake()
    {
        Location = transform.position;

        foreach (Node node in children)
        {
            listChildren.Add(node);
        }
    }
   
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
    
