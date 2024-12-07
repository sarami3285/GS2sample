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

using Gs2.Unity.Gs2Enchant.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Enchant.Context;
using Gs2.Unity.UiKit.Gs2Enchant.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Enchant.Editor
{
    [CustomEditor(typeof(Gs2EnchantOwnBalanceParameterStatusFetcher))]
    public class Gs2EnchantOwnBalanceParameterStatusFetcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2EnchantOwnBalanceParameterStatusFetcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2EnchantOwnBalanceParameterStatusContext>() ?? original.GetComponentInParent<Gs2EnchantOwnBalanceParameterStatusContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2EnchantOwnBalanceParameterStatusContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2EnchantOwnBalanceParameterStatusContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2EnchantOwnBalanceParameterStatusList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2EnchantOwnBalanceParameterStatusContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("BalanceParameterStatus is auto assign from Gs2EnchantOwnBalanceParameterStatusList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2EnchantOwnBalanceParameterStatusContext), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.ObjectField("BalanceParameterStatus", context.BalanceParameterStatus, typeof(OwnBalanceParameterStatus), false);
                    if (context.BalanceParameterStatus != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.BalanceParameterStatus?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("ParameterName", context.BalanceParameterStatus?.ParameterName?.ToString());
                        EditorGUILayout.TextField("PropertyId", context.BalanceParameterStatus?.PropertyId?.ToString());
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