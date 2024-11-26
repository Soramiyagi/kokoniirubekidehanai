using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Load_BattleScene : MonoBehaviour
{
    [SerializeField] private GameObject[] obj;
    [SerializeField] private BattleCamera battleCamera;

    private GameObject[] targetObj = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < obj.Length; i++)
        {
            if (CharacterSelect_Save.characterIndex[0] == i)
            {
                PlayerInput instantiatedObject = PlayerInput.Instantiate(prefab: obj[i], pairWithDevice: CharacterSelect_Save.joinedDevices[0]);
                instantiatedObject.transform.position = new Vector3(1, 2, 19);
                targetObj[0] = instantiatedObject.gameObject;
            }
            if (CharacterSelect_Save.characterIndex[1] == i)
            {
                PlayerInput instantiatedObject = PlayerInput.Instantiate(prefab: obj[i], pairWithDevice: CharacterSelect_Save.joinedDevices[1]);
                instantiatedObject.transform.position = new Vector3(19, 2, 19);
                targetObj[1] = instantiatedObject.gameObject;
            }
            if (CharacterSelect_Save.characterIndex[2] == i)
            {
                PlayerInput instantiatedObject = PlayerInput.Instantiate(prefab: obj[i], pairWithDevice: CharacterSelect_Save.joinedDevices[2]);
                instantiatedObject.transform.position = new Vector3(1, 2, 1);
                targetObj[2] = instantiatedObject.gameObject;
            }
            if (CharacterSelect_Save.characterIndex[3] == i)
            {
                PlayerInput instantiatedObject = PlayerInput.Instantiate(prefab: obj[i], pairWithDevice: CharacterSelect_Save.joinedDevices[3]);
                instantiatedObject.transform.position = new Vector3(19, 2, 1);
                targetObj[3] = instantiatedObject.gameObject;
            }
        }

        battleCamera.FirstSet(targetObj);
    }
}
