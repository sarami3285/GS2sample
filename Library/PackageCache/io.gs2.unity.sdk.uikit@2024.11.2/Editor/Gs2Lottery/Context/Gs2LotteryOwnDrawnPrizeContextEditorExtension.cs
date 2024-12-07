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

using Gs2.Unity.Gs2Lottery.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Lottery.Context;
using Gs2.Unity.UiKit.Gs2Lottery.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Lottery.Editor
{
    [CustomEditor(typeof(Gs2LotteryOwnDrawnPrizeContext))]
    public class Gs2LotteryOwnDrawnPrizeContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2LotteryOwnDrawnPrizeContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.DrawnPrize == null) {
                if (original.GetComponentInParent<Gs2LotteryOwnDrawnPrizeList>(true) != null) {
                    EditorGUILayout.HelpBox("OwnDrawnPrize is auto assign from Gs2LotteryOwnDrawnPrizeList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("OwnDrawnPrize not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("DrawnPrize"), true);
                }
            }
            else {
                original.DrawnPrize = EditorGUILayout.ObjectField("OwnDrawnPrize", original.DrawnPrize, typeof(OwnDrawnPrize), false) as OwnDrawnPrize;
                EditorGUI.BeginDisabledGroup(true);
                if (original.DrawnPrize != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.DrawnPrize?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("PrizeId", (original.DrawnPrize?.Index ?? 0).ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}