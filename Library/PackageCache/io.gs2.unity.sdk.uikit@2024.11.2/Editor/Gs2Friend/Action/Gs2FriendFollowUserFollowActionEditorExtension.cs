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

namespace Gs2.Unity.UiKit.Gs2Friend.Editor
{
    [CustomEditor(typeof(Gs2FriendFollowUserFollowAction))]
    public class Gs2FriendFollowUserFollowActionEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FriendFollowUserFollowAction;

            if (original == null) return;

            var context = original.GetComponent<Gs2FriendOwnFollowUserContext>() ?? original.GetComponentInParent<Gs2FriendOwnFollowUserContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2FriendOwnFollowUserContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2FriendOwnFollowUserContext>();
                }
            }
            else {
                if (context.transform.GetComponentInParent<Gs2FriendOwnFollowUserList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FriendOwnFollowUserContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("FollowUser is auto assign from Gs2FriendFollowUserList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FriendOwnFollowUserContext), false);
                    EditorGUI.indentLevel++;
                    context.FollowUser = EditorGUILayout.ObjectField("OwnFollowUser", context.FollowUser, typeof(OwnFollowUser), false) as OwnFollowUser;
                    if (context.FollowUser != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.FollowUser?.NamespaceName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();

            DrawDefaultInspector();
        }
    }
}