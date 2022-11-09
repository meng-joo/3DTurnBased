using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairCol : MonoBehaviour
{
    [SerializeField] int _floor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (_floor)
            {
                case 1:
                    {
                        other.GetComponent<MainModule>().vCam1.Priority = 10;//.Cam
                        other.GetComponent<MainModule>().vCam2.Priority = 5;//.Cam
                        other.GetComponent<MainModule>().vCam3.Priority = 5;//.Cam
                        break;
                    }
                case 2:
                    {
                        other.GetComponent<MainModule>().vCam1.Priority = 5;//.Cam
                        other.GetComponent<MainModule>().vCam2.Priority = 10;//.Cam
                        other.GetComponent<MainModule>().vCam3.Priority = 5;//.Cam
                        break;
                    }
                case 3:
                    {
                        other.GetComponent<MainModule>().vCam1.Priority = 5;//.Cam
                        other.GetComponent<MainModule>().vCam2.Priority = 5;//.Cam
                        other.GetComponent<MainModule>().vCam3.Priority = 10;//.Cam
                        break;
                    }
            }
        }
    }
}