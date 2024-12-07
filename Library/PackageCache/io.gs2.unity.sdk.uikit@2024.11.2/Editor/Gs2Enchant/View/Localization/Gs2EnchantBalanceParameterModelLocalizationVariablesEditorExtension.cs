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

using Gs2.Unity.Gs2Enchant.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Enchant.Context;
using Gs2.Unity.UiKit.Gs2Enchant.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Enchant.Localization.Editor
{
    [CustomEditor(typeof(Gs2EnchantBalanceParameterModelLocalizationVariables))]
    public class Gs2EnchantBalanceParameterModelLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2EnchantBalanceParameterModelLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2EnchantBalanceParameterModelFetcher>() ?? original.GetComponentInParent<Gs2EnchantBalanceParameterModelFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2EnchantBalanceParameterModelFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2EnchantBalanceParameterModelFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2EnchantBalanceParameterModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2EnchantBalanceParameterModelFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("BalanceParameterModel is auto assign from Gs2EnchantBalanceParameterModelList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2EnchantBalanceParameterModelContext>() ?? original.GetComponentInParent<Gs2EnchantBalanceParameterModelContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2EnchantBalanceParameterModelFetcher), false);
                    EditorGUI.indentLevel++;
                    context.BalanceParameterModel = EditorGUILayout.ObjectField("BalanceParameterModel", context.BalanceParameterModel, typeof(BalanceParameterModel), false) as BalanceParameterModel;
                    if (context.BalanceParameterModel != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.BalanceParameterModel?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("ParameterName", context.BalanceParameterModel?.ParameterName?.ToString());
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