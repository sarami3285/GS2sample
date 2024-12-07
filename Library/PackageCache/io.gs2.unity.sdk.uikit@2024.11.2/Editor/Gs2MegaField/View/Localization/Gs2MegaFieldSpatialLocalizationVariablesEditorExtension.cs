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

using Gs2.Unity.Gs2MegaField.ScriptableObject;
using Gs2.Unity.UiKit.Gs2MegaField.Context;
using Gs2.Unity.UiKit.Gs2MegaField.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2MegaField.Localization.Editor
{
    [CustomEditor(typeof(Gs2MegaFieldSpatialLocalizationVariables))]
    public class Gs2MegaFieldSpatialLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MegaFieldSpatialLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2MegaFieldSpatialFetcher>() ?? original.GetComponentInParent<Gs2MegaFieldSpatialFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2MegaFieldSpatialFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2MegaFieldSpatialFetcher>();
                }
            }
            else {
                var context = original.GetComponent<Gs2MegaFieldSpatialContext>() ?? original.GetComponentInParent<Gs2MegaFieldSpatialContext>(true);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2MegaFieldSpatialFetcher), false);
                EditorGUI.indentLevel++;
                context.Spatial = EditorGUILayout.ObjectField("Spatial", context.Spatial, typeof(Spatial), false) as Spatial;
                if (context.Spatial != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.Spatial?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("AreaModelName", context.Spatial?.AreaModelName?.ToString());
                    EditorGUILayout.TextField("LayerModelName", context.Spatial?.LayerModelName?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            original.target = EditorGUILayout.ObjectField("Target", original.target, typeof(LocalizeStringEvent), true) as LocalizeStringEvent;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif