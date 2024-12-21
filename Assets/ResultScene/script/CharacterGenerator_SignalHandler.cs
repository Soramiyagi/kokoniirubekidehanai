using UnityEngine;

public class CharacterGenerator_SignalHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] obj = new GameObject[8];

    public void ActiveObj()
    {
        if(Winner_Save.winnerPlayer == -1)
        {
            return;
        }

        obj[CharacterSelect_Save.characterIndex[Winner_Save.winnerPlayer]].SetActive(true);
    }

    public void DanceStart()
    {
        if (Winner_Save.winnerPlayer == -1)
        {
            return;
        }

        obj[CharacterSelect_Save.characterIndex[Winner_Save.winnerPlayer]].GetComponent<ResultAction>().StartAnima();
    }
}
