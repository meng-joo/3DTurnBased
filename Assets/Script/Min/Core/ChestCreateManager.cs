using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCreateManager : MonoBehaviour
{
    public List<Transform> chestTrms;

    [SerializeField] private int chestCnt;

    [SerializeField] private GameObject chestPrefab;

    public List<Animator> chestAnimators;

    public GameObject parentTrm;

    void Start()
    {
        CreateChest();
    }

    void CreateChest()
    {
        List<Transform> tTrms = new List<Transform>();
        for (int i = 0; i < chestTrms.Count; i++)
        {
            tTrms.Add(chestTrms[i]);
        }


        for (int i = 0; i < chestCnt; i++)
        {
            int rand = Random.Range(0, tTrms.Count);
            if (tTrms.Contains(tTrms[rand]))
            {
                GameObject chestObj = Instantiate(chestPrefab, tTrms[rand].position, tTrms[rand].rotation);
                chestObj.transform.SetParent(parentTrm.transform);
                chestObj.name += $"_{i}";
                chestAnimators.Add(chestObj.GetComponentInChildren<Animator>());
                tTrms.Remove(tTrms[rand]);
            }
            else
            {
                continue;
            }
        }
   
    }

}
