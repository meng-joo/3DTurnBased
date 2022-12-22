using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class InputModule : MonoBehaviour
{
    MainModule mainModule;
    private void Start()
    {
        mainModule = GetComponent<MainModule>();
    }

    private void Update()
    {
        InputMove();
        InputUI();
    }

    void InputMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool isRun = Input.GetKey(KeyCode.LeftShift);

        Vector3 direc = Vector3.zero;

        Vector3 forward = mainModule.playerCam.transform.TransformDirection(Vector3.forward);

        forward.y = 0f;
        Vector3 right = new Vector3(forward.z, 0f, -forward.x);

        direc = (forward * z + right * x).normalized;//.normalized; // ¹æÇâ

        mainModule.MovePlayer(direc, isRun);
        //RotateBody(_amount);
    }

    void InputUI()
    {
        if (Input.GetKeyDown($"{mainModule._UIModule.KeyName}") && mainModule._UIModule.canInteration)
        {
            StartCoroutine(mainModule._UIModule.FuncName);
        }

        if(Input.GetKeyDown(KeyCode.Escape) && mainModule._UIModule._uiManager.isSignUp)
        {
            UndoSignImage();
        }
    }

    public void UndoSignImage()
    {
        mainModule.canMove = false;
        mainModule._UIModule._uiManager.SignUIOff();
    }

    IEnumerator SignUI()
    {
        mainModule.canMove = true;
        mainModule._UIModule._uiManager.SignUIOn();
        yield return null;
    }

    IEnumerator ExitDoor()
    {
        if (mainModule.stageClear)
        {
       AudioManager.PlayAudio( UISoundManager.Instance.data.sanhojacyoung);


            mainModule.canMove = true;
            mainModule.MapManager.StartInit(mainModule.playerDataSO.stage + 1);
        }
        else
        {

        }
        yield return null;
    }

    IEnumerator ChestUI()
    {
        if (mainModule.isTrophy)
        {
            yield break;
        }
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            AudioManager.PlayAudio(UISoundManager.Instance.data.sanhojacyoung);

            mainModule.canInven = false;
            mainModule.chestCreateManager.chestAnimators[mainModule.physicsModule.index].Play("Open");
            mainModule.isTrophy = true; 
            mainModule.canMove = true;
            mainModule.canRelic = true;

            mainModule.trophyUI.bagBtn.interactable = false;
            mainModule.trophyUI.settingBtn.interactable = false;
        });
        seq.AppendInterval(1.35f);
        //seq.AppendCallback(() =>
        //{
        //    mainModule.ChestOpenParticle.Play();
        //});

        seq.AppendInterval(2f);
        seq.AppendCallback(() =>
        {
            mainModule.chestCreateManager.chestAnimators[mainModule.physicsModule.index].GetComponentInParent<DestroyCheast>().Des();
            //
            mainModule._UIModule.OnInteractionKeyImage(false, "", "f", "");
            // mainModule.ChestOpenParticle.Stop();
            mainModule.canRelic = true;
            mainModule._UIModule.TrophyUIManager.AppearTrophy();
        });

        yield return null;

    }

}
