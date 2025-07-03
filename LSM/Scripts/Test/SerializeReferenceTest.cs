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
        public string className;  // ��: "Fireball", "Heal"

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
                //Null, ����ų�, �����̸� true��ȯ
                if (string.IsNullOrWhiteSpace(entry.className))
                    continue;

                // ���� Ÿ���� ��ġ���� ������ ���� ����
                if (entry.skill == null || entry.skill.GetType().Name != entry.className)
                {
                    var type = FindTypeByName(entry.className);
                    // type�� �ְ� ISkill�� ����ϰų� �����ߴٸ�
                    if (type != null && typeof(ISkill).IsAssignableFrom(type))
                    {
                        entry.skill = (ISkill)Activator.CreateInstance(type);
                    }
                    else
                    {
                        Debug.LogWarning($"Ŭ���� '{entry.className}' ��(��) ã�� �� ���ų� ISkill�� �������� �ʾҽ��ϴ�.");
                    }
                }
            }
        }

        private Type FindTypeByName(string name)
        {
            //���� �����ο� �ε�� ��� ������� �����´�.
            //�߻� Ŭ������ �ƴϸ� ��ȯ �߻�Ŭ������ �ν��Ͻ��� ���� �� ���� ����
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.Name == name && !t.IsAbstract);
        }
    }


}
