using UnityEngine;

public class CharacterGenerator_SignalHandler : MonoBehaviour
{
    [SerializeField] private GameObject GeneratePoint;
    [SerializeField] private GameObject[] obj = new GameObject[8];
    
    //��������I�u�W�F�N�g�̑傫��
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
        // �I�u�W�F�N�g���w�肵���ʒu�ɐ���
        GameObject spawnedObject = Instantiate(obj[i], GeneratePoint.transform.position, Quaternion.identity);

        // �I�u�W�F�N�g�̃X�P�[����ݒ�
        spawnedObject.transform.localScale = spawnScale;
    }
}
