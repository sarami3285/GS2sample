/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 *
 * deny overwrite
 */
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CheckNamespace
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantUsingDirective
// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming

#pragma warning disable CS0472

using Gs2.Unity.Gs2Friend.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Friend.Context;
using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
using UnityEditor;
using UnityEngine;
using OwnFriendUser = Gs2.Unity.Gs2Friend.ScriptableObject.OwnFriendUser;

namespace Gs2.Unity.UiKit.Gs2Friend.Editor
{
    [CustomEditor(typeof(Gs2FriendReceiveFriendRequestAcceptAction))]
    public class Gs2FriendReceiveFriendRequestAcceptActionEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FriendReceiveFriendRequestAcceptAction;

            if (original == null) return;

            var context = original.GetComponent<Gs2FriendOwnFriendUserContext>() ?? original.GetComponentInParent<Gs2FriendOwnFriendUserContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2FriendOwnFriendUserContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2FriendOwnFriendUserContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2FriendOwnReceiveFriendRequestList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FriendOwnFriendUserContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("ReceiveFriendRequest is auto assign from Gs2FriendReceiveFriendRequestList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FriendOwnFriendUserContext), false);
                    EditorGUI.indentLevel++;
                    context.FriendUser = EditorGUILayout.ObjectField("OwnFriendUser", context.FriendUser, typeof(OwnFriendUser), false) as OwnFriendUser;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.FriendUser?.NamespaceName.ToString());
                    EditorGUILayout.TextField("FromUserId", context.FriendUser?.TargetUserId.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onAcceptComplete"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}