using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    private float time;         //�u���b�N��������܂ł̎���
    private float scale;

    public float timeSet = 0;
    private BoxCollider boxCol;

    private bool revival = true;

    //�}�e���A���֘A
    [SerializeField] private Material safety, caution, danger;
    new Renderer renderer;

    private Coroutine countdownCoroutine; // �R���[�`���̎Q��

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        boxCol = this.GetComponent<BoxCollider>();
        StateReset();
    }

    void StateReset()
    {
        time = timeSet;
        scale = 1;
        this.transform.localScale = new Vector3(scale, scale, scale);
        boxCol.isTrigger = false;
        renderer.material = safety;

        // �R���[�`�������s���ł���Β�~
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null; // �R���[�`���̎Q�Ƃ��N���A
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Player�����ɐڐG����ƈ�莞�Ԍo�ߌ�ɏ���������
        {
            if (countdownCoroutine == null) // �R���[�`�������s���łȂ��ꍇ�̂݊J�n
            {
                countdownCoroutine = StartCoroutine(Countdown()); // �R���[�`�����J�n
                renderer.material = caution;
            }
        }
        else if (collision.gameObject.CompareTag("Break")) // Break�̃R���W�����ڐG���N�����u�Ԃɏ���������
        {
            countdownCoroutine = StartCoroutine(Countdown()); // �R���[�`�����J�n
            time = 0;
            renderer.material = danger;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Player�����ɐڐG����ƈ�莞�Ԍo�ߌ�ɏ���������
        {
            if (countdownCoroutine == null) // �R���[�`�������s���łȂ��ꍇ�̂݊J�n
            {
                countdownCoroutine = StartCoroutine(Countdown()); // �R���[�`�����J�n
                renderer.material = caution;
            }
        }
        else if (collision.gameObject.CompareTag("Break")) // Break�̃R���W�����ڐG���N�����u�Ԃɏ���������
        {
            countdownCoroutine = StartCoroutine(Countdown()); // �R���[�`�����J�n
            time = 0;
            renderer.material = danger;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("CountStart"))
        {
            if (countdownCoroutine == null) // �R���[�`�������s���łȂ��ꍇ�̂݊J�n
            {
                countdownCoroutine = StartCoroutine(Countdown()); // �R���[�`�����J�n
                renderer.material = caution;
            }
        }
        else if (other.gameObject.CompareTag("Break"))
        {
            countdownCoroutine = StartCoroutine(Countdown()); // �R���[�`�����J�n
            time = 0;
            renderer.material = danger;
        }
        else if (other.gameObject.CompareTag("Fix"))
        {
            if (revival == true)
            {
                StateReset();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("CautionArea"))
        {
            if (revival == true)
            {
                revival = false;

                countdownCoroutine = StartCoroutine(Countdown()); // �R���[�`�����J�n
                time = 0;
                renderer.material = danger;
            }
        }
    }

    private IEnumerator Countdown()
    {
        while (time >= 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }

        renderer.material = danger;

        while (scale > 0)
        {
            scale -= Time.deltaTime * 0.75f;

            if (scale <= 0)
            {
                scale = 0;
                boxCol.isTrigger = true;
            }

            this.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }

        // �R���[�`���I�����ɎQ�Ƃ��N���A
        countdownCoroutine = null;
    }
}
