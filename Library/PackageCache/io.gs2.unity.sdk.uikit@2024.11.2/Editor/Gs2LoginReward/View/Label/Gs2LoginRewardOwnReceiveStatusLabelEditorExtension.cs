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

using Gs2.Unity.Gs2LoginReward.ScriptableObject;
using Gs2.Unity.UiKit.Gs2LoginReward.Context;
using Gs2.Unity.UiKit.Gs2LoginReward.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2LoginReward.Editor
{
    [CustomEditor(typeof(Gs2LoginRewardOwnReceiveStatusLabel))]
    public class Gs2LoginRewardOwnReceiveStatusLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2LoginRewardOwnReceiveStatusLabel;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2LoginRewardOwnReceiveStatusFetcher>() ?? original.GetComponentInParent<Gs2LoginRewardOwnReceiveStatusFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2LoginRewardOwnReceiveStatusFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2LoginRewardOwnReceiveStatusFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2LoginRewardOwnReceiveStatusList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2LoginRewardOwnReceiveStatusFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("ReceiveStatus is auto assign from Gs2LoginRewardOwnReceiveStatusList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2LoginRewardOwnReceiveStatusFetcher), false);
                    EditorGUI.indentLevel++;
                    if (fetcher.Context != null) {
                        EditorGUILayout.ObjectField("ReceiveStatus", fetcher.Context.ReceiveStatus, typeof(OwnReceiveStatus), false);
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", fetcher.Context.ReceiveStatus?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("BonusModelName", fetcher.Context.ReceiveStatus?.BonusModelName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("BonusModelName")) {
                original.format += "{bonusModelName}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ReceivedSteps")) {
                original.format += "{receivedSteps}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("LastReceivedAt(Year:2020)")) {
                original.format += "{lastReceivedAt:yyyy}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("LastReceivedAt(Year:20)")) {
                original.format += "{lastReceivedAt:yy}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("LastReceivedAt(Month:12)")) {
                original.format += "{lastReceivedAt:MM}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("LastReceivedAt(Month:Dec)")) {
                original.format += "{lastReceivedAt:MMM}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("LastReceivedAt(Day:25)")) {
                original.format += "{lastReceivedAt:dd}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("LastReceivedAt(Hour:6)")) {
                original.format += "{lastReceivedAt:hh}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("LastReceivedAt(Hour:18)")) {
                original.format += "{lastReceivedAt:HH}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("LastReceivedAt(AM/PM)")) {
                original.format += "{lastReceivedAt:tt}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("LastReceivedAt(Min:05)")) {
                original.format += "{lastReceivedAt:mm}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("LastReceivedAt(Sec:09)")) {
                original.format += "{lastReceivedAt:ss}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}