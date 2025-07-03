using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace YUI.PatternModules.Editor
{
    public class PatternManagerEditor : EditorWindow
    {
        //[SerializeField] private VisualTreeAsset visualAsset = default;
        //[SerializeField] private BossListSO bossList= default;
        //[SerializeField] private BossPatternDataSO patternList = default;
        //[SerializeField] private VisualTreeAsset patternAsset = default; 
        //[SerializeField] private VisualTreeAsset bossAsset = default;
        //[SerializeField] private VisualTreeAsset patternInspecterAsset = default;

        //private Button _createBtn;

        //private ScrollView _bossView;
        //private ScrollView _patternListView;
        //private VisualElement _patternView;
        //private VisualElement _moduleView;

        //private List<BossUI> _bossList;
        //private BossUI _currentBoss;

        //private List<PatternUI> _patternList;
        //private PatternUI _currentPattern;

        //private PatternInspecterUI _currentPatterInspecter;

        //private UnityEditor.Editor _cachedEditor;

        //private string _rootFolder = "Assets/Pattern";

        //[MenuItem("Tools/PoolManager")]
        //public static void ShowWindow()
        //{
        //    PatternManagerEditor wnd = GetWindow<PatternManagerEditor>();
        //    wnd.titleContent = new GUIContent("Pattern");
        //}

        //public void CreateGUI()
        //{
        //    VisualElement root = rootVisualElement;
        //    visualAsset.CloneTree(root);

        //    InitializeItems(root);
        //    GeneratePoolingItemUI();
        //}
        //private void InitializeItems(VisualElement root)
        //{
        //    _createBtn = root.Q<Button>("CreateBtn");
        //    _createBtn.clicked += HandleCreateItem;
        //    _bossView = root.Q<ScrollView>("BossView");
        //    _bossList = new List<BossUI>();
        //    _patternListView = root.Q<ScrollView>("PatternListView");
        //    _patternList = new List<PatternUI>();
        //    _patternView = root.Q<VisualElement>("PatternView");
        //    _moduleView = root.Q<VisualElement>("ModuleView");
        //}
        //private void GeneratePoolingItemUI()
        //{
        //    _bossView.Clear();
        //    _patternListView.Clear();
        //    _patternView.Clear();
        //    _moduleView.Clear();

        //    if (bossList == null)
        //    {
        //        string filePath = $"{_rootFolder}/BossList.asset";
        //        bossList = AssetDatabase.LoadAssetAtPath<BossListSO>(filePath);
        //        if (bossList == null)
        //        {
        //            Debug.LogWarning("pool manager so is not exist, create new one");
        //            bossList = ScriptableObject.CreateInstance<BossListSO>();
        //            AssetDatabase.CreateAsset(bossList, filePath);
        //        }
        //    }

        //    foreach (BossPatternDataSO pattern in bossList.bosses)
        //    {
        //        TemplateContainer itemUI = bossAsset.Instantiate();
        //        BossUI poolItem = new BossUI(itemUI, pattern);
        //        _bossView.Add(itemUI); //스크롤뷰에 넣고 리스트 관리
        //        _bossList.Add(poolItem);
        //        poolItem.Name = pattern.bossName;

        //        poolItem.OnSelectEvent += HandleBossSelectionEvent;
        //        poolItem.OnDeleteEvent += HandleBossDeleteEvent;
        //        poolItem.OnAddEvent += HandleBossAddEvent;
        //    }

        //}

        //private void HandleCreateItem()
        //{
        //    Guid itemGUID = Guid.NewGuid();
        //    BossPatternDataSO newBoss = ScriptableObject.CreateInstance<BossPatternDataSO>();
        //    newBoss.bossName = itemGUID.ToString();

        //    if (Directory.Exists($"{_rootFolder}/patterns") == false)
        //    {
        //        Directory.CreateDirectory($"{_rootFolder}/patterns");
        //    }

        //    AssetDatabase.CreateAsset(newBoss, $"{_rootFolder}/patterns/{newBoss.bossName}.asset");
        //    bossList.bosses.Add(newBoss);

        //    EditorUtility.SetDirty(bossList);
        //    AssetDatabase.SaveAssets();

        //    GeneratePoolingItemUI();
        //}


        
        //#region Boss
        //private void HandleBossDeleteEvent(BossUI boss)
        //{
        //    bossList.bosses.Remove(boss.poolingItem);
        //    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(boss.poolingItem));
        //    EditorUtility.SetDirty(bossList);
        //    AssetDatabase.SaveAssets();

        //    if (boss == _currentBoss)
        //    {
        //        _currentBoss = null;
        //    }

        //    GeneratePoolingItemUI();

        //}
        //private void HandleBossAddEvent(BossUI selectPattern)
        //{
        //    Guid patternGUID = Guid.NewGuid();
        //    PatternSO newPatternList = ScriptableObject.CreateInstance<PatternSO>();
        //    newPatternList.patternName = patternGUID.ToString();
        //    if (Directory.Exists($"{_rootFolder}/Items") == false)
        //    {
        //        Directory.CreateDirectory($"{_rootFolder}/Items");
        //    }
        //    AssetDatabase.CreateAsset(newPatternList, $"{_rootFolder}/Items/{newPatternList.patternName}.asset");
        //    patternList.patterns.Add(newPatternList);
        //    EditorUtility.SetDirty(patternList);
        //    AssetDatabase.SaveAssets();
        //    GeneratePoolingItemUI(); // 래결해야함
        //}
        //private void HandleBossSelectionEvent(BossUI selectBoss)
        //{
        //    _bossList.ForEach(boss => boss.IsActive = false);
        //    selectBoss.IsActive = true;
        //    _currentBoss = selectBoss;
        //    patternList = _currentBoss.poolingItem;

        //    _patternListView.Clear();
        //    _patternList.Clear();

        //    if (patternList.patterns.Count > 0)
        //    {
        //        foreach (PatternSO patternList in patternList.patterns)
        //        {
        //            TemplateContainer bossUI = patternAsset.Instantiate();
        //            PatternUI boss = new PatternUI(bossUI, patternList);

        //            _patternListView.Add(bossUI); //스크롤뷰에 넣고 리스트 관리
        //            _patternList.Add(boss);
        //            boss.Name = patternList.patternName;
        //            boss.OnSelectEvent += HandleSelectionEvent;
        //            boss.OnDeleteEvent += HandleDeleteEvent;
        //        }
        //    }
        //}
        //#endregion

        //#region PatterList
        //private void HandleDeleteEvent(PatternUI pattern)
        //{
        //    patternList.patterns.Remove(pattern.poolingItem);
        //    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(pattern.poolingItem));
        //    EditorUtility.SetDirty(patternList);
        //    AssetDatabase.SaveAssets();

        //    if (pattern == _currentPattern)
        //    {
        //        _currentPattern = null;
        //        //인스펙터 클리어도 해야함. 아직 인스펙터가 없어서 여기서 끝.
        //    }
        //    GeneratePoolingItemUI();
        //}
        //private void HandleSelectionEvent(PatternUI selectPattern)
        //{
        //    _patternList.ForEach(item => item.IsActive = false);
        //    selectPattern.IsActive = true;
        //    _currentPattern = selectPattern;

        //    _patternView.Clear();
        //    _currentPatterInspecter = null;

        //    TemplateContainer itemUI = patternInspecterAsset.Instantiate();
        //    PatternInspecterUI poolItem = new PatternInspecterUI(itemUI, _currentPattern.poolingItem);
        //    _patternView.Add(itemUI);
        //    _currentPatterInspecter = poolItem;
        //    poolItem.Name = _currentPattern.poolingItem.patternName;
        //}

        //#endregion

        //#region Pattern
        //#endregion
    }
}
