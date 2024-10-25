using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Load_BattleScene : MonoBehaviour
{
    [SerializeField] private GameObject[] obj;
    [SerializeField] private int maxNum;    //総キャラクター数

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxNum; i++)
        {
            if (CharacterSelect_Save.characterIndex[0] == i)
            {
                Debug.Log(CharacterSelect_Save.joinedDevices[0]);
                PlayerInput.Instantiate(prefab: obj[i], pairWithDevice: CharacterSelect_Save.joinedDevices[0]).transform.position = new Vector3(1, 2, 19); ;

                //Instantiate(obj[i], new Vector3(1, 2, 19), Quaternion.identity);
            }
            if (CharacterSelect_Save.characterIndex[1] == i)
            {
                Debug.Log(CharacterSelect_Save.joinedDevices[1]);
                PlayerInput.Instantiate(prefab: obj[i], pairWithDevice: CharacterSelect_Save.joinedDevices[1]).transform.position = new Vector3(19, 2, 19); ;

                //Instantiate(obj[i], new Vector3(19, 2, 19), Quaternion.identity);
            }
            if (CharacterSelect_Save.characterIndex[2] == i)
            {
                PlayerInput.Instantiate(prefab: obj[i], pairWithDevice: CharacterSelect_Save.joinedDevices[2]).transform.position = new Vector3(1, 2, 1); ;

                //Instantiate(obj[i], new Vector3(1, 2, 1), Quaternion.identity);
            }
            if (CharacterSelect_Save.characterIndex[3] == i)
            {
                PlayerInput.Instantiate(prefab: obj[i], pairWithDevice: CharacterSelect_Save.joinedDevices[3]).transform.position = new Vector3(19, 2, 1); ;

                //Instantiate(obj[i], new Vector3(19, 2, 1), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
