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

namespace Gs2.Unity.UiKit.Gs2Inventory.Enabler.Editor
{
    [CustomEditor(typeof(Gs2InventoryOwnBigItemItemNameEnabler))]
    public class Gs2InventoryOwnBigItemItemNameEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2InventoryOwnBigItemItemNameEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2InventoryOwnBigItemContext>() ?? original.GetComponentInParent<Gs2InventoryOwnBigItemContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2InventoryOwnBigItemContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2InventoryOwnBigItemContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2InventoryOwnBigItemList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2InventoryOwnBigItemContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("BigItem is auto assign from Gs2InventoryOwnBigItemList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2InventoryOwnBigItemContext), false);
                    EditorGUI.indentLevel++;
                    context.BigItem = EditorGUILayout.ObjectField("BigItem", context.BigItem, typeof(OwnBigItem), false) as OwnBigItem;
                    if (context.BigItem != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.BigItem?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("InventoryName", context.BigItem?.InventoryName?.ToString());
                        EditorGUILayout.TextField("ItemName", context.BigItem?.ItemName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2InventoryOwnBigItemItemNameEnabler.Expression.In || original.expression == Gs2InventoryOwnBigItemItemNameEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableItemNames"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableItemName"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}