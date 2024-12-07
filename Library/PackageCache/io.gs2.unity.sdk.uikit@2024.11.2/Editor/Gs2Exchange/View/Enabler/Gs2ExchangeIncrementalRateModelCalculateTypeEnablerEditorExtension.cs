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

using Gs2.Unity.Gs2Exchange.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Exchange.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Exchange.Enabler.Editor
{
    [CustomEditor(typeof(Gs2ExchangeIncrementalRateModelCalculateTypeEnabler))]
    public class Gs2ExchangeIncrementalRateModelCalculateTypeEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ExchangeIncrementalRateModelCalculateTypeEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2ExchangeIncrementalRateModelContext>() ?? original.GetComponentInParent<Gs2ExchangeIncrementalRateModelContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2ExchangeIncrementalRateModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2ExchangeIncrementalRateModelContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2ExchangeIncrementalRateModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ExchangeIncrementalRateModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("IncrementalRateModel is auto assign from Gs2ExchangeIncrementalRateModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ExchangeIncrementalRateModelContext), false);
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
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2ExchangeIncrementalRateModelCalculateTypeEnabler.Expression.In || original.expression == Gs2ExchangeIncrementalRateModelCalculateTypeEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableCalculateTypes"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableCalculateType"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}