using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCreateManager : MonoBehaviour
{
    [SerializeField] private List<Transform> chestTrms;

    [SerializeField] private int chestCnt;

    [SerializeField] private GameObject chest;
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
                Instantiate(chest, tTrms[rand].position, Quaternion.identity);
                tTrms.Remove(tTrms[rand]);
            }
            else
            {
                continue;
            }
        }
   
    }

}
