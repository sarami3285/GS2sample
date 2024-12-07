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

using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Mission.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Mission.Enabler.Editor
{
    [CustomEditor(typeof(Gs2MissionOwnCounterNameEnabler))]
    public class Gs2MissionOwnCounterNameEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MissionOwnCounterNameEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2MissionOwnCounterContext>() ?? original.GetComponentInParent<Gs2MissionOwnCounterContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2MissionOwnCounterContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2MissionOwnCounterContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2MissionOwnCounterList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MissionOwnCounterContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Counter is auto assign from Gs2MissionOwnCounterList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MissionOwnCounterContext), false);
                    EditorGUI.indentLevel++;
                    context.Counter = EditorGUILayout.ObjectField("Counter", context.Counter, typeof(OwnCounter), false) as OwnCounter;
                    if (context.Counter != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.Counter?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("CounterName", context.Counter?.CounterName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2MissionOwnCounterNameEnabler.Expression.In || original.expression == Gs2MissionOwnCounterNameEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableNames"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableName"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}