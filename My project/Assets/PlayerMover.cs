using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    //Player Object
    public GameObject player;
    //The wall object, to be instantiated
    public GameObject wall;

    //Node where the player currently is, and where they want to be
    public Node currentNode;
    Vector3 goalNode;

    public static List<Vector3> visited = new List<Vector3>();

    public List<Node> returnList = new List<Node>();

    public static List<GameObject> blockList = new List<GameObject>();



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    //Our custom method. 
                    move(raycastHit.transform.gameObject);
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    //Our custom method. 
                    makeWall(raycastHit.transform.gameObject);
                }
            }
        }
    }
    
    void move(GameObject gameObject)
    {
        if (gameObject.tag == "Floor")
        {        
            Vector3 destPoint = gameObject.transform.position;
            goalNode = destPoint+ new Vector3(0,1.25f,0);
            Vector3 ppos = player.transform.position;
            print("Goal Node: " + goalNode);
            Node currentNode = new Node(ppos);
            Queue<Node> queue = new Queue<Node>();

            queue.Enqueue(currentNode);
            print("First onNode: " + currentNode.data);
            while (queue.Count != 0)
            {
                Node onNode = queue.Dequeue();
                
                if (onNode.data == goalNode)
                {
                    returnList.Clear();
                    print("Goal found");
                    Node parent = onNode.parent;
                    while (onNode.parent != null)
                    {
                        returnList.Add(onNode);
                        onNode = onNode.parent;
                    }
                    foreach (Node node in returnList)
                    {
                        print("NodeVector: " + node.data);
                    }
                    returnList.Reverse();
                    break;
                }
                else {
                    Vector3 left = new Vector3(onNode.data.x - 1f, onNode.data.y, onNode.data.z);
                    if (left.x >= -2) { isVisited(left, queue, onNode); };
                    Vector3 right = new Vector3(onNode.data.x + 1f, onNode.data.y, onNode.data.z);
                    if (right.x <= 2) { isVisited(right, queue, onNode); };
                    Vector3 up = new Vector3(onNode.data.x, onNode.data.y, onNode.data.z + 1f);
                    if (up.z <= 4) { isVisited(up, queue, onNode); };
                    Vector3 down = new Vector3(onNode.data.x, onNode.data.y, onNode.data.z - 1f);
                    if (down.z >= 0) { isVisited(down, queue, onNode); };
                }

            }
           // if (queue.Count == 0 && destPoint != ppos) { print("No valid path"); }
            StartCoroutine(showMovement(returnList)); 

                /*
                foreach(Node node in returnList)
                {
                
                player.transform.position = Vector3.Lerp(player.transform.position, node.data,t);
                print("NodeData: " + node.data);
                }
                */
            }
        visited.Clear();
        foreach (GameObject g in blockList)
        {
            visited.Add(g.transform.position + new Vector3(0, .25f, 0));
        }



    }

    IEnumerator showMovement(List<Node> returnList)
    {
        foreach(Node node in returnList)
        {
            player.transform.position = node.data;
            yield return new WaitForSeconds(.5f);
        }
        
    }



    void makeWall(GameObject gameObject)
    {
        Vector3 t = gameObject.transform.position;
        if (gameObject.tag=="Floor")
        {
            GameObject newWall = Instantiate(wall, t+ new Vector3(0,t.y+1f,0), gameObject.transform.rotation);
            visited.Add(newWall.transform.position + new Vector3(0, .25f, 0));
            blockList.Add(newWall);
        }
        else if (gameObject.tag == "Wall")
        {
            blockList.Remove(gameObject);
            visited.Remove(gameObject.transform.position + new Vector3(0, .25f, 0));
            Destroy(gameObject);
        }
    }

    void isVisited(Vector3 checkCoords, Queue<Node> queue, Node onNode)
    {
        if (!visited.Contains(checkCoords))
        {
            Node newNode = new Node(checkCoords, onNode);
            queue.Enqueue(newNode);
            visited.Add(checkCoords);
            print("Visited: " + newNode.data);
        }
    }
    

   
}


 public class Node
    {
        public Vector3 data;
        public Node parent;

        public Node(Vector3 d, Node p=null) { data = d; parent = p; }
    }

class LinkedList
{

    

}
