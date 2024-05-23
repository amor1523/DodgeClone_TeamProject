using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // 벽에 부딪혔을 때 판정할 레이어
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
            // 벽에서는 충돌한 지점으로부터 약간 앞 쪽에서 발사체를 파괴합니다.
            Vector2 destroyPosition = collision.ClosestPoint(transform.position) - direction * .2f;
            DestroyBullet(destroyPosition, fxOnDestory);
        }
        else if (IsLayerMatched(attackData.target.value, collision.gameObject.layer))
        {
            // 충돌한 지점에서 발사체를 파괴
            DestroyBullet(collision.ClosestPoint(transform.position), fxOnDestory);

            //아래부터 체력 관련코드
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                // 충돌한 오브젝트의 체력을 감소시킵니다.
                healthSystem.ChangeHealth(-(int)attackData.power);
            }
            //여기까지 체력코드

        }
    }

    // 레이어가 일치하는지 확인
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
            // TODO : ParticleSystem에 대해서 배우고, 무기 NameTag로 해당하는 FX가져오기
        }
        gameObject.SetActive(false);
    }
}
