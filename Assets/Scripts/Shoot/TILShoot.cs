using UnityEngine;


public class TILShoot : MonoBehaviour
{
    private TILController controller;

    //private ObjectPool objectPool;

    [SerializeField] private Transform bulletSpawnPosition;
    protected Vector2 aimDirection = Vector2.up;

    public GameObject CharacterBulletPrefab;

    private void Awake()
    {
        controller = GetComponent<TILController>();
        //objectPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        controller.OnAttackEvent += Onshoot;
        controller.OnLookEvent += OnAim;
    }
    private void OnAim(Vector2 newAimDirection)
    {
        aimDirection = newAimDirection;
    }

    private void Onshoot(AttackSO attackSO)
    {
        AttackSO AttackSO = attackSO as AttackSO;
        float projectilesAngleSpace =AttackSO.multupleBulletAngel;
        int numberOfProjectilesPerShot = AttackSO.numberofBulletshot;

        //중간부터 펼쳐지는게 아니라 minangle부터 커지면서 쏘는 것으로 설계
        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * AttackSO.multupleBulletAngel;
        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            // 그냥 올라가면 재미없으니 랜덤으로 변하는 randomSpread를 넣었어요!
            float randomSpread = Random.Range(-AttackSO.spread, AttackSO.spread);
            angle += randomSpread;
            CreateProjectile(AttackSO, angle);
        }
        //CreateProjectile(attackSO);    
    }

    private void CreateProjectile(AttackSO attackSO, float angle)
    {
        GameObject obj = GameManager.Instance.ObjectPool.SpawnFromPool(attackSO.bulletNameTag);
        obj.transform.position = bulletSpawnPosition.position;
        BulletController attackController = obj.GetComponent<BulletController>();

        Vector3 rotatevector = Quaternion.AngleAxis(angle, Vector3.forward)*aimDirection;
        attackController.InitializeAttack(rotatevector, attackSO);
    }

    private static Vector2 RotateVector2(Vector2 v, float angle)
    {
        return Quaternion.Euler(0f, 0f, 0f) * v;
    }
}
