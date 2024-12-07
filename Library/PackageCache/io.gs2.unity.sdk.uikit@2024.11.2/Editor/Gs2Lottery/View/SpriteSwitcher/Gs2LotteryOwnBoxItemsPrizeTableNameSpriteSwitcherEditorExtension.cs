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
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Lottery.SpriteSwitcher.Editor
{
    [CustomEditor(typeof(Gs2LotteryOwnBoxItemsPrizeTableNameSpriteSwitcher))]
    public class Gs2LotteryOwnBoxItemsPrizeTableNameSpriteSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2LotteryOwnBoxItemsPrizeTableNameSpriteSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2LotteryOwnBoxItemsContext>() ?? original.GetComponentInParent<Gs2LotteryOwnBoxItemsContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2LotteryOwnBoxItemsContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2LotteryOwnBoxItemsContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2LotteryOwnBoxItemsList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2LotteryOwnBoxItemsContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("BoxItems is auto assign from Gs2LotteryOwnBoxItemsList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2LotteryOwnBoxItemsContext), false);
                    EditorGUI.indentLevel++;
                    context.BoxItems = EditorGUILayout.ObjectField("BoxItems", context.BoxItems, typeof(OwnBoxItems), false) as OwnBoxItems;
                    if (context.BoxItems != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.BoxItems?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("PrizeTableName", context.BoxItems?.PrizeTableName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2LotteryOwnBoxItemsPrizeTableNameSpriteSwitcher.Expression.In || original.expression == Gs2LotteryOwnBoxItemsPrizeTableNameSpriteSwitcher.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyPrizeTableNames"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyPrizeTableName"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}