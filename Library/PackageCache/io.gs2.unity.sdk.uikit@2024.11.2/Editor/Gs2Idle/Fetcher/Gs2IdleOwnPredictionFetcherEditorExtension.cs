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

using Gs2.Unity.Gs2Idle.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Idle.Context;
using Gs2.Unity.UiKit.Gs2Idle.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Idle.Editor
{
    [CustomEditor(typeof(Gs2IdleOwnPredictionFetcher))]
    public class Gs2IdleOwnPredictionFetcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2IdleOwnPredictionFetcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2IdleOwnPredictionContext>() ?? original.GetComponentInParent<Gs2IdleOwnPredictionContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2IdleOwnPredictionContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2IdleOwnPredictionContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2IdleOwnPredictionList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2IdleOwnPredictionContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Prediction is auto assign from Gs2IdleOwnPredictionList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2IdleOwnPredictionContext), false);
                    EditorGUI.indentLevel++;
                    context.Prediction = EditorGUILayout.ObjectField("Prediction", context.Prediction, typeof(OwnPrediction), false) as OwnPrediction;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.Prediction?.NamespaceName.ToString());
                    EditorGUILayout.TextField("CategoryName", context.Prediction?.CategoryName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }
            
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}