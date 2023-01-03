using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TutorialManager : MonoSingleton<TutorialManager>
{
    public bool isTuto = false;
    public bool isInvenClear = false;
    public bool isDragItem = false;
    public bool isCardClick = false;

    public bool isItem;

    public bool isDragCard = false;

    public PlayerDataSO pdSO;

    public MainModule mainModule;

    private void Start()
    {
       
        if (isTuto)
        {
            isDragCard = false;

            Invoke("First", 14f);
        }
        else
        {
            isDragCard = true;

        }

    }
    public void First()
    {
        mainModule.canMove = true;
        pdSO.canBattle = false;
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("The Endless Tower에 오신것을 환영합니다");
        });
        seq.AppendInterval(4f);
        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("앞에 있는 상자를 여세요");
            mainModule.canMove = false;
        });
    }
    private void Update()
    {
        if (isInvenClear && isItem)
        {
            InvenItem();
            isInvenClear = false;
        }
        if (isDragItem && isTuto)
        {
            InvenCard();
            isDragItem = false;
        }
        if (isCardClick && isTuto)
        {
            BattleInfo();
            isCardClick = false;
        }
    }

    public void OpenChestCallBack()
    {
        DialogManager.Instance.ShowText("E 키를 눌러 인벤토리를 열고 닫으세요");
        mainModule.canInven = true;
    }

    public void InvenItem()
    {
        DialogManager.Instance.ShowText("인벤을 열고 아이템을 드래그 해 장착하세요");
        isItem = false;
    }

    public void InvenCard()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("잘하셨어요 칭찬해요!!");
        });
        seq.AppendInterval(2f);
        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("교체할 카드를 클릭해 자신만의 카드 덱을 커스텀하세요");

        });
    }
    public void BattleInfo()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("잘하셨어요 칭찬해요!!");
        });
        seq.AppendInterval(2f);
        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("이제 전투를 하러 떠나볼까요?");
            pdSO.canBattle = true;
        });
        seq.AppendInterval(3f);
        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("맵을 돌아다니다보면 적과 마주하게 됩니다");
            pdSO.canBattle = true;
        });
    }

    public void Battle()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(5f);
        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("주어진 카드를 적에게 드래그하여 공격하세요");
        });
        seq.AppendInterval(6f);
        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("카드 사용에 필요한 코스트는 턴마다 모두 채워집니다");
        });
        seq.AppendInterval(6f);

        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("남은 코스트의 개수는 왼쪽에 보석 모양으로 존재합니다");
        });
        seq.AppendInterval(6f);

        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("더 이상 사용할 수있는 카드가 없거나 코스트가 없으면");
        });
        seq.AppendInterval(6f);

        seq.AppendCallback(() =>
        {
            DialogManager.Instance.ShowText("Turn End 버튼을 눌러 턴을 종료합니다");
            isDragCard = true;
        });
    }
}
