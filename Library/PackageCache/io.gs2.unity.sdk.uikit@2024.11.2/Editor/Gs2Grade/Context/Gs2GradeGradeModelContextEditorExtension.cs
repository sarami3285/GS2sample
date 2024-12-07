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

using Gs2.Unity.Gs2Grade.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Grade.Context;
using Gs2.Unity.UiKit.Gs2Grade.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Grade.Editor
{
    [CustomEditor(typeof(Gs2GradeGradeModelContext))]
    public class Gs2GradeGradeModelContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2GradeGradeModelContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.GradeModel == null) {
                var list = original.GetComponentInParent<Gs2GradeGradeModelList>(true);
                if (list != null) {
                    EditorGUILayout.HelpBox("GradeModel is auto assign from Gs2GradeGradeModelList.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("List", list, typeof(Gs2GradeGradeModelList), false);
                    EditorGUI.EndDisabledGroup();
                }
                else {
                    EditorGUILayout.HelpBox("GradeModel not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_gradeModel"), true);
                }
            }
            else {
                original.GradeModel = EditorGUILayout.ObjectField("GradeModel", original.GradeModel, typeof(GradeModel), false) as GradeModel;
                EditorGUI.BeginDisabledGroup(true);
                if (original.GradeModel != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.GradeModel?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("GradeName", original.GradeModel?.GradeName?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}