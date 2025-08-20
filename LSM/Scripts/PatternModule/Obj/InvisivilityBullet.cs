using System.Collections;
using UnityEngine;
using YUI.Agents.Bosses;
using YUI.Agents.players;

namespace YUI.PatternModules
{
    public class InvisivilityBullet : BossBaseBullet
    {
        private float _startInvisibilityTime;
        private float _invisibilityDuration;
        private float _visibilityDistance;
        private float _visibilityDuration;
        private float _invisivilityAlphaValue;
        private float _originAlpha;
        private Player _player;
        //private Color _originColor = Color.white;
        //private Color _invisibilityTargetColor = new Color(1, 1, 1, 0);

        public void SetObj(float speed, float damage, Vector2 scale, float startInvisibilityTime, float invisibilityDuration, float visibilityDistance, float visibilityDuration, float originAlpha, float invisivilityAlphaValue)
        {
            _player = PlayerManager.Instance.Player;
            _speed = speed;
            _damage = damage;
            _scale = scale;
            _startInvisibilityTime = startInvisibilityTime;
            _invisibilityDuration = invisibilityDuration;
            _visibilityDistance = visibilityDistance;
            _visibilityDuration = visibilityDuration;
            _originAlpha = originAlpha;
            _invisivilityAlphaValue = invisivilityAlphaValue;
        }

        public override void SetCanMove(bool canMove = false)
        {
            base.SetCanMove(canMove);
            StartCoroutine(Invisivility());
        }

        public IEnumerator Invisivility()
        {
            yield return new WaitForSeconds(_startInvisibilityTime);
            yield return DOFade(_invisivilityAlphaValue, _invisibilityDuration);
            yield return new WaitUntil(() => DistanceCheck());
            yield return DOFade(_originAlpha, _visibilityDuration);
        }

        private bool DistanceCheck()
        {
            if ((_player.transform.position - transform.position).sqrMagnitude <= _visibilityDistance * _visibilityDistance)
                return true;
            else
                return false;
        }
    }
}
