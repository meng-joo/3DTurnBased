using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RoomPlayerMove : MonoBehaviour
{
    Vector3 moveDir = Vector3.zero;

    private NavMeshAgent _nav;

    [SerializeField] private float speed = 5f;

    [SerializeField] private float rotationSpd = 720f;
    void Start()
    {
        _nav = GetComponent<NavMeshAgent>();    
    }
    void Update()
    {
        Move();
    }
    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical"); 

        moveDir = new Vector3(h, 0, v).normalized * speed * Time.deltaTime;

        _nav.Move(moveDir);

        if (moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * rotationSpd);
        }
    }
}
