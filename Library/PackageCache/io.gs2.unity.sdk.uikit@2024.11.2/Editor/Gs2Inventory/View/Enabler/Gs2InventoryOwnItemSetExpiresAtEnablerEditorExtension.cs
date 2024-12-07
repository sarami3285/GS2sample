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
    [CustomEditor(typeof(Gs2InventoryOwnItemSetExpiresAtEnabler))]
    public class Gs2InventoryOwnItemSetExpiresAtEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2InventoryOwnItemSetExpiresAtEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2InventoryOwnItemSetContext>() ?? original.GetComponentInParent<Gs2InventoryOwnItemSetContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2InventoryOwnItemSetContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2InventoryOwnItemSetContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2InventoryOwnItemSetList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2InventoryOwnItemSetContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("ItemSet is auto assign from Gs2InventoryOwnItemSetList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2InventoryOwnItemSetContext), false);
                    EditorGUI.indentLevel++;
                    context.ItemSet = EditorGUILayout.ObjectField("ItemSet", context.ItemSet, typeof(OwnItemSet), false) as OwnItemSet;
                    if (context.ItemSet != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.ItemSet?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("InventoryName", context.ItemSet?.InventoryName?.ToString());
                        EditorGUILayout.TextField("ItemName", context.ItemSet?.ItemName?.ToString());
                        EditorGUILayout.TextField("ItemSetName", context.ItemSet?.ItemSetName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2InventoryOwnItemSetExpiresAtEnabler.Expression.In || original.expression == Gs2InventoryOwnItemSetExpiresAtEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableExpiresAts"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableExpiresAt"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}