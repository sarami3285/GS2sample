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

#if GS2_ENABLE_LOCALIZATION

using Gs2.Unity.Gs2Friend.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Friend.Context;
using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Friend.Localization.Editor
{
    [CustomEditor(typeof(Gs2FriendFollowUserLocalizationVariables))]
    public class Gs2FriendFollowUserLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FriendFollowUserLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2FriendOwnFollowUserFetcher>() ?? original.GetComponentInParent<Gs2FriendOwnFollowUserFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2FriendOwnFollowUserFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2FriendOwnFollowUserFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2FriendOwnFollowUserList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2FriendOwnFollowUserFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("FollowUser is auto assign from Gs2FriendOwnFollowUserList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2FriendOwnFollowUserContext>() ?? original.GetComponentInParent<Gs2FriendOwnFollowUserContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2FriendOwnFollowUserFetcher), false);
                    EditorGUI.indentLevel++;
                    context.FollowUser = EditorGUILayout.ObjectField("FollowUser", context.FollowUser, typeof(OwnFollowUser), false) as OwnFollowUser;
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
            original.target = EditorGUILayout.ObjectField("Target", original.target, typeof(LocalizeStringEvent), true) as LocalizeStringEvent;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif