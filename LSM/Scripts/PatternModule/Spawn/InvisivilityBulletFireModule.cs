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
        [Tooltip("스폰이 시작되는 각도 설정")]
        [SerializeField] private float _startAngle;
        [Tooltip("한번 발사하는 총알 간격 설정")]
        [SerializeField] private float _settingAngle;
        [SerializeField] private float _damage;
        [SerializeField] private float _speed;
        [SerializeField] private Vector2 _scale;

        [Space]
        [Header("SpawnSetting")]
        [SerializeField] private float _spawnCnt;
        [Tooltip("Bullet을 보스로부터 얼마나 멀리 소환할지 설정")]
        [SerializeField] private float _spawnDistance;
        [Tooltip("Distance만큼 이동하는데 걸리는 시간 설정")]
        [SerializeField] private float _spawnMoveTime;
        [Tooltip("위치로 이동 후 Fade되는 시간을 설정")]
        [SerializeField] private float _spawnFadeTime;
        [Tooltip("각 소환되는데 걸리는 딜레이 설정")]
        [SerializeField] private float _spawnDelay;


        [Space]
        [Header("ShootingSetting")]
        [Tooltip("처음 발사하는데 걸리는 Delay 설정")]
        [SerializeField] private float _shootStartDelay;
        [Tooltip("처음 발사 이후 발사하는데 걸리는 시간 설정")]
        [SerializeField] private float _shootCooldown;
        [Tooltip("한번에 발사하는 총알 갯수 설정")]
        [SerializeField] private float _shootCnt;

        [Space]
        [Header("InvisivilitySetting")]
        [Tooltip("스폰되고 n초 뒤 투명해지기 시작")]
        [SerializeField] private float _startInvisibilityTime;
        [Tooltip("n초 동안 투명해짐")]
        [SerializeField] private float _invisibilityDuration;
        [Tooltip("플레이어와의 거리가 n 이하일 때 원래대로 돌아오기 시작")]
        [SerializeField] private float _visibilityDistance;
        [Tooltip("n초 동안 원래대로 돌아옴")]
        [SerializeField] private float _visibilityDuration;
        [Tooltip("기본 Alpha")]
        [SerializeField] private float _originAlpha;
        [Tooltip("투면화 상태 Alpha")]
        [SerializeField] private float _InvisivilityAlpha;

        [Space]
        [Header("MoveSetting")]
        [Tooltip("보스가 이동 후 발사를 할지 설정")]
        [SerializeField] private bool _moveBeforeFire;
        [Tooltip("보스가 이동하는데 걸리는 시간 설정")]
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
