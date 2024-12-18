using UnityEngine;

public class CharacterGenerator_SignalHandler : MonoBehaviour
{
    [SerializeField] private GameObject GeneratePoint;
    [SerializeField] private GameObject[] obj = new GameObject[8];
    
    //生成するオブジェクトの大きさ
    public Vector3 spawnScale;

    public void OnSignalReceived()
    {
        Debug.Log(Winner_Save.winnerPlayer);

        if (Winner_Save.winnerPlayer == -1)
        {
            Debug.Log(CharacterSelect_Save.characterIndex[0]);
            GenerateModel(0);
        }
    }

    private void GenerateModel(int i)
    {
        // オブジェクトを指定した位置に生成
        GameObject spawnedObject = Instantiate(obj[i], GeneratePoint.transform.position, Quaternion.identity);

        // オブジェクトのスケールを設定
        spawnedObject.transform.localScale = spawnScale;
    }
}
