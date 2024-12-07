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

#if GS2_ENABLE_LOCALIZATION

using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Inventory.Context;
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Inventory.Localization.Editor
{
    [CustomEditor(typeof(Gs2InventoryInventoryModelLocalizationVariables))]
    public class Gs2InventoryInventoryModelLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2InventoryInventoryModelLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2InventoryInventoryModelFetcher>() ?? original.GetComponentInParent<Gs2InventoryInventoryModelFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2InventoryInventoryModelFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2InventoryInventoryModelFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2InventoryInventoryModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2InventoryInventoryModelFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("InventoryModel is auto assign from Gs2InventoryInventoryModelList.", MessageType.Info);
                }
                else if (fetcher.gameObject.GetComponentInParent<Gs2InventoryOwnInventoryList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2InventoryOwnInventoryFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("InventoryModel is auto assign from Gs2InventoryOwnInventoryList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2InventoryInventoryModelContext>() ?? original.GetComponentInParent<Gs2InventoryInventoryModelContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2InventoryInventoryModelFetcher), false);
                    EditorGUI.indentLevel++;
                    context.InventoryModel = EditorGUILayout.ObjectField("InventoryModel", context.InventoryModel, typeof(InventoryModel), false) as InventoryModel;
                    if (context.InventoryModel != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.InventoryModel?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("InventoryName", context.InventoryModel?.InventoryName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            original.target = EditorGUILayout.ObjectField("Target", original.target, typeof(LocalizeStringEvent), true) as LocalizeStringEvent;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif