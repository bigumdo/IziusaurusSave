using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using YUI.Cores;
using YUI.ObjPooling;

namespace YUI.Agents.Bosses
{
    public class Phantom : Boss 
    {

        private void Start()
        {
            BossManager.Instance.SetBoss(this);
        }

        private void Update()
        {
            if (Keyboard.current.xKey.wasPressedThisFrame)
            {
                BossManager.Instance.StartBT();
            }
        }
    }
}
