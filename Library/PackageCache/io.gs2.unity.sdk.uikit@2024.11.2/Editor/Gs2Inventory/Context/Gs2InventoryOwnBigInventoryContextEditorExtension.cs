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
    [CustomEditor(typeof(Gs2InventoryOwnBigInventoryContext))]
    public class Gs2InventoryOwnBigInventoryContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2InventoryOwnBigInventoryContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.BigInventory == null) {
                EditorGUILayout.HelpBox("OwnBigInventory not assigned.", MessageType.Error);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_bigInventory"), true);
            }
            else {
                original.BigInventory = EditorGUILayout.ObjectField("OwnBigInventory", original.BigInventory, typeof(OwnBigInventory), false) as OwnBigInventory;
                EditorGUI.BeginDisabledGroup(true);
                if (original.BigInventory != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.BigInventory?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("InventoryName", original.BigInventory?.InventoryName?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}