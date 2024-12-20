using UnityEngine;

public class CharacterGenerator_SignalHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] obj = new GameObject[8];

    //生成するオブジェクトの大きさ
    public Vector3 spawnScale;

    public void ActiveObj()
    {
        obj[CharacterSelect_Save.characterIndex[Winner_Save.winnerPlayer]].SetActive(true);
    }

    public void DanceStart()
    {
        obj[CharacterSelect_Save.characterIndex[Winner_Save.winnerPlayer]].GetComponent<ResultAction>().StartAnima();
    }
}
