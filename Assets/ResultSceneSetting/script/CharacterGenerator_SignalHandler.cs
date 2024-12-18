using UnityEngine;

public class CharacterGenerator_SignalHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] obj = new GameObject[8];
    
    //生成するオブジェクトの大きさ
    public Vector3 spawnScale;

    public void ActiveObj()
    {
        Debug.Log(Winner_Save.winnerPlayer);

        if (Winner_Save.winnerPlayer == -1)
        {
            Debug.Log(CharacterSelect_Save.characterIndex[0]);
            obj[0].SetActive(true);
        }
    }

    public void DanceStart()
    {
        obj[0].GetComponent<ResultAction>().StartAnima();
    }
}
