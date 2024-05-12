using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMesh : MonoBehaviour
{
    public Animator playerAnim;
    public float velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        velocity = GetComponent<NavMeshAgent>().velocity.magnitude;
        playerAnim.SetFloat("velocity", velocity);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                GetComponent<UnityEngine.AI.NavMeshAgent>().destination = hit.point;
            }
        }
    }
}
