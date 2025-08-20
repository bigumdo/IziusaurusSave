using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YUI.Agents.Bosses;
using YUI.Agents.players;

namespace YUI.PatternModules
{
    [CreateAssetMenu(fileName = "SetCounterDirModule", menuName = "SO/Boss/Module/Counter/SetCounterDirModule")]
    public class SetCounterDirModule : PatternModule
    {
        public override IEnumerator Execute()
        {
            _boss = BossManager.Instance.Boss;

            bool hasWallUp = Physics2D.Raycast(_boss.transform.position, Vector2.up, _boss.Collider.size.y * 2, 1 << LayerMask.NameToLayer("Wall"));
            bool hasWallDown = Physics2D.Raycast(_boss.transform.position, Vector2.down, _boss.Collider.size.y * 2, 1 << LayerMask.NameToLayer("Wall"));
            bool hasWallLeft = Physics2D.Raycast(_boss.transform.position, Vector2.right, _boss.Collider.size.x * 2, 1 << LayerMask.NameToLayer("Wall"));
            bool hasWallRight = Physics2D.Raycast(_boss.transform.position, Vector2.left, _boss.Collider.size.x * 2, 1 << LayerMask.NameToLayer("Wall"));

            var possibleDirs = new List<Vector2>();
            if (!hasWallUp) possibleDirs.Add(Vector2.up);
            if (!hasWallDown) possibleDirs.Add(Vector2.down);
            if (!hasWallLeft) possibleDirs.Add(Vector2.left);
            if (!hasWallRight) possibleDirs.Add(Vector2.right);

            Vector2 randomDir = possibleDirs[Random.Range(0, possibleDirs.Count)];
            CompleteActionExecute();
            yield break;
        }
    }
}
