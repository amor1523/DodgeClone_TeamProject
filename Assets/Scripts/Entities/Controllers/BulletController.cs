using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // ���� �ε����� �� ������ ���̾�
    [SerializeField] private LayerMask levelCollisionLayer;

    private AttackSO attackData;
    private float currentDuration;
    private Vector2 direction;
    private bool isReady;

    private Rigidbody2D rigidbody;
    private TrailRenderer trailRenderer;

    public bool fxOnDestory = true;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (!isReady)
        {
            return;
        }

        currentDuration += Time.deltaTime;

        if (currentDuration > attackData.duration)
        {
            DestroyBullet(transform.position, false);
        }

        rigidbody.velocity = direction * attackData.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsLayerMatched(levelCollisionLayer.value, collision.gameObject.layer))
        {
            // �������� �浹�� �������κ��� �ణ �� �ʿ��� �߻�ü�� �ı��մϴ�.
            Vector2 destroyPosition = collision.ClosestPoint(transform.position) - direction * .2f;
            DestroyBullet(destroyPosition, fxOnDestory);
        }
        else if (IsLayerMatched(attackData.target.value, collision.gameObject.layer))
        {
            // �浹�� �������� �߻�ü�� �ı�
            DestroyBullet(collision.ClosestPoint(transform.position), fxOnDestory);

            //�Ʒ����� ü�� �����ڵ�
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                // �浹�� ������Ʈ�� ü���� ���ҽ�ŵ�ϴ�.
                healthSystem.ChangeHealth(-(int)attackData.power);
            }
            //������� ü���ڵ�

        }
    }

    // ���̾ ��ġ�ϴ��� Ȯ��
    private bool IsLayerMatched(int layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }

    public void InitializeAttack(Vector2 direction, AttackSO attackData)
    {
        this.attackData = attackData;
        this.direction = direction;

        trailRenderer.Clear();
        currentDuration = 0;

        transform.up = this.direction;

        isReady = true;
    }

    private void DestroyBullet(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            // TODO : ParticleSystem�� ���ؼ� ����, ���� NameTag�� �ش��ϴ� FX��������
        }
        gameObject.SetActive(false);
    }
}
