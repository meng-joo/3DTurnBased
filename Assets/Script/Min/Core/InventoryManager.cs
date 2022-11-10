using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public bool isInven;
    public Image Inven;


    [SerializeField] private GameObject skillUI;
    [SerializeField] private GameObject itemUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Inven.gameObject.SetActive(true);

        }
    }

    public void OnSkillUI()
    {
    }

    public void OnItemUI()
    {

    }
}
