using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveModule : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent _agent;
    private MainModule mainModule;

    private void Awake()
    {
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        mainModule = GetComponent<MainModule>();

        _agent.updatePosition = true;
        _agent.updateRotation = true;
    }

    public void MovePlayer(Vector3 direc, float speed)
    {
        Vector3 movePos = direc * speed * Time.deltaTime;
        UnityEngine.AI.NavMeshHit hit;
        bool hitted = UnityEngine.AI.NavMesh.Raycast(transform.position, movePos, out hit, _agent.areaMask);
        //if (hitted)
        //    transform.position = hit.position;

        //else
        //{
        _agent.Move(movePos);
        //}


        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direc), 7 * Time.deltaTime);
        //_characterController.Move(direc * playerDataSO.speed * Time.deltaTime);
    }
}
