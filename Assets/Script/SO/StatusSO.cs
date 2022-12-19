using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Status  SO")]
public class StatusSO : ScriptableObject
{
    [SerializeField]
    public List<StatusImage> statusImages;
}
