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

using Gs2.Unity.Gs2Exchange.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Exchange.Context;
using Gs2.Unity.UiKit.Gs2Exchange.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Exchange.Localization.Editor
{
    [CustomEditor(typeof(Gs2ExchangeIncrementalRateModelLocalizationVariables))]
    public class Gs2ExchangeIncrementalRateModelLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ExchangeIncrementalRateModelLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2ExchangeIncrementalRateModelFetcher>() ?? original.GetComponentInParent<Gs2ExchangeIncrementalRateModelFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2ExchangeIncrementalRateModelFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2ExchangeIncrementalRateModelFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2ExchangeIncrementalRateModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2ExchangeIncrementalRateModelFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("IncrementalRateModel is auto assign from Gs2ExchangeIncrementalRateModelList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2ExchangeIncrementalRateModelContext>() ?? original.GetComponentInParent<Gs2ExchangeIncrementalRateModelContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2ExchangeIncrementalRateModelFetcher), false);
                    EditorGUI.indentLevel++;
                    context.IncrementalRateModel = EditorGUILayout.ObjectField("IncrementalRateModel", context.IncrementalRateModel, typeof(IncrementalRateModel), false) as IncrementalRateModel;
                    if (context.IncrementalRateModel != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.IncrementalRateModel?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("RateName", context.IncrementalRateModel?.RateName?.ToString());
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