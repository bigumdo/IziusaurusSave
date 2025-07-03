using System;
using UnityEngine.UIElements;

namespace YUI.PatternModules.Editor
{
    public class BossUI
    {
        private Label _nameLabel;
        private Button _deleteBtn;
        private Button _addBtn;
        private VisualElement _rootElement;
        public event Action<BossUI> OnDeleteEvent;
        public event Action<BossUI> OnSelectEvent;
        public event Action<BossUI> OnAddEvent;
        public string Name
        {
            get => _nameLabel.text;
            set
            {
                _nameLabel.text = value;
            }
        }
        public BossPatternDataSO poolingItem;
        public bool IsActive
        {
            get => _rootElement.ClassListContains("active");
            set
            {
                if (value)
                {
                    _rootElement.AddToClassList("active");
                }
                else
                {
                    _rootElement.RemoveFromClassList("active");
                }
            }
        }

        public BossUI(VisualElement root, BossPatternDataSO item)
        {
            poolingItem = item;
            _rootElement = root.Q("Boss");
            _nameLabel = _rootElement.Q<Label>("BossName");
            _deleteBtn = _rootElement.Q<Button>("DeleteBtn");
            _addBtn = _rootElement.Q<Button>("AddBtn");

            _addBtn.RegisterCallback<ClickEvent>(evt => {
                OnAddEvent?.Invoke(this);
                evt.StopPropagation(); //Stop event propagation to parent
            });
            _deleteBtn.RegisterCallback<ClickEvent>(evt => {
                OnDeleteEvent?.Invoke(this);
                evt.StopPropagation(); //Stop event propagation to parent
            });
            _rootElement.RegisterCallback<ClickEvent>(evt => {
                OnSelectEvent?.Invoke(this);
                evt.StopPropagation(); //Stop event propagation to parent
            });
        }
    }
}
