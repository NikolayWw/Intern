using CodeBase.UI.Windows.LoseWindow;
using UnityEngine;

namespace CodeBase.StaticData.Windows
{
    [CreateAssetMenu(menuName = "Static Data/Window static data")]
    public class WindowStaticData : ScriptableObject
    {
        [field: SerializeField] public GameObject UIRoot { get; private set; }
        [field: SerializeField] public GameObject HudPrefab { get; private set; }
        [field: SerializeField] public LoseWindow LoseWindowPrefab { get; private set; }
    }
}