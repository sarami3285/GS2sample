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
using Gs2.Unity.UiKit.Gs2Exchange.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Exchange.Editor
{
    [CustomEditor(typeof(Gs2ExchangeIncrementalRateModelContext))]
    public class Gs2ExchangeIncrementalRateModelContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ExchangeIncrementalRateModelContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.IncrementalRateModel == null) {
                var list = original.GetComponentInParent<Gs2ExchangeIncrementalRateModelList>(true);
                if (list != null) {
                    EditorGUILayout.HelpBox("IncrementalRateModel is auto assign from Gs2ExchangeIncrementalRateModelList.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("List", list, typeof(Gs2ExchangeIncrementalRateModelList), false);
                    EditorGUI.EndDisabledGroup();
                }
                else {
                    EditorGUILayout.HelpBox("IncrementalRateModel not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_incrementalRateModel"), true);
                }
            }
            else {
                original.IncrementalRateModel = EditorGUILayout.ObjectField("IncrementalRateModel", original.IncrementalRateModel, typeof(IncrementalRateModel), false) as IncrementalRateModel;
                EditorGUI.BeginDisabledGroup(true);
                if (original.IncrementalRateModel != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.IncrementalRateModel?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("RateName", original.IncrementalRateModel?.RateName?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}