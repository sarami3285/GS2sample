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
    [CustomEditor(typeof(Gs2StateMachineOwnStatusFetcher))]
    public class Gs2StateMachineOwnStatusFetcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2StateMachineOwnStatusFetcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2StateMachineOwnStatusContext>() ?? original.GetComponentInParent<Gs2StateMachineOwnStatusContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2StateMachineOwnStatusContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2StateMachineOwnStatusContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2StateMachineOwnStatusList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2StateMachineOwnStatusContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Status is auto assign from Gs2StateMachineOwnStatusList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2StateMachineOwnStatusContext), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.ObjectField("Status", context.Status, typeof(OwnStatus), false);
                    if (context.Status != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.Status?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("StatusName", context.Status?.StatusName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }
            
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}