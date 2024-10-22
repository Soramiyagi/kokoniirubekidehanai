using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] GameObject block, flyStone;

    public int blockWidth, blockHeight;
    public float blockInterval = 0;   //ブロック同士の幅


    // Start is called before the first frame update
    void Start()
    {
        //フィールドのブロックを配置
        for (int i = 0; i < blockWidth; i++)
        {
            for (int j = 0; j < blockHeight; j++)
            {
                Instantiate(block, new Vector3(blockInterval * i, 0, blockInterval * j), Quaternion.identity, this.transform).name = $"Block_{i}_{j}";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

    }

    void FallCheck()
    {
    }
}
