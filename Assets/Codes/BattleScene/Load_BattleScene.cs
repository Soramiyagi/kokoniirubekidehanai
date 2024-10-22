using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_BattleScene : MonoBehaviour
{
    [SerializeField] private GameObject[] obj;
    [SerializeField] private int maxNum;    //総キャラクター数

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxNum; i++)
        {
            if (CharacterSelect_Save.P1_C == i)
            {
                Instantiate(obj[i], new Vector3(1, 2, 19), Quaternion.identity);
            }
            if (CharacterSelect_Save.P2_C == i)
            {
                Instantiate(obj[i], new Vector3(19, 2, 19), Quaternion.identity);
            }
            if (CharacterSelect_Save.P3_C == i)
            {
                Instantiate(obj[i], new Vector3(1, 2, 1), Quaternion.identity);
            }
            if (CharacterSelect_Save.P4_C == i)
            {
                Instantiate(obj[i], new Vector3(19, 2, 1), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
