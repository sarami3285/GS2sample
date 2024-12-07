using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Formation.Prefabs
{
    public static class Gs2FormationMoldModelMaxCapacityValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Formation/Namespace/MoldModel/Label/MaxCapacityValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Formation/Prefabs/MoldModelMaxCapacityValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}