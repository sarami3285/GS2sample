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

using Gs2.Unity.Gs2Ranking.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Ranking.Context;
using Gs2.Unity.UiKit.Gs2Ranking.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Ranking.Localization.Editor
{
    [CustomEditor(typeof(Gs2RankingSubscribeUserLocalizationVariables))]
    public class Gs2RankingSubscribeUserLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2RankingSubscribeUserLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2RankingOwnSubscribeUserFetcher>() ?? original.GetComponentInParent<Gs2RankingOwnSubscribeUserFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2RankingOwnSubscribeUserFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2RankingOwnSubscribeUserFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2RankingOwnSubscribeUserList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2RankingOwnSubscribeUserFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("SubscribeUser is auto assign from Gs2RankingOwnSubscribeUserList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2RankingOwnSubscribeUserContext>() ?? original.GetComponentInParent<Gs2RankingOwnSubscribeUserContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2RankingOwnSubscribeUserFetcher), false);
                    EditorGUI.indentLevel++;
                    context.SubscribeUser = EditorGUILayout.ObjectField("SubscribeUser", context.SubscribeUser, typeof(OwnSubscribeUser), false) as OwnSubscribeUser;
                    if (context.SubscribeUser != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.SubscribeUser?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("CategoryName", context.SubscribeUser?.CategoryName?.ToString());
                        EditorGUILayout.TextField("TargetUserId", context.SubscribeUser?.TargetUserId?.ToString());
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