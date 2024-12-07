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

using Gs2.Unity.Gs2Gateway.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Gateway.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Gateway.Enabler.Editor
{
    [CustomEditor(typeof(Gs2GatewayOwnWebSocketSessionUserIdEnabler))]
    public class Gs2GatewayOwnWebSocketSessionUserIdEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2GatewayOwnWebSocketSessionUserIdEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2GatewayOwnWebSocketSessionContext>() ?? original.GetComponentInParent<Gs2GatewayOwnWebSocketSessionContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2GatewayOwnWebSocketSessionContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2GatewayOwnWebSocketSessionContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2GatewayOwnWebSocketSessionContext), false);
                EditorGUI.indentLevel++;
                context.WebSocketSession = EditorGUILayout.ObjectField("WebSocketSession", context.WebSocketSession, typeof(OwnWebSocketSession), false) as OwnWebSocketSession;
                if (context.WebSocketSession != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.WebSocketSession?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("ConnectionId", context.WebSocketSession?.ConnectionId?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2GatewayOwnWebSocketSessionUserIdEnabler.Expression.In || original.expression == Gs2GatewayOwnWebSocketSessionUserIdEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableUserIds"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableUserId"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}