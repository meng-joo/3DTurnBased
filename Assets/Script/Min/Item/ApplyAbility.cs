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

    public static void ChangeAbility(object p, object m)
    {
        ItemAbility[] pitemAbility = p as ItemAbility[];
        ItemAbility[] mitemAbility = m as ItemAbility[];

        int ad = 0;
        int hp = 0;

        if (mitemAbility != null)
        {
            for (int i = 0; i < mitemAbility.Length; i++)
            {
                switch (mitemAbility[i].characterStack)
                {
                    case CharacterStack.Str:
                        PlayerData.Ad -= mitemAbility[i].valStack;
                        ad = mitemAbility[i].valStack;
                        break;
                    case CharacterStack.Hp:
                        PlayerData.Health -= mitemAbility[i].valStack;
                        hp = mitemAbility[i].valStack;
                        break;
                    case CharacterStack.Benefit_Effect:
                        break;
                    case CharacterStack.Detrimental_Effect:
                        break;
                }
            }
        }

        if (pitemAbility != null)
        {
            for (int i = 0; i < pitemAbility.Length; i++)
            {
                switch (pitemAbility[i].characterStack)
                {
                    case CharacterStack.Str:
                        PlayerData.Ad += pitemAbility[i].valStack;
                        ad = pitemAbility[i].valStack;
                        break;
                    case CharacterStack.Hp:
                        PlayerData.Health += pitemAbility[i].valStack;
                        hp = pitemAbility[i].valStack;
                        break;
                    case CharacterStack.Benefit_Effect:
                        break;
                    case CharacterStack.Detrimental_Effect:
                        break;
                }
            }
        }
    }
}
