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

using Gs2.Unity.Gs2Ranking.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Ranking.Context;
using Gs2.Unity.UiKit.Gs2Ranking.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Ranking.Editor
{
    [CustomEditor(typeof(Gs2RankingOwnSubscribeUserFetcher))]
    public class Gs2RankingOwnSubscribeUserFetcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2RankingOwnSubscribeUserFetcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2RankingOwnSubscribeContext>() ?? original.GetComponentInParent<Gs2RankingOwnSubscribeContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2RankingOwnSubscribeContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2RankingOwnSubscribeContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2RankingOwnSubscribeContext), false);
                EditorGUILayout.ObjectField("OwnSubscribeUser", context.Subscribe, typeof(OwnSubscribe), false);
                EditorGUI.EndDisabledGroup();
            }
            
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}