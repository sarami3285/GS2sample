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

using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Inventory.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Inventory.Editor
{
    [CustomEditor(typeof(Gs2InventoryOwnInventoryCurrentInventoryMaxCapacitySpriteSwitcher))]
    public class Gs2InventoryOwnInventoryCurrentInventoryMaxCapacitySpriteSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2InventoryOwnInventoryCurrentInventoryMaxCapacitySpriteSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2InventoryOwnInventoryContext>() ?? original.GetComponentInParent<Gs2InventoryOwnInventoryContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2InventoryOwnInventoryContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2InventoryOwnInventoryContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2InventoryOwnInventoryList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2InventoryOwnInventoryContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Inventory is auto assign from Gs2InventoryOwnInventoryList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2InventoryOwnInventoryContext), false);
                    EditorGUI.indentLevel++;
                    context.Inventory = EditorGUILayout.ObjectField("Inventory", context.Inventory, typeof(OwnInventory), false) as OwnInventory;
                    if (context.Inventory != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.Inventory?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("InventoryName", context.Inventory?.InventoryName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2InventoryOwnInventoryCurrentInventoryMaxCapacitySpriteSwitcher.Expression.In || original.expression == Gs2InventoryOwnInventoryCurrentInventoryMaxCapacitySpriteSwitcher.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyCurrentInventoryMaxCapacities"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyCurrentInventoryMaxCapacity"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}