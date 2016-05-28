using UnityEngine;
using System.Collections;

public class NpcMove : MonoBehaviour {

    public Transform listOfOption;
    public int currentPoint;

    public bool waiting;
    public float waitingTime;

    private NavMeshAgent navA;

    void Awake()
    {
        navA = GetComponent<NavMeshAgent>();
        currentPoint = Random.Range(0, listOfOption.childCount - 1);
    }

    void Update()
    {
        if (waitingTime <= 0)
        {
            if (Vector3.Distance(transform.position, listOfOption.GetChild(currentPoint).position) > navA.radius + navA.radius/3)
                navA.SetDestination(listOfOption.GetChild(currentPoint).position);
            else if (Random.value > 0.4f)
                currentPoint = Random.Range(0, listOfOption.childCount - 1);
            else waitingTime = Random.Range(1, 10);
        }
        else waitingTime -= Time.deltaTime;
    }
}
