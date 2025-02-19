using UnityEngine;

public class CharacterGeneratorSignalHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] obj = new GameObject[8];

    public void ActiveObj()
    {
        if(WinnerSave.winnerPlayer == -1)
        {
            return;
        }

        obj[CharacterSelectSave.characterIndex[WinnerSave.winnerPlayer]].SetActive(true);
    }

    public void DanceStart()
    {
        if (WinnerSave.winnerPlayer == -1)
        {
            return;
        }

        obj[CharacterSelectSave.characterIndex[WinnerSave.winnerPlayer]].GetComponent<ResultAction>().StartAnima();
    }
}
