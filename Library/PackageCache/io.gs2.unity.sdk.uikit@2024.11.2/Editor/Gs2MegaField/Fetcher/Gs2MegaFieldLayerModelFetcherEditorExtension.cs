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

using Gs2.Unity.Gs2MegaField.ScriptableObject;
using Gs2.Unity.UiKit.Gs2MegaField.Context;
using Gs2.Unity.UiKit.Gs2MegaField.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2MegaField.Editor
{
    [CustomEditor(typeof(Gs2MegaFieldLayerModelFetcher))]
    public class Gs2MegaFieldLayerModelFetcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MegaFieldLayerModelFetcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2MegaFieldLayerModelContext>() ?? original.GetComponentInParent<Gs2MegaFieldLayerModelContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2MegaFieldLayerModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2MegaFieldLayerModelContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2MegaFieldLayerModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MegaFieldLayerModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("LayerModel is auto assign from Gs2MegaFieldLayerModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MegaFieldLayerModelContext), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.ObjectField("LayerModel", context.LayerModel, typeof(LayerModel), false);
                    if (context.LayerModel != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("LayerModelName", context.LayerModel?.LayerModelName?.ToString());
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