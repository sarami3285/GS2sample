using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Friend.Prefabs
{
    public static class Gs2FriendReceiveFriendRequestAcceptButton
    {
        [MenuItem("GameObject/UI/Game Server Services/Friend/Namespace/User/ReceiveFriendRequest/Action/Accept", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Friend/Prefabs/ReceiveFriendRequestAcceptButton.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}