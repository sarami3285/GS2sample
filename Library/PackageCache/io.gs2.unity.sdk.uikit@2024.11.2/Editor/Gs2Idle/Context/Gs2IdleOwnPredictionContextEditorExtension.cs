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
    [CustomEditor(typeof(Gs2IdleOwnPredictionContext))]
    public class Gs2IdleOwnPredictionContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2IdleOwnPredictionContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.Prediction == null) {
                if (original.GetComponentInParent<Gs2IdleOwnPredictionList>(true) != null) {
                    EditorGUILayout.HelpBox("OwnPrediction is auto assign from Gs2IdleOwnPredictionList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("OwnPrediction not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("Prediction"), true);
                }
            }
            else {
                original.Prediction = EditorGUILayout.ObjectField("OwnPrediction", original.Prediction, typeof(OwnPrediction), false) as OwnPrediction;
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.Prediction?.NamespaceName.ToString());
                EditorGUILayout.TextField("CategoryName", original.Prediction?.CategoryName.ToString());
                EditorGUILayout.TextField("Index", original.Prediction?.Index.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}