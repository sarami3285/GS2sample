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

using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Inventory.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Inventory.SpriteSwitcher.Editor
{
    [CustomEditor(typeof(Gs2InventorySimpleInventoryModelNameSpriteSwitcher))]
    public class Gs2InventorySimpleInventoryModelNameSpriteSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2InventorySimpleInventoryModelNameSpriteSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2InventorySimpleInventoryModelContext>() ?? original.GetComponentInParent<Gs2InventorySimpleInventoryModelContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2InventorySimpleInventoryModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2InventorySimpleInventoryModelContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2InventorySimpleInventoryModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2InventorySimpleInventoryModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("SimpleInventoryModel is auto assign from Gs2InventorySimpleInventoryModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2InventorySimpleInventoryModelContext), false);
                    EditorGUI.indentLevel++;
                    context.SimpleInventoryModel = EditorGUILayout.ObjectField("SimpleInventoryModel", context.SimpleInventoryModel, typeof(SimpleInventoryModel), false) as SimpleInventoryModel;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.SimpleInventoryModel?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("InventoryName", context.SimpleInventoryModel?.InventoryName?.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2InventorySimpleInventoryModelNameSpriteSwitcher.Expression.In || original.expression == Gs2InventorySimpleInventoryModelNameSpriteSwitcher.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableNames"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableName"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}