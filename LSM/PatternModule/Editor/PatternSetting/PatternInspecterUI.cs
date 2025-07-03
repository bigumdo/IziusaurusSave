using System;
using UnityEngine.UIElements;

namespace YUI.PatternModules.Editor
{
    public class PatternInspecterUI 
    {
        private VisualElement _rootElement;
        private TextField _nameField;
        private ScrollView _patternStepView;
        private Button _addBtn;
        private Button _deleteBtn;
        //public event Action<PatternInspecterUI> OnDeleteEvent;
        //public event Action<PatternInspecterUI> OnAddEvent;


        public string Name
        {
            get => _nameField.value;
            set
            {
                patternSO.patternName = value;
                _nameField.value = value;
            }
        }
        public PatternSO patternSO;

        public PatternInspecterUI(VisualElement root, PatternSO item)
        {
            _rootElement = root.Q("Container");
            patternSO = item;
            _nameField = _rootElement.Q<TextField>("PatternNameField");
            _deleteBtn = _rootElement.Q<Button>("DelBtn");
            _addBtn = _rootElement.Q<Button>("AddBtn");

            _nameField.RegisterValueChangedCallback(evt =>
            {
                Name = evt.newValue;
            });
        }
    }
}
