using UnityEngine;

public class PathMovement : MonoBehaviour
{
    public PathManager path;
    public float speed = 3f;
    public int startingIndex;
    private int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startingIndex == -1 ? path.GetPosition(path.points.Length-1) : path.GetPosition(startingIndex);
        transform.LookAt(path.points[1]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveForward();
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveBackwards();
        }
    }

    void MoveForward()
    {
        if (Vector3.Distance(transform.position, path.GetPosition(currentIndex+1)) < Mathf.Epsilon)
        {
            if (currentIndex+1 >= path.points.Length-1) return;
            currentIndex++;
        }
        transform.position = Vector3.MoveTowards(transform.position, path.GetPosition(currentIndex+1), Time.deltaTime * speed);
        transform.LookAt(path.GetPosition(currentIndex+1));
    }

    void MoveBackwards()
    {
        if (Vector3.Distance(transform.position, path.GetPosition(currentIndex)) < Mathf.Epsilon)
        {
            if (currentIndex == 0) return;
            currentIndex--;
        }
        transform.position = Vector3.MoveTowards(transform.position, path.GetPosition(currentIndex), Time.deltaTime * speed);
        transform.LookAt(path.GetPosition(currentIndex));
    }
}
