using System.Collections.Generic;
using UnityEngine;

public class UIMgr : MonoSingleton<UIMgr>
{
    [SerializeField] private Canvas rootCanvas = null;

    private readonly Dictionary<string, RectTransform> _allUiObjectDictionary = new Dictionary<string, RectTransform>();
    public Dictionary<string, RectTransform> AllUiObjectDictionary()
    {
        if(_allUiObjectDictionary.Count == 0)
        {
            var allUiRectTransform = rootCanvas.GetComponentsInChildren<RectTransform>();
            for(var i=0; i < allUiRectTransform.Length; i++)
            {
                _allUiObjectDictionary.Add(allUiRectTransform[i].name,allUiRectTransform[i]);
            }
        }
        return _allUiObjectDictionary;
    }
}
