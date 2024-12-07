using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Exchange.Prefabs
{
    public static class Gs2ExchangeAwaitSkipSecondsValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Exchange/Namespace/User/Await/Label/SkipSecondsValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Exchange/Prefabs/AwaitSkipSecondsValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}