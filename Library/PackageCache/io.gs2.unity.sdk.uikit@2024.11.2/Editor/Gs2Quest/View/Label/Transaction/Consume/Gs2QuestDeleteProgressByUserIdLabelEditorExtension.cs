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

using Gs2.Unity.UiKit.Gs2Quest.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Quest.Label.Editor
{
    [CustomEditor(typeof(Gs2QuestDeleteProgressByUserIdLabel))]
    public class Gs2QuestDeleteProgressByUserIdLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2QuestDeleteProgressByUserIdLabel;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2QuestDeleteProgressByUserIdFetcher>() ?? original.GetComponentInParent<Gs2QuestDeleteProgressByUserIdFetcher>(true);
             var userDataFetcher = original.GetComponent<Gs2QuestOwnProgressFetcher>() ?? original.GetComponentInParent<Gs2QuestOwnProgressFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2QuestDeleteProgressByUserIdFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2QuestDeleteProgressByUserIdFetcher>();
                }
            }
            if (userDataFetcher == null) {
                EditorGUILayout.HelpBox("Gs2QuestOwnProgressFetcher not found. Adding a Fetcher allows more values to be used.", MessageType.Warning);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2QuestOwnProgressFetcher>();
                }
            }

            serializedObject.Update();
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("NamespaceName")) {
                original.format += "{namespaceName}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("UserId")) {
                original.format += "{userId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("TimeOffsetToken")) {
                original.format += "{timeOffsetToken}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (userDataFetcher != null) {
                if (GUILayout.Button("UserData:ProgressId")) {
                    original.format += "{userData:progressId}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
                if (GUILayout.Button("UserData:TransactionId")) {
                    original.format += "{userData:transactionId}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
                if (GUILayout.Button("UserData:QuestModelId")) {
                    original.format += "{userData:questModelId}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
                if (GUILayout.Button("UserData:RandomSeed")) {
                    original.format += "{userData:randomSeed}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
                if (GUILayout.Button("UserData:Rewards")) {
                    original.format += "{userData:rewards}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
                if (GUILayout.Button("UserData:FailedRewards")) {
                    original.format += "{userData:failedRewards}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}