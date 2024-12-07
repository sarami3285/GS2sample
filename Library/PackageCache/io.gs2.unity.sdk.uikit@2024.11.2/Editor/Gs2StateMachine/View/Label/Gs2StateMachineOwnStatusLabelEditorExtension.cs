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

using Gs2.Unity.Gs2StateMachine.ScriptableObject;
using Gs2.Unity.UiKit.Gs2StateMachine.Context;
using Gs2.Unity.UiKit.Gs2StateMachine.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2StateMachine.Editor
{
    [CustomEditor(typeof(Gs2StateMachineOwnStatusLabel))]
    public class Gs2StateMachineOwnStatusLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2StateMachineOwnStatusLabel;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2StateMachineOwnStatusFetcher>() ?? original.GetComponentInParent<Gs2StateMachineOwnStatusFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2StateMachineOwnStatusFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2StateMachineOwnStatusFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2StateMachineOwnStatusList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2StateMachineOwnStatusFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Status is auto assign from Gs2StateMachineOwnStatusList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2StateMachineOwnStatusFetcher), false);
                    EditorGUI.indentLevel++;
                    if (fetcher.Context != null) {
                        EditorGUILayout.ObjectField("Status", fetcher.Context.Status, typeof(OwnStatus), false);
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", fetcher.Context.Status?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("StatusName", fetcher.Context.Status?.StatusName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("StatusId")) {
                original.format += "{statusId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Name")) {
                original.format += "{name}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("EnableSpeculativeExecution")) {
                original.format += "{enableSpeculativeExecution}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("StateMachineDefinition")) {
                original.format += "{stateMachineDefinition}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("RandomStatus")) {
                original.format += "{randomStatus}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Stacks")) {
                original.format += "{stacks}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Variables")) {
                original.format += "{variables}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Status")) {
                original.format += "{status}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("LastError")) {
                original.format += "{lastError}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("TransitionCount")) {
                original.format += "{transitionCount}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}