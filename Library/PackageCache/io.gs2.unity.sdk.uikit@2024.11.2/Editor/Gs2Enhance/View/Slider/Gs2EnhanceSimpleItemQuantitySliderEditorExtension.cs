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

using Gs2.Unity.Gs2Enhance.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Enhance.Context;
using Gs2.Unity.UiKit.Gs2Enhance.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Enhance.Editor
{
    [CustomEditor(typeof(Gs2EnhanceSimpleItemQuantitySlider))]
    public class Gs2EnhanceSimpleItemQuantitySliderEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2EnhanceSimpleItemQuantitySlider;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2EnhanceSimpleItemExperienceValueFetcher>() ?? original.GetComponentInParent<Gs2EnhanceSimpleItemExperienceValueFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2EnhanceExperienceValueFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2EnhanceSimpleItemExperienceValueFetcher>();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("type"), true);
            if (original.type == Gs2EnhanceSimpleItemQuantitySlider.Type.Value) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("quantity"), true);
            }
            if (original.type == Gs2EnhanceSimpleItemQuantitySlider.Type.Action) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("action"), true);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}