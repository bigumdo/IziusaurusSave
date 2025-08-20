using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YUI.Agents.Bosses;
using YUI.Agents.players;
using YUI.Cores;
using YUI.ObjPooling;

namespace YUI.PatternModules
{
    [CreateAssetMenu(fileName = "InvisivilityBulletFireModule", menuName = "SO/Boss/Module/Spawn/InvisivilityBulletFireModule")]
    public class InvisivilityBulletFireModule : PatternModule
    {
        [Header("BulletSetting")]
        [Tooltip("������ ���۵Ǵ� ���� ����")]
        [SerializeField] private float _startAngle;
        [Tooltip("�ѹ� �߻��ϴ� �Ѿ� ���� ����")]
        [SerializeField] private float _settingAngle;
        [SerializeField] private float _damage;
        [SerializeField] private float _speed;
        [SerializeField] private Vector2 _scale;

        [Space]
        [Header("SpawnSetting")]
        [SerializeField] private float _spawnCnt;
        [Tooltip("Bullet�� �����κ��� �󸶳� �ָ� ��ȯ���� ����")]
        [SerializeField] private float _spawnDistance;
        [Tooltip("Distance��ŭ �̵��ϴµ� �ɸ��� �ð� ����")]
        [SerializeField] private float _spawnMoveTime;
        [Tooltip("��ġ�� �̵� �� Fade�Ǵ� �ð��� ����")]
        [SerializeField] private float _spawnFadeTime;
        [Tooltip("�� ��ȯ�Ǵµ� �ɸ��� ������ ����")]
        [SerializeField] private float _spawnDelay;


        [Space]
        [Header("ShootingSetting")]
        [Tooltip("ó�� �߻��ϴµ� �ɸ��� Delay ����")]
        [SerializeField] private float _shootStartDelay;
        [Tooltip("ó�� �߻� ���� �߻��ϴµ� �ɸ��� �ð� ����")]
        [SerializeField] private float _shootCooldown;
        [Tooltip("�ѹ��� �߻��ϴ� �Ѿ� ���� ����")]
        [SerializeField] private float _shootCnt;

        [Space]
        [Header("InvisivilitySetting")]
        [Tooltip("�����ǰ� n�� �� ���������� ����")]
        [SerializeField] private float _startInvisibilityTime;
        [Tooltip("n�� ���� ��������")]
        [SerializeField] private float _invisibilityDuration;
        [Tooltip("�÷��̾���� �Ÿ��� n ������ �� ������� ���ƿ��� ����")]
        [SerializeField] private float _visibilityDistance;
        [Tooltip("n�� ���� ������� ���ƿ�")]
        [SerializeField] private float _visibilityDuration;
        [Tooltip("�⺻ Alpha")]
        [SerializeField] private float _originAlpha;
        [Tooltip("����ȭ ���� Alpha")]
        [SerializeField] private float _InvisivilityAlpha;

        [Space]
        [Header("MoveSetting")]
        [Tooltip("������ �̵� �� �߻縦 ���� ����")]
        [SerializeField] private bool _moveBeforeFire;
        [Tooltip("������ �̵��ϴµ� �ɸ��� �ð� ����")]
        [SerializeField] private float _moveDuration;

        private const string _bulletName = "BossInvisivilityBullet";

        public override IEnumerator Execute()
        {
            _boss = BossManager.Instance.Boss;
            List<InvisivilityBullet> bullets = new List<InvisivilityBullet>();
            Vector3 playerPos = PlayerManager.Instance.Player.transform.position;
            for (int i = 0; i < _spawnCnt; ++i)
            {
                float angle = -_startAngle + _settingAngle * (i % _shootCnt);
                InvisivilityBullet bullet = PoolingManager.Instance.Pop(_bulletName) as InvisivilityBullet;
                bullet.transform.position = BossManager.Instance.bossTrm.position;
                Vector3 dir = (playerPos - bullet.transform.position).normalized;
                bullet.transform.right = dir;
                bullet.transform.Rotate(new Vector3(0, 0, angle));
                bullet.SetObj(_speed, _damage, _scale, _startInvisibilityTime, _invisibilityDuration, _visibilityDistance, _visibilityDuration, _originAlpha, _InvisivilityAlpha);

                if (i < _shootCnt)
                {
                    yield return bullet.DOMove(_spawnDistance, _spawnMoveTime);
                    SoundManager.Instance.PlaySound("SFX_Boss_BulletFireSpawn");
                    yield return bullet.DOFade(1, _spawnFadeTime);
                }
                else
                {
                    bullet.transform.position += bullet.transform.right * _spawnDistance;
                    bullet.SetAlpha(_originAlpha);
                }
                bullet.transform.parent = _boss.transform;
                bullets.Add(bullet);
                if (_spawnDelay > 0)
                    yield return new WaitForSeconds(_spawnDelay);
            }

            if (_moveBeforeFire)
            {
                CompleteActionExecute();
                yield return new WaitForSeconds(_moveDuration);
            }

            if (_shootStartDelay > 0)
                yield return new WaitForSeconds(_shootStartDelay);

            SoundManager.Instance.PlaySound("SFX_Boss_BulletFireFire");
            for (int i = 0; i < bullets.Count; ++i)
            {
                if (i % _shootCnt == 0 && _shootCnt < _spawnCnt)
                {
                    yield return new WaitForSeconds(_shootCooldown);
                    SoundManager.Instance.PlaySound("SFX_Boss_BulletFireFire");
                }
                bullets[i].SetCanMove(true);
            }

            if (!_moveBeforeFire)
                CompleteActionExecute();
        }
    }
}
