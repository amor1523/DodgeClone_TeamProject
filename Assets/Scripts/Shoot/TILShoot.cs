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

        //�߰����� �������°� �ƴ϶� minangle���� Ŀ���鼭 ��� ������ ����
        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * AttackSO.multupleBulletAngel;
        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            // �׳� �ö󰡸� ��̾����� �������� ���ϴ� randomSpread�� �־����!
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
