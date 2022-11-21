using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyAbility : MonoBehaviour
{
    public static PlayerDataSO PlayerData; //;;

    private void Awake()
    {
        PlayerData = AddressableManager.Instance.GetResource<PlayerDataSO>("Assets/SO/Player/PlayerDataSO.asset");
    }

    public static void ChangeAbility(object plus)
    {
        ItemAbility[] itemAbility = plus as ItemAbility[];

        //for (int i = 0; i < itemAbility.Length; i++)
        //{
        //    switch (itemAbility[i].characterStack)
        //    {
        //        case CharacterStack.Str:
        //            PlayerData.Ad -= itemAbility[i].valStack;
        //            break;
        //        case CharacterStack.Hp:
        //            PlayerData.Health -= itemAbility[i].valStack;
        //            break;
        //        case CharacterStack.Benefit_Effect:
        //            break;
        //        case CharacterStack.Detrimental_Effect:
        //            break;
        //    }
        //} 

        for (int i = 0; i < itemAbility.Length; i++)
        {
            switch (itemAbility[i].characterStack)
            {
                case CharacterStack.Str:
                    PlayerData.Ad += itemAbility[i].valStack;
                    break;
                case CharacterStack.Hp:
                    PlayerData.Health += itemAbility[i].valStack;
                    break;
                case CharacterStack.Benefit_Effect:
                    break;
                case CharacterStack.Detrimental_Effect:
                    break;
            }
        }

        //���� �����ۿ� ������ ����ؼ� ������ ���������


        
    }


}