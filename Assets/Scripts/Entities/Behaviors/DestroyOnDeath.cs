using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Rigidbody2D rigidbody;
    private GameManager gameManager;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        rigidbody = GetComponent<Rigidbody2D>();
        // ���� ���� ��ü�� healthSystem��
        healthSystem.OnDeath += OnDeath;
        gameManager = FindObjectOfType<GameManager>();


    }

    void OnDeath()
    {
        // ���ߵ��� ����
        rigidbody.velocity = Vector3.zero;

        // �ణ �������� �������� ����
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        // ȿ�����߻�
        if (gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Destroy ȿ�����߻�");
            SoundManager.Sound.Play("Explode.mp3", SoundType.Effect, 0.2f);
        }

        // ��ũ��Ʈ ���̻� �۵� ���ϵ��� ��
        //foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        //{
        //    component.enabled = false;
        //}


        if (gameObject.CompareTag("Player"))
        {
            Debug.Log("��");
            gameManager.PlayerDeath();

        }
        // 2�ʵڿ� �ı�
        Invoke("SetDisable", 1.2f);
    }
    void SetDisable()
    {
        gameObject.SetActive(false);
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 1f;
            renderer.color = color;
        }
    }
}