using System;
using UnityEngine.UIElements;

namespace YUI.PatternModules.Editor
{
    public class PatternUI
    {
        private Label _nameLabel;
        private Button _deleteBtn;
        private VisualElement _rootElement;
        public event Action<PatternUI> OnDeleteEvent;
        public event Action<PatternUI> OnSelectEvent;
        public string Name
        {
            get => _nameLabel.text;
            set
            {
                _nameLabel.text = value;
            }
        }
        public PatternSO poolingItem;
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

        public PatternUI(VisualElement root, PatternSO item)
        {
            poolingItem = item;
            _rootElement = root.Q("Pattern");
            _nameLabel = _rootElement.Q<Label>("PatternName");
            _deleteBtn = _rootElement.Q<Button>("DeleteBtn");
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
