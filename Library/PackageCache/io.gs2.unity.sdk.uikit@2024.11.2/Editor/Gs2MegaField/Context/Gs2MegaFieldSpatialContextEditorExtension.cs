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

using Gs2.Unity.Gs2MegaField.ScriptableObject;
using Gs2.Unity.UiKit.Gs2MegaField.Context;
using Gs2.Unity.UiKit.Gs2MegaField.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2MegaField.Editor
{
    [CustomEditor(typeof(Gs2MegaFieldSpatialContext))]
    public class Gs2MegaFieldSpatialContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MegaFieldSpatialContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.Spatial == null) {
                EditorGUILayout.HelpBox("Spatial not assigned.", MessageType.Error);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Spatial"), true);
            }
            else {
                original.Spatial = EditorGUILayout.ObjectField("Spatial", original.Spatial, typeof(Spatial), false) as Spatial;
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.Spatial?.NamespaceName.ToString());
                EditorGUILayout.TextField("UserId", original.Spatial?.UserId.ToString());
                EditorGUILayout.TextField("AreaModelName", original.Spatial?.AreaModelName.ToString());
                EditorGUILayout.TextField("LayerModelName", original.Spatial?.LayerModelName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}