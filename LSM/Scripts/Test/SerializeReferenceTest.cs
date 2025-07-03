using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YUI.Test
{
    public interface ISkill
    {
        void Use();
    }

    [System.Serializable]
    public class Fireball : ISkill
    {
        public float damage;

        public void Use() => Debug.Log($"Fireball! {damage} damage");
    }

    [System.Serializable]
    public class Heal : ISkill
    {
        public float healAmount;

        public void Use() => Debug.Log($"Heal! {healAmount} HP");
    }

    [System.Serializable]
    public class SkillEntry
    {
        public string className;  // 예: "Fireball", "Heal"

        [SerializeReference]
        public ISkill skill;
    }

    [CreateAssetMenu(fileName = "Test", menuName = "SO/ReferenceTest")]
    public class SerializeReferenceTest : ScriptableObject
    {
        public List<SkillEntry> skillEntries = new();

        private void OnValidate()
        {
            foreach (var entry in skillEntries)
            {
                //Null, 비었거나, 공백이면 true반환
                if (string.IsNullOrWhiteSpace(entry.className))
                    continue;

                // 현재 타입이 일치하지 않으면 새로 생성
                if (entry.skill == null || entry.skill.GetType().Name != entry.className)
                {
                    var type = FindTypeByName(entry.className);
                    // type이 있고 ISkill을 상속하거나 구현했다면
                    if (type != null && typeof(ISkill).IsAssignableFrom(type))
                    {
                        entry.skill = (ISkill)Activator.CreateInstance(type);
                    }
                    else
                    {
                        Debug.LogWarning($"클래스 '{entry.className}' 을(를) 찾을 수 없거나 ISkill을 구현하지 않았습니다.");
                    }
                }
            }
        }

        private Type FindTypeByName(string name)
        {
            //현재 도메인에 로드된 모든 어셈블리를 가져온다.
            //추상 클래스가 아니면 반환 추상클래스는 인스턴스를 만들 수 없기 때문
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.Name == name && !t.IsAbstract);
        }
    }


}
