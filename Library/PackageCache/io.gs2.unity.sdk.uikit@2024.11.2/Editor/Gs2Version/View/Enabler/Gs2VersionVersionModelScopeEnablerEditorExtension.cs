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

using Gs2.Unity.Gs2Version.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Version.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Version.Enabler.Editor
{
    [CustomEditor(typeof(Gs2VersionVersionModelScopeEnabler))]
    public class Gs2VersionVersionModelScopeEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2VersionVersionModelScopeEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2VersionVersionModelContext>() ?? original.GetComponentInParent<Gs2VersionVersionModelContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2VersionVersionModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2VersionVersionModelContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2VersionVersionModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2VersionVersionModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("VersionModel is auto assign from Gs2VersionVersionModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2VersionVersionModelContext), false);
                    EditorGUI.indentLevel++;
                    context.VersionModel = EditorGUILayout.ObjectField("VersionModel", context.VersionModel, typeof(VersionModel), false) as VersionModel;
                    if (context.VersionModel != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.VersionModel?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("VersionName", context.VersionModel?.VersionName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2VersionVersionModelScopeEnabler.Expression.In || original.expression == Gs2VersionVersionModelScopeEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableScopes"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableScope"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}