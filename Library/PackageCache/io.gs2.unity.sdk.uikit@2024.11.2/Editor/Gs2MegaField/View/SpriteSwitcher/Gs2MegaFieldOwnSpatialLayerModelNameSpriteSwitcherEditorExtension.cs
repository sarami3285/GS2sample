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
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2MegaField.SpriteSwitcher.Editor
{
    [CustomEditor(typeof(Gs2MegaFieldOwnSpatialLayerModelNameSpriteSwitcher))]
    public class Gs2MegaFieldOwnSpatialLayerModelNameSpriteSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MegaFieldOwnSpatialLayerModelNameSpriteSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2MegaFieldSpatialContext>() ?? original.GetComponentInParent<Gs2MegaFieldSpatialContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2MegaFieldSpatialContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2MegaFieldSpatialContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MegaFieldSpatialContext), false);
                EditorGUI.indentLevel++;
                context.Spatial = EditorGUILayout.ObjectField("Spatial", context.Spatial, typeof(Spatial), false) as Spatial;
                if (context.Spatial != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.Spatial?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("AreaModelName", context.Spatial?.AreaModelName?.ToString());
                    EditorGUILayout.TextField("LayerModelName", context.Spatial?.LayerModelName?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2MegaFieldOwnSpatialLayerModelNameSpriteSwitcher.Expression.In || original.expression == Gs2MegaFieldOwnSpatialLayerModelNameSpriteSwitcher.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyLayerModelNames"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyLayerModelName"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}