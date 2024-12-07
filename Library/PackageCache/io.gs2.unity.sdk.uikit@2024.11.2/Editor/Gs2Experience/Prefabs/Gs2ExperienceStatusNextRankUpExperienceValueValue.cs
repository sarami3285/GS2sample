using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Experience.Prefabs
{
    public static class Gs2ExperienceStatusNextRankUpExperienceValueValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Experience/Namespace/User/Status/Label/NextRankUpExperienceValueValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Experience/Prefabs/StatusNextRankUpExperienceValueValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}