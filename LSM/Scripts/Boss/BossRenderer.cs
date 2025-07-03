using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace YUI.Agents.Bosses
{
    public class BossRenderer : AgentRenderer
    {
        public override void Initialize(Agent agent)
        {
            base.Initialize(agent);
            SpriteRenderer.material.SetFloat("_IsCounter", 0);
            SpriteRenderer.material.SetFloat("_BossIntensity", 0);
            SpriteRenderer.material.SetFloat("_OutLineTest", 0);
        }

        public IEnumerator CounterShader(float activeTime)
        {
            float time = 0;
            SpriteRenderer.material.SetFloat("_IsCounter", 1);
            SpriteRenderer.material.SetFloat("_BossIntensity",0);
            SpriteRenderer.material.SetFloat("_OutLineTest",0);
            while (time <= activeTime)
            {
                time += Time.deltaTime;

                SpriteRenderer.material.SetFloat("_BossIntensity", Mathf.Lerp(0,5,time / activeTime));
                SpriteRenderer.material.SetFloat("_OutLineTest", Mathf.Lerp(0, 1, time / activeTime));
                yield return null;
            }
        }

        public IEnumerator CounterOff()
        {
            float time = 0;

            while (time <= 0.1f)
            {
                time += Time.deltaTime;

                SpriteRenderer.material.SetFloat("_BossIntensity", Mathf.Lerp(0, 5, time / 0.1f));
                SpriteRenderer.material.SetFloat("_OutLineTest", Mathf.Lerp(0, 1, time / 0.1f));
                yield return null;
            }
            SpriteRenderer.material.SetFloat("_IsCounter", 0);
        }

    }
}
