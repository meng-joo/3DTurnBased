using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveModule : MonoBehaviour
{
    private NavMeshAgent _agent;
    private MainModule mainModule;

    public float moveTime;

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
        _agent.Move(movePos);

        if (movePos != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direc), 7 * Time.deltaTime);
            if (mainModule.playerDataSO.canBattle) moveTime += Time.deltaTime;
        }
    }
}
