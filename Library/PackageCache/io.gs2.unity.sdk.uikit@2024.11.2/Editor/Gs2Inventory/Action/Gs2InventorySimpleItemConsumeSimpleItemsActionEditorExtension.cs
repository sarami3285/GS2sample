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
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Inventory.Editor
{
    [CustomEditor(typeof(Gs2InventorySimpleItemConsumeSimpleItemsAction))]
    public class Gs2InventorySimpleItemConsumeSimpleItemsActionEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2InventorySimpleItemConsumeSimpleItemsAction;

            if (original == null) return;

            var context = original.GetComponent<Gs2InventoryOwnSimpleItemContext>() ?? original.GetComponentInParent<Gs2InventoryOwnSimpleItemContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2InventoryOwnSimpleItemContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2InventoryOwnSimpleItemContext>();
                }
            }
            else {
                if (context.transform.GetComponentInParent<Gs2InventoryOwnSimpleItemList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2InventoryOwnSimpleItemContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("SimpleItem is auto assign from Gs2InventorySimpleItemList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2InventoryOwnSimpleItemContext), false);
                    EditorGUI.indentLevel++;
                    context.SimpleItem = EditorGUILayout.ObjectField("OwnSimpleItem", context.SimpleItem, typeof(OwnSimpleItem), false) as OwnSimpleItem;
                    if (context.SimpleItem != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.SimpleItem?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("InventoryName", context.SimpleItem?.InventoryName?.ToString());
                        EditorGUILayout.TextField("ItemName", context.SimpleItem?.ItemName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();

            DrawDefaultInspector();
        }
    }
}