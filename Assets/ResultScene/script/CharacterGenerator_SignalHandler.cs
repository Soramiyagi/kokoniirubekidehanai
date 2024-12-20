using UnityEngine;

public class CharacterGenerator_SignalHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] obj = new GameObject[8];

    //��������I�u�W�F�N�g�̑傫��
    public Vector3 spawnScale;

    public void ActiveObj()
    {
        obj[1].SetActive(true);
        
        //obj[CharacterSelect_Save.characterIndex[Winner_Save.winnerPlayer]].SetActive(true);
    }

    public void DanceStart()
    {
        obj[1].GetComponent<ResultAction>().StartAnima();

        //obj[CharacterSelect_Save.characterIndex[Winner_Save.winnerPlayer]].GetComponent<ResultAction>().StartAnima();
    }
}
