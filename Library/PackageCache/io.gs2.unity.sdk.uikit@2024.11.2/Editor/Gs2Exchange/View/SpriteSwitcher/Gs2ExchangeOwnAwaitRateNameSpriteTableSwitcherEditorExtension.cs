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

namespace Gs2.Unity.UiKit.Gs2Exchange.SpriteSwitcher.Editor
{
    [CustomEditor(typeof(Gs2ExchangeOwnAwaitRateNameSpriteTableSwitcher))]
    public class Gs2ExchangeOwnAwaitRateNameSpriteTableSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ExchangeOwnAwaitRateNameSpriteTableSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2ExchangeOwnAwaitContext>() ?? original.GetComponentInParent<Gs2ExchangeOwnAwaitContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2ExchangeOwnAwaitContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2ExchangeOwnAwaitContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2ExchangeOwnAwaitList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ExchangeOwnAwaitContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Await is auto assign from Gs2ExchangeOwnAwaitList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ExchangeOwnAwaitContext), false);
                    EditorGUI.indentLevel++;
                    context.Await = EditorGUILayout.ObjectField("Await", context.Await, typeof(OwnAwait), false) as OwnAwait;
                    if (context.Await != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.Await?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("AwaitName", context.Await?.AwaitName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprites"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultSprite"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}